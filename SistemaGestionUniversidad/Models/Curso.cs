using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionUniversidad.Models
{
    // Relación de composición con la clase Escuela y Departamento
    public class Curso
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public Departamento Departamento { get; set; }
        public Escuela Escuela { get; set; }
        public Profesor Profesor { get; set; }

        public Curso(int codigo, string nombre, Departamento departamento, Escuela escuela, Profesor profesor)
        {
            Codigo = codigo;
            Nombre = nombre;
            Departamento = departamento;
            Escuela = escuela;
            Profesor = profesor;
        }

        public Curso() { }

        public int GetCodigo()
        {
            return Codigo;
        }

        public string GetNombre()
        {
            return Nombre;
        }

        public Departamento GetDepartamento()
        {
            return Departamento;
        }

        public Escuela GetEscuela()
        {
            return Escuela;
        }

        public Profesor GetProfesor()
        {
            return Profesor;
        }
    }
}
