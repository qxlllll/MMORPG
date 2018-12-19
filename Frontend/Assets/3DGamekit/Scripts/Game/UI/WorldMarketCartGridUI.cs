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

public class WorldMarketCartGridUI : MonoBehaviour
{
    public GameObject WorldMarketCartItem;

    private Dictionary<string, GameObject> m_items = new Dictionary<string, GameObject>();
    public TextMeshProUGUI GValue;
    public TextMeshProUGUI SValue;
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
        //GValue.SetText("555", true);
        //SValue.SetText("1000", true);
        Sprite sprite;
        GameObject item;
        if (!GetAllIcons.icons.TryGetValue(name, out sprite))
        {
            return;
        }
        bool exists = m_items.TryGetValue(name, out item);
        if (!exists)
        {
            item = GameObject.Instantiate(WorldMarketCartItem);
            if (item == null)
            {
                return;
            }
            item.transform.SetParent(transform, false);
            item.SetActive(true);
            m_items.Add(name, item);
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
            handler.Init(name);
        }
    }

    public void RemoveFromCart(string name)
    {
        GameObject item;
        if (m_items.TryGetValue(name, out item))
        {
            m_items.Remove(name);
            Destroy(item);
        }
    }

    public void OnBuyButtonClicked()
    {
        int sum = 0;
        int count = 0;
        int gold_price = 0;
        GameObject item;
        CBuy buy = new CBuy();
        foreach (var kv in m_items)
        {
            item = kv.Value;
            WorldMarketCartItemUI handler = item.GetComponent<WorldMarketCartItemUI>();
            count=handler.count;
            gold_price = handler.price;
            sum += gold_price * count;
            buy.products.Add(handler.itemName,count);
        }
        
        buy.sum_gold_price = sum;
        buy.buy_by_gold = 1;
        Client.Instance.Send(buy);
    }
    public void OnBuyBySilverButtonClicked()
    {
        int sum = 0;
        int count = 0;
        int silver_price = 0;
        GameObject item;
        CBuy buy = new CBuy();
        foreach (var kv in m_items)
        {
            item = kv.Value;
            WorldMarketCartItemUI handler = item.GetComponent<WorldMarketCartItemUI>();
            count = handler.count;
            silver_price = handler.price;
            sum += silver_price * count;
            buy.products.Add(handler.itemName, count);
        }

        buy.sum_silver_price = sum;
        buy.buy_by_silver = 1;
        Client.Instance.Send(buy);
    }
}
