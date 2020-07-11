//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ItemManager : MonoBehaviour {

    ScrollManager scrollManager;
    ScoreManager scoreManager;
    MoveController player;
    SkillController characterSkill;
    private Rigidbody rb;

    //public GameObject particleEffect;
    Vector2 moveDirection = -Vector3.right;
    float speed = 820f;
    public int itemScore = 1;
    public float itemEnergy = 0.05f;

    private void Start()
    {
        scrollManager = GameObject.FindGameObjectWithTag("GM").GetComponent<ScrollManager>();
        scoreManager = GameObject.FindGameObjectWithTag("GM").GetComponent<ScoreManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveController>();
        characterSkill = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillController>();
        rb = GetComponent<Rigidbody>();
        //target = GameObject.FindGameObjectWithTag("ObstacleTarget").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        //transform.Translate(-Vector3.right * speed * scrollManager.speedMultiplier * Time.deltaTime);
        //rb.AddForce(new Vector2(-1 * speed * scrollManager.speedMultiplier * Time.deltaTime, 0), ForceMode.Force);
        //rb.MovePosition(rb.transform.position + new Vector3(moveDirection,0f,0f) * speed * scrollManager.speedMultiplier * Time.deltaTime);
        //Vector3 moveDirection = target.position - transform.position;

        rb.velocity = (moveDirection * speed * scrollManager.speedMultiplier * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !characterSkill.invincible)
        {
            scoreManager.AddScore(itemScore);         
            characterSkill.RegenEnergy(itemEnergy);         

            //Instantiate(particleEffect, transform.position, transform.rotation);
            //Destroy(pe, 0.5f);
            player.getPuddingAudioSource.Play();
            Destroy(gameObject);
        }
        
    }
}
