using UnityEngine;
using MyLinkedList; 

public class TP02Execute : MonoBehaviour
{
    void Start()
    {
        // Crear una lista de enteros
        MyList<int> lista = new MyList<int>();

        // Agregar elementos
        lista.Add(10);
        lista.Add(20);
        lista.Add(30);
        Debug.Log("Lista inicial: " + lista);

        // Insertar en índice 1
        lista.Insert(1, 15);
        Debug.Log("Después de Insert(1, 15): " + lista);

        // Remover un valor
        lista.Remove(20);
        Debug.Log("Después de Remove(20): " + lista);

        // Remover por índice
        lista.RemoveAt(1);
        Debug.Log("Después de RemoveAt(1): " + lista);

        // Agregar varios elementos con array
        lista.AddRange(new int[] { 40, 50, 60 });
        Debug.Log("Después de AddRange([40,50,60]): " + lista);

        // Acceder a un índice con indexador
        Debug.Log("Elemento en índice 2: " + lista[2]);

        // Vaciar la lista
        lista.Clear();
        Debug.Log("Después de Clear(): " + lista + " (Count=" + lista.Count + ")");
    }

    void Update()
    {
        // No necesitamos nada en Update por ahora
    }
}