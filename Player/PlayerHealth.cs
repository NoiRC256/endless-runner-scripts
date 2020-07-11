//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    MoveController player;
    SkillController characterSkill;
    public int maxHealth = 1;
    public int currentHealth = 1;
    public bool lose = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveController>();
        characterSkill = GetComponent<SkillController>();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            lose = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!characterSkill.invincible)
        {
            player.hitAudioSource.Play();
            currentHealth -= damage;
        }
    }

}
