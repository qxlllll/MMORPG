using Common;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;using System.Collections.Generic;
using TMPro;
using Gamekit3D;
using UnityEngine.UI;
using Gamekit3D.Network;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvPlayerAttribute(IChannel channel, Message message)
        {
            //MyNetwork network = GameObject.FindObjectOfType<MyNetwork>();
            //GameStart startup = GameObject.FindObjectOfType<GameStart>();
        
            SPlayerAttribute msg = message as SPlayerAttribute;
            
            Attribute.name = msg.name;
            Attribute.InteligenceValue = msg.InteligenceValue;
            Attribute.SpeedValue = msg.SpeedValue;
            Attribute.LevelValue = msg.LevelValue;
            Attribute.AttackValue = msg.AttackValue;
            Attribute.DefenseValue = msg.DefenseValue;
            Attribute.gold_coins = msg.gold_coins;
            Attribute.silver_coins = msg.silver_coins;
            Attribute.inteligence_item = msg.inteligence_item;
            Attribute.speed_item = msg.speed_item;
            Attribute.defense_item = msg.defense_item;
            //Debug.Log("defense_item");
            //Debug.Log(Attribute.defense_item);
            Attribute.attack_item = msg.attack_item;

            Sprite defense_icon = GetAllIcons.icons[Attribute.defense_item];
            GameObject.Find("DefenseImage").GetComponent<Image>().sprite = defense_icon;            
            Sprite attack_icon = GetAllIcons.icons[Attribute.attack_item];
            GameObject.Find("AttackImage").GetComponent<Image>().sprite = attack_icon;
            Sprite in_icon = GetAllIcons.icons[Attribute.inteligence_item];
            GameObject.Find("InteligenceImage").GetComponent<Image>().sprite = in_icon;
            Sprite speed_icon = GetAllIcons.icons[Attribute.speed_item];
            GameObject.Find("SpeedImage").GetComponent<Image>().sprite = speed_icon;
            GameObject.FindObjectOfType<CoinsUI>().GValue.SetText(Convert.ToString(Attribute.gold_coins), true);
            GameObject.FindObjectOfType<CoinsUI>().SValue.SetText(Convert.ToString(Attribute.silver_coins), true);
        }
    }
}
