 //NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManagerMainMenu : MonoBehaviour {

    Spine.Unity.Examples.BasicPlatformerController moveController;

    private void Awake()
    {
        moveController = GameObject.FindGameObjectWithTag("Player").GetComponent<Spine.Unity.Examples.BasicPlatformerController>();
    }

    public void onPressLeft()
    {
        moveController.mobileInput.x = -1;
    }

    public void onReleaseLeft()
    {
        moveController.mobileInput.x = 0;
    }

    public void onPressRight()
    {
        moveController.mobileInput.x = 1;
    }

    public void onReleaseRight()
    {
        moveController.mobileInput.x = 0;
    }

    public void onPressJump()
    {
        moveController.mobileJumpStart = true;
        moveController.mobileJumpStop = false;
    }

    public void onReleaseJump()
    {
        moveController.mobileJumpStart = false;
        moveController.mobileJumpStop = true;
    }

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
