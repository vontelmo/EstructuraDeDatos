using System.Collections.Generic;

namespace MyLinkedList
{
    // Nodo doblemente enlazado
    public class MyNode<T>
    {
        // Valor que guarda el nodo
        public T Value { get; set; }

        // Nodo anterior
        public MyNode<T> Prev { get; set; }

        // Nodo siguiente
        public MyNode<T> Next { get; set; }

        // Constructor del nodo
        public MyNode(T value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }

        // Devuelve el valor como string (seguro ante null)
        public override string ToString()
        {
            return Value?.ToString() ?? "null";
        }

        // Compara el valor del nodo con otro valor
        public bool IsEquals(T value)
        {
            return EqualityComparer<T>.Default.Equals(Value, value);
        }
    }
}