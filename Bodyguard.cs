using System;
using System.Globalization;

namespace _26
{
    internal class Bodyguard
    {
        public string LastName;
        public string FirstName;
        public string Patronymic;
        public string Address;
        public DateTime BirthDate;
        
        public static Bodyguard FromString(string line)
        {
            var parts = line.Split(';');
            return new Bodyguard
            {
                LastName = parts[0],
                FirstName = parts[1],
                Patronymic = parts[2],
                Address = parts[3],
                BirthDate = DateTime.ParseExact(parts[4], "yyyy-MM-dd", CultureInfo.InvariantCulture)
            };
        }
        
        public override string ToString()
        {
            return $"{LastName} {FirstName} {Patronymic}, {Address}, род. {BirthDate:dd.MM.yyyy}";
        }
    }
}
