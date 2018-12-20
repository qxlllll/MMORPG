using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Gamekit3D.Network;
using Common;

public class InventoryUI : MonoBehaviour
{

    public GameObject InventoryCell;
    public GameObject InventoryGridContent;

    // Use this for initialization

    private void Awake()
    {
        InventoryCell.SetActive(false);
    }
    public void OnApplyClicked()
    {
        CApply apply = new CApply();
        apply.to_apply = Attribute.apply;
        Client.Instance.Send(apply);
    }
    public void OnUnapplyClicked()
    {
        CUnapply unapply = new CUnapply();
        unapply.to_unapply = Attribute.apply;
        Client.Instance.Send(unapply);
    }
    public void OnSellClicked()
    {
        CSell sell = new CSell();
        sell.to_sell = Attribute.apply;
        sell.to_sell_price = FMarket.all_silver_price[sell.to_sell] / 2;
        Client.Instance.Send(sell);
    }
    public void OnRefreshClicked()
    {
        GameObject.FindObjectOfType<RoleUI>().InteligenceValue.SetText(Attribute.InteligenceValue, true);
        GameObject.FindObjectOfType<RoleUI>().SpeedValue.SetText(Attribute.SpeedValue, true);
        GameObject.FindObjectOfType<RoleUI>().LevelValue.SetText(Attribute.LevelValue, true);
        GameObject.FindObjectOfType<RoleUI>().AttackValue.SetText(Attribute.AttackValue, true);
        GameObject.FindObjectOfType<RoleUI>().DefenseValue.SetText(Attribute.DefenseValue, true);
        CGetInventory getInventory = new CGetInventory();
        Client.Instance.Send(getInventory);
        OnDisable();
        OnEnable();
    }
    private void OnEnable()
    {
        PlayerMyController.Instance.EnabledWindowCount++;
        int capacity = PlayerMyController.Instance.InventoryCapacity;
        int count = PlayerMyController.Instance.Inventory.Count;
        
        //OnRefresh();
        foreach (var kv in Inventory.player_Inventory)
        {


            // TODO ... specify icon by item types
            int i = 0;
            int num = kv.Value;
            for (i=0; i<num; i++)
            {
                GameObject cloned = GameObject.Instantiate(InventoryCell);
                Button button = cloned.GetComponent<Button>();
                Sprite icon = GetAllIcons.icons[kv.Key];
                button.onClick.AddListener(delegate ()
                {
                    Attribute.apply = kv.Key;
                    GameObject.Find("ItemImage").GetComponent<Image>().sprite = icon;
                });
                button.image.sprite = icon;
                cloned.SetActive(true);
                cloned.transform.SetParent(InventoryGridContent.transform, false);
            }
            
            
        }
        /*foreach (var kv in PlayerMyController.Instance.Inventory)
        {
            GameObject cloned = GameObject.Instantiate(InventoryCell);
            Button button = cloned.GetComponent<Button>();
            // TODO ... specify icon by item types
            Sprite icon = GetAllIcons.icons["Sword_2"];
            button.image.sprite = icon;
            button.onClick.AddListener(delegate ()
            {
                GameObject.Find("ItemImage").GetComponent<Image>().sprite = icon;
            });
            
            cloned.SetActive(true);
            cloned.transform.SetParent(InventoryGridContent.transform, false);
        }*/

        for (int i = 0; i < capacity - count; i++)
        {
            GameObject cloned = GameObject.Instantiate(InventoryCell);
            cloned.SetActive(true);
            cloned.transform.SetParent(InventoryGridContent.transform, false);
        }
    }
    
    private void OnDisable()
    {
        int cellCount = InventoryGridContent.transform.childCount;
        foreach (Transform transform in InventoryGridContent.transform)
        {
            Destroy(transform.gameObject);
        }
        PlayerMyController.Instance.EnabledWindowCount--;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ExtendBagCapacity(int n)
    {
        int cellCount = InventoryGridContent.transform.childCount;
        for (int i = 0; i < n - cellCount; i++)
        {
            GameObject cloned = GameObject.Instantiate(InventoryCell);
            cloned.SetActive(true);
            cloned.transform.SetParent(InventoryGridContent.transform, false);
        }
    }
}
