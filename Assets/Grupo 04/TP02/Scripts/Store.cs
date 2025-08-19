using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Store : MonoBehaviour
{
    public Transform gridLayout;
    public Transform hotbarLayout;
    public GameObject itemPrefab;         // prefab con botón + TMP_Text + Image
    public TMP_Text moneyText;            // texto que muestra el dinero del jugador
    public int playerMoney = 500;         // dinero inicial

    // Diccionario de ítems en la tienda
    private Dictionary<int, Item> storeItems = new Dictionary<int, Item>();

    // Diccionario de inventario del jugador
    private Dictionary<int, PlayerItem> playerInventory = new Dictionary<int, PlayerItem>();

    private void Start()
    {
        Item sword = new Item { ID = 1, name = "Sword", price = 200, quality = "Common", type = "Weapon", icon = Resources.Load<Sprite>("Sprites/Sword") };
        Item potion = new Item { ID = 2, name = "Potion", price = 50, quality = "Common", type = "Consumable", icon = Resources.Load<Sprite>("Sprites/Potion") };
        Item bow = new Item { ID = 3, name = "Bow", price = 100, quality = "Common", type = "Range Weapon", icon = Resources.Load<Sprite>("Sprites/Bow") };


        storeItems.Add(sword.ID, sword);
        storeItems.Add(potion.ID, potion);
        storeItems.Add(bow.ID, bow);


        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Transform child in gridLayout)
        {
            Destroy(child.gameObject);
        }

        moneyText.text = "Money: " + playerMoney;

        foreach (KeyValuePair<int, Item> kvp in storeItems)
        {
            GameObject newItem = Instantiate(itemPrefab, gridLayout);

            TMP_Text[] texts = newItem.GetComponentsInChildren<TMP_Text>();
            Image icon = newItem.GetComponentInChildren<Image>();

            texts[0].text = kvp.Value.name;
            texts[1].text = kvp.Value.price.ToString() + " coins";
            if (icon != null && kvp.Value.icon != null)
            {
                icon.sprite = kvp.Value.icon;
            }
            Button btn = newItem.GetComponent<Button>();
            int id = kvp.Key;
            btn.onClick.AddListener(() => BuyItem(id));
        }
    }

    private void BuyItem(int itemId)
    {
        if (storeItems.ContainsKey(itemId))
        {
            Item item = storeItems[itemId];

            if (playerMoney >= item.price)
            {
                playerMoney -= item.price;

                if (playerInventory.ContainsKey(itemId))
                {
                    playerInventory[itemId].cantidad++;
                }
                else
                {
                    playerInventory.Add(itemId, new PlayerItem(item));
                }
                UpdateUI();
                UpdateHotbar();
            }
            else
            {
                Debug.Log("no money");
            }
        }
    }

    private void UpdateHotbar()
    {
        foreach (Transform child in hotbarLayout)
        {
            Destroy(child.gameObject);
        }

        foreach (KeyValuePair<int, PlayerItem> kvp in playerInventory)
        {
            GameObject newItem = Instantiate(itemPrefab, hotbarLayout);

            TMP_Text[] texts = newItem.GetComponentsInChildren<TMP_Text>();
            Image icon = newItem.GetComponentInChildren<Image>();

            texts[0].text = kvp.Value.item.name;
            texts[1].text = kvp.Value.cantidad.ToString();

            if (icon != null && kvp.Value.item.icon != null)
            {
                icon.sprite = kvp.Value.item.icon;
            }

            Button btn = newItem.GetComponent<Button>();
            int id = kvp.Key;
            btn.onClick.AddListener(() => SellItem(id));
        }
    }

    private void SellItem(int itemId)
    {
        if (playerInventory.ContainsKey(itemId)) 
        {
            playerInventory[itemId].cantidad--;
            playerMoney += playerInventory[itemId].item.price;
        }
        if (playerInventory[itemId].cantidad < 1) 
        {
            playerInventory.Remove(itemId);
        }

        UpdateUI();
        UpdateHotbar();
    }

}
