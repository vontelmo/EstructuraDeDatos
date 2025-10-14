using System.Collections.Generic;

namespace MyLinkedList
{
    // Node doblemente enlazado
    public class MyNode<T>
    {
        // Valor que guarda el Node
        public T Value { get; set; }

        // Node anterior
        public MyNode<T> Prev { get; set; }

        // Node siguiente
        public MyNode<T> Next { get; set; }

        // Constructor del Node
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

        // Compara el valor del Node con otro valor
        public bool IsEquals(T value)
        {
            return EqualityComparer<T>.Default.Equals(Value, value);
        }
    }
}