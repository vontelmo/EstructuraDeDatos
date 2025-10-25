using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TP09Execute : MonoBehaviour
{
    private MyALGraph<GameObject> graph = new MyALGraph<GameObject>();

    [SerializeField] private string[] planetsNames;

    [SerializeField] private GameObject[] planets;
    
    public void AddPlanet()
    {
        graph.AddVertex(planets[Random.Range(0, planets.Length)]);
    }
}
