using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using EmployeeRegister.Models;
using System.Threading.Tasks;

namespace EmployeeRegister.Data
{
    public class SQLiteHelper
    {

        SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Employee>().Wait();
            db.CreateTableAsync<Curso>().Wait();
        }
        public Task<int> SaveEmployeeAsync(Employee employee)
        {
            if (employee.IdEmpl != 0)
            {
                return db.UpdateAsync(employee);
            }
            else
            {
                return db.InsertAsync(employee);
            }
        }
        public Task<List<Employee>> GetEmployeesAsync()
        {
            return db.Table<Employee>().ToListAsync();
        }
        public Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return db.Table<Employee>().Where(a => a.IdEmpl == id).FirstOrDefaultAsync();
        }
        public Task<int> DeleteEmployee(Employee employee)
        {
            return db.DeleteAsync(employee);
        }
        public Task<Employee> GetEmployeeByEmployeeNumberAsync(int employeeNumber)
        {
            return db.Table<Employee>().Where(a => a.EmployeeNumber == employeeNumber).FirstOrDefaultAsync();
        }


        //     Subir Videos
        public Task<int> SaveCursoAsync(Curso curso)
        {
            if (curso.IdCurso != 0)
            {
                return db.UpdateAsync(curso);
            }
            else
            {
                return db.InsertAsync(curso);
            }
        }

        public Task<List<Curso>> GetCursosAsync()
        {
            return db.Table<Curso>().ToListAsync();
        }

        public Task<Curso> GetCursoByIdAsync(int id)
        {
            return db.Table<Curso>().Where(c => c.IdCurso == id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteCurso(Curso curso)
        {
            return db.DeleteAsync(curso);
        }



    }
}
