using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using System;

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
            string key = kv.Value;
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
            handler.Init(key);
        }
      /*  foreach (KeyValuePair<string, Sprite> kv in WorldMarketGetAllIcons.my_icons)
        {
            string key = kv.Key;
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
            handler.Init(key);
        }
        WorldMarketShelfItem.SetActive(false);*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
