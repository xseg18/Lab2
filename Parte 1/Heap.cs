using System;

namespace Parte_1
{
    public class Heap<T, Y> where T: IComparable
    {
        internal T[] Queue;
        int p;
        internal class Node
        {
            internal Node Right;
            internal Node Left;
            internal Node Parent;
            T prio;
            Y Key;
            internal T Priority
            {
                get => prio;
                set => prio = value;
            }
        }
        Node root;
        public Heap(int x)
        {
            Queue = new T[x];
            p = 1;
        }
        public void Add(T value)
        {
            if(root == null)
            {
                root = new Node();
                Queue[p] = value;
                p++;
            }
            else
            {
                Node add = FindChild(root);
                add.Priority = value;
                Queue[p] = value;
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
                    if(raiz.Priority.CompareTo(raiz.Left.Priority) > 0)
                    {
                        T prov = raiz.Left.Priority;
                        raiz.Left.Priority = raiz.;
                        raiz.Priority = 
                    }
                }
            }
        }
        struct 
    }
}
