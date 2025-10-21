using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

    private string currentOrder;

    private void Start()
    {
        Item[] items = Resources.LoadAll<Item>("ItemsFolder");

        foreach (Item item in items)
        {
            storeItems.Add(item.ID, item);
        }

        UpdateUI();
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        moneyText.text = "Money: " + playerMoney;
    }

    private void UpdateUI(string orderBy = "ID")
    {
        foreach (Transform child in gridLayout)
        {
            Destroy(child.gameObject);
        }


        IEnumerable<KeyValuePair<int, Item>> items = storeItems;

        switch (orderBy)
        {
            case "Price":
                items = storeItems.OrderBy(kvp => kvp.Value.price);
                break;
            case "Quality":
                items = storeItems.OrderBy(kvp => kvp.Value.quality);
                break;
            case "Name":
                items = storeItems.OrderBy(kvp => kvp.Value.name);
                break;
            case "Type":
                items = storeItems.OrderBy(kvp => kvp.Value.type);
                break;
            default:
                items = storeItems.OrderBy(kvp => kvp.Value.ID);
                break;
        }


        foreach (KeyValuePair<int, Item> kvp in items)
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

        currentOrder = orderBy;
    }

    private void BuyItem(int itemId)
    {
        if (storeItems.ContainsKey(itemId))
        {
            Item item = storeItems[itemId];

            if (playerMoney >= item.price)
            {
                playerMoney -= item.price;

                UpdatePrice(item.type);

                if (playerInventory.ContainsKey(itemId))
                {
                    playerInventory[itemId].quantity++;
                }
                else
                {
                    playerInventory.Add(itemId, new PlayerItem(item));
                }
                UpdateMoney();
                UpdateHotbar();
            }
            else
            {
                Debug.Log("no money");
            }

            PlayerPrefs.SetInt($" {playerInventory[itemId]}Quantity ", playerInventory[itemId].quantity);
        }
    }


    private void UpdatePrice(ItemsType itemsType)
    {
        foreach (KeyValuePair<int, Item> i in storeItems)
        {
            if (i.Value.type != itemsType)
            {
                i.Value.ChangePrice(-5);
            }
            else if (i.Value.type == itemsType)
            {
                i.Value.ChangePrice(5);
            }

        }
        UpdateUI(currentOrder);
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
            texts[1].text = PlayerPrefs.GetInt($" {kvp.Value.item.ID}Quantity ", 1).ToString();

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
            playerInventory[itemId].quantity--;
            playerMoney += playerInventory[itemId].item.price;
        }
        if (playerInventory[itemId].quantity < 1)
        {
            playerInventory.Remove(itemId);
        }

        UpdateMoney();
        UpdateHotbar();
    }



    public void OrderByPrice()
    {
        UpdateUI("Price");
    }

    public void OrderByQuality()
    {
        UpdateUI("Quality");
    }

    public void OrderByName()
    {
        UpdateUI("Name");
    }

    public void OrderByType()
    {
        UpdateUI("Type");
    }

}