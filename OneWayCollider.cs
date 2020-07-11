//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayCollider : MonoBehaviour {

    public Collider floorCollider;
    CharacterController charController;


    private void Start()
    {
        charController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {           
            Physics.IgnoreCollision(floorCollider, charController, true);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(floorCollider, charController, false);
        }
    }
}
