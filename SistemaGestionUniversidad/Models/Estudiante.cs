using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionUniversidad.Models
{
    // Herencia de la clase Persona
    public class Estudiante : Persona
    {
        public string Direccion { get; set; }
        public List<Curso> CursosInscritos { get; set; }

        public Estudiante(int id, string nombre, string direccion)
            : base(id, nombre)
        {
            Direccion = direccion;
            CursosInscritos = new List<Curso>();
        }

        public void InscribirCurso(Curso curso)
        {
            CursosInscritos.Add(curso);
        }

        public string GetDireccion()
        {
            return Direccion;
        }

        public List<Curso> GetCursosInscritos()
        {
            return CursosInscritos;
        }
    }
}
