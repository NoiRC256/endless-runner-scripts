//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour {

    public MainMenuManager mainMenu;
    public string sceneName;

    private void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("GM").GetComponent<MainMenuManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mainMenu.LoadLevel(sceneName);
        }
    }
}
