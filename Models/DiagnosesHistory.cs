using SQLite;
using System;

namespace Models
{
    [Table("DiagnosesHistory")]
    public class DiagnosesHistory
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Diagnosis { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public int DoctorId { get; set; }
    }
}
