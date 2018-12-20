using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gamekit3D;
using UnityEngine.UI;
using Gamekit3D.Network;
using Common;

public class RoleUI : MonoBehaviour
{

    public TextMeshProUGUI HPValue;
    public TextMeshProUGUI InteligenceValue;
    public TextMeshProUGUI SpeedValue;
    public TextMeshProUGUI LevelValue;
    public TextMeshProUGUI AttackValue;
    public TextMeshProUGUI DefenseValue;

    private Damageable m_damageable;
    private PlayerController m_controller;

    private void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {

    }

    private void OnEnable()
    {
        PlayerMyController.Instance.EnabledWindowCount++;
        CGetInventory getInventory = new CGetInventory();
        Client.Instance.Send(getInventory);
        if (m_controller == null || m_damageable == null)
        {
            m_controller = PlayerController.Mine;
            m_damageable = PlayerController.Mine.GetComponent<Damageable>();
        }
        string hp = string.Format("{0}/{1}", m_damageable.currentHitPoints, m_damageable.maxHitPoints);
        HPValue.SetText(hp, true);
        InteligenceValue.SetText(Attribute.InteligenceValue, true);
        SpeedValue.SetText(Attribute.SpeedValue, true);
        LevelValue.SetText(Attribute.LevelValue, true);
        AttackValue.SetText(Attribute.AttackValue, true);
        DefenseValue.SetText(Attribute.DefenseValue, true);
        if (Attribute.defense_item !="0")
        {
            Sprite defense_icon = GetAllIcons.icons[Attribute.defense_item];
            GameObject.Find("DefenseImage").GetComponent<Image>().sprite = defense_icon;
        }
        if (Attribute.inteligence_item!="0")
        {
            Sprite in_icon = GetAllIcons.icons[Attribute.inteligence_item];
            GameObject.Find("InteligenceImage").GetComponent<Image>().sprite = in_icon;
        }
        if (Attribute.attack_item != "0")
        {
            Sprite attack_icon = GetAllIcons.icons[Attribute.attack_item];
            GameObject.Find("AttackImage").GetComponent<Image>().sprite = attack_icon;
        }
        if (Attribute.speed_item != "0")
        {
            Sprite speed_icon = GetAllIcons.icons[Attribute.speed_item];
            GameObject.Find("SpeedImage").GetComponent<Image>().sprite = speed_icon;
        }
    }

    private void OnDisable()
    {
        PlayerMyController.Instance.EnabledWindowCount--;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Test()
    {
        HPValue.text = "100";
        InteligenceValue.text = "100";
    }

    public void InitUI(PlayerController controller)
    {
        m_damageable = controller.GetComponent<Damageable>();
        m_controller = controller;
    }
}
