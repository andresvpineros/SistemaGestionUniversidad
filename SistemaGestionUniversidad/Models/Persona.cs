using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionUniversidad.Models
{
    // Clase base
    public abstract class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Persona(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public int GetId () { return Id; }
        public string GetNombre () {  return Nombre; }
    }
}
