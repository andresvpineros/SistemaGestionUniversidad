// See https://aka.ms/new-console-template for more information


using SistemaGestionUniversidad.Models;

public class Program
{
    static Universidad Universidad = new Universidad();
    static string NombreUniversidad = "";

    static void Main(string[] args)
    {
        Console.WriteLine("----- Bienvenid@ al Sistema de Gestión de Universidad -----");
        Console.WriteLine("(Hecho por Brandon Vargas)\n");

        // Validación para asegurarse de que el usuario ingrese un nombre válido
        do
        {
            Console.WriteLine("Por favor, digite el nombre de su Universidad: ");
            NombreUniversidad = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(NombreUniversidad))
            {
                Console.WriteLine("[AVISO] El nombre de la universidad no puede estar vacío. Inténtelo de nuevo.\n");
            }

        } while (string.IsNullOrWhiteSpace(NombreUniversidad)); 


        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine($"\n--- Sistema de Gestión de {NombreUniversidad} ---");
            Console.WriteLine("1. Gestionar Escuelas");
            Console.WriteLine("2. Gestionar Profesores");
            Console.WriteLine("3. Gestionar Cursos");
            Console.WriteLine("4. Gestionar Estudiantes");
            Console.WriteLine("0. Salir");

            Console.Write($"\nSeleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    GestionarEscuelas();
                    break;
                case "2":
                    GestionarProfesores();
                    break;
                case "3":
                    GestionarCursos();
                    break;
                case "4":
                    GestionarEstudiantes();
                    break;
                case "0":
                    continuar = false;
                    Console.WriteLine("Saliendo del sistema...");
                    break;
                default:
                    Console.WriteLine("Opción no válida, intente de nuevo.");
                    break;
            }
        }

        // Operaciones principales

        static void GestionarEscuelas()
        {
            bool volverMenuPrincipal = false;
            while (!volverMenuPrincipal)
            {
                Console.WriteLine($"\n--- Gestión de Escuelas en {NombreUniversidad} ---");
                Console.WriteLine("1. Agregar una nueva escuela a la universidad");
                Console.WriteLine("2. Asignar profesor a una escuela");
                Console.WriteLine("3. Ver escuelas registradas");
                Console.WriteLine("4. Ver profesores registrados en la universidad");
                Console.WriteLine("0. Retroceder al menú principal");

                Console.Write($"\nSeleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        string nombreEscuela;
                        bool nombreEscuelaValido = false;

                        do
                        {
                            Console.Write("Ingrese el nombre de la nueva escuela: ");
                            nombreEscuela = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(nombreEscuela))
                            {
                                Console.WriteLine("[AVISO] El nombre de la escuela no puede estar vacío. Inténtelo de nuevo.");
                            }
                            else if (Universidad.Escuelas.Exists(e => e.Nombre.Equals(nombreEscuela, StringComparison.OrdinalIgnoreCase)))
                            {
                                Console.WriteLine("[ERROR] Ya existe una escuela con ese nombre. Ingrese un nombre diferente.");
                            }
                            else
                            {
                                nombreEscuelaValido = true;
                            }

                        } while (!nombreEscuelaValido);

                        int idNuevaEscuela = Universidad.Escuelas.Count + 1;
                        Escuela nuevaEscuela = new Escuela(idNuevaEscuela, nombreEscuela);
                        Universidad.AgregarEscuela(nuevaEscuela);
                        Console.WriteLine("[EXITO] Escuela agregada exitosamente.");

                        break;

                    case "2":
                        Profesor profesor = null;
                        Escuela escuela = null;

                        // Validación del ID del profesor
                        if (Universidad.Profesores.Count > 0)
                        {
                            while (profesor == null)
                            {
                                Console.WriteLine($"\n---------------------");

                                Console.WriteLine($"Profesores registrados en la Universidad: ");
                                foreach (var profesorItem in Universidad.Profesores)
                                {
                                    Console.WriteLine($"\n- ID: {profesorItem.Id}, Nombre: {profesorItem.Nombre}, Salario: {profesorItem.Salario}:");

                                    if (profesorItem.CursosImpartidos.Count > 0)
                                    {
                                        Console.WriteLine("  Cursos:");
                                        foreach (var cursoImpartidoItem in profesorItem.CursosImpartidos)
                                        {
                                            Console.WriteLine($"    - Código: {cursoImpartidoItem.Codigo}, Nombre: {cursoImpartidoItem.Nombre}, Profesor: {(cursoImpartidoItem.Profesor != null ? cursoImpartidoItem.Profesor.Nombre : "No asignado")}, Departamento: {cursoImpartidoItem.Departamento.Nombre}, Escuela: {cursoImpartidoItem.Escuela.Nombre}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("  No hay cursos asociados a este profesor.");
                                    }
                                }

                                Console.WriteLine($"---------------------\n");

                                Console.Write("Ingrese el ID del profesor que desea asignar a una escuela: ");
                                string idProfesorInput = Console.ReadLine();

                                if (int.TryParse(idProfesorInput, out int idProfesor))
                                {
                                    // Verificar si el profesor ya existe en la universidad
                                    profesor = Universidad.Profesores.FirstOrDefault(p => p.Id == idProfesor);

                                    if (profesor == null)
                                    {
                                        Console.WriteLine("[ERROR] No se encontró un profesor con ese ID en la universidad. Intente de nuevo.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("[ERROR] ID de profesor no válido. Intente de nuevo.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("[ERROR] No hay profesores registrados.");
                            break;
                        }
                        

                        // Validación del ID de la escuela

                        if (Universidad.Escuelas.Count > 0)
                        {
                            while (escuela == null)
                            {
                                Console.WriteLine($"\n---------------------");

                                Console.WriteLine("Escuelas registradas: ");

                                foreach (var escuelaList in Universidad.Escuelas)
                                {
                                    Console.WriteLine($"\n- ID: {escuelaList.Id}, Nombre: {escuelaList.Nombre}:");

                                    if (escuelaList.Profesores.Count > 0)
                                    {
                                        Console.WriteLine("  Profesores asignados:");
                                        foreach (var profesorItem in escuelaList.Profesores)
                                        {
                                            Console.WriteLine($"    - ID: {profesorItem.Id}, Nombre: {profesorItem.Nombre}, Salario: {profesorItem.Salario}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("  No hay profesores asignados a esta escuela.");
                                    }

                                    if (escuelaList.Cursos.Count > 0)
                                    {
                                        Console.WriteLine("  Cursos asignados:");
                                        foreach (var cursoItem in escuelaList.Cursos)
                                        {
                                            Console.WriteLine($"    - Código: {cursoItem.Codigo}, Nombre: {cursoItem.Nombre}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("  No hay cursos asignados a esta escuela.");
                                    } 
                                }

                                Console.WriteLine($"---------------------\n");

                                Console.Write("Ingrese el ID de la escuela a la que desea asignar el profesor: ");
                                string idEscuelaInput = Console.ReadLine();

                                if (int.TryParse(idEscuelaInput, out int idEscuela))
                                {
                                    // Buscar la escuela en la universidad
                                    escuela = Universidad.Escuelas.FirstOrDefault(e => e.Id == idEscuela);

                                    if (escuela == null)
                                    {
                                        Console.WriteLine("[ERROR] No se encontró una escuela con ese ID. Intente de nuevo.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("[ERROR] ID de escuela no válido. Intente de nuevo.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("[ERROR] No hay escuelas registradas.");
                            break;
                        }
                        

                        // Validar que el profesor no esté ya registrado en esta escuela
                        if (escuela.Profesores.Any(p => p.Id == profesor.Id))
                        {
                            Console.WriteLine($"[ERROR] El profesor {profesor.Nombre} ya está asignado a la escuela {escuela.Nombre}.");
                        }
                        else
                        {
                            // Asignar el profesor a la escuela
                            escuela.Profesores.Add(profesor);
                            Console.WriteLine($"[EXITO] Profesor {profesor.Nombre} asignado exitosamente a la escuela {escuela.Nombre}.");
                        }
                        break;

                    case "3":
                        Console.WriteLine($"\n---------------------");

                        if (Universidad.Escuelas.Count > 0)
                        {
                            Console.WriteLine($"Escuelas registradas: ");

                            foreach (var escuelaList in Universidad.Escuelas)
                            {
                                Console.WriteLine($"\n- ID: {escuelaList.Id}, Nombre: {escuelaList.Nombre}:");

                                if (escuelaList.Profesores.Count > 0)
                                {
                                    Console.WriteLine("  Profesores asignados:");
                                    foreach (var profesorItem in escuelaList.Profesores)
                                    {
                                        Console.WriteLine($"    - ID: {profesorItem.Id}, Nombre: {profesorItem.Nombre}, Salario: {profesorItem.Salario}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("  No hay profesores asignados a esta escuela.");
                                }


                                if (escuelaList.Cursos.Count > 0)
                                {
                                    Console.WriteLine("  Cursos asignados:");
                                    foreach (var cursoItem in escuelaList.Cursos)
                                    {
                                        Console.WriteLine($"    - Código: {cursoItem.Codigo}, Nombre: {cursoItem.Nombre}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("  No hay cursos asignados a esta escuela.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay Escuelas registradas.");
                        }

                        Console.WriteLine($"---------------------\n");
                        break;

                    case "4":
                        Console.WriteLine($"\n---------------------");

                        if (Universidad.Profesores.Count > 0)
                        {
                            Console.WriteLine($"Profesores registrados en la Universidad: ");
                            foreach (var profesorItem in Universidad.Profesores)
                            {
                                Console.WriteLine($"\n- ID: {profesorItem.Id}, Nombre: {profesorItem.Nombre}, Salario: {profesorItem.Salario}:");

                                if (profesorItem.CursosImpartidos.Count > 0)
                                {
                                    Console.WriteLine("  Cursos:");
                                    foreach (var cursoImpartidoItem in profesorItem.CursosImpartidos)
                                    {
                                        Console.WriteLine($"    - Código: {cursoImpartidoItem.Codigo}, Nombre: {cursoImpartidoItem.Nombre}, Profesor: {(cursoImpartidoItem.Profesor != null ? cursoImpartidoItem.Profesor.Nombre : "No asignado")}, Departamento: {cursoImpartidoItem.Departamento.Nombre}, Escuela: {cursoImpartidoItem.Escuela.Nombre}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("  No hay cursos asociados a este profesor.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay Profesores registrados en el Sistema.");
                        }
                        Console.WriteLine($"---------------------\n");

                        break;

                    case "0":
                        volverMenuPrincipal = true; // Opción para retroceder al menú principal
                        break;

                    default:
                        Console.WriteLine("[ERROR] Opción no válida.");
                        break;
                }
            }
        }

        static void GestionarProfesores()
        {
            bool volverMenuPrincipal = false;
            while (!volverMenuPrincipal)
            {
                Console.WriteLine($"\n--- Gestión de Profesores en {NombreUniversidad} ---");
                Console.WriteLine("1. Registrar un nuevo profesor a la universidad");
                Console.WriteLine("2. Asignar curso a un profesor");
                Console.WriteLine("3. Ver profesores registrados");
                Console.WriteLine("4. Ver cursos impartidos");
                Console.WriteLine("0. Retroceder al menú principal");

                Console.Write($"\nSeleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        string nombreProfesor;
                        Profesor nuevoProfesor = null;
                        bool nombreProfesorValido = false;

                        do
                        {
                            Console.Write("Ingrese el nombre del nuevo profesor: ");
                            nombreProfesor = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(nombreProfesor))
                            {
                                Console.WriteLine("[AVISO] El nombre del profesor no puede estar vacío. Inténtelo de nuevo.");
                            }
                            else if (Universidad.Profesores.Any(p => p.Nombre.Equals(nombreProfesor, StringComparison.OrdinalIgnoreCase)))
                            {
                                Console.WriteLine("[ERROR] Ya existe un profesor con este nombre en la universidad. Inténtelo de nuevo.");
                            }
                            else
                            {
                                nombreProfesorValido = true;
                            }
                        } while (!nombreProfesorValido);

                        // Solicitar salario del profesor
                        float salarioProfesor = 0;
                        bool salarioValido = false;

                        do
                        {
                            Console.Write("Ingrese el salario del profesor: ");
                            string salarioInput = Console.ReadLine();

                            if (float.TryParse(salarioInput, out salarioProfesor))
                            {
                                salarioValido = true;
                            }
                            else
                            {
                                Console.WriteLine("[ERROR] El salario ingresado no es válido. Inténtelo de nuevo.");
                            }
                        } while (!salarioValido);

                        // Crear y agregar el profesor a la lista de profesores de la universidad (sistema)
                        int idProfesor = Universidad.Profesores.Count + 1;
                        nuevoProfesor = new Profesor(idProfesor, nombreProfesor, salarioProfesor);
                        Universidad.Profesores.Add(nuevoProfesor);
                        Console.WriteLine($"[EXITO] Profesor {nombreProfesor} registrado exitosamente al sistema.");
                        break;

                    case "2":
                        Profesor profesor = null;
                        Curso curso = null;

                        // Verificar si hay profesores registrados
                        if (Universidad.Profesores.Count == 0)
                        {
                            Console.WriteLine("[ERROR] No hay profesores registrados en la universidad.");
                            break;
                        }

                        // Verificar si hay cursos registrados
                        if (!Universidad.Escuelas.Any(e => e.Cursos.Count > 0))
                        {
                            Console.WriteLine("[ERROR] No hay cursos registrados en la universidad.");
                            break;
                        }

                        // Solicitar ID del profesor
                        bool profesorValido = false;

                        Console.WriteLine($"\n---------------------");

                        Console.WriteLine($"Profesores registrados: ");
                        foreach (var profesorItem in Universidad.Profesores)
                        {
                            Console.WriteLine($"\n- ID: {profesorItem.Id}, Nombre: {profesorItem.Nombre}, Salario: {profesorItem.Salario}:");

                            if (profesorItem.CursosImpartidos.Count > 0)
                            {
                                Console.WriteLine("  Cursos:");
                                foreach (var cursoImpartidoItem in profesorItem.CursosImpartidos)
                                {
                                    Console.WriteLine($"    - Código: {cursoImpartidoItem.Codigo}, Nombre: {cursoImpartidoItem.Nombre}, Profesor: {(cursoImpartidoItem.Profesor != null ? cursoImpartidoItem.Profesor.Nombre : "No asignado")}, Departamento: {cursoImpartidoItem.Departamento.Nombre}, Escuela: {cursoImpartidoItem.Escuela.Nombre}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("  No hay cursos asociados a este profesor.");
                            }
                        }

                        Console.WriteLine($"---------------------\n");


                        do
                        {
                            Console.Write("Ingrese el ID del profesor: ");
                            string idProfesorInput = Console.ReadLine();
                            if (int.TryParse(idProfesorInput, out int idProfesorAsignado))
                            {
                                // Buscar si el profesor existe en la universidad
                                profesor = Universidad.Profesores.FirstOrDefault(p => p.Id == idProfesorAsignado);
                                if (profesor == null)
                                {
                                    Console.WriteLine("[ERROR] No se encontró un profesor con ese ID. Intente nuevamente.");
                                }
                                else
                                {
                                    profesorValido = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("[ERROR] ID de profesor no válido. Intente nuevamente.");
                            }
                        } while (!profesorValido);

                        // Solicitar código del curso
                        bool cursoValido = false;

                        Console.WriteLine($"\n---------------------");

                        var cursosImpartidos = Universidad.Escuelas
                            .SelectMany(escuela => escuela.Cursos)
                            .ToList();

                        if (cursosImpartidos.Count > 0)
                        {
                            Console.WriteLine($"Cursos registrados en la Universidad: ");
                            foreach (var cursoItem in cursosImpartidos)
                            {
                                Console.WriteLine($"\n- Código: {cursoItem.Codigo}, Curso: {cursoItem.Nombre}, Escuela: {cursoItem.Escuela.Nombre}, Departamento: {cursoItem.Departamento.Nombre}, Profesor: {(cursoItem.Profesor != null ? cursoItem.Profesor.Nombre : "No asignado")}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay cursos registrados en la universidad.");
                        }

                        Console.WriteLine($"---------------------\n");

                        do
                        {
                            Console.Write("Ingrese el código del curso a asignar: ");
                            string codigoCursoInput = Console.ReadLine();
                            if (int.TryParse(codigoCursoInput, out int codigoCurso))
                            {
                                // Verificar si el curso existe en la universidad
                                curso = Universidad.Escuelas
                                    .SelectMany(e => e.Cursos)
                                    .FirstOrDefault(c => c.Codigo == codigoCurso);

                                if (curso == null)
                                {
                                    Console.WriteLine("[ERROR] No se encontró un curso con ese código. Intente nuevamente.");
                                }
                                else
                                {
                                    cursoValido = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("[ERROR] Código de curso no válido. Intente nuevamente.");
                            }
                        } while (!cursoValido);

                        // Verificar si el curso ya está asignado a otro profesor
                        Profesor profesorAnterior = Universidad.Profesores
                            .FirstOrDefault(p => p.CursosImpartidos.Any(c => c.Codigo == curso.Codigo));

                        if (profesorAnterior != null)
                        {
                            // Eliminar el curso del profesor anterior
                            profesorAnterior.CursosImpartidos.Remove(curso);
                            Console.WriteLine($"[AVISO] El curso {curso.Nombre} ha sido removido del profesor {profesorAnterior.Nombre}.");
                        }

                        // Verificar si el profesor ya tiene asignado un curso con el mismo nombre y código
                        if (profesor.CursosImpartidos.Any(c => c.Nombre == curso.Nombre && c.Codigo == curso.Codigo))
                        {
                            Console.WriteLine("[ERROR] El profesor ya tiene asignado un curso con el mismo nombre y código.");
                        }
                        else
                        {
                            // Asignar el curso al profesor
                            profesor.CursosImpartidos.Add(curso);
                            curso.Profesor = profesor;
                            Console.WriteLine($"[EXITO] El curso {curso.Nombre} ha sido asignado al profesor {profesor.Nombre}.");
                        }

                        break;

                    case "3":
                        Console.WriteLine("Consultar profesores registrados:");

                        Console.WriteLine($"\n---------------------");

                        if (Universidad.Profesores.Any())
                        {
                            Console.WriteLine($"Profesores registrados: ");
                            foreach (var profesorItem in Universidad.Profesores)
                            {
                                Console.WriteLine($"\n- ID: {profesorItem.Id}, Nombre: {profesorItem.Nombre}, Salario: {profesorItem.Salario}:");

                                if (profesorItem.CursosImpartidos.Count > 0)
                                {
                                    Console.WriteLine("  Cursos:");
                                    foreach (var cursoImpartidoItem in profesorItem.CursosImpartidos)
                                    {
                                        Console.WriteLine($"    - Código: {cursoImpartidoItem.Codigo}, Nombre: {cursoImpartidoItem.Nombre}, Profesor: {(cursoImpartidoItem.Profesor != null ? cursoImpartidoItem.Profesor.Nombre : "No asignado")}, Departamento: {cursoImpartidoItem.Departamento.Nombre}, Escuela: {cursoImpartidoItem.Escuela.Nombre}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("  No hay cursos asociados a este profesor.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay Profesores registrados.");
                        }

                        Console.WriteLine($"---------------------\n");
                        break;

                    case "4":
                        Console.WriteLine("Consultar cursos impartidos:");

                        Console.WriteLine($"\n---------------------");

                        var cursosRegistrados = Universidad.Escuelas
                            .SelectMany(escuela => escuela.Cursos)
                            .ToList();

                        if (cursosRegistrados.Count > 0)
                        {
                            Console.WriteLine($"Cursos registrados en la Universidad: ");
                            foreach (var cursoItem in cursosRegistrados)
                            {
                                Console.WriteLine($"\n- Código: {cursoItem.Codigo}, Curso: {cursoItem.Nombre}, Escuela: {cursoItem.Escuela.Nombre}, Departamento: {cursoItem.Departamento.Nombre}, Profesor: {(cursoItem.Profesor != null ? cursoItem.Profesor.Nombre : "No asignado")}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay cursos registrados en la universidad.");
                        }

                        Console.WriteLine($"---------------------\n");

                        break;

                    case "0":
                        volverMenuPrincipal = true; // Opción para retroceder al menú principal
                        break;

                    default:
                        Console.WriteLine("[ERROR] Opción no válida.");
                        break;
                }
            }
        }

        static void GestionarCursos()
        {
            bool volverMenuPrincipal = false;
            while (!volverMenuPrincipal)
            {
                Console.WriteLine($"\n--- Gestión de Cursos en {NombreUniversidad} ---");
                Console.WriteLine("1. Agregar un nuevo curso a una escuela");
                Console.WriteLine("2. Registrar un departamento a la universidad");
                Console.WriteLine("3. Consultar curso por nombre o código");
                Console.WriteLine("4. Ver cursos registrados");
                Console.WriteLine("5. Ver departamentos registrados en la universidad");
                Console.WriteLine("6. Ver escuelas registradas en la universidad");
                Console.WriteLine("0. Retroceder al menú principal");

                Console.Write($"\nSeleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        // Ver las escuelas registradas
                        if (Universidad.Escuelas.Count > 0)
                        {
                            Console.WriteLine($"\n---------------------");

                            Console.WriteLine("Escuelas registradas: ");
                            foreach (var escuela in Universidad.Escuelas)
                            {
                                Console.WriteLine($"\n- ID: {escuela.Id}, Nombre: {escuela.Nombre}:");

                                // Listado de profesores de la escuela
                                if (escuela.Profesores.Count > 0)
                                {
                                    Console.WriteLine("  Profesores:");
                                    foreach (var profesor in escuela.Profesores)
                                    {
                                        Console.WriteLine($"    - ID: {profesor.Id}, Nombre: {profesor.Nombre}, Salario: {profesor.Salario}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("  No hay profesores asociados a esta escuela.");
                                }

                                // Listado de cursos de la escuela
                                if (escuela.Cursos.Count > 0)
                                {
                                    Console.WriteLine("  Cursos:");
                                    foreach (var cursoItem in escuela.Cursos)
                                    {
                                        Console.WriteLine($"    - Código: {cursoItem.Codigo}, Nombre: {cursoItem.Nombre}, Profesor: {(cursoItem.Profesor != null ? cursoItem.Profesor.Nombre : "No asignado")}, Departamento: {cursoItem.Departamento.Nombre}, Escuela: {cursoItem.Escuela.Nombre}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("  No hay cursos asociados a esta escuela.");
                                }
                            }

                            Console.WriteLine($"---------------------\n");

                            Console.Write("Ingrese el ID de la escuela a la que desea agregar el curso: ");
                            string idEscuelaInput = Console.ReadLine();

                            if (int.TryParse(idEscuelaInput, out int idEscuela))
                            {
                                var escuelaSeleccionada = Universidad.Escuelas.FirstOrDefault(e => e.Id == idEscuela);
                                if (escuelaSeleccionada == null)
                                {
                                    Console.WriteLine("[ERROR] No se encontró una escuela con ese ID.");
                                }
                                else
                                {
                                    // Pedir el nombre del curso
                                    string nombreCurso;
                                    bool nombreCursoValido = false;

                                    do
                                    {
                                        Console.Write("Ingrese el nombre del curso: ");
                                        nombreCurso = Console.ReadLine();
                                        if (string.IsNullOrWhiteSpace(nombreCurso))
                                        {
                                            Console.WriteLine("[ERROR] El nombre del curso no puede estar vacío.");
                                        }
                                        else
                                        {
                                            nombreCursoValido = true;
                                        }
                                    } while (!nombreCursoValido);

                                    // Pedir el código único del curso
                                    string codigoCurso;
                                    bool codigoCursoValido = false;

                                    do
                                    {
                                        Console.Write("Ingrese el código único del curso: ");
                                        codigoCurso = Console.ReadLine();

                                        // Verificar si el campo está vacío
                                        if (string.IsNullOrWhiteSpace(codigoCurso))
                                        {
                                            Console.WriteLine("[AVISO] El código del curso no puede estar vacío.");
                                        }
                                        else if (!int.TryParse(codigoCurso, out int codigoCursoInt))
                                        {
                                            Console.WriteLine("[ERROR] El código del curso debe ser un número válido.");
                                        }
                                        else if (Universidad.Escuelas.Any(e => e.Cursos.Any(c => c.Codigo == codigoCursoInt)))
                                        {
                                            Console.WriteLine("[ERROR] Ya existe un curso con este código en alguna escuela de la universidad.");
                                        }
                                        else
                                        {
                                            codigoCursoValido = true;
                                        }
                                    } while (!codigoCursoValido);

                                    // Seleccionar un departamento existente
                                    if (Universidad.Departamentos.Count > 0)
                                    {
                                        Console.WriteLine($"\n---------------------");

                                        Console.WriteLine("Departamentos registrados: ");
                                        foreach (var departamentoItem in Universidad.Departamentos)
                                        {
                                            Console.WriteLine($"\n- ID: {departamentoItem.Id}, Nombre: {departamentoItem.Nombre}:");

                                            if (departamentoItem.Cursos.Count > 0)
                                            {
                                                Console.WriteLine("  Cursos: ");
                                                foreach (var cursoItem in departamentoItem.Cursos)
                                                {
                                                    Console.WriteLine($"     - Código: {cursoItem.Codigo}, Nombre: {cursoItem.Nombre}, Profesor: {(cursoItem.Profesor != null ? cursoItem.Profesor.Nombre : "No asignado")}, Departamento: {cursoItem.Departamento.Nombre}, Escuela: {cursoItem.Escuela.Nombre}");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("  No hay cursos asociados a este departamento.");
                                            }
                                        }

                                        Console.WriteLine($"---------------------\n");

                                        Console.Write("Ingrese el ID del departamento a asignar: ");
                                        string idDepartamentoInput = Console.ReadLine();

                                        if (int.TryParse(idDepartamentoInput, out int idDepartamento))
                                        {
                                            var departamento = Universidad.Departamentos.FirstOrDefault(d => d.Id == idDepartamento);
                                            if (departamento == null)
                                            {
                                                Console.WriteLine("[ERROR] No se encontró un departamento con ese ID.");
                                            }
                                            else
                                            {
                                                // Crear y agregar el nuevo curso
                                                Curso nuevoCurso = new Curso
                                                {
                                                    Codigo = int.Parse(codigoCurso),
                                                    Nombre = nombreCurso,
                                                    Departamento = departamento,
                                                    Escuela = escuelaSeleccionada,
                                                };

                                                escuelaSeleccionada.Cursos.Add(nuevoCurso);
                                                departamento.Cursos.Add(nuevoCurso);
                                                Console.WriteLine($"[EXITO] Curso '{nombreCurso}' agregado exitosamente a la escuela {escuelaSeleccionada.Nombre}.");
                                            }    
                                        }
                                        else
                                        {
                                            Console.WriteLine("[ERROR] ID de departamento no válido.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("[ERROR] No hay departamentos registrados.");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("[ERROR] No hay escuelas registradas en la universidad.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("[ERROR] No hay escuelas registradas en la universidad.");
                        }
                        break;

                    case "2":
                        Console.Write("Ingrese el nombre del nuevo departamento: ");
                        string nombreDepartamento = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(nombreDepartamento))
                        {
                            Console.WriteLine("[AVISO] El nombre del departamento no puede estar vacío.");
                        }
                        else if (Universidad.Departamentos.Any(d => d.Nombre.Equals(nombreDepartamento, StringComparison.OrdinalIgnoreCase)))
                        {
                            Console.WriteLine($"[ERROR] El departamento '{nombreDepartamento}' ya está registrado en la universidad.");
                        }
                        else
                        {
                            int idDepartamentoNuevo = Universidad.Departamentos.Count + 1;

                            Departamento nuevoDepartamento = new Departamento(idDepartamentoNuevo, nombreDepartamento);
                            Universidad.Departamentos.Add(nuevoDepartamento);
                            Console.WriteLine($"[EXITO] Departamento '{nombreDepartamento}' agregado exitosamente.");
                        }
                        break;

                    case "3":
                        Console.Write("Ingrese el nombre o código del curso a buscar: ");

                        string busqueda = Console.ReadLine();

                        var cursosEncontrados = Universidad.Escuelas
                            .SelectMany(escuela => escuela.Cursos)
                            .Where(curso => curso.Nombre.Equals(busqueda, StringComparison.OrdinalIgnoreCase) || curso.Codigo.ToString() == busqueda)
                            .ToList();

                        Console.WriteLine($"\n---------------------");

                        if (cursosEncontrados.Count > 0)
                        {
                            Console.WriteLine($"Cursos encontrados para la búsqueda '{busqueda}': ");
                            foreach (var curso in cursosEncontrados)
                            {
                                Console.WriteLine($"\n- Código: {curso.Codigo}, Curso: {curso.Nombre}, Escuela: {curso.Escuela.Nombre}, Departamento: {curso.Departamento.Nombre}, Profesor: {(curso.Profesor != null ? curso.Profesor.Nombre : "No asignado")}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No se encontraron cursos con el nombre o código '{busqueda}'.");
                        }

                        Console.WriteLine($"---------------------\n");

                        break;

                    case "4":
                        Console.WriteLine($"\n---------------------");

                        var cursosRegistrados = Universidad.Escuelas
                            .SelectMany(escuela => escuela.Cursos)
                            .ToList();

                        if (cursosRegistrados.Count > 0)
                        {
                            Console.WriteLine($"Cursos registrados en la Universidad: ");
                            foreach (var curso in cursosRegistrados)
                            {
                                Console.WriteLine($"\n- Código: {curso.Codigo}, Curso: {curso.Nombre}, Escuela: {curso.Escuela.Nombre}, Departamento: {curso.Departamento.Nombre}, Profesor: {(curso.Profesor != null ? curso.Profesor.Nombre : "No asignado")}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay cursos registrados en la universidad.");
                        }

                        Console.WriteLine($"---------------------\n");

                        break;

                    case "5":
                        Console.WriteLine($"\n---------------------");

                        if (Universidad.Departamentos.Count > 0)
                        {
                            Console.WriteLine($"Departamentos registrados en la Universidad: ");

                            foreach (var departamentoItem in Universidad.Departamentos)
                            {
                                Console.WriteLine($"\n- ID: {departamentoItem.Id}, Nombre: {departamentoItem.Nombre}:");

                                if (departamentoItem.Cursos.Count > 0)
                                {
                                    Console.WriteLine("  Cursos:");
                                    foreach (var cursoItem in departamentoItem.Cursos)
                                    {
                                        Console.WriteLine($"     - Código: {cursoItem.Codigo}, Nombre: {cursoItem.Nombre}, Profesor: {(cursoItem.Profesor != null ? cursoItem.Profesor.Nombre : "No asignado")}, Departamento: {cursoItem.Departamento.Nombre}, Escuela: {cursoItem.Escuela.Nombre}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("  No hay cursos asociados a este departamento.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay departamentos registrados en la universidad.");
                        }

                        Console.WriteLine($"---------------------\n");
                        break;

                    case "6":
                        Console.WriteLine($"\n---------------------");

                        if (Universidad.Escuelas.Count > 0)
                        {
                            Console.WriteLine("Escuelas registradas en la Universidad: ");

                            foreach (var escuela in Universidad.Escuelas)
                            {
                                Console.WriteLine($"\n- ID: {escuela.Id}, Nombre: {escuela.Nombre}:");

                                // Listado de profesores de la escuela
                                if (escuela.Profesores.Count > 0)
                                {
                                    Console.WriteLine("  Profesores:");
                                    foreach (var profesor in escuela.Profesores)
                                    {
                                        Console.WriteLine($"     - ID: {profesor.Id}, Nombre: {profesor.Nombre}, Salario: {profesor.Salario}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("  No hay profesores asociados a esta escuela.");
                                }

                                // Listado de cursos de la escuela
                                if (escuela.Cursos.Count > 0)
                                {
                                    Console.WriteLine("  Cursos:");
                                    foreach (var cursoItem in escuela.Cursos)
                                    {
                                        Console.WriteLine($"     - Código: {cursoItem.Codigo}, Nombre: {cursoItem.Nombre}, Profesor: {(cursoItem.Profesor != null ? cursoItem.Profesor.Nombre : "No asignado")}, Departamento: {cursoItem.Departamento.Nombre}, Escuela: {cursoItem.Escuela.Nombre}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("  No hay cursos asociados a esta escuela.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay escuelas registradas en la universidad.");
                        }

                        Console.WriteLine($"---------------------\n");
                        break;

                    case "0":
                        volverMenuPrincipal = true; 
                        break;

                    default:
                        Console.WriteLine("[ERROR] Opción no válida.");
                        break;
                }
            }
        }
        
        static void GestionarEstudiantes()
        {
            bool volverMenuPrincipal = false;
            while (!volverMenuPrincipal)
            {
                Console.WriteLine($"\n--- Gestión de Estudiantes en {NombreUniversidad} ---");
                Console.WriteLine("1. Registrar un estudiante a la universidad");
                Console.WriteLine("2. Inscribir estudiante a un curso");
                Console.WriteLine("3. Ver estudiante registrados");
                Console.WriteLine("4. Ver cursos registrados en la universidad");
                Console.WriteLine("0. Retroceder al menú principal");

                Console.Write($"\nSeleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        string nombreEstudiante = null;
                        bool nombreEstudianteValido = false;

                        do
                        {
                            Console.Write("Ingrese el nombre del estudiante: ");
                            nombreEstudiante = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(nombreEstudiante))
                            {
                                Console.WriteLine("[AVISO] El nombre del estudiante no puede estar vacío. Inténtelo de nuevo.");
                            }
                            else if (Universidad.Estudiantes.Any(p => p.Nombre.Equals(nombreEstudiante, StringComparison.OrdinalIgnoreCase)))
                            {
                                Console.WriteLine("[ERROR] Ya existe un estudiante con este nombre en la universidad. Inténtelo de nuevo.");
                            }
                            else
                            {
                                nombreEstudianteValido = true;
                            }
                        } while (!nombreEstudianteValido);

                        string direccionEstudiante = null;
                        bool direccionEstudianteValido = false;

                        do
                        {
                            Console.Write("Ingrese la dirección del estudiante: ");
                            direccionEstudiante = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(direccionEstudiante))
                            {
                                Console.WriteLine("[AVISO] La dirección del estudiante no puede estar vacía. Inténtelo de nuevo.");
                            }
                            else
                            {
                                direccionEstudianteValido = true;
                            }
                        } while (!direccionEstudianteValido);

                        // Crear y registrar el nuevo estudiante
                        int idEstudiante = Universidad.Estudiantes.Count + 1;
                        Estudiante nuevoEstudiante = new Estudiante(idEstudiante, nombreEstudiante, direccionEstudiante);

                        Universidad.Estudiantes.Add(nuevoEstudiante);
                        Console.WriteLine($"[EXITO] Estudiante {nombreEstudiante} registrado exitosamente.");

                        break;
                    case "2":
                        Estudiante estudiante = null;
                        Curso curso = null;

                        if (Universidad.Estudiantes.Count > 0)
                        {
                            while (estudiante == null)
                            {
                                Console.WriteLine($"\n---------------------");

                                Console.WriteLine("Estudiantes registrados: ");
                                foreach (var estudianteItem in Universidad.Estudiantes)
                                {
                                    Console.WriteLine($"\n- ID: {estudianteItem.Id}, Nombre: {estudianteItem.Nombre}, Dirección: {estudianteItem.Direccion}:");

                                    // Listado de cursos inscritos del estudiante
                                    if (estudianteItem.CursosInscritos.Count > 0)
                                    {
                                        Console.WriteLine("  Cursos inscritos:");
                                        foreach (var cursoInscritoItem in estudianteItem.CursosInscritos)
                                        {
                                            Console.WriteLine($"     - Código: {cursoInscritoItem.Codigo}, Nombre: {cursoInscritoItem.Nombre}, Profesor: {(cursoInscritoItem.Profesor != null ? cursoInscritoItem.Profesor.Nombre : "No asignado")}, Departamento: {cursoInscritoItem.Departamento.Nombre}, Escuela: {cursoInscritoItem.Escuela.Nombre}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("  No hay cursos inscritos a este estudiante.");
                                    }
                                }

                                Console.WriteLine($"---------------------\n");

                                Console.Write("Ingrese el ID del estudiante que desea inscribir a un curso: ");
                                string idEstudianteInput = Console.ReadLine();

                                if (int.TryParse(idEstudianteInput, out int idEstudianteCurso))
                                {
                                    // Verificar si el profesor ya existe en la universidad
                                    estudiante = Universidad.Estudiantes.FirstOrDefault(p => p.Id == idEstudianteCurso);

                                    if (estudiante == null)
                                    {
                                        Console.WriteLine("[ERROR] No se encontró un estudiante con ese ID en la universidad. Intente de nuevo.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("[ERROR] ID de estudiante no válido. Intente de nuevo.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("[ERROR] No hay estudiantes registrados.");
                            break;
                        }

                        // Validación del ID del Curso

                        if (Universidad.Escuelas.All(e => e.Cursos.Count > 0))
                        {
                            while (curso == null)
                            {
                                Console.WriteLine($"\n---------------------");

                                var cursosInscritos = Universidad.Escuelas
                                    .SelectMany(escuela => escuela.Cursos)
                                    .ToList();

                                if (cursosInscritos.Count > 0)
                                {
                                    Console.WriteLine($"Cursos registrados: ");
                                    foreach (var cursoItem in cursosInscritos)
                                    {
                                        Console.WriteLine($"\n- Código: {cursoItem.Codigo}, Curso: {cursoItem.Nombre}, Escuela: {cursoItem.Escuela.Nombre}, Departamento: {cursoItem.Departamento.Nombre}, Profesor: {(cursoItem.Profesor != null ? cursoItem.Profesor.Nombre : "No asignado")}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No hay cursos registrados en la universidad.");
                                }

                                Console.WriteLine($"---------------------\n");

                                Console.Write("Ingrese el código del curso: ");
                                string codigoCursoInput = Console.ReadLine();

                                if (int.TryParse(codigoCursoInput, out int codigoCurso))
                                {
                                    // Buscar la escuela en la universidad
                                    curso = Universidad.Escuelas
                                         .SelectMany(e => e.Cursos)
                                         .FirstOrDefault(c => c.Codigo == codigoCurso);

                                    if (curso == null)
                                    {
                                        Console.WriteLine("[ERROR] No se encontró un curso con ese código.");
                                    }

                                    // Inscribir al estudiante en el curso si no está ya inscrito
                                    if (estudiante.CursosInscritos.Any(c => c.Codigo == codigoCurso))
                                    {
                                        Console.WriteLine("[ERROR] El estudiante ya está inscrito en este curso.");
                                    }
                                    else
                                    {
                                        estudiante.CursosInscritos.Add(curso);
                                        Console.WriteLine($"[EXITO] Estudiante {estudiante.Nombre} inscrito exitosamente en el curso {curso.Nombre}.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("[ERROR] ID de escuela no válido. Intente de nuevo.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("[ERROR] No hay cursos registrados.");
                        }

                        break;
                    case "3":
                        Console.WriteLine($"\n---------------------");

                        if (Universidad.Estudiantes.Count > 0)
                        {
                            Console.WriteLine($"Estudiantes registrados en la Universidad: ");
                            foreach (var estudianteItem in Universidad.Estudiantes)
                            {
                                Console.WriteLine($"\n- ID: {estudianteItem.Id}, Nombre: {estudianteItem.Nombre}, Dirección: {estudianteItem.Direccion}:");

                                // Listado de cursos inscritos del estudiante
                                if (estudianteItem.CursosInscritos.Count > 0)
                                {
                                    Console.WriteLine("  Cursos inscritos:");
                                    foreach (var cursoInscritoItem in estudianteItem.CursosInscritos)
                                    {
                                        Console.WriteLine($"     - Código: {cursoInscritoItem.Codigo}, Nombre: {cursoInscritoItem.Nombre}, Profesor: {(cursoInscritoItem.Profesor != null ? cursoInscritoItem.Profesor.Nombre : "No asignado")}, Departamento: {cursoInscritoItem.Departamento.Nombre}, Escuela: {cursoInscritoItem.Escuela.Nombre}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("  No hay cursos inscritos a este estudiante.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay estudiantes registrados en la universidad.");
                        }

                        Console.WriteLine($"---------------------\n");

                        break;

                    case "4":
                        Console.WriteLine($"\n---------------------");

                        var cursosRegistrados = Universidad.Escuelas
                            .SelectMany(escuela => escuela.Cursos)
                            .ToList();

                        if (cursosRegistrados.Count > 0)
                        {
                            Console.WriteLine($"Cursos registrados en la Universidad: ");
                            foreach (var cursoItem in cursosRegistrados)
                            {
                                Console.WriteLine($"\n- Código: {cursoItem.Codigo}, Curso: {cursoItem.Nombre}, Escuela: {cursoItem.Escuela.Nombre}, Departamento: {cursoItem.Departamento.Nombre}, Profesor: {(cursoItem.Profesor != null ? cursoItem.Profesor.Nombre : "No asignado")}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay cursos registrados en la universidad.");
                        }

                        Console.WriteLine($"---------------------\n");

                        break;
                    case "0":
                        volverMenuPrincipal = true;
                        break;
                    default:
                        Console.WriteLine("[ERROR] Opción no válida.");
                        break;
                }
            }
        }
    }
}