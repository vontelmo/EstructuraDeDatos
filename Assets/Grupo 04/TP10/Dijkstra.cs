using System.Collections.Generic;
using System.Linq;

public class Dijkstra
{
    public static Dictionary<MyGraphNode, (MyGraphNode predecesor, int distance)> ExecuteDijksta (MyALGraph<MyGraphNode> graph, MyGraphNode origin)
    {
        Dictionary<MyGraphNode, (MyGraphNode, int)> result = new();

        // Coleccion de nodos no vistados
        Queue<MyGraphNode> UnvisitedNodes = new();

        // Coleccion de visitados
        HashSet<MyGraphNode> visitedNodes = new HashSet<MyGraphNode>();

        // encolamos el primer nodo
        UnvisitedNodes.Enqueue(origin);

        //Inicializar distancias
        foreach (var node in graph.Vertices)
        {
            result[node] = (null, int.MaxValue);
        }
        result[origin] = (null, 0);

        //Sacan nodos no visitados mientras hayan nodos no visitados
        while (UnvisitedNodes.Count > 0)
        {
            MyGraphNode currentNode = UnvisitedNodes.Dequeue();
            List<MyGraphNode> unvisitedNeighbors = new();

            //Actualizar vecinos
            foreach (MyGraphNode neighbor in currentNode.neighbors.Keys)
            {
                //-Chequear los costos del nodo que estas visitando a cada vecino, SI NO estan ya visitados
                if (!visitedNodes.Contains(neighbor))
                {
                    //-Sumar esos costos al costo que tiene el nodo que estas visitando en el diccionario
                    int costToNeighbor = (int)graph.GetWeight(currentNode, neighbor);
                    
                    //Si esto fuera A*, a totalCost le sumamos la heuristica (calculo estimado desde este vecino al destination)
                    int totalCost = costToNeighbor + result[currentNode].Item2;

                    //-Para cada vecino, si la suma de ambos costos es < al costo actual del vecino en el diccionario, actualizamos ese valor
                    if (totalCost < result[neighbor].Item2)
                    {
                        result[neighbor] = (currentNode, totalCost);
                    }

                    unvisitedNeighbors.Add(neighbor);
                }
            }

            //Ordenamos la lista de vecinos de menor a mayor por el costo total del diccionario
            unvisitedNeighbors = unvisitedNeighbors.OrderBy(node => result[node].Item2).ToList();

            //-Encolamos todos los vecinos en orden de menor costo en el diccionario a mayor
            foreach (MyGraphNode neighbor in unvisitedNeighbors)
            {
                UnvisitedNodes.Enqueue(neighbor);
            }

            //Ya visitamos el actual
            visitedNodes.Add(currentNode);
        }

        return result;
    }

    //Aca iria lo mismo que el Dijkstra anterior, PERO
    //1) En vez de int, float (lo sacan de los neighbors de cada nodo)
    //2) No necesitan el grafo (idem punto anterior)
    //3) Cuando encuentran el nodo destination dentro del while, retorna el camino calculado
    //4) Si llegan al final del while y no encontraron el nodo destination, devuelven la lista vacia
    //5) Para reconstruir el path, usan el previo de cada nodo, desde el ultimo (seria, del diccionario el Item1) hasta el origin

    public List<MyGraphNode> ExecuteDijkstaPathfinding(MyGraphNode origin, MyGraphNode destination, MyALGraph<MyGraphNode> graph)
    {
        Dictionary<MyGraphNode, (MyGraphNode, float)> result = new();

        // Coleccion de nodos no vistados
        Queue<MyGraphNode> UnvisitedNodes = new();

        // Coleccion de visitados
        HashSet<MyGraphNode> visitedNodes = new HashSet<MyGraphNode>();

        // encolamos el primer nodo
        UnvisitedNodes.Enqueue(origin);

        //Inicializar distancias
        foreach (MyGraphNode node in graph.Vertices)
        {
            result[node] = (null, int.MaxValue);
        }
        result[origin] = (null, 0);

        //Sacan nodos no visitados mientras hayan nodos no visitados
        while (UnvisitedNodes.Count > 0)
        {
            MyGraphNode currentNode = UnvisitedNodes.Dequeue();
            List<MyGraphNode> unvisitedNeighbors = new();

            if (currentNode.Equals(destination))
            {
                // Reconstruir path
                List<MyGraphNode> path = new();
                MyGraphNode node = destination;

                while (node != null)
                {
                    path.Add(node);
                    node = result[node].Item1;
                }

                path.Reverse();
                return path;
            }

            //Actualizar vecinos
            foreach (var neighbor in currentNode.neighbors.Keys)
            {
                //-Chequear los costos del nodo que estas visitando a cada vecino, SI NO estan ya visitados
                if (!visitedNodes.Contains(neighbor))
                {
                    //-Sumar esos costos al costo que tiene el nodo que estas visitando en el diccionario
                    float costToNeighbor = result[currentNode].Item2 + result[neighbor].Item2;

                    //Si esto fuera A*, a totalCost le sumamos la heuristica (calculo estimado desde este vecino al destination)
                    float totalCost = costToNeighbor + result[currentNode].Item2;

                    //-Para cada vecino, si la suma de ambos costos es < al costo actual del vecino en el diccionario, actualizamos ese valor
                    if (totalCost < result[neighbor].Item2)
                    {
                        result[neighbor] = (currentNode, totalCost);
                    }

                    unvisitedNeighbors.Add(neighbor);
                }
            }

            //Ordenamos la lista de vecinos de menor a mayor por el costo total del diccionario
            unvisitedNeighbors.OrderBy(node => result[node].Item2).ToList();

            //-Encolamos todos los vecinos en orden de menor costo en el diccionario a mayor
            foreach (var neighbor in unvisitedNeighbors)
            {
                UnvisitedNodes.Enqueue(neighbor);
            }

            //Ya visitamos el actual
            visitedNodes.Add(currentNode);
        }

        // No se encontro destino
        return new List<MyGraphNode>();
    }

    //Si fueran a hacer A*, seria mejor usar una list que una Queue, asi siempre sacan el de menor costo
}
