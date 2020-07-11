//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIscroller : MonoBehaviour {

    Image _image;
    public float scrollSpeed = 1;
    float currentScroll = 0;
    public float speedMultiplier = 1;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        currentScroll += (scrollSpeed * Time.deltaTime);
        _image.materialForRendering.SetTextureOffset("_MainTex", new Vector2(currentScroll, 0f));

    }
}
