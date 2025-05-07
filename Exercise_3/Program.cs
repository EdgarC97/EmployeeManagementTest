namespace Punto3
{
    class Program
    {
        static void Main(string[] args)
        {
            ImprimirNumeros(0);
        }

        static void ImprimirNumeros(int numero)
        {
            if (numero > 100) return;

            Console.WriteLine(numero);
            ImprimirNumeros(numero + 3);
        }
    }
}
