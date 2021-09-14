using System;

namespace Parte_1
{
    public class Heap
    { 
        internal Node[] Queue;
        int p;
        internal class Node
        {
            internal Node Right;
            internal Node Left;
            internal Node Parent;
            IHuffman.Node Key;
            internal IHuffman.Node Keys
            {
                get => Key;
                set => Key = value;
            }
        }
        Node root;
        internal Heap(int x)
        {
            Queue = new Node[x + 1];
            p = 1;
        }

        internal void Add(IHuffman.Node value)
        {
            if(root == null)
            {
                root = new Node();
                root.Keys = value;
                Queue[p] = root;
                p++;
            }
            else
            {
                Node add = new Node
                {
                    Keys = value
                };
                //hijo izquierdo
                if(p%2 == 0)
                {
                    Queue[p / 2].Left = add;
                    add.Parent = Queue[p / 2];
                }
                else
                {
                    Queue[p / 2].Right = add;
                    add.Parent = Queue[p / 2];
                }
                Queue[p] = add;
                Ordenar(p);
                p++;
            }
        }

       
        void Ordenar(int x)
        {
            if (Queue[x] != null)
            {
                if (Queue[x].Keys.Prio < Queue[x/2].Keys.Prio)
                {
                    IHuffman.Node prov = Queue[x].Keys;
                    Queue[x].Keys = Queue[x/2].Keys;
                    Queue[x / 2].Keys = prov;
                    if (x/2 != 1)
                    {
                        Ordenar(x / 2);
                    }
                }
            }    
        }

        void OrdenarRaiz(int x)
        {
            //hijo izquierdo y que hijo izquierdo sea menor que hijo derecho
            if (Queue[(x * 2) + 1] != null)
            {
                if (Queue[x * 2].Keys.Prio < Queue[x].Keys.Prio && Queue[(x * 2) + 1].Keys.Prio >= Queue[x * 2].Keys.Prio)
                {
                    IHuffman.Node prov = Queue[x].Keys;
                    Queue[x].Keys = Queue[x * 2].Keys;
                    Queue[x * 2].Keys = prov;
                    if ((x * 2) * 2 < p || ((x * 2) * 2) + 1 < p)
                    {
                        OrdenarRaiz(x * 2);
                    }
                }
                else if (Queue[(x * 2) + 1].Keys.Prio < Queue[x].Keys.Prio)
                {
                    IHuffman.Node prov = Queue[x].Keys;
                    Queue[x].Keys = Queue[(x * 2) + 1].Keys;
                    Queue[(x * 2) + 1].Keys = prov;
                    if (((x * 2) + 1) * 2 < p || (((x * 2) + 1) * 2) + 1 < p)
                    {
                        OrdenarRaiz((x * 2) + 1);
                    }
                }
            }
            else if(Queue[(x * 2) + 1] == null)
            {
                if (Queue[x * 2].Keys.Prio < Queue[x].Keys.Prio)
                {
                    IHuffman.Node prov = Queue[x].Keys;
                    Queue[x].Keys = Queue[x * 2].Keys;
                    Queue[x * 2].Keys = prov;
                    if ((x * 2) * 2 < p || ((x * 2) * 2) + 1 < p)
                    {
                        OrdenarRaiz(x * 2);
                    }
                }
            }
        }

        internal IHuffman.Node Pop()
        {
            IHuffman.Node pop = root.Keys;
            if(Queue[2] != null)
            {
                Queue[1].Keys = Queue[p - 1].Keys;
                Queue[p - 1] = null;
                p--;
                if(p > 2)
                {
                    OrdenarRaiz(1);
                }
            }
            else
            {
                p = 1;
                root = null;
            }
            return pop;
        }

        internal int Count()
        {
            return p - 1;
        }
    }
}
