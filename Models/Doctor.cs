using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    [Table("Doctor")]
    public class Doctor
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Specialization { get; set; }

        public string Url { get; set; }

        public List<string> Prices { get; set; } = new List<string>();

        public string Geolocation { get; set; }

        public string WorkExperience { get; set; }
    }
}
