using System;
using System.Collections.Generic;
using System.Text;

namespace MyLinkedList
{
    // Lista doblemente enlazada
    public class MyList<T>
    {
        // Nodo inicial (root)
        private MyNode<T> root;
        public MyNode<T> Root { get => root; private set => root = value; }

        // Nodo final (tail)
        private MyNode<T> tail;
        public MyNode<T> Tail { get => tail; private set=> tail = value; }

        // Constructor
        public MyList()
        {
            root = null;
            tail = null;
            Count = 0;
        }

        // Cantidad de elementos en la lista
        public int Count { get; private set; }

        // Indexador: permite acceder a un elemento por índice
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index out of range");

                MyNode<T> current = root;
                for (int i = 0; i < index; i++)
                    current = current.Next;

                return current.Value;
            }
        }

        // Agrega un nuevo valor al final de la lista
        public void Add(T value)
        {
            MyNode<T> newNode = new MyNode<T>(value);

            if (tail != null)
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            else
            {
                // Lista vacía: root y tail apuntan al nuevo nodo
                root = newNode;
                tail = newNode;
            }

            Root = root;
            Tail = tail;
            Count++;
        }

        // Agrega todos los elementos de otra MyList
        public void AddRange(MyList<T> values)
        {
            if (values == null || values.Count == 0) return;

            MyNode<T> current = values.root;
            while (current != null)
            {
                Add(current.Value);
                current = current.Next;
            }
        }

        // Agrega todos los elementos de un array
        public void AddRange(T[] values)
        {
            if (values == null) return;
            foreach (T value in values)
            {
                Add(value);
            }
        }

        // Elimina la primera aparición de un valor
        public bool Remove(T value)
        {
            if (root == null) return false;

            MyNode<T> current = root;

            while (current != null)
            {
                if (current.IsEquals(value))
                {
                    if (current == root && current == tail)
                    {
                        // Único nodo en la lista
                        root = tail = null;
                    }
                    else if (current == root)
                    {
                        // Primer nodo
                        root = root.Next;
                        root.Prev = null;
                    }
                    else if (current == tail)
                    {
                        // Último nodo
                        tail = tail.Prev;
                        tail.Next = null;
                    }
                    else
                    {
                        // Nodo intermedio
                        current.Prev.Next = current.Next;
                        current.Next.Prev = current.Prev;
                    }

                    Root = root;
                    Tail = tail;
                    Count--;
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        // Elimina el nodo en un índice específico
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range");

            MyNode<T> current = root;
            for (int i = 0; i < index; i++)
                current = current.Next;

            Remove(current.Value);
        }

        // Inserta un valor en un índice específico
        public void Insert(int index, T value)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range");

            MyNode<T> newNode = new MyNode<T>(value);

            if (index == 0)
            {
                // Insertar al principio
                if (root != null)
                {
                    newNode.Next = root;
                    root.Prev = newNode;
                    root = newNode;
                }
                else
                {
                    root = tail = newNode;
                }
            }
            else if (index == Count)
            {
                // Insertar al final
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            else
            {
                // Insertar en medio
                MyNode<T> current = root;
                for (int i = 0; i < index; i++)
                    current = current.Next;

                newNode.Next = current;
                newNode.Prev = current.Prev;
                current.Prev.Next = newNode;
                current.Prev = newNode;
            }

            Root = root;
            Tail = tail;
            Count++;
        }

        // Devuelve true si la lista está vacía
        public bool IsEmpty() => Count == 0;

        // Vacía la lista
        public void Clear()
        {
            root = null;
            tail = null;
            Count = 0;

            Root = null;
            Tail = null;
        }

        // Devuelve la lista como string, mostrando los enlaces
        public override string ToString()
        {
            var current = root;
            StringBuilder sb = new StringBuilder();

            while (current != null)
            {
                sb.Append(current.Value);
                if (current.Next != null)
                    sb.Append(" <-> ");
                current = current.Next;
            }

            return sb.ToString();
        }
    }
}