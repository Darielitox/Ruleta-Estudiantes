using System;
using System.Diagnostics;
using System.Threading;

namespace Ruleta_Estudiantes
{
    public static class GUI 
    {
        public static bool ConfirmarSalida()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("╔════════════════════════════════╗");
                Console.WriteLine("║   ¿SEGURAS QUE DESEAS SALIR?   ║");
                Console.WriteLine("║      [S] SI  /  [N] NO         ║");
                Console.WriteLine("╚════════════════════════════════╝");
                Console.ResetColor();
                ConsoleKey tecla = Console.ReadKey(true).Key;
                if (tecla == ConsoleKey.S)
                { 
                    return true;
                }
                else if (tecla == ConsoleKey.N)
                { 
                    return false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Solo tiene 2 opciones, S/N");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                }
            }
        }
        public static int MostrarMenu(string[] opciones, int seleccionado, bool esMenuPrincipal = false)
        {
            ConsoleKey tecla;
            Console.CursorVisible = false;
            do
            {
                Console.Clear();
                MostrarBanner(); 
                for (int i = 0; i < opciones.Length; i++)
                {
                    if (i == seleccionado)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"> {opciones[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"  {opciones[i]}");
                    }
                }
                if (esMenuPrincipal)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n---------------------------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("                      [Escape] SALIR DEL PROGRAMA                    "); 
                    Console.ResetColor();
                }
                tecla = Console.ReadKey(true).Key;
                if (esMenuPrincipal && tecla == ConsoleKey.Escape)
                { 
                    return -1;
                }
                if (tecla == ConsoleKey.UpArrow || tecla == ConsoleKey.W)
                {
                    seleccionado = (seleccionado == 0) ? opciones.Length - 1 : seleccionado - 1;
                }
                else if (tecla == ConsoleKey.DownArrow || tecla == ConsoleKey.S)
                {
                    seleccionado = (seleccionado == opciones.Length - 1) ? 0 : seleccionado + 1;
                }
            } while (tecla != ConsoleKey.Enter);
            return seleccionado;
        }
        public static void MostrarBanner()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            string tituloParte1 = @"
        ██████╗ ██╗   ██╗██╗     ███████╗████████╗ █████╗ 
        ██╔══██╗██║   ██║██║     ██╔════╝╚══██╔══╝██╔══██╗
        ██████╔╝██║   ██║██║     █████╗     ██║   ███████║
        ██╔══██╗██║   ██║██║     ██╔══╝     ██║   ██╔══██║
        ██║  ██║╚██████╔╝███████╗███████╗   ██║   ██║  ██║
        ╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚══════╝   ╚═╝   ╚═╝  ╚═╝";
            ImprimirMulticolor(tituloParte1);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            string tituloParte2 = @"
 █████╗ ██╗     ███████╗ █████╗ ████████╗ ██████╗ ██████╗ ██╗ █████╗ 
██╔══██╗██║     ██╔════╝██╔══██╗╚══██╔══╝██╔═══██╗██╔══██╗██║██╔══██╗
███████║██║     █████╗  ███████║   ██║   ██║   ██║██████╔╝██║███████║
██╔══██║██║     ██╔══╝  ██╔══██║   ██║   ██║   ██║██╔══██╗██║██╔══██║
██║  ██║███████╗███████╗██║  ██║   ██║   ╚██████╔╝██║  ██║██║██║  ██║
╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚═╝╚═╝  ╚═╝";
            ImprimirMulticolor(tituloParte2);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("                            DE ESTUDIANTES                             ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("-----------------------------------------------------------------------");
            ImprimirMulticolor("by Dariel De Jesus 2025-2155");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.ResetColor();
        }
        private static void ImprimirMulticolor(string texto)
        {
            ConsoleColor[] colores = {ConsoleColor.Red, ConsoleColor.DarkYellow, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.DarkCyan, ConsoleColor.Cyan, ConsoleColor.DarkMagenta, ConsoleColor.Magenta};
            Console.Write("                     ");
            for (int i = 0; i < texto.Length; i++)
            {
                Console.ForegroundColor = colores[i % colores.Length];
                Console.Write(texto[i]);
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}