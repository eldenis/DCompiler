using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CDb.Compilacion;

namespace CDb.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    var texto = string.Join(" ", args);
                    Console.WriteLine("Iniciando compilación de : \n\n{0}\n\n", texto);
                    MostrarResultadoCompilacion(CompiladorDb.Compilar(texto));
                }
                catch (ExCompilacion ex)
                {
                    Console.WriteLine("Se produjo la siguiente excepción de compilación: " + ex.Message);
                }
            }
            else
                Console.WriteLine("Debe escribir el texto a compilar.");


            Console.WriteLine("\nPresione una tecla para cerrar el programa.");
            Console.ReadKey();
        }

        private static void MostrarResultadoCompilacion(ResCompilacion compilacion)
        {
            Console.WriteLine("Compilación concluida con éxito!\n");

            Console.WriteLine("Cantidad de comentarios: {0}", compilacion.Comentarios.Count);

            for (int i = 0; i < compilacion.Comentarios.Count; i++)
            {
                Console.WriteLine("[{0}, Pos {1}] : {2}", i + 1, compilacion.Comentarios[i].Posicion, compilacion.Comentarios[i]);
            }

            Console.WriteLine("\nTexto Sin Comentarios: {0}", compilacion.TextoSinComentarios);
        }
    }
}
