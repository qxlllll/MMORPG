using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Gamekit3D.Network;
using Common;

public class SellingInventoryUI : MonoBehaviour
{

    public GameObject InventoryCell;
    public GameObject InventoryGridContent;

    // Use this for initialization

    private void Awake()
    {
        InventoryCell.SetActive(false);
    }

    public void OnRetrieveClicked()
    {
        CRetrieve re = new CRetrieve();
        re.to_retrieve = Attribute.apply;
        Client.Instance.Send(re);
    }
    public void OnRefreshClicked()
    {
        CGetSellingInventory getSInventory = new CGetSellingInventory();
        Client.Instance.Send(getSInventory);
        OnDisable();
        OnEnable();
    }
    private void OnEnable()
    {
        PlayerMyController.Instance.EnabledWindowCount++;
        int capacity = PlayerMyController.Instance.InventoryCapacity;
        int count = PlayerMyController.Instance.Inventory.Count;
        
        //OnRefresh();
        foreach (var kv in Inventory.player_selling_Inventory)
        {
            string item_id = kv.Key;
            string item_name = kv.Value;
            Debug.Log(item_id + " " + item_name);
            GameObject cloned = GameObject.Instantiate(InventoryCell);
            Button button = cloned.GetComponent<Button>();
            Sprite icon = GetAllIcons.icons[kv.Value];
            button.onClick.AddListener(delegate ()
            {
                Attribute.retrieve = kv.Key;
                GameObject.Find("ItemImage").GetComponent<Image>().sprite = icon;
            });
            button.image.sprite = icon;
            cloned.SetActive(true);
            cloned.transform.SetParent(InventoryGridContent.transform, false);
        }

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
