using System;
using System.IO; 

namespace Ruleta_Estudiantes
{
    public static class Metodos
    {
        private static string archivoEstudiantes = "estudiantes.txt";
        private static string archivoHistorial = "Historial de selecciones.txt";
        private static string archivoRoles = "roles.txt";
        public static string[] CargarEstudiantes()
        {
            if (!File.Exists(archivoEstudiantes))
            {
                string[] estudiantesIniciales = {"Mario, Alan, Marcos, Willy, Elizabeth, Elvis"};
                File.WriteAllLines(archivoEstudiantes, estudiantesIniciales);
                return estudiantesIniciales;
            }
            return File.ReadAllLines(archivoEstudiantes);
        }
        public static void GuardarEstudiantes(string[] lista)
        {
            File.WriteAllLines(archivoEstudiantes, lista);
        }
        public static void RegistrarEnHistorial(string[] nombresRoles, string[] ganadores)
        {
            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string registro = $"\n🗓️ {fechaActual}\n";
            for (int i = 0; i < nombresRoles.Length; i++)
            {
                string simbolo = (i == nombresRoles.Length - 1) ? "└─" : "├─";
                registro += $"{simbolo} {nombresRoles[i]}: {ganadores[i]}\n";
            }
            File.AppendAllText(archivoHistorial, registro);
        }
        public static void verHistorial()
        {
            Console.Clear();
            if (!File.Exists(archivoHistorial))
            {
                Console.WriteLine("El historial esta vacio");
            }
            else
            {
                Console.WriteLine(File.ReadAllText(archivoHistorial));
            }
            Console.WriteLine("\nPresione una tecla para volver");
            Console.ReadKey();
        }
        public static void LimpiarHistorial()
        {
            if (File.Exists(archivoHistorial))
            {
                string encabezadoPrincipal = "==================================================\n" +
                                             "             Historial de selecciones             \n" +
                                             "==================================================\n"; 
                File.WriteAllText(archivoHistorial, encabezadoPrincipal);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nHistorial limpiado correctamente");
                Thread.Sleep(1000);
            }
        }
        public static void EscribirNuevaEjecucion()
        {
            string separador = "\n" + "==================================================" + "\n" +
                               $"🚀 NUEVA EJECUCIÓN - {DateTime.Now:dd/MM/yyyy HH:mm}\n" +
                               "==================================================" + "\n";
            File.AppendAllText(archivoHistorial, separador);
        }
        public static void IniciarArchivoHistorial()
        {
            if (!File.Exists(archivoHistorial))
            {
                string encabezadoPrincipal = "==================================================\n" +
                                             "             Historial de selecciones             \n" +
                                             "==================================================\n"; 
                File.WriteAllText(archivoHistorial, encabezadoPrincipal);
            }
        }
        public static void AgregarEstudiante()
        {
            Console.Clear();
            string nombre = "";
            bool esValido = false;

            while (!esValido)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Ingrese el nombre del nuevo estudiante: ");
                Console.ResetColor();
                nombre = Console.ReadLine()?.Trim()!;

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️  El nombre no puede estar vacío.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Clear();
                    continue;
                }
                bool yaExisteEstudiante = false;
                for (int i = 0; i < Program.estudiantes.Length; i++)
                {
                    if (Program.estudiantes[i].ToLower() == nombre.ToLower())
                    {
                        yaExisteEstudiante = true;
                        i = Program.estudiantes.Length;
                    }
                }
                if (yaExisteEstudiante)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"⚠️ Error: El estudinte '{nombre}' ya existe.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Clear();
                    continue;
                }
                esValido = true; 
                for (int i = 0; i < nombre.Length; i++)
                {
                    char letra = nombre[i];
                    if (!char.IsLetter(letra) && !char.IsWhiteSpace(letra))
                    {
                        esValido = false;
                        i = nombre.Length;
                    }
                }
                if (!esValido)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️  Error: Use solo letras y espacios (sin números ni símbolos).");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
            string[] nuevoArray = new string[Program.estudiantes.Length + 1];
            for (int i = 0; i < Program.estudiantes.Length; i++)
            {
                nuevoArray[i] = Program.estudiantes[i];
            }
            nuevoArray[nuevoArray.Length - 1] = nombre;
            Program.estudiantes = nuevoArray;
            GuardarEstudiantes(Program.estudiantes);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n☑️  Estudiante añadido.");
            Thread.Sleep(1000);
        }
        
        public static void ModificarEstudiante()
        {
            Console.Clear();
            int indice = 0;
            bool seleccionValida = false;
            while (!seleccionValida)
            {
                for (int i = 0; i < Program.estudiantes.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Program.estudiantes[i]}");
                }
                Console.Write("\nSeleccione el número a modificar: ");
                string entrada = Console.ReadLine()!;
                if (int.TryParse(entrada, out indice) && indice > 0 && indice <= Program.estudiantes.Length)
                {
                    seleccionValida = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ Error: Ingrese un número de la lista (solo dígitos).");
                    Console.ResetColor();
                    Thread.Sleep(500);
                    Console.Clear();
                    continue;
                }
            }   
            string nuevoNombre = "";
            bool nombreValido = false;
            while (!nombreValido)
            {
                Console.Write($"Nuevo nombre para {Program.estudiantes[indice - 1]}: ");
                nuevoNombre = Console.ReadLine()?.Trim()!;
                if (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ El nombre no puede estar vacío.");
                    Console.ResetColor();
                    Thread.Sleep(500);
                    Console.Clear();
                    continue;
                }
                nombreValido = true;
                for (int i = 0; i < nuevoNombre.Length; i++)
                {
                    if (!char.IsLetter(nuevoNombre[i]) && !char.IsWhiteSpace(nuevoNombre[i]))
                    {
                        nombreValido = false;
                        i = nuevoNombre.Length;
                    }
                }
                if (!nombreValido)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ Error: El nombre solo debe contener letras.");
                    Console.ResetColor();
                }
            }
            Program.estudiantes[indice - 1] = nuevoNombre;
            GuardarEstudiantes(Program.estudiantes);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n☑️ Nombre actualizado.");
            Console.ResetColor();
            Thread.Sleep(1000);
        }

        public static void EliminarEstudiante()
        {
            Console.Clear();
            if (Program.estudiantes.Length <= 2)
            {
                Console.WriteLine("⚠️  No puedes tener menos de 2 estudiantes para que la ruleta funcione.");
                Thread.Sleep(2000);
                return;
            }
            int seleccionado = 0;
            bool seleccionValida = false;
            while (!seleccionValida)
            {
                for (int i = 0; i < Program.estudiantes.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Program.estudiantes[i]}");
                }
                Console.Write("\nNúmero del estudiante a eliminar: ");
                string entrada = Console.ReadLine()!;

                if (int.TryParse(entrada, out seleccionado) && seleccionado > 0 && seleccionado <= Program.estudiantes.Length)
                {
                    seleccionValida = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ Error: Ingrese un número válido de la lista.");
                    Console.ResetColor();
                    Thread.Sleep(500);
                    Console.Clear();
                    continue;
                }
            }
            string[] nuevoArray = new string[Program.estudiantes.Length - 1];
            int j = 0;
            for (int i = 0; i < Program.estudiantes.Length; i++)        
    {
        if (i == seleccionado - 1) continue;
        nuevoArray[j] = Program.estudiantes[i];
        j++;
    }
            Program.estudiantes = nuevoArray;
            GuardarEstudiantes(Program.estudiantes);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n☑️ Estudiante eliminado.");
            Console.ResetColor();
            Thread.Sleep(1000);
        }
        public static string[] CargarRoles() 
        {
            if (!File.Exists(archivoRoles)) 
            {
                string[] rolesIniciales = { "Desarrollador en Vivo", "Facilitador de Ejercicio" };
                File.WriteAllLines(archivoRoles, rolesIniciales);
                return rolesIniciales;
            }
            return File.ReadAllLines(archivoRoles);
        }

        public static void AgregarRol() 
        {
            Console.Clear();
            string nombre = "";
            bool esValido = false;

            while (!esValido)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Nombre del nuevo rol: ");
                Console.ResetColor();
                nombre = Console.ReadLine()?.Trim()!;
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ Error: El nombre no puede estar vacío.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Clear(); 
                    continue;
                }
                bool yaExiste = false;
                for (int i = 0; i < Program.roles.Length; i++)
                {
                    if (Program.roles[i].ToLower() == nombre.ToLower())
                    {
                        yaExiste = true;
                        i = Program.roles.Length;
                    }
                }
                if (yaExiste)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"⚠️ Error: El rol '{nombre}' ya existe.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Clear();
                    continue;
                }
                esValido = true;
                for (int i = 0; i < nombre.Length; i++)
                {
                    if (!char.IsLetter(nombre[i]) && !char.IsWhiteSpace(nombre[i]))
                    {
                        esValido = false;
                        i = nombre.Length;
                    }
                }
                if (!esValido)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ Error: Use solo letras (sin números).");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
            string[] nuevo = new string[Program.roles.Length + 1];
            for (int i = 0; i < Program.roles.Length; i++)
            { 
                nuevo[i] = Program.roles[i];
            }
            nuevo[nuevo.Length - 1] = nombre;
            Program.roles = nuevo;
            File.WriteAllLines(archivoRoles, Program.roles);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n☑️  Rol añadido.");
            Console.ResetColor();
            Thread.Sleep(1000);
        }
        public static void ModificarRol() 
        {
            Console.Clear();
            if (Program.roles.Length <= 2) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo hay roles adicionales para modificar.");
                Console.ResetColor();
                Thread.Sleep(1500);
                return;
            }
            for (int i = 2; i < Program.roles.Length; i++)      
            {
                Console.WriteLine($"{i + 1}. {Program.roles[i]}");
            }
            int seleccionado = 0;
            bool seleccionValida = false;
            while (!seleccionValida)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("\nNúmero de rol a modificar: ");
                if (int.TryParse(Console.ReadLine(), out seleccionado) && seleccionado > 2 && seleccionado <= Program.roles.Length)     
                {
                    seleccionValida = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ Error: Seleccione un número válido (3 en adelante).");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Clear();
                    for (int i = 2; i < Program.roles.Length; i++)
                    { 
                        Console.WriteLine($"{i + 1}. {Program.roles[i]}");
                    }
                }
            }
            string nuevoNombre = "";
            bool nombreValido = false;
            while (!nombreValido)       
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"\nNuevo nombre para {Program.roles[seleccionado - 1]}: ");
                nuevoNombre = Console.ReadLine()?.Trim()!;

                if (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ El nombre no puede estar vacío.");
                    Console.ResetColor();
                    Thread.Sleep(500);
                    Console.Clear();
                    continue;
                }
                nombreValido = true;
                for (int i = 0; i < nuevoNombre.Length; i++)
                {
                    if (!char.IsLetter(nuevoNombre[i]) && !char.IsWhiteSpace(nuevoNombre[i]))
                    {
                        nombreValido = false;
                        i = nuevoNombre.Length;
                    }
                }
                if (!nombreValido)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ Error: Use solo letras (sin números ni símbolos).");
                    Console.ResetColor();
                    Thread.Sleep(500);
                    Console.Clear();
                }
            }
            Program.roles[seleccionado - 1] = nuevoNombre;
            File.WriteAllLines(archivoRoles, Program.roles);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n☑️ Rol actualizado.");
            Console.ResetColor();
            Thread.Sleep(1000);
        }
        public static void EliminarRol() 
        {
            Console.Clear();
            if (Program.roles.Length <= 2) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay roles adicionales para eliminar.");
                Console.ResetColor();
                Thread.Sleep(1500);
                return;
            }
            for (int i = 2; i < Program.roles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Program.roles[i]}");
            }
            int seleccionado = 0;
            bool seleccionValida = false;
            while (!seleccionValida)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("\nNúmero de rol a eliminar: ");
                if (int.TryParse(Console.ReadLine(), out seleccionado) && seleccionado > 2 && seleccionado <= Program.roles.Length)
                {
                    seleccionValida = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("⚠️ Error: Ingrese un número válido de la lista.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    Console.Clear();
                    for (int i = 2; i < Program.roles.Length; i++) 
                    {
                        Console.WriteLine($"{i + 1}. {Program.roles[i]}");
                    }
                }
            }
            string[] nuevo = new string[Program.roles.Length - 1];
            int j = 0;
            for (int i = 0; i < Program.roles.Length; i++) 
            {
                if (i == seleccionado - 1) continue;
                nuevo[j] = Program.roles[i];
                j++;
            }
            Program.roles = nuevo;
            File.WriteAllLines(archivoRoles, Program.roles);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n☑️  Rol eliminado correctamente.");
            Console.ResetColor();
            Thread.Sleep(1000);
        }
    }
}