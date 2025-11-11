using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;



public class TP09Execute : MonoBehaviour
{

    private MyALGraph<Planet> planetGraph = new MyALGraph<Planet>();

    [SerializeField] private Planet[] planetList;

    [SerializeField] private GameObject planetPrefab;

    [SerializeField] private RectTransform buttonsPlanetGrid;
    [SerializeField] private RectTransform hotbarPlanetGrid;
    [SerializeField] private GameObject GraphSpreadSheet;


    [SerializeField] private TMP_Text textResult;
    [SerializeField] private TMP_Text completedGraph;


    List<string> completedList = new List<string>();
    private List<Planet> selectedPath = new List<Planet>();

    int travelWeight = 0;

    private void Start()
    {
        GraphSpreadSheet.SetActive(false);
        CreateGalaxy();
        completedList = CreateGraphList(planetGraph.NodeList.Keys.ToArray());
        CreateStartButtons();

        foreach (string s in completedList)
        {
            TMP_Text text = completedGraph.GetComponentInChildren<TMP_Text>();
            text.text += s + "\n";
        }

    }

    public void CreateStartButtons()
    {
        foreach (Planet planet in planetList)
        {
            GameObject newPlanet = Instantiate(planetPrefab, buttonsPlanetGrid);

            Image icon = newPlanet.GetComponentInChildren<Image>();
            icon.sprite = planet.Image;

            Button btn = newPlanet.GetComponent<Button>();
            Planet capturedPlanet = planet;

            btn.onClick.AddListener(() => OnPlanetClicked(capturedPlanet, btn));
        }
    }

    private void OnPlanetClicked(Planet capturedPlanet, Button btn)
    {
        btn.enabled = !btn.enabled;
        selectedPath.Add(capturedPlanet);

        GameObject newHotbarButton = Instantiate(planetPrefab, hotbarPlanetGrid);
        newHotbarButton.GetComponentInChildren<Image>().sprite = capturedPlanet.Image;
        Button hotbarBtn = newHotbarButton.GetComponent<Button>();

        hotbarBtn.onClick.AddListener(() => OnHotbarBtnClicked(newHotbarButton, btn, capturedPlanet));



        Debug.Log("added : " + capturedPlanet.name + " " + selectedPath.Count);
    }

    private void OnHotbarBtnClicked(GameObject hotbatBtn, Button btn, Planet planet)
    {
        selectedPath.Remove(planet);
        Destroy(hotbatBtn);
        btn.enabled = !btn.enabled;
    }

    public void CheckSelectedPath()
    {
        if (selectedPath.Count < 2)
        {
            Debug.LogWarning("Select more than one planet...");
            return;
        }

        bool validPath = true;

        for (int i = 0; i < selectedPath.Count - 1; i++)
        {
            Planet current = selectedPath[i];
            Planet next = selectedPath[i + 1];

            if (!planetGraph.ContainsEdge(current, next))
            {
                validPath = false;
                textResult.text = "Invalid Path :(";
                break;
            }

            travelWeight += (int)planetGraph.GetWeight(current, next); 

        }
        if (validPath)
        {
            textResult.text = $"Valid Path :D, Total Weight of the path: {travelWeight}";
        }
        travelWeight = 0;
    }

    public void SpreedSheetOnOff()
    {
        
        GraphSpreadSheet.SetActive(!GraphSpreadSheet.activeSelf);

    }

    private List<string> CreateGraphList(Planet[] planetCollection)
    {
        List<(Planet, int)> list = new();
        List<string> graphList = new List<string>();
        foreach (Planet planet in planetCollection)
        {
            list = planetGraph.NodeList[planet];
            for (int i = 0; i < list.Count; i++) 
            {
                graphList.Add(list[i].Item1.ToString() + " " + list[i].Item2 + " : " + planet.name);
            }

        }
        return graphList;
    }

    private void CreateGalaxy()
    {
        foreach (Planet planetVertex in planetList)
        {
            planetGraph.AddVertex(planetVertex);

            foreach(Planet planetEdge in planetList)
            {
                if (RandomInt(100) > 80 && planetVertex != planetEdge)
                    planetGraph.AddEdge(planetVertex, (planetEdge, RandomInt(10)));
            }
        }
    }

    private int RandomInt(int range) { return Random.Range(0, range); }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GUIStyle black = new GUIStyle();
        black.normal.textColor = Color.black;
        black.fontStyle = FontStyle.Bold;

        if (planetGraph == null || planetGraph.NodeList == null || planetGraph.NodeList.Count == 0)
            return;

        Gizmos.color = Color.cyan;

        // asigna posiciones espaciales a cada planeta
        Dictionary<Planet, Vector3> planetPositions = new Dictionary<Planet, Vector3>();

        float radius = 4f;
        int total = planetGraph.NodeList.Count;
        int index = 0;

        // distribuir planetas en círculo
        foreach (Planet planet in planetGraph.NodeList.Keys)
        {
            float angle = index * Mathf.PI * 2 / total;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            planetPositions[planet] = pos;
            index++;
        }

        // dibujar nodos y aristas
        foreach (var kvp in planetGraph.NodeList)
        {
            Planet planet = kvp.Key;
            Vector3 planetPos = planetPositions[planet];

            // Dibuja el nodo
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(planetPos, 0.1f);
            UnityEditor.Handles.Label(planetPos + Vector3.up * 0.5f, planet.name, black);

            // Dibuja las conexiones
            Gizmos.color = Color.black;
            foreach (var edge in kvp.Value)
            {
                Planet target = edge.Item1;
                int weight = edge.Item2;

                if (planetPositions.ContainsKey(target))
                {
                    Vector3 targetPos = planetPositions[target];
                    Gizmos.DrawLine(planetPos, targetPos);

                    // dibujar peso
                    Vector3 mid = (planetPos + targetPos) / 2;
                    UnityEditor.Handles.Label(mid, weight.ToString());
                }
            }
        }
    }
#endif

}
