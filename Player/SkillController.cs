//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour {

    UIManager uiManager;

    [Header("Skill")]
    public float maxEnergy = 5;
    public float currentEnergy = 5;
    public float energyRegen = 1;
    //float energyRegenTick = 1;
    //public float skillDelay = 0;
    public float skillEnergyCost = 1;
    public float skillEnergyDrain = 1;
    //float skillEnergyDrainTick = 1;
    public float skillMinTime = 1;
    public float skillTimer = 0;

    public bool trySkill = false;
    public bool skillActive = false;
    public float skillEndTransition = 0.5f;
    public float skillCooldown = 2;
    public float skillCooldownTimer = 0;
    public bool invincible = false;

    public bool mobileTrySkill = false;

    [Header("Animation")]
    public Spine.Unity.AnimationReferenceAsset skillAnimation;
    public Spine.Unity.AnimationReferenceAsset runAnimation;
    public Spine.Unity.AnimationReferenceAsset targetAnimation;


    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
    }

    private void Update()
    {
        HandleSkills();
        HandleEnergy();
    }

    private void FixedUpdate()
    {
    }

    void HandleSkills()
    {
        if (Input.GetButtonDown("Skill") | mobileTrySkill)
        {
            trySkill = true;
        }
        if (Input.GetButtonUp("Skill") | !mobileTrySkill)
        {
            trySkill = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            trySkill = false;
            skillActive = false;
            DelayedDeactivate();
        }

        if (trySkill && currentEnergy >= skillEnergyCost && skillCooldownTimer >= skillCooldown)
        {          
            skillActive = true; 
        }
        if (!trySkill & (skillTimer == 0 | skillTimer >= skillMinTime) || Input.GetButtonUp("Skill") & skillActive == true & skillTimer >= skillMinTime || currentEnergy <= 0)
        {
            skillActive = false;
        }
    }

    void HandleEnergy()
    {
        uiManager.UpdateEnergy(currentEnergy);
        uiManager.UpdateCooldown(skillCooldownTimer / skillCooldown);
        currentEnergy = Mathf.Clamp(currentEnergy,0f, maxEnergy);

        if (skillActive == true)
        {
            currentEnergy -= skillEnergyDrain * Time.deltaTime;

            // Timers
            skillTimer += 1 * Time.deltaTime;
            skillCooldownTimer = 0;

            targetAnimation = skillAnimation;
        }
        else
        {
            if (currentEnergy < maxEnergy)
            {
                currentEnergy += energyRegen * Time.deltaTime;
            }

            // Timers
            skillTimer = 0;
            skillCooldownTimer += 1 * Time.deltaTime;

            targetAnimation = runAnimation;
        }
    }

    public void RegenEnergy(float energy)
    {
        currentEnergy += energy;
    }

    public void Activate()
    {
        invincible = true;
    }

    public void DelayedDeactivate()
    {
        invincible = false;
    }

}
