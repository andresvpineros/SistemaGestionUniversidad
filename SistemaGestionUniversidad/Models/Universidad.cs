using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionUniversidad.Models
{
    public class Universidad
    {
        public List<Escuela> Escuelas { get; set; }
        public List<Profesor> Profesores { get; set; }
        public List<Estudiante> Estudiantes { get; set; }
        public List<Departamento> Departamentos { get; set; }

        public Universidad()
        {
            Escuelas = new List<Escuela>();
            Profesores = new List<Profesor>();
            Estudiantes = new List<Estudiante>();
            Departamentos = new List<Departamento>();
        }

        public void AgregarEscuela(Escuela escuela)
        {
            Escuelas.Add(escuela);
        }

        public void AgregarProfesor(Profesor profesor)
        {
            Profesores.Add(profesor);
        }
        public void AgregarEstudiante(Estudiante estudiante)
        {
            Estudiantes.Add(estudiante);
        }

        public void AgregarDepartamento(Departamento departamento)
        {
            Departamentos.Add(departamento);
        }

        public List<Escuela> getEscuelas()
        {
            return Escuelas;
        }

        public List<Profesor> getProfesores()
        {
            return Profesores;
        }
        public List<Estudiante> getEstudiantes()
        {
            return Estudiantes;
        }

        public List<Departamento> getDepartamentos()
        {
            return Departamentos;
        }
    }
}
