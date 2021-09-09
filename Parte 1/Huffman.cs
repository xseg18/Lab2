using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parte_1
{
    interface IHuffman
    {
        internal class Node
        {
            int Priority;
            char Symbol;
            string Add;
            internal Node Left;
            internal Node Right;
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
        }
        public string Comprimir(string compress);
        public string Descomprimir(string decompress);
    }

    class ImplementationClass : IHuffman
    {

        public string IHuffman.Comprimir(string compress)
        {
            Dictionary<char, int> Frequencies = new Dictionary<char, int>();
            for (int i = 0; i < compress.Length; i++)
            {
                if (!Frequencies.ContainsKey(compress[i]))
                {
                    Frequencies.Add(compress[i], 0);
                }
                Frequencies[compress[i]]++;
            }
            Heap PrioHeap = new Heap(Frequencies.Count);
            foreach (KeyValuePair<char, int> symbol in Frequencies)
            {
                IHuffman.Node a = new IHuffman.Node
                {
                    Symb = symbol.Key,
                    Prio = symbol.Value
                };
                PrioHeap.Add(a);
            }
            while (2 <= PrioHeap.Count())
            {
                IHuffman.Node Left = PrioHeap.Pop();
                IHuffman.Node Right = PrioHeap.Pop();
                IHuffman.Node Parent = new IHuffman.Node
                {
                    Prio = Left.Prio + Right.Prio,
                    Symb = '*',
                    Left = Left,
                    Right = Right
                };
                PrioHeap.Add(Parent);
            }
            
            return 
        }

        public string IHuffman.Descomprimir(string decompress)
        {
            return
        }
    }
}
