using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using System;
using Attribute = Gamekit3D.Attribute;
using Gamekit3D.Network;
using Common;


public class WorldMarketShelfGridUI : MonoBehaviour
{
    public GameObject WorldMarketShelfItem;

    private void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        foreach (KeyValuePair<string,string>kv in WorldMarket.world_market_item_name)
        {
            string key = kv.Key;
            string seller = WorldMarket.world_market_item_seller[key];
            if (seller != Attribute.name)
            {
                GameObject cloned = GameObject.Instantiate(WorldMarketShelfItem);
                if (cloned == null)
                {
                    continue;
                }
                cloned.SetActive(true);
                cloned.transform.SetParent(this.transform, false);
                WorldMarketShelfItemUI handler = cloned.GetComponent<WorldMarketShelfItemUI>();
                if (handler == null)
                {
                    continue;
                }

               // Debug.Log(key + " seller  " + seller + " name " + Attribute.name);
                handler.Init(key);
            }
            
        }
        WorldMarketShelfItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRefreshClicked()
    {
        foreach (Transform transform in this.transform)
        {
            Destroy(transform.gameObject);
        }
        
        CGetWorldMarket get = new CGetWorldMarket();
        Client.Instance.Send(get);
        
        foreach (KeyValuePair<string, string> kv in WorldMarket.world_market_item_name)
        {
            string key = kv.Key;
            string seller = WorldMarket.world_market_item_seller[key];
            if (seller != Attribute.name)
            {
                GameObject cloned = GameObject.Instantiate(WorldMarketShelfItem);
                if (cloned == null)
                {
                    continue;
                }
                cloned.SetActive(true);
                cloned.transform.SetParent(this.transform, false);
                WorldMarketShelfItemUI handler = cloned.GetComponent<WorldMarketShelfItemUI>();
                if (handler == null)
                {
                    continue;
                }

                //Debug.Log(key + " seller  " + seller + " name " + Attribute.name);
                handler.Init(key);
            }
        }
        WorldMarketShelfItem.SetActive(false);
    }
}
