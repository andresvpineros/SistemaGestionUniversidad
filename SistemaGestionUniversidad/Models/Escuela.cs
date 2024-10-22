using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionUniversidad.Models
{
    // Relación de agregación con la clase Profesor y de composición con la clase Cursos
    public class Escuela
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Profesor> Profesores { get; set; }
        public List<Curso> Cursos { get; set; }

        public Escuela(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            Profesores = new List<Profesor>();
            Cursos = new List<Curso>();
        }

        public void AgregarProfesor(Profesor profesor)
        {
            Profesores.Add(profesor);
        }

        public void AgregarCurso(Curso curso)
        {
            Cursos.Add(curso);
        }

        public int GetId()
        {
            return Id;
        }

        public string GetNombre()
        {
            return Nombre;
        }

        public List<Profesor> GetProfesores()
        {
            return Profesores;
        }

        public List<Curso> GetCursos()
        {
            return Cursos;
        }
    }
}
