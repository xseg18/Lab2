using System;
using Parte_1;

namespace Parte_2
{
    class Program
    {
        static void Main(string[] args)
        {
            IHuffman h = new ImplementationClass();
            string prueba = h.Comprimir("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ullamcorper eros ut convallis placerat. Praesent tincidunt facilisis orci in aliquet. Sed convallis sapien erat, ac cursus felis tempus nec. Ut placerat tortor felis, sed accumsan velit elementum quis. Pellentesque finibus elit iaculis, luctus metus vitae, volutpat odio. Fusce feugiat quis lectus et dapibus. Nullam rutrum nisl massa, sed aliquet nisi faucibus non. Etiam eu ullamcorper metus. Sed vel leo aliquet, ullamcorper felis vitae, commodo velit. Phasellus aliquet condimentum convallis. Integer id ultrices magna. Mauris semper vitae odio non commodo.");
            string prueba2 = h.Descomprimir(prueba);
        }
    }
}
