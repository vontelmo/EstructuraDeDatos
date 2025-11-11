using MyBST;
using MyLinkedList;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class TP07Execute : MonoBehaviour
{
    int counter;

    private AVLTree<int> tree = new();

    SimpleList<string> names = new SimpleList<string>() {
    "KARNO", "VELTA", "SIRAN", "DOLME", "FARON", "MERIX", "TALEN", "JORAX", "NELVA", "BIRAN",
    "LORAX", "FERON", "GALDA", "TIRAN", "MORIX", "CERAN", "DALVO", "XERON", "PAVEN", "NORIX",
    "VELON", "HARIX", "SOMEN", "DERAX", "LIRAN", "TORIX", "KARIX", "VERON", "NALVO", "MIRAN",
    "BORIX", "TERAN", "JARIX", "YERON", "PALIX", "MORAN", "XELTA", "DARIX", "NELON", "FERIX",
    "SOLAN", "MERAN", "GARIX", "VORAN", "KELON", "HIRAX", "TALIX", "NIRAN", "PELON", "JORIN",
    "DARON", "LEROX", "ZALEN", "PORAN", "MEROX", "VALON", "SORIN", "WORAN", "KIRAN", "TERIX",
    "LARON", "XARIX", "NELIX", "VIRAN", "QARON", "ZERIX", "TORAN", "MELIX", "SARON", "JIRAX",
    "RALON", "LERAN", "KORIX", "NERAN", "FIRAN", "WALIX", "YARON", "QIRAN", "MIRIX", "TARON",
    "BELIX", "CARON", "DORIX", "FERAX", "HARON", "JIROX", "LERIX", "MORAX", "NORAN", "PERIX",
    "RANDY", "RARIX", "SERAN", "TARIX", "VERAN", "XARON", "YERIX", "ZORAN", "KERIX", "LORIN"};


    [SerializeField] private RectTransform contentHolder;

    [SerializeField] private GameObject displayNamePrefab;

    [SerializeField] TMP_InputField inputField;

    SimpleList<int> orderedScores = new SimpleList<int>();

    MyList<string> leaderboard = new MyList<string>();


    private Dictionary<string, int> scoreDictionary = new Dictionary<string, int>();

    private void Start()
    {
        for (int i = 0; i < names.Count ; i++)
        {
            int randScore = Random.Range(100, 10000);
            tree.Insert(randScore);
        }
        orderedScores = tree.InOrderList();
        //Debug.Log(names.Count + " " + orderedScores.Count.ToString());

        for (int i = 0; i <100; i++)
        {
            string name = names[Random.Range(0, names.Count)];
            leaderboard.Add($"{name}: {orderedScores[i]}");
            //scoreDictionary.Add(name, orderedScores[i]);
            names.Remove(name);
        }

        DrawLeaderboard();

    }

    private void DrawLeaderboard()
    {

        foreach (Transform child in contentHolder)
            Destroy(child.gameObject);

        int index = 0;

        foreach (int value in orderedScores)
        {
            GameObject newItem = Instantiate(displayNamePrefab, contentHolder);
            TMP_Text text = newItem.GetComponentInChildren<TMP_Text>();

            text.text = leaderboard[index];
            index++;
        }
    }

    public void InsertScore() 
    {
        string inputText = inputField.text;

        if (int.TryParse(inputText, out int value))
        {
            tree.Insert(value);
            orderedScores = tree.InOrderList();
            
            string lastName = "PLAYER_" + counter;

            int index = -1;

            for (int i = 0; i < orderedScores.Count; i++) 
            {
                if (value == orderedScores[i])
                {
                    index = i;
                    break;
                }
            }

            leaderboard.Insert(index, $"{lastName}: {value}");


            inputField.text = "";
            
        }
        else
        {
            Debug.Log("invalid dataa");
        }
        counter++;
        DrawLeaderboard(); 

    }

}

