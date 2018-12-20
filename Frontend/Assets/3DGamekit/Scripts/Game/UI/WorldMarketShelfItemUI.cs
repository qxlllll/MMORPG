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
    public string seller;
    public string this_item_id;
    WorldMarketCartGridUI handler;
    

    private void Awake()
    {
        if (cartContent != null)
        {
            handler = cartContent.GetComponent<WorldMarketCartGridUI>();
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
        this_item_id = name;
        itemName = WorldMarket.world_market_item_name[this_item_id];
        seller = WorldMarket.world_market_item_seller[this_item_id];
        price = WorldMarket.world_market_item_price[this_item_id];
        Sprite sprite;
        if (button == null || textName == null || textCost == null)
        {
            return;
        }
        if (!GetAllIcons.icons.TryGetValue(itemName, out sprite))
        {
            return;
        }
        button.image.sprite = sprite;
        textName.text = itemName;
        textCost.text = Convert.ToString(seller);
    }

    public void AddToCart()
    {
        if (handler != null)
            handler.AddToCart(this_item_id);
    }
}
