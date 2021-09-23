using System;
using Parte_1;
using Lab3;
using System.Text;

namespace Parte_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //IHuffman h = new ImplementationClass();
            //string prueba = h.Comprimir("Cuando cuentes cuentos, cuenta cuántos cuentos cuentas; porque si no cuentas cuántos cuentos cuentas, nunca sabrás cuántos cuentos cuentas tú");
            //string prueba2 = h.Descomprimir(prueba);
            ILZW lzw = new ImplementationClassL();
            string d = lzw.Comprimir("WABBAWABBAWABBAWABBA");
            int prueba = d.Length;
            double fileCount = Convert.ToDouble(Encoding.UTF8.GetByteCount("WABBAWABBAWABBAWABBA"));
            double compressedCount = Convert.ToDouble(Encoding.ASCII.GetByteCount(d));
            //Como poco coco como\npoco coco compro\nComo compro poco coco\npoco coco como
        }
    }
}
