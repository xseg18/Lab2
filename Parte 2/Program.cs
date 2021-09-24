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
            string d = lzw.Comprimir("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla et imperdiet lectus, vitae suscipit risus. Vestibulum vehicula id erat at auctor. Ut a dapibus massa. In a enim id sapien vulputate sodales a vitae erat. Etiam elementum ut erat a eleifend. Praesent rutrum congue velit, et pulvinar sapien fringilla sed. Morbi tristique rutrum enim ac sodales. Phasellus lobortis nisi libero, commodo varius magna efficitur rhoncus. Quisque sodales sem eu dui dapibus vehicula. Vivamus aliquam accumsan magna, vitae pretium libero laoreet in.\nProin id iaculis lacus.Nullam hendrerit dignissim cursus.Integer nec purus massa.Pellentesque rutrum, dui sit amet commodo imperdiet, erat diam tincidunt ligula, et consequat mi ligula dapibus eros.Sed semper ligula sed leo ultricies pharetra.Nunc sit amet tempor mauris.Phasellus quis gravida eros.Proin tempor purus laoreet sapien suscipit, eget rutrum lorem ultricies.Praesent commodo fermentum rutrum.Integer luctus gravida massa, sit amet pharetra massa vestibulum vitae.In viverra molestie ligula vitae fermentum.In consequat aliquam vehicula.Donec sit amet semper dui.\nSed vel laoreet ipsum, in interdum massa.Donec nisl velit, ullamcorper eu massa a, viverra porttitor ipsum.Donec id laoreet urna.Suspendisse ullamcorper gravida ipsum et lacinia.Duis fermentum libero sit amet sollicitudin dignissim.Etiam eget elit a nisi molestie finibus lobortis eget lectus.Vestibulum a velit sit amet felis suscipit posuere ac eget ipsum.Suspendisse sed sem felis.\nCurabitur aliquam varius dolor non mollis.Proin nec facilisis arcu.Phasellus a cursus arcu.Nam augue risus, pharetra sit amet pellentesque pulvinar, rhoncus at enim.Quisque volutpat lobortis elit.Vestibulum iaculis iaculis auctor.Curabitur vitae nulla id turpis vulputate malesuada at quis augue.Pellentesque vestibulum augue eget odio vestibulum aliquet.Mauris congue quam purus, quis pellentesque diam molestie sed.Duis viverra orci risus, sit amet condimentum nisl eleifend ultrices.Ut tincidunt nunc at felis elementum, sit amet tincidunt turpis lacinia.Aliquam ut lacus diam.Vestibulum nec ligula eleifend, sagittis nunc vitae, laoreet nulla.Nulla auctor pulvinar erat blandit suscipit.\nSed a velit a nulla finibus finibus nec sit amet sem.Morbi nec vulputate dui, et fermentum enim.Sed volutpat turpis mauris, a semper diam sagittis ac.Donec at urna nec quam interdum eleifend.Quisque eget libero non ipsum pharetra congue.Curabitur ut purus a lacus ultrices placerat quis sed ipsum.Phasellus enim ligula, interdum quis elit a, lobortis hendrerit lectus.Nam sem dui, tempus id purus sit amet, elementum cursus erat.Aliquam nunc libero, commodo eu fermentum in, accumsan ac risus.Curabitur sapien metus, imperdiet in gravida et, rutrum et velit.Nulla finibus purus mi.Nullam dignissim turpis vel odio dapibus, in pellentesque enim euismod.Sed rhoncus neque lectus, et rhoncus ipsum congue ac.Integer elit velit, faucibus vel tortor et, lobortis ultrices sapien.");
            string r = lzw.Descomprimir(d);
            string a = lzw.Comprimir("WABBAWABBAWABBAWABBA");
            string b = lzw.Descomprimir(a);
            int prueba = d.Length;
            double fileCount = Convert.ToDouble(Encoding.UTF8.GetByteCount("Como poco coco como\npoco coco compro\nComo compro poco coco\npoco coco como"));
            double compressedCount = Convert.ToDouble(Encoding.ASCII.GetByteCount(d));
            //Como poco coco como\npoco coco compro\nComo compro poco coco\npoco coco como
            //WABBAWABBAWABBAWABBA
        }
    }
}
