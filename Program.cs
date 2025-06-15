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
        Console.Write("Выберите задание (1-6): ");
        int choice = int.Parse(Console.ReadLine());
        if (int.TryParse(Console.ReadLine(), out choice))
        {
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
                default:
                    Console.WriteLine("Неверный выбор. Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    break;
            }
        }

        Console.WriteLine("Файл перезаписан согласно условиям задачи 6.");
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
        string content = File.ReadAllText("numbers.txt");
        var numbers = content.Split(' ').Select(int.Parse);
        int count = numbers.Count(x => x % 2 == 1 && Math.Sqrt(x) % 1 == 0);
        File.WriteAllText("odd_squares_count.txt", $"Количество квадратов нечётных чисел: {count}");
        Console.WriteLine($"Количество квадратов нечётных чисел: {count}");
    }
    static void Task3()
    {
        string file1 = "matrices1.txt", file2 = "matrices2.txt", file3 = "matrices3.txt";

        List<string> m1 = new List<string>(File.ReadAllLines(file1));
        List<string> m2 = new List<string>(File.ReadAllLines(file2));

        int k = m1.Count / 3; // матриц в первом файле
        int l = m2.Count / 3; // во втором
        int count = Math.Min(k, l);

        for (int i = 0; i < count; i++)
        {
            if ((i + 1) % 2 == 0 && i < k && i < l)
            {
                // swap m1[i] <-> m2[i]
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

        List<(string, int, double)> records = new List<(string, int, double)>();
        using (BinaryReader reader = new BinaryReader(File.Open("structs.dat", FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string str = reader.ReadString();
                reader.ReadInt32();
                reader.ReadDouble();
                records.Add((str, str.Length, (double)str.Length));
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
    }
}
