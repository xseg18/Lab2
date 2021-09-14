using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace Parte_1
{
    public interface IHuffman
    {
        //nodo árbol
        internal class Node
        {
            int Priority;
            char Symbol;
            internal Node Left;
            internal Node Right;
            internal Node Parent;
            internal int Prio
            {
                get => Priority;
                set => Priority = value;
            }
            internal char Symb
            {
                get => Symbol;
                set => Symbol = value;
            }
            //recorrido para código prefijo
            internal List<bool> Recorrido(Node search, List<bool> binary)
            {
                if(search != null)
                {
                    if(search.Parent.Left == search)
                    {
                        binary.Add(false);
                        if (search.Parent.Parent != null)
                        {
                            Recorrido(search.Parent, binary);
                        }
                    }
                    else
                    {
                        binary.Add(true);
                        if (search.Parent.Parent != null)
                        {
                            Recorrido(search.Parent, binary);
                        }
                    }
                    return binary;
                }
                return null;
            }
            //encuentra el nodo a partir del caracter
            internal Node Find(char s, Node root)
            {
                if (root != null)
                {
                    if (root.Symb == s)
                    {
                        return root;
                    }
                    else
                    {
                        Node found = Find(s, root.Left);
                        if (found == null)
                        {
                            found = Find(s, root.Right);
                        }
                        return found;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        public string Comprimir(string compress);
        //public string Descomprimir(string decompress);
    }

    public class ImplementationClass : IHuffman
    {
        string IHuffman.Comprimir(string compress)
        {
            //diccionario donde se guardan las frecuencias
            Dictionary<char, int> Leyenda = new Dictionary<char, int>();
            for (int i = 0; i < compress.Length; i++)
            {
                if (!Leyenda.ContainsKey(compress[i]))
                {
                    Leyenda.Add(compress[i], 0);
                }
                Leyenda[compress[i]]++;
            }
            //cola de prioridad
            Heap PrioHeap = new Heap(Leyenda.Count);
            foreach (KeyValuePair<char, int> symbol in Leyenda)
            {
                IHuffman.Node a = new IHuffman.Node
                {
                    Symb = symbol.Key,
                    Prio = symbol.Value
                };
                PrioHeap.Add(a);
            }
            //árbol de códigos prefijo
            IHuffman.Node Parent = new IHuffman.Node();
            while (2 <= PrioHeap.Count())
            {
                IHuffman.Node Left = PrioHeap.Pop();
                IHuffman.Node Right = PrioHeap.Pop();
                Parent = new IHuffman.Node
                {
                    Prio = Left.Prio + Right.Prio,
                    Left = Left,
                    Right = Right
                };
                Left.Parent = Parent;
                Right.Parent = Parent;
                PrioHeap.Add(Parent);
            }
            //código binario de todo el árbol
            List<bool> binary = new List<bool>();
            for (int i = 0; i < compress.Length; i++)
            {
                IHuffman.Node s = Parent.Find(compress[i], Parent);
                Parent.Recorrido(s, binary);
            }
            //string final 
            String final = string.Empty;
            //número de bytes a leer
            int res = Leyenda.Values.Max();
            int byteslectura = 0;
            if(Leyenda.Values.Max() > 255)
            {
                while (res > 0)
                {
                    byteslectura++;
                    res -= 255;
                }
                byteslectura++;
            }
            else
            {
                byteslectura = 1;
            }
            final += byteslectura.ToString() + '\n';
            //Leyenda de letras
            char[] chars = Leyenda.Keys.ToArray<char>();
            for (int i = 0; i < Leyenda.Count; i++)
            {
                final += chars[i];
                int contchars = 1;
                while(contchars <= byteslectura)
                {
                    if(Leyenda[chars[i]] <= 255)
                    {
                        final += "[" + Convert.ToChar(Leyenda[chars[i]]) + "]";
                        Leyenda[chars[i]] = 0;
                    }
                    else if(Leyenda[chars[i]] == 0)
                    {
                        final += "["+""+"]";
                    }
                    else
                    {
                        final += "[" + Convert.ToChar(255) + "]";
                        Leyenda[chars[i]] -= 255;
                    }
                    contchars++;
                }
            }
            final += '\n';
            //Final de metadata
            final += "METADATA-ENDS!HERE*'\n";
            //archivo comprimido
            string p = string.Empty;
            for (int i = 0; i < binary.Count; i++)
            {
                p+= BitConverter.ToString(BitConverter.GetBytes(binary[i]));
                binary.RemoveAt(i);
                if(p.Length >= 8)
                {
                    string x = p.Substring(0, 8);
                    p = p.Remove(0,8);
                    final += Convert.ToChar(BinDec(Convert.ToInt32(x)));
                }
            }

            return final;
        }
        //public string IHuffman.Descomprimir(string decompress)
        //{
        //    return
        //}
    int BinDec(long binary)
    {

        int decim = 0;
        int digit = 0;
        const int div = 10;

        for (long i = binary, j = 0; i > 0; i /= div, j++)
        {
            digit = (int)i % div;
            if (digit != 1 && digit != 0)
            {
                return -1;
            }
            decim += digit * (int)Math.Pow(2, j);
        }
        return decim;
    }
    }
}
