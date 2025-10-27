using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TP08Execute : MonoBehaviour
{
    MySetArray<Item> playerInventory = new MySetArray<Item>();
    MySetArray<Item> npcInventory = new MySetArray<Item>();

    MySetList<Item> tradeList = new MySetList<Item>();

    [SerializeField] private RectTransform playerContainer;
    [SerializeField] private RectTransform npcContainer;
    [SerializeField] private RectTransform tradeContainer;

    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private int itemCount = 40;
    private SimpleList<Item> runtimeItems = new SimpleList<Item>();

    private void Start()
    {
        GenerateRuntimeItems();
        FillInventories();
        DrawInventories();
    }

    private void GenerateRuntimeItems()
    {
        for (int i = 0; i < itemCount; i++)
        {
            Item newItem = ScriptableObject.CreateInstance<Item>();
            newItem.objName = GenerateRandomName(5);
            runtimeItems.Add(newItem);
        }
    }

    private void DrawInventories()
    {
        // Limpiar contenedores
        foreach (Transform child in playerContainer) Destroy(child.gameObject);
        foreach (Transform child in npcContainer) Destroy(child.gameObject);
        foreach (Transform child in tradeContainer) Destroy(child.gameObject);

        // Dibujar jugador
        foreach (Item item in playerInventory)
        {
            CreateItemUI(item, playerContainer, Color.cyan);
        }

        // Dibujar NPC
        foreach (Item item in npcInventory)
        {
            CreateItemUI(item, npcContainer, Color.yellow);
        }

    }

    private void CreateItemUI(Item item, RectTransform parent, Color bgColor)
    {
        GameObject newItemUI = Instantiate(itemPrefab, parent);

        Image bg = newItemUI.GetComponent<Image>();
        if (bg != null) bg.color = bgColor;

        TMP_Text[] texts = newItemUI.GetComponentsInChildren<TMP_Text>();
        if (texts.Length > 0) texts[0].text = item.objName;
    }

    private string GenerateRandomName(int length)
    {
        string[] names = new string[]
        {
        "KARNO", "VELTA", "SIRAN", "DOLME", "FARON", "MERIX", "TALEN", "JORAX", "NELVA", "BIRAN",
        "LORAX", "FERON", "GALDA", "TIRAN", "MORIX", "CERAN", "DALVO", "XERON", "PAVEN", "NORIX",
        "VELON", "HARIX", "SOMEN", "DERAX", "LIRAN", "TORIX", "KARIX", "VERON", "NALVO", "MIRAN",
        "BORIX", "TERAN", "JARIX", "YERON", "PALIX", "MORAN", "XELTA", "DARIX", "NELON", "FERIX",
        "SOLAN", "MERAN", "GARIX", "VORAN", "KELON", "HIRAX", "TALIX", "NIRAN", "PELON", "JORIN",
        "DARON", "LERIX", "ZALEN", "PORAN", "MERIX", "VALON", "SORIN", "WORAN", "KIRAN", "TERIX",
        "LARON", "XARIX", "NELIX", "VIRAN", "QARON", "ZERIX", "TORAN", "MELIX", "SARON", "JIRAX",
        "RALON", "LERAN", "KORIX", "NERAN", "FIRAN", "WALIX", "YARON", "QIRAN", "MIRIX", "TARON",
        "BELIX", "CARON", "DORIX", "FERAX", "HARON", "JORAX", "LERIX", "MORAX", "NORAN", "PERIX",
        "QORAN", "RARIX", "SERAN", "TARIX", "VERAN", "XARON", "YERIX", "ZORAN", "KERIX", "LORIN"
        };
        string result = "";

        for (int i = 0; i < length; i++)
        {
            result += names[Random.Range(0, names.Length)];
        }
        return result;
    }


    private void FillInventories()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Random.value <= 0.7f) 
            {
                int randomIndex = Random.Range(0, runtimeItems.Count);
                playerInventory.Add(runtimeItems[randomIndex]);
            }

            if (Random.value <= 0.7f)
            {
                int randomIndex = Random.Range(0, runtimeItems.Count);
                npcInventory.Add(runtimeItems[randomIndex]);
            }
        }
    }

    public void ShowUnion()
    {
        tradeList.Clear();
        tradeList.Union(playerInventory, npcInventory);
        DisplayTradeList();
    }

    public void ShowIntersection()
    {
        tradeList.Clear();
        tradeList.Intersect(playerInventory, npcInventory);
        DisplayTradeList();
    }

    public void ShowDifference()
    {
        tradeList.Clear();
        tradeList.Difference(playerInventory, npcInventory);
        DisplayTradeList();
    }

    public void ShowQuantity()
    {
        Debug.Log("items player : " + playerInventory.Cardinality() + " items NPC : " + npcInventory.Cardinality());
    }

    public void ShowMissingItems()
    {
        tradeList.Clear();
        MySetList<Item> unionSet = new MySetList<Item>();
        unionSet.Union(playerInventory, npcInventory);

        foreach (Item item in runtimeItems)
        {
            if (!unionSet.Contains(item))
                tradeList.Add(item);
        }

        DisplayTradeList();

    }

    private void DisplayTradeList()
    {
        foreach (Transform child in tradeContainer)
            Destroy(child.gameObject);


        foreach (Item item in tradeList)
        {
            GameObject newItem = Instantiate(itemPrefab, tradeContainer);
            newItem.GetComponentInChildren<TMPro.TMP_Text>().text = item.objName;

        }
    }

}
