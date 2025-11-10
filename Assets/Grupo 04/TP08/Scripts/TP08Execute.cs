using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TP08Execute : MonoBehaviour
{
    MySetArray<Item> playerInventory = new MySetArray<Item>(20);
    MySetArray<Item> npcInventory = new MySetArray<Item>(20);

    MySetList<Item> tradeList = new MySetList<Item>();

    [SerializeField] private RectTransform playerContainer;
    [SerializeField] private RectTransform npcContainer;
    [SerializeField] private RectTransform tradeContainer;

    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private TMP_Text countText;

    private SimpleList<Item> inventorytItems = new SimpleList<Item>();

    

    private void Start()
    {

        Item[] items = Resources.LoadAll<Item>("Items");

        foreach (Item item in items)
        {
            inventorytItems.Add(item);
        }

        FillInventories();
        DrawInventories();
        ShowQuantity();

    }

    private void DrawInventories()
    {
        // Limpiar contenedores
        foreach (Transform child in playerContainer) Destroy(child.gameObject);
        foreach (Transform child in npcContainer) Destroy(child.gameObject);

        // Dibujar jugador
        foreach (Item item in playerInventory)
        {
            if(item != null)
            CreateItemUI(item, playerContainer);
        }

        // Dibujar NPC
        foreach (Item item in npcInventory)
        {
            if (item != null)
            CreateItemUI(item, npcContainer);
        }

    }

    private void CreateItemUI(Item item, RectTransform parent)
    {
        GameObject newItemUI = Instantiate(itemPrefab, parent);

        Image bg = newItemUI.GetComponentInChildren<Image>();

        bg.sprite = item.icon; 

        TMP_Text[] texts = newItemUI.GetComponentsInChildren<TMP_Text>();
        if (texts.Length > 0) texts[0].text = item.name;
    }

    private void FillInventories()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Random.value <= 0.7f) 
            {
                int randomIndex = Random.Range(0, inventorytItems.Count);
                playerInventory.Add(inventorytItems[randomIndex]);
            }

            if (Random.value <= 0.7f)
            {
                int randomIndex = Random.Range(0, inventorytItems.Count);
                npcInventory.Add(inventorytItems[randomIndex]);
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
        countText.text = "items player : " + playerInventory.Cardinality() + " items NPC : " + npcInventory.Cardinality();
    }

    public void ShowMissingItems()
    {
        tradeList.Clear();
        MySetList<Item> unionSet = new MySetList<Item>();
        unionSet.Union(playerInventory, npcInventory);

        foreach (Item item in inventorytItems)
        {
            if (!unionSet.Contains(item))
                tradeList.Add(item);
        }

        DisplayTradeList();

    }

    private void DisplayTradeList()
    {
        foreach (Transform child in tradeContainer) Destroy(child.gameObject);

        foreach (Item item in tradeList)
        {
            CreateItemUI(item,tradeContainer);
        }
    }

}
