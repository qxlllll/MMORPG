using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Gamekit3D;

public class CartItemUI : MonoBehaviour
{
    public Button button;
    public Text textCost;
    public InputField inputCount;
    public int count = 0;
    public string itemName;
    public short gold_price;
    public short silver_price;
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
        foreach (KeyValuePair<string, short> kvp in FMarket.all_gold_price)
        {
            if (kvp.Key.Equals(name))
            {
                gold_price = kvp.Value;
            }
        }
        foreach (KeyValuePair<string, short> kvp in FMarket.all_silver_price)
        {
            if (kvp.Key.Equals(name))
            {
                silver_price = kvp.Value;
            }
        }

        textCost.text = "$"+Convert.ToString(gold_price)+"or"+ Convert.ToString(silver_price);
    }

    public void Increase()
    {
        count++;
        inputCount.text = System.Convert.ToString(count);
        textCost.text = "$" + Convert.ToString(gold_price) + "or" + Convert.ToString(silver_price);
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
            CartGridUI gridHandler = transform.parent.GetComponent<CartGridUI>();
            if (gridHandler != null)
            {
                gridHandler.RemoveFromCart(itemName);
            }
        }
        else
        {
            inputCount.text = System.Convert.ToString(count);
            textCost.text = "$" + Convert.ToString(gold_price)+"or" + Convert.ToString(silver_price);
        }
    }

}
