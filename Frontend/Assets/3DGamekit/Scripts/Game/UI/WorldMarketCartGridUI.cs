using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Common;
using Gamekit3D.Network;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using TMPro;
using System;
using Attribute = Gamekit3D.Attribute;


public class WorldMarketCartGridUI : MonoBehaviour
{
    public GameObject WorldMarketCartItem;
    public GameObject cloned;
    private Dictionary<string, GameObject> wm_items = new Dictionary<string, GameObject>();
    public TextMeshProUGUI GValue;
    public TextMeshProUGUI SValue;
    public GameObject WorldMarketShelfItem;
    public string this_item_id;
    public string itemName;
    private Dictionary<string, int> buy_info = new Dictionary<string, int>();
    private void Awake()
    {
        WorldMarketCartItem.SetActive(false);

    }
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToCart(string name)
    {
        Sprite sprite;
        GameObject item;
        this_item_id = name;
        itemName = WorldMarket.world_market_item_name[this_item_id];
        if (!GetAllIcons.icons.TryGetValue(itemName, out sprite))
        {
            return;////////////要改name
        }
        bool exists = wm_items.TryGetValue(itemName, out item);
        if (!exists)
        {
            item = GameObject.Instantiate(WorldMarketCartItem);
            if (item == null)
            {
                return;
            }
            item.transform.SetParent(transform, false);
            item.SetActive(true);
            wm_items.Add(this_item_id, item);
        }
        WorldMarketCartItemUI handler = item.GetComponent<WorldMarketCartItemUI>();
        if (handler == null)
        {
            return;
        }

        if (exists)
        {
            handler.Increase();
        }
        else
        {
            handler.Init(this_item_id);
        }
    }

    public void RemoveFromCart(string name)
    {
        GameObject item;
        this_item_id = name;
        itemName = WorldMarket.world_market_item_name[this_item_id];
        if (wm_items.TryGetValue(this_item_id, out item))
        {
            Debug.Log(this_item_id);
            wm_items.Remove(this_item_id);
            Destroy(item);
        }
    }
    public void OnBuyBySilverButtonClicked()
    {
        int sum = 0;
        int count = 0;
        int silver_price = 0;
        GameObject item;
        CWorldMarketBuy worldmarketbuy = new CWorldMarketBuy();
        foreach (var kv in wm_items)
        {
            item = kv.Value;
            WorldMarketCartItemUI handler = item.GetComponent<WorldMarketCartItemUI>();
            count = handler.count;
            silver_price = handler.price;
            sum += silver_price * count;
            worldmarketbuy.buy_info.Add(handler.this_item_id, silver_price);
            //Debug.Log(handler.this_item_id + " " + silver_price);
            Destroy(item);
            WorldMarket.world_market_item_name.Remove(handler.this_item_id);
        }
        Client.Instance.Send(worldmarketbuy);
        wm_items.Clear();
    }
   
}
