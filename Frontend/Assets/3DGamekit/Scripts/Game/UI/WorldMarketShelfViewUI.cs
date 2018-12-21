﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit3D;
using Gamekit3D.Network;
using Common;

public class WorldMarketShelfViewUI : MonoBehaviour
{
    private void Awake()
    {
    }

    private void OnEnable()
    {
        PlayerMyController.Instance.EnabledWindowCount++;
    }

    private void OnDisable()
    {
        PlayerMyController.Instance.EnabledWindowCount--;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
}