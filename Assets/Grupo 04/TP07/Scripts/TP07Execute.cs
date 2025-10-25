using MyBST;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TP07Execute : MonoBehaviour
{
    private AVLTree<int> tree = new();

    [SerializeField] private TextAsset jsonName;

    [SerializeField] private RectTransform contentHolder;

    [SerializeField] private GameObject displayNamePrefab;

    [SerializeField] TMP_InputField inputField;


    private Dictionary<string, int> scoreDictionary = new Dictionary<string, int>();

    [System.Serializable]
    public class NamesList{public List<string> names;}

    private List<string> names = new();


    private void Start()
    {
        NamesList namesList = JsonUtility.FromJson<NamesList>(jsonName.text);

        names = namesList.names;

        Debug.Log(names.Count);
     
        foreach (string name in namesList.names)
        {
            int randScore = Random.Range(100, 10000);
            tree.Insert(randScore);

            
        }

        DrawLeaderboard();

    }
    private void DrawLeaderboard()
    {
        // Limpiar lo que haya antes
        foreach (Transform child in contentHolder)
            Destroy(child.gameObject);

        // Obtener lista ordenada del árbol
        List<int> orderedScores = tree.InOrderList();

        // Crear un item por cada puntaje
        foreach (int score in orderedScores)
        {
            GameObject newItem = Instantiate(displayNamePrefab, contentHolder);
            TMPro.TMP_Text text = newItem.GetComponentInChildren<TMPro.TMP_Text>();
            text.text = score.ToString();
        }
    }

    public void InsertScore() 
    {
        string inputText = inputField.text;

        if (int.TryParse(inputText, out int value))
        {
            tree.Insert(value);
            inputField.text = "";
        }
        else
        {
            Debug.Log("invalid dataa");
        }

        DrawLeaderboard(); 

    }

}

