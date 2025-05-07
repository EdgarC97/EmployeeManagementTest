using System;
using System.Collections.Generic;

namespace Punto4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] entradas = { "hat", "abc", "Zu6" };
            int contador = 1;
            foreach (var palabra in entradas)
            {
                var permutaciones = ObtenerPermutaciones(palabra);
                Console.WriteLine($"{contador} {string.Join(",", permutaciones)}");
                contador++;
            }
        }

        static List<string> ObtenerPermutaciones(string texto)
        {
            var resultado = new List<string>();
            Permutar("", texto, resultado);
            return resultado;
        }

        static void Permutar(string prefijo, string resto, List<string> resultado)
        {
            if (resto.Length == 0)
            {
                resultado.Add(prefijo);
            }
            else
            {
                for (int i = 0; i < resto.Length; i++)
                {
                    string nuevoPrefijo = prefijo + resto[i];
                    string nuevoResto = resto.Substring(0, i) + resto.Substring(i + 1);
                    Permutar(nuevoPrefijo, nuevoResto, resultado);
                }
            }
        }
    }
}
