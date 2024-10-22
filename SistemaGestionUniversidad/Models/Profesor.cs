using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionUniversidad.Models
{
    // Hereda de la clase Persona
    public class Profesor : Persona
    {
        public float Salario { get; set; }
        public List<Curso> CursosImpartidos { get; set; }

        public Profesor(int id, string nombre, float salario)
            : base(id, nombre)
        {
            Salario = salario;
            CursosImpartidos = new List<Curso>();
        }

        public void AsignarCurso(Curso curso)
        {
            CursosImpartidos.Add(curso);
        }

        public float GetSalario()
        {
            return Salario;
        }

        public List<Curso> GetCursosImpartidos()
        {
            return CursosImpartidos;
        }
    }
}
