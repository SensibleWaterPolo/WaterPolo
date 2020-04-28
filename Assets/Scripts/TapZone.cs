﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapZone : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.keep)
            GetComponent<CircleCollider2D>().enabled = true;
        else
            GetComponent<CircleCollider2D>().enabled = false;
    }
}