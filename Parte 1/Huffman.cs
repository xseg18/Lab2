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
            internal List<char> Recorrido(Node search, List<char> binary)
            {
                if(search != null)
                {
                    if(search.Parent.Left == search)
                    {
                        if (search.Parent.Parent != null)
                        {
                            Recorrido(search.Parent, binary);
                        }
                        binary.Add('0');
                    }
                    else
                    {
                        if (search.Parent.Parent != null)
                        {
                            Recorrido(search.Parent, binary);
                        }
                        binary.Add('1');
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
        public string Descomprimir(string decompress);
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
            List<char> binary = new List<char>();
            for (int i = 0; i < compress.Length; i++)
            {
                IHuffman.Node s = Parent.Find(compress[i], Parent);
                Parent.Recorrido(s, binary);
            }
            //string final 
            String final = string.Empty;
            //Leyenda de letras
            foreach(var par in Leyenda)
            {
                final += par.Key + "[" + par.Value.ToString() + "]";
            }
            //Final de metadata
            final += "!eNd¡";
            //archivo comprimido    
            string p = "";
            for (int i = 0; i < binary.Count; i++)
            {
                p+= binary[i];
                if(p.Length == 8)
                {
                    final += Convert.ToChar(BinDec(Convert.ToInt32(p)));
                    p = "";
                }
            }
            if(p != "")
            {
                while (p.Length < 8)
                {
                    p += '0';
                }
                final += Convert.ToChar(BinDec(Convert.ToInt32(p)));
            }
            int x = final.Length;
            return final;
        }
        string IHuffman.Descomprimir(string decompress)
        {
            //encontrar metadata
            int pos = 0;
            string info = string.Empty;
            while (!info.Contains("!eNd¡"))
            {
                info += decompress[pos];
                pos++;
            }
            //quitamos metadata
            decompress = decompress.Remove(0, pos);
            //quitamos separador
            info = info.Replace("!eNd¡", "");
            //array de pares ordenados
            string[] par = info.Split(']');
            //pares ordenados separados
            string[] frequencies = info.Split('[', ']');
            //heap y contador de caracteres
            Heap PrioHeap = new Heap(par.Length);
            int charcount = 0;
            for (int i = 0; i < frequencies.Length - 1; i++)
            {
                IHuffman.Node a = new IHuffman.Node
                {
                    Symb = Convert.ToChar(frequencies[i]),
                    Prio = Convert.ToInt32(frequencies[i+1])
                };
                charcount += Convert.ToInt32(frequencies[i + 1]);
                PrioHeap.Add(a);
                i++;
            }
            //creación codigos prefijo
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
            //diccionario códigos prefijo
            Dictionary<char, string> Prefix = new Dictionary<char, string>();
            for (int i = 0; i < par.Length - 1; i++)
            {
                IHuffman.Node found = Parent.Find(par[i][0], Parent);
                List<char> binary = new List<char>();
                Parent.Recorrido(found, binary);
                string path = "";
                for (int j = 0; j < binary.Count; j++)
                {
                    path += binary[j];
                }
                Prefix.Add(par[i][0], path); 
            }
            //descompresión de archivo 
            string actualpath = "";
            string final = "";
            char[] deco = decompress.ToArray<char>();
            //de caracteres a prefijos
            for (int i = 0; i < decompress.Length; i++)
            {
                actualpath += DecBin(decompress[i]);
            }
            //de prefijos a letras
            int cc = 0;
            while (charcount != cc)
            {
                foreach (var pars in Prefix)
                {
                    if (actualpath.StartsWith(pars.Value))
                    {
                        final += pars.Key;
                        actualpath = actualpath.Substring(pars.Value.Length);
                        break;
                    }
                }
                cc++;
            }
            return final;
        }
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
        string DecBin(int decim)
        {
            long bin = 0;
            const int div = 2;
            long digit = 0;
            for (int i = decim % div, j = 0; decim > 0; decim /= div, i = decim % div, j++)
            {
                digit = i % div;
                bin += digit * (long)Math.Pow(10, j);
            }
            string Bin = bin.ToString();
            while (Bin.Length < 8)
            {
                Bin = "0" + Bin;
            }
            return Bin;
        }
    }
}
