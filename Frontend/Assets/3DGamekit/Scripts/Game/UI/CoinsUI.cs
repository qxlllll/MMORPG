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

public class CoinsUI : MonoBehaviour
{

    public TextMeshProUGUI GValue;
    public TextMeshProUGUI SValue;
   
    private void OnEnable()
    {
        GValue.SetText(Convert.ToString(Gamekit3D.Attribute.gold_coins), true);
        SValue.SetText(Convert.ToString(Gamekit3D.Attribute.silver_coins), true);
    }

}
