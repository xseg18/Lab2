using System;
using Parte_1;

namespace Parte_2
{
    class Program
    {
        static void Main(string[] args)
        {
            IHuffman h = new ImplementationClass();
            string prueba = h.Comprimir("Cuando cuentes cuentos, cuenta cuántos cuentos cuentas; porque si no cuentas cuántos cuentos cuentas, nunca sabrás cuántos cuentos cuentas tú");
            string prueba2 = h.Descomprimir(prueba);
        }
    }
}
