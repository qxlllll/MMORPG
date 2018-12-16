using Common;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

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
            
            

        }
    }
}
