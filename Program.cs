using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.Write("Введите номер задания (0 для выхода): ");
            string choiceInput = Console.ReadLine();

            if (int.TryParse(choiceInput, out int choice))
            {
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    case 4:
                        Task4();
                        break;
                    case 5:
                        Task5();
                        break;
                    case 6:
                        Task6();
                        break;
                    case 0:
                        Console.WriteLine("Завершение программы.");
                        return;
                    default:
                        Console.WriteLine("Неверный номер задания. Пожалуйста, выберите номер из меню.");
                        break;
                }
            }
        }
    }
    static void Task1()
    {
        string[] data =
     {
        "Иванов;Алексей;Петрович;123456, Россия, Москва, ул. Мира, 10, 1;1980-05-12",
        "Петров;Иван;Сергеевич;654321, Россия, Тверь, ул. Победы, 7, 2;1975-11-23",
        "Сидоров;Николай;Андреевич;987654, Россия, СПб, Невский, 15, 8;1988-03-30"
    };
        File.WriteAllLines("bodyguards.txt", data);

        // 👉 Вывод содержимого ДО обработки
        Console.WriteLine("Содержимое файла bodyguards.txt:");
        Console.WriteLine(File.ReadAllText("bodyguards.txt"));
        Console.WriteLine();

        string[] lines = File.ReadAllLines("bodyguards.txt");
        DateTime oldestDate = DateTime.MaxValue;
        string oldest = "";

        foreach (string line in lines)
        {
            var parts = line.Split(';');
            DateTime birth = DateTime.Parse(parts[4]);
            if (birth < oldestDate)
            {
                oldestDate = birth;
                oldest = line;
            }
        }

        File.WriteAllText("oldest_guard.txt", oldest);
        Console.WriteLine("Старший телохранитель:");
        Console.WriteLine(oldest);
    }
    static void Task2()
    {
        File.WriteAllText("numbers.txt", "1 2 3 4 5 6 7 8 9 10 11");

        // 👉 Вывод содержимого ДО обработки
        Console.WriteLine("Содержимое файла numbers.txt:");
        Console.WriteLine(File.ReadAllText("numbers.txt"));
        Console.WriteLine();

        string content = File.ReadAllText("numbers.txt");
        var numbers = content.Split(' ').Select(int.Parse);
        int count = numbers.Count(x => x % 2 == 1 && Math.Sqrt(x) % 1 == 0);

        File.WriteAllText("odd_squares_count.txt", $"Количество квадратов нечётных чисел: {count}");
        Console.WriteLine($"Количество квадратов нечётных чисел: {count}");
    }
    static void Task3()
    {
        string file1 = "matrices1.txt";
        string file2 = "matrices2.txt";
        string file3 = "matrices3.txt";

        // Если файлов нет — создаём с тестовыми матрицами
        if (!File.Exists(file1))
        {
            string[] test1 =
            {
            "1 2 3",
            "4 5 6",
            "7 8 9",
            "10 11 12",
            "13 14 15",
            "16 17 18"
        };
            File.WriteAllLines(file1, test1);
        }

        if (!File.Exists(file2))
        {
            string[] test2 =
            {
            "91 92 93",
            "94 95 96",
            "97 98 99",
            "20 21 22",
            "23 24 25",
            "26 27 28"
        };
            File.WriteAllLines(file2, test2);
        }

        List<string> m1 = new List<string>(File.ReadAllLines(file1));
        List<string> m2 = new List<string>(File.ReadAllLines(file2));

        int k = m1.Count / 3;
        int l = m2.Count / 3;
        int count = Math.Min(k, l);

        Console.WriteLine("Изначальное содержимое matrices1.txt:");
        Console.WriteLine(File.ReadAllText(file1));
        Console.WriteLine("Изначальное содержимое matrices2.txt:");
        Console.WriteLine(File.ReadAllText(file2));
        Console.WriteLine();

        for (int i = 0; i < count; i++)
        {
            if ((i + 1) % 2 == 0 && i < k && i < l)
            {
                for (int j = 0; j < 3; j++)
                {
                    string temp = m1[i * 3 + j];
                    m1[i * 3 + j] = m2[i * 3 + j];
                    m2[i * 3 + j] = temp;
                }
            }
        }

        File.WriteAllLines(file1, m1);
        File.WriteAllLines(file2, m2);
        File.WriteAllLines(file3, (k < l ? m2.Skip(k * 3) : m1.Skip(l * 3)).ToArray());

        Console.WriteLine("Матрицы из первого файла:");
        Console.WriteLine(File.ReadAllText(file1));

        Console.WriteLine("Матрицы из второго файла:");
        Console.WriteLine(File.ReadAllText(file2));
    }
    static void Task4()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open("books.dat", FileMode.Create)))
        {
            writer.Write("Иванов");
            writer.Write("C++ для начинающих");
            writer.Write(100);
            writer.Write(999.99);

            writer.Write("Смирнов");
            writer.Write("Java на практике");
            writer.Write(50);
            writer.Write(699.99);
        }

        Console.WriteLine("Содержимое books.dat (все книги):");
        using (BinaryReader reader = new BinaryReader(File.Open("books.dat", FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string author = reader.ReadString();
                string title = reader.ReadString();
                int copies = reader.ReadInt32();
                double price = reader.ReadDouble();

                Console.WriteLine($"Автор: {author}, Название: {title}, Тираж: {copies}, Цена: {price}");
            }
        }

        using (BinaryReader reader = new BinaryReader(File.Open("books.dat", FileMode.Open)))
        {
            int count = 0;
            double sum = 0;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string author = reader.ReadString();
                string title = reader.ReadString();
                int copies = reader.ReadInt32();
                double price = reader.ReadDouble();

                if (title.Contains("C++"))
                {
                    count++;
                    sum += copies * price;
                }
            }
            Console.WriteLine($"Количество книг по C++: {count}, Общая стоимость: {sum}");
        }
    }
    static void Task5()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        using (BinaryWriter writer = new BinaryWriter(File.Open("nums1.dat", FileMode.Create)))
        {
            foreach (int num in numbers)
                writer.Write(num);
        }

        int[] reversed;
        using (BinaryReader reader = new BinaryReader(File.Open("nums1.dat", FileMode.Open)))
        {
            List<int> list = new List<int>();

            while (reader.BaseStream.Position < reader.BaseStream.Length)
                list.Add(reader.ReadInt32());
            reversed = list.AsEnumerable().Reverse().ToArray();
        }

        using (BinaryWriter writer = new BinaryWriter(File.Open("nums2.dat", FileMode.Create)))
        {
            foreach (int num in reversed)
                writer.Write(num);
        }

        Console.WriteLine("Исходные данные:");
        Console.WriteLine(string.Join(" ", numbers));
        Console.WriteLine("В обратном порядке:");
        Console.WriteLine(string.Join(" ", reversed));
    }
    static void Task6()
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open("structs.dat", FileMode.Create)))
        {
            writer.Write("Hello");
            writer.Write(1);
            writer.Write(2.3);

            writer.Write("World!");
            writer.Write(5);
            writer.Write(6.7);
        }

        // 👉 Считываем и показываем содержимое ДО изменений
        Console.WriteLine("Исходные записи из structs.dat:");
        using (BinaryReader reader = new BinaryReader(File.Open("structs.dat", FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string str = reader.ReadString();
                int i = reader.ReadInt32();
                double d = reader.ReadDouble();

                Console.WriteLine($"Строка: \"{str}\", Целое: {i}, Вещественное: {d}");
            }
        }

        // 👉 Модифицируем и сохраняем заново
        List<(string, int, double)> records = new List<(string, int, double)>();
        using (BinaryReader reader = new BinaryReader(File.Open("structs.dat", FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string str = reader.ReadString();
                reader.ReadInt32(); // старое целое
                reader.ReadDouble(); // старое вещественное
                int len = str.Length;
                records.Add((str, len, (double)len));
            }
        }

        using (BinaryWriter writer = new BinaryWriter(File.Open("structs.dat", FileMode.Create)))
        {
            foreach (var record in records)
            {
                writer.Write(record.Item1);
                writer.Write(record.Item2);
                writer.Write(record.Item3);
            }
        }

        // 👉 Показываем изменённые записи
        Console.WriteLine("\nОбновлённые записи:");
        using (BinaryReader reader = new BinaryReader(File.Open("structs.dat", FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string str = reader.ReadString();
                int i = reader.ReadInt32();
                double d = reader.ReadDouble();
                Console.WriteLine($"Строка: \"{str}\", Новое целое: {i}, Новое вещественное: {d}");
            }
        }

        Console.WriteLine("\nФайл structs.dat успешно перезаписан.");
    }
}
