using System;
using System.Threading;

namespace Ruleta_Estudiantes
{
    class Program
    {
        public static string[] estudiantes = Metodos.CargarEstudiantes();
        public static string[] roles = Metodos.CargarRoles();
        static void Main(string[] args)
        {
            Metodos.IniciarArchivoHistorial();
            string[] opcioneMenu = {"🎰 - INICIAR RULETA", "📄 - HISTORIAL", "🎓 - ADMINISTRAR ESTUDIANTES", "🔑 - ADMINISTRAR ROLES", "🔄️ - REINICIAR SISTEMA"};
            bool salidaBuclePrincipal = false;
            int posicionSeleccionador = 0;

            do
            {
                Console.Clear();
                posicionSeleccionador = GUI.MostrarMenu(opcioneMenu, posicionSeleccionador, true);
                if (posicionSeleccionador == -1)
                {
                    if (GUI.ConfirmarSalida())
                    {
                        Console.Clear();
                        Console.WriteLine("SALIENDO DEL PROGRAMA.....");
                        Thread.Sleep(500);
                        salidaBuclePrincipal = true;
                        continue;
                    }
                    else 
                    {
                        posicionSeleccionador = 0;
                        continue; 
                    }
                }

                switch (posicionSeleccionador)
                {
                    case 0: 
                        Ruleta.GirarRuleta();
                    break;

                    case 1: 
                        string[] opcionesMenuHistorial = {"VER HISTORIAL", "VACIAR HISTORIAL", "VOLVER"};
                        int seleccionadorOpcionesHistorial = 0;
                        bool salidaMenuHistorial = false;
                        do
                        {
                            seleccionadorOpcionesHistorial = GUI.MostrarMenu(opcionesMenuHistorial, seleccionadorOpcionesHistorial);
                            if (seleccionadorOpcionesHistorial == 0) 
                            {
                                Metodos.verHistorial();
                            }
                            else if (seleccionadorOpcionesHistorial == 1)
                            {
                                bool salidaBucleLimpieza = false;
                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("¿Está seguro de querer limpiar el historial? [S] SI / [N] NO");
                                    ConsoleKey teclaConfirmacion = Console.ReadKey(intercept: true).Key;
                                    if (teclaConfirmacion == ConsoleKey.S)
                                    {
                                        Metodos.LimpiarHistorial();
                                        Thread.Sleep(1000);
                                        salidaBucleLimpieza = true;
                                    }
                                    else if (teclaConfirmacion == ConsoleKey.N)
                                    {
                                        Console.WriteLine("Volviendo al menú...");
                                        Thread.Sleep(1000);
                                        salidaBucleLimpieza = true;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\n Solo puede presionar S o N.");
                                        Console.ResetColor();
                                        Thread.Sleep(1000);
                                    }
                                } while (!salidaBucleLimpieza);
                            }
                            else
                            {
                                salidaMenuHistorial = true;
                            }
                        } while (!salidaMenuHistorial);
                    break;

                    case 2: 
                        string[] opcionesAdmin = { "AÑADIR ESTUDIANTE", "MODIFICAR ESTUDIANTE", "ELIMINAR ESTUDIANTE", "VER LISTA", "VOLVER" };
                        int seleccionadorOpcionesAdmin = 0;
                        bool salidaMenuAdmin = false;
                        do
                        {
                            seleccionadorOpcionesAdmin = GUI.MostrarMenu(opcionesAdmin, seleccionadorOpcionesAdmin);
                            switch (seleccionadorOpcionesAdmin)
                            {
                                case 0: Metodos.AgregarEstudiante(); break;
                                case 1: Metodos.ModificarEstudiante(); break;
                                case 2: Metodos.EliminarEstudiante(); break;
                                case 3: 
                                    Console.Clear();
                                    Console.WriteLine("=== LISTA ACTUAL DE ESTUDIANTES ===");
                                    for (int i = 0; i < Program.estudiantes.Length; i++) 
                                    {
                                        Console.WriteLine($"- {Program.estudiantes[i]}");
                                    }
                                    Console.WriteLine("\nPresione una tecla para continuar...");
                                    Console.ReadKey(true);
                                break;
                                case 4: salidaMenuAdmin = true; break;
                            }
                        } while (!salidaMenuAdmin);
                    break;

                    case 3: 
                        string[] opcionesRoles = { "AÑADIR ROL", "MODIFICAR ROL", "ELIMINAR ROL", "VER ROLES", "VOLVER" };
                        int seleccionadorOpcionesRoles = 0;
                        bool salirMenuRoles = false;
                        do 
                        {
                            seleccionadorOpcionesRoles = GUI.MostrarMenu(opcionesRoles, seleccionadorOpcionesRoles);
                            switch (seleccionadorOpcionesRoles) {
                                case 0: Metodos.AgregarRol(); break;
                                case 1: Metodos.ModificarRol(); break;
                                case 2: Metodos.EliminarRol(); break;
                                case 3:
                                    Console.Clear();
                                    Console.WriteLine("=== ROLES ACTUALES ===");
                                    for(int i=0; i<Program.roles.Length; i++) Console.WriteLine($"{i+1}. {Program.roles[i]}");
                                    Console.WriteLine("\nPresione una tecla...");
                                    Console.ReadKey(true);
                                break;
                                case 4: salirMenuRoles = true; break;
                            }
                        } while (!salirMenuRoles);
                    break;

                    case 4:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("¿Seguro que desea reiniciar el progreso de la sesión? S/N");
                        Console.ResetColor();
                        ConsoleKey teclaReinicio = Console.ReadKey(true).Key;
                        if (teclaReinicio == ConsoleKey.S)
                        {
                            Ruleta.registroRoles = null;
                            Program.estudiantes = Metodos.CargarEstudiantes();
                            Program.roles = Metodos.CargarRoles();
                            Metodos.EscribirNuevaEjecucion();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n ⚠️ Sistema restablecido.");
                            Console.WriteLine(" ⚠️ Todos los estudiantes y roles han sido reiniciados.");
                            Console.ResetColor();
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Operación cancelada. Volviendo al menú...");
                            Console.ResetColor();
                            Thread.Sleep(1000);
                        }    
                    break;
                }
            } while (!salidaBuclePrincipal);
        }
    }

}