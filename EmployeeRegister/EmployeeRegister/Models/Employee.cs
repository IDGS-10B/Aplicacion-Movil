using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace EmployeeRegister.Models
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int IdEmpl { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string MotherLastName { get; set; }

        public string Role { get; set; }
        [MaxLength(8)]
        public int EmployeeNumber { get; set; }
        [MaxLength(4)]
        public string Password { get; set; }
    }

    public class Video
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
