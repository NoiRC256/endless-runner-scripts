//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObstacleController : MonoBehaviour {

    ScrollManager scrollManager;
    DamageManager damageManager;
    //MoveController player;
    private Rigidbody rb;
    Vector2 moveDirection = -Vector3.right;
    float speed = 820f;
    public int damage = 1;

    private void Start()
    {
        scrollManager = GameObject.FindGameObjectWithTag("GM").GetComponent<ScrollManager>();
        damageManager = GameObject.FindGameObjectWithTag("GM").GetComponent<DamageManager>();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveController>();
        rb = GetComponent<Rigidbody>();
        //target = GameObject.FindGameObjectWithTag("ObstacleTarget").GetComponent<Transform>();
        
    }

    private void FixedUpdate()
    {
        //transform.Translate(-Vector3.right * speed * scrollManager.speedMultiplier * Time.deltaTime);
        //rb.AddForce(new Vector2(moveDirection * speed * scrollManager.speedMultiplier * Time.deltaTime, 0), ForceMode.Force);
        //rb.MovePosition(rb.transform.position + new Vector3(moveDirection,0f,0f) * speed * scrollManager.speedMultiplier * Time.deltaTime);
        //Vector3 moveDirection = target.position - transform.position;

        rb.velocity = (moveDirection * speed * scrollManager.speedMultiplier * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            damageManager.SendDamage(damage);
            //player.hitAudioSource.Play();
        }
    }
}
