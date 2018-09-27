using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour {

    [SerializeField]
    private GameObject tower;

    [SerializeField]
    private Sprite sprite;

    public GameObject Tower
    {
        get
        {
            return tower;
        }

    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }

    }
}
