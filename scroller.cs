//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroller : MonoBehaviour {

    ScrollManager scrollManager;
    Material material;
    public float scrollSpeed = 1;
    public float currentScroll = 0;
    public float speedMultiplier = 1;

    private void Start()
    {
        scrollManager = GameObject.FindGameObjectWithTag("GM").GetComponent<ScrollManager>();
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        speedMultiplier = scrollManager.speedMultiplier;
        currentScroll += (scrollSpeed * 0.01f * speedMultiplier * Time.deltaTime) % 1;
        material.mainTextureOffset = new Vector2(currentScroll, 0);       
    }
}
