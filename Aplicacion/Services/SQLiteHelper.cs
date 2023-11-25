using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Models;

namespace Aplicacion.DATA
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Empleados>().Wait();
            db.CreateTableAsync<Cursodb>().Wait();
            db.CreateTableAsync<SegCursoEmp>().Wait();
            db.CreateTableAsync<Usuarios>().Wait();
        }
        
        public Task<Usuarios> AuthenticateAsync(string email, string password)
        {
            return db.Table<Usuarios>().Where(a => a.Email == email && a.Password == password).FirstOrDefaultAsync();
        }
        public Task<int> SaveUserAsync(Usuarios user)
        {
            if (user.Id != 0)
            {
                return db.UpdateAsync(user);
            }
            else
            {
                return db.InsertAsync(user);
            }
        }
        public Task<int> SaveEmpleadoAsync(Empleados emp)
        {
            if (emp.idEmp != 0)
            {
                return db.UpdateAsync(emp);
            }
            else
            {
                return db.InsertAsync(emp);
            }
        }
        public Task<int> SaveCursoAsync(Cursodb curso)
        {
            if (curso.idC != 0)
            {
                return db.UpdateAsync(curso);
            }
            else
            {
                return db.InsertAsync(curso);
            }
        }
        public Task<int> SaveSeguimientoAsync(SegCursoEmp seg)
        {
            if (seg.idSeguimiento != 0)
            {
                return db.UpdateAsync(seg);
            }
            else
            {
                return db.InsertAsync(seg);
            }
        }


        public Task<List<Empleados>> GetEmpleadosAsync()
        {
            return db.Table<Empleados>().ToListAsync();
        }
        public Task<List<Cursodb>> GetCursoAsync()
        {
            return db.Table<Cursodb>().ToListAsync();
        }
        public Task<List<SegCursoEmp>> GetSeguimientoAsync()
        {
            return db.Table<SegCursoEmp>().ToListAsync();
        }


        public Task<Empleados> GetEmpleadoByIdAsync(int id)
        {
            return db.Table<Empleados>().Where(a => a.idEmp == id).FirstOrDefaultAsync();
        }
        public Task<Cursodb> GetCursoByIdAsync(int id)
        {
            return db.Table<Cursodb>().Where(a => a.idC == id).FirstOrDefaultAsync();
        }
        public Task<SegCursoEmp> GetSeguimientoByIdAsync(int id)
        {
            return db.Table<SegCursoEmp>().Where(a => a.idSeguimiento == id).FirstOrDefaultAsync();
        }


        public Task<int> DeleteEmpleadoAsync(Empleados empleado)
        {
            return db.DeleteAsync(empleado);
        }
        public Task<int> DeleteCursosAsync(Cursodb curso)
        {
            return db.DeleteAsync(curso);
        }
        public Task<int> DeleteSeguimientoAsync(SegCursoEmp seg)
        {
            return db.DeleteAsync(seg);
        }
    }
}
