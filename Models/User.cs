using SQLite;
using System;

namespace Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string Weight { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Sex { get; set; }
    }
}
