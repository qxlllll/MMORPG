using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using System;
public class WorldMarketShelfItemUI : MonoBehaviour
{
    public string itemName;
    public GameObject cartContent;

    public Button button;
    public Text textName;
    public Text textCost;
    public int price;
    CartGridUI handler;

    private void Awake()
    {
        if (cartContent != null)
        {
            handler = cartContent.GetComponent<CartGridUI>();
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(string name)
    {
        itemName = name;
        Sprite sprite; 
        if (button == null || textName == null || textCost == null)
        {
            return;
        }
        if (!GetAllIcons.icons.TryGetValue(name, out sprite))
        {
            return;
        }
        button.image.sprite = sprite;
        textName.text = name;
        Debug.Log(name);
        foreach (KeyValuePair<string, int> kvp in WorldMarket.world_market_item_price)
        {
            if (kvp.Key.Equals(name))
            {
                price = kvp.Value;
                Debug.Log(kvp.Value);
            }
        }
        textCost.text = "$" + Convert.ToString(price);
        //textCost.text = "4";
    }

    public void AddToCart()
    {
        if (handler != null)
            handler.AddToCart(itemName);
    }
}
