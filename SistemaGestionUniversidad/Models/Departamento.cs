using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionUniversidad.Models
{
    // Relación de composición con la clase Curso
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Curso> Cursos { get; set; }

        public Departamento(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            Cursos = new List<Curso>();
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

        public List<Curso> GetCursos()
        {
            return Cursos;
        }
    }

}
