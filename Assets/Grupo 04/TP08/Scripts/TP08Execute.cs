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
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string result = "";
        for (int i = 0; i < length; i++)
        {
            result += chars[Random.Range(0, chars.Length)];
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
