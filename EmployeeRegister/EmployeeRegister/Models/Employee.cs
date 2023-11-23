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
            public string FullName { get; set; }

            public string Role { get; set; }
            [MaxLength(5)]
            public int EmployeeNumber { get; set; }
            // Nuevos campos
            [MaxLength(100)]
            public string Address { get; set; }

            [MaxLength(15)]
            public string PhoneNumber { get; set; }

            public DateTime BirthDate { get; set; }

            [MaxLength(18)]
            public string Curp { get; set; }
        }

    public class Curso
    {
        [PrimaryKey, AutoIncrement]
        public int IdCurso { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }


    }



}
