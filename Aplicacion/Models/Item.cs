using SQLite;
using System;

namespace Aplicacion.Models
{
    public class Usuarios
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class Empleados
    {
        [PrimaryKey, AutoIncrement]
        public int idEmp { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(50)]
        public string Direccion { get; set; }
        public string CURP { get; set; }
        public string TipoEmp { get; set; }
        public int Edad { get; set; }
        public long Telefono { get; set; }
    }

    public class Cursodb
    {
        [PrimaryKey, AutoIncrement]
        public int idC { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(50)]
        public string Tipo { get; set; }

        [MaxLength(50)]
        public string Descripcion { get; set; }
        public int Horas { get; set; }
    }

    public class SegCursoEmp
    {
        [PrimaryKey, AutoIncrement]
        public int idSeguimiento { get; set; }
        public int idEmpleado { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(50)]
        public string Curso { get; set; }
        [MaxLength(50)]
        public string Lugar { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Estatus { get; set; }
        public int Calificacion { get; set; }

    }
}