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
        if (button == null || textCost == null || textCost == null)
        {
            return;
        }
        if (!GetAllIcons.icons.TryGetValue(name, out sprite))
        {
            return;
        }
        itemName = name;
        count++;
        button.image.sprite = sprite;
        inputCount.text = System.Convert.ToString(count);
        foreach (KeyValuePair<string, int> kvp in WorldMarket.world_market_item_price)
        {
            if (kvp.Key.Equals(name))
            {
                price = kvp.Value;
            }
        }

        textCost.text = "$" + Convert.ToString(price);
    }

    public void Increase()
    {
        count++;
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
                gridHandler.RemoveFromCart(itemName);
            }
        }
        else
        {
            inputCount.text = System.Convert.ToString(count);
            textCost.text = "$" + Convert.ToString(price);
        }
    }

}
