using System;
using System.Media;
using System.Net.Http.Headers;

namespace Ruleta_Estudiantes
{
    public static class Ruleta
    {
        private static Random numeroRandom = new Random();
        public static bool[][]? registroRoles;
        private static SoundPlayer sonidoGiro = new SoundPlayer(@"audio/ruleta_giro.wav");
        private static SoundPlayer sonidoGanador = new SoundPlayer(@"audio/ganador.wav");
        private static SoundPlayer sonidoCelebracion = new SoundPlayer(@"audio/ganadoresGenerales.wav");
        private static bool omitirAnimacion = false;
        public static void GirarRuleta()
        {
            if (Program.estudiantes == null || Program.estudiantes.Length < Program.roles.Length)       
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine( "╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine( " 🚫  ERROR: Faltan estudiantes.                                   ");
                Console.WriteLine($" Necesitas al menos {Program.roles.Length} para cubrir roles. ");
                Console.WriteLine( "╚══════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.ReadKey(true);
                return;
            }

            if (registroRoles == null || registroRoles.Length != Program.roles.Length || registroRoles[0].Length != Program.estudiantes.Length)
            {
                registroRoles = new bool[Program.roles.Length][];
                for (int i = 0; i < Program.roles.Length; i++)
                {
                    registroRoles[i] = new bool[Program.estudiantes.Length];
                }
            }
            int[] indicesGanadoresDeEstaRonda = new int[Program.roles.Length];
            string[] nombresGanadores = new string[Program.roles.Length];
            omitirAnimacion = false;
            for (int i = 0; i < Program.roles.Length; i++)
            {
                int indiceElegido = -1;
                int intentos = 0;
                while (intentos < 1000)
                {
                    int elejido = numeroRandom.Next(Program.estudiantes.Length);
                    bool yaSalioEnEstaRonda = false;
                    for (int j = 0; j < i; j++)
                    {
                        if (indicesGanadoresDeEstaRonda[j] == elejido) 
                        {
                            yaSalioEnEstaRonda = true;
                        }
                    }
                    if (!registroRoles[i][elejido] && !yaSalioEnEstaRonda)
                    {
                        indiceElegido = elejido;
                        break;
                    }
                    intentos++;
                }

                if (indiceElegido == -1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"║⚠️ No hay combinaciones libres para el rol: {Program.roles[i]}");   
                    Console.WriteLine( "║⚠️ Reinicie el sistema para limpiar el historial de seleccionados");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ResetColor();
                    Console.ReadKey(true);
                    return;
                }
                indicesGanadoresDeEstaRonda[i] = indiceElegido;
                nombresGanadores[i] = Program.estudiantes[indiceElegido];
                AnimacionSeleccion(Program.roles[i], nombresGanadores[i]);
                registroRoles[i][indiceElegido] = true;
            }
            try { sonidoCelebracion.Play();} catch{ }
            MostrarResultadoGeneral(Program.roles, nombresGanadores);
            try { sonidoCelebracion.Stop(); } catch { }
            Metodos.RegistrarEnHistorial(Program.roles, nombresGanadores);
        }
        private static int SeleccionarIndiceDisponible(bool[] registro, int excluir = -1)
        {
            int intentos = 0;
            while (intentos < 500)
            {
                int r = numeroRandom.Next(Program.estudiantes.Length);
                if (!registro[r] && r != excluir) return r;
                intentos++;
            }
            return -1; 
        }
        private static void MostrarResultadoGeneral(string[] nombresRoles, string[] ganadores)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║        ¡RESULTADOS DE LA SELECCIÓN!        ║");
            Console.WriteLine("╠════════════════════════════════════════════╣");

            for (int i = 0; i < nombresRoles.Length; i++)
            {
                Console.ForegroundColor = (i % 2 == 0) ? ConsoleColor.Cyan : ConsoleColor.Magenta;
                string linea = $"{nombresRoles[i].ToUpper()}: {ganadores[i]}";
                Console.WriteLine($"║ {linea.PadRight(42)} ║");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("\nPresione una tecla para volver al menú...");
            Console.ReadKey(true);
        }
        private static void AnimacionSeleccion(string rol, string ganador)
        {
            if (omitirAnimacion) {return;}
            int iteraciones = 65;
            int retardo = 60;
            try 
            { 
                sonidoGiro.PlayLooping();
            } 
            catch
            {
                
            }

            for (int i = 0; i < iteraciones; i++)
            {
                if (Console.KeyAvailable)
                {
                    var tecla = Console.ReadKey(true).Key;
                    if (tecla == ConsoleKey.O)
                    {
                        omitirAnimacion = true;
                        sonidoGiro.Stop();
                        return;
                    }
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(" [ Presiones 'O' para saltar la animacion ]\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n  SELECCIONANDO: {rol.ToUpper()}...");
                Console.ResetColor();
                string nombreFalso = Program.estudiantes[numeroRandom.Next(Program.estudiantes.Length)];
                Console.WriteLine("\n\t" + Program.estudiantes[numeroRandom.Next(Program.estudiantes.Length)]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\t=> {nombreFalso.ToUpper()} ");
                Console.ResetColor();
                Console.WriteLine("\t" + Program.estudiantes[numeroRandom.Next(Program.estudiantes.Length)]);
                Thread.Sleep(retardo);
                if (i > iteraciones - 12) 
                {
                    retardo += 80;
                }
            }
            sonidoGiro.Stop(); 
            try 
            { 
                sonidoGanador.Play();
            } 
            catch { }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ☑️ {rol.ToUpper()} ASIGNADO:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"\n\t=> {ganador.ToUpper()} ");
            Console.ResetColor();
            Thread.Sleep(2500);
        }
    }
}