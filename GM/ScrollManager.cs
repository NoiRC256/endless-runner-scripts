//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour {

    public float speedMultiplier = 0.5f;
    public float speedIncrease = 0.1f;

    public void IncreaseSpeed()
    {
        speedMultiplier += speedIncrease;
    }

    public void StopScroll()
    {
        speedMultiplier = Mathf.Lerp(speedMultiplier, 0f, 2 * Time.deltaTime);      
    }
}
