using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMarketGetAllIcons : MonoBehaviour
{

    public static Dictionary<string, Sprite> my_icons = new Dictionary<string, Sprite>();

    // Use this for initialization
    void Awake()
    {
        Component[] components = GetComponentsInChildren(typeof(SpriteRenderer), true);
        foreach (Component component in components)
        {
            SpriteRenderer sr = (SpriteRenderer)component;
            my_icons.Add(sr.gameObject.name, sr.sprite);
        }

        //my_icons.Clear();
        
        gameObject.SetActive(false);
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
