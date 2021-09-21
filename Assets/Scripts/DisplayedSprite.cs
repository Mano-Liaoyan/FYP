using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayedSprite : MonoBehaviour
{
    private SpriteRenderer _sp;
    public Sprite[]  pics;
    
    [Range(0,5)]
    public int displaySprite;

    // Start is called before the first frame update
    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpriteByAnotherSprite();
    }

    private void ChangeSpriteByAnotherSprite(){
        _sp.sprite = pics[displaySprite];
    }
}
