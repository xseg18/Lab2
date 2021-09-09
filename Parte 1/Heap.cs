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
            internal IHuffman.Node Key;
            internal IHuffman.Node Keys
            {
                get => Key;
                set => Key = value;
            }
        }
        Node root;
        internal Heap(int x)
        {
            Queue = new Node[x];
            p = 1;
        }

        internal void Add(IHuffman.Node value)
        {
            if(root == null)
            {
                root = new Node();
                root.Key = value;
                Queue[p] = root;
                p++;
            }
            else
            {
                Node add = FindChild(root);
                add.Key = value;
                Queue[p] = add;
                p++;
                Ordenar(root);
            }
        }

        Node FindChild(Node raiz)
        {
            
            if(raiz.Left == null)
            {
                raiz.Left = new Node();
                raiz.Left.Parent = raiz;
                return raiz.Left;
            }
            else if(raiz.Right == null)
            {
                raiz.Right = new Node();
                raiz.Right.Parent = raiz;
                return raiz.Right;
            }
            else
            {
                if(FindChild(raiz.Left) != null)
                {
                    return FindChild(raiz.Left);
                } 
                else if(FindChild(raiz.Right) != null)
                {
                    return FindChild(raiz.Right);
                }
            }
            return null;
        }
        void Ordenar(Node raiz)
        {
            if(raiz != null)
            {
                if(raiz.Left != null)
                {
                    if(raiz.Keys.Prio > raiz.Left.Keys.Prio)
                    {
                        IHuffman.Node prov = raiz.Left.Keys;
                        raiz.Left.Keys = raiz.Keys;
                        raiz.Keys = prov;
                        if(raiz != root)
                        {
                            Ordenar(raiz.Parent);
                        }
                    }
                }
                else if(raiz.Right != null)
                {
                    if (raiz.Keys.Prio > raiz.Right.Keys.Prio)
                    {
                        IHuffman.Node prov = raiz.Right.Keys;
                        raiz.Right.Keys = raiz.Keys;
                        raiz.Keys = prov;
                        if (raiz != root)
                        {
                            Ordenar(raiz.Parent);
                        }
                    }
                }
                Ordenar(raiz.Left);
                Ordenar(raiz.Right);
            }
        }
        internal Node FindLast()
        {
            Node last;
            if (p -1 != 1)
            {
                last = Queue[p - 1];
                Node parentdata = last.Parent;
                if ((p - 1) % 2 == 0)
                {
                    parentdata.Left = null;
                    Queue[p - 1] = null;
                }
                else
                {
                    parentdata.Right = null;
                    Queue[p - 1] = null;
                }
                p--;
                return last;
            }
            else
            {
                return root;
            }
        }
        internal IHuffman.Node Pop()
        {
            Node pop = root;
            if(Queue[2].Equals(null))
            {
                Node temp = FindLast();
                root.Key = temp.Key;
                Ordenar(root);
            }
            else
            {
                root = null;
                Queue[1] = null;
            }
            return pop.Key;
        }

        internal int Count()
        {
            return p;
        }
    }
}
