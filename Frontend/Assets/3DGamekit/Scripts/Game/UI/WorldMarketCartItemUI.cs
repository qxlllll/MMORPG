using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Gamekit3D;

public class WorldMarketCartItemUI : MonoBehaviour
{
    public Button button;
    public Text textCost;
    public InputField inputCount;
    public int count = 0;
    public string itemName;
    public string this_item_id;
    public int price;
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(string name)
    {
        Sprite sprite;
        this_item_id = name;
        itemName = WorldMarket.world_market_item_name[this_item_id];
        if (button == null || textCost == null || textCost == null)
        {
            return;
        }
        if (!GetAllIcons.icons.TryGetValue(itemName, out sprite))
        {
            return;
        }
        
        count++;
        button.image.sprite = sprite;
        inputCount.text = System.Convert.ToString(count);
        string item_id = "";
        foreach (KeyValuePair<string, string> kvp in WorldMarket.world_market_item_name)
        {
            if (kvp.Value.Equals(itemName))
            {
                item_id = kvp.Key;
            }
        }
        price = WorldMarket.world_market_item_price[item_id];

        textCost.text = "$" + Convert.ToString(price);
    }

    public void Increase()
    {
        //count++;
        inputCount.text = System.Convert.ToString(count);
        textCost.text = "$" + Convert.ToString(price);
    }

    public void Decrease()
    {
        count--;
        if (count == 0)
        {
            if (transform.parent == null)
            {
                return;
            }
            WorldMarketCartGridUI gridHandler = transform.parent.GetComponent<WorldMarketCartGridUI>();
            if (gridHandler != null)
            {
                Debug.Log("removing"+this_item_id);
                gridHandler.RemoveFromCart(this_item_id);
            }
        }
        else
        {
            inputCount.text = System.Convert.ToString(count);
            textCost.text = "$" + Convert.ToString(price);
        }
    }

}
