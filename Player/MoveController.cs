//NoiR_CCC

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(CharacterController))]
public class MoveController : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public SkillController characterSkill;
    public Transform visuals;

    [Header("Controls")]
    public string XAxis = "Horizontal";
    public string YAxis = "Vertical";
    public string JumpButton = "Jump";
    

    [Header("Moving")]
    //public float walkSpeed = 1.5f;
    public float runSpeed = 7f;
    public float gravityScale = 6.6f;
    public float fallBack = -8f;

    [Header("Jumping")]
    public float jumpSpeed = 25;
    public float minimumJumpDuration = 0.5f;
    public float jumpInterruptFactor = 0.5f;
    //public float forceCrouchVelocity = 25;
    //public float forceCrouchDuration = 0.5f;

    public bool mobileJumpStart = false;
    public bool mobileJumpStop = false;

    [Header("Skill")]
    public float skillDelay = 0;

    [Header("Visuals")]
    public SkeletonAnimation skeletonAnimation;

    [Header("Animation")]
    public Spine.Unity.Examples.TransitionDictionaryExample transitions;
    //public AnimationReferenceAsset walk;
    public AnimationReferenceAsset run;
    //public AnimationReferenceAsset idle;
    public AnimationReferenceAsset jump;
    public AnimationReferenceAsset fall;
    //public AnimationReferenceAsset crouch;
    public AnimationReferenceAsset runFromFall;
    public AnimationReferenceAsset lose;

    [Header("Effects")]
    public AudioSource jumpAudioSource;
    public AudioSource hardfallAudioSource;
    public ParticleSystem landParticles;
    public AudioSource getPuddingAudioSource;
    public AudioSource hitAudioSource;
    public Spine.Unity.Examples.HandleEventWithAudioExample footstepHandler;

    CharacterController controller;
    Vector2 input = default(Vector2);
    public Vector2 mobileInput = default(Vector2);
    Vector3 velocity = default(Vector3);
    float minimumJumpEndTime = 0;
    float forceCrouchEndTime = 0;
    bool wasGrounded = false;
    float timer;

    AnimationReferenceAsset targetAnimation;
    AnimationReferenceAsset previousTargetAnimation;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealth>();
        characterSkill = GetComponent<SkillController>();
        //skeletonAnimation.Skeleton.SetAttachment("shadow", null);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        bool isGrounded = controller.isGrounded;
        bool landed = !wasGrounded && isGrounded;

        // Dummy input.
        input.x = Input.GetAxis(XAxis) + mobileInput.x;
        input.y = Input.GetAxis(YAxis) + mobileInput.y;
        bool inputJumpStop = Input.GetButtonUp(JumpButton);
        bool inputJumpStart = Input.GetButtonDown(JumpButton);
        bool doCrouch = (isGrounded && input.y < -0.5f) || (forceCrouchEndTime > Time.time);
        bool doJumpInterrupt = false;
        bool doJump = false;
        bool hardLand = false;

        /*
         if (landed)
         {
             if (-velocity.y > forceCrouchVelocity)
             {
                 hardLand = true;
                 doCrouch = true;
                 forceCrouchEndTime = Time.time + forceCrouchDuration;
             }
         }
         */

        if (!doCrouch)
        {
            if (isGrounded)
            {
                if (inputJumpStart | mobileJumpStart)
                {
                    doJump = true;
                    mobileJumpStop = false;
                }
            }
            else
            {
                doJumpInterrupt = (inputJumpStop && Time.time < minimumJumpEndTime) || (mobileJumpStop && Time.time < minimumJumpEndTime);
            }
        }

        // Dummy physics and controller using UnityEngine.CharacterController.
        Vector3 gravityDeltaVelocity = Physics.gravity * gravityScale * dt;


        if (doJump)
        {
            velocity.y = jumpSpeed;
            minimumJumpEndTime = Time.time + minimumJumpDuration;

        }
        else if (doJumpInterrupt)
        {
            if (velocity.y > 0)
                velocity.y *= jumpInterruptFactor;
        }

        velocity.x = 0;
        if (!doCrouch)
        {
            if (input.x != 0)
            {
                velocity.x = Mathf.Abs(input.x);
                velocity.x *= Mathf.Sign(input.x) * runSpeed;
            }
        }


        if (!isGrounded)
        {
            if (wasGrounded)
            {
                if (velocity.y < 0)
                    velocity.y = 0;
            }
            else
            {
                velocity += gravityDeltaVelocity;
            }
        }
        controller.Move(velocity * dt);

        // Animation
        if (playerHealth.currentHealth <= 0)
        {
            targetAnimation = lose;
            runSpeed = 0;

            visuals.localPosition = Vector2.Lerp(visuals.localPosition, new Vector2(fallBack, 0f), 2 * Time.deltaTime);
        }
        else
        {
            if (isGrounded)
            {
                if (characterSkill.skillActive)
                {
                    targetAnimation = characterSkill.targetAnimation;
                    characterSkill.Activate();
                }
                else
                {
                    targetAnimation = run;
                }
            }
            else
            {
                if (characterSkill.skillActive)
                {
                    targetAnimation = characterSkill.targetAnimation;
                    characterSkill.Activate();
                }
                else
                {
                    targetAnimation = velocity.y > 0 ? jump : fall;
                }
            }
        }   

        // Animation transition
        if (previousTargetAnimation != targetAnimation)
        {
            Spine.Animation transition = null;
            if (transitions != null && previousTargetAnimation != null)
            {
                transition = transitions.GetTransition(previousTargetAnimation, targetAnimation);
            }

            if (transition != null)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, transition, false).MixDuration = 0.10f;
                skeletonAnimation.AnimationState.AddAnimation(0, targetAnimation, true, 0f);

                if (!characterSkill.skillActive)
                {
                    characterSkill.Invoke("DelayedDeactivate", characterSkill.skillEndTransition);
                }
            }
            else if (playerHealth.currentHealth <= 0)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, targetAnimation, false);
            }
            else
            {
                skeletonAnimation.AnimationState.SetAnimation(0, targetAnimation, true);
            }
        }
        previousTargetAnimation = targetAnimation;

        // Effects
        if (doJump)
        {
            jumpAudioSource.Stop();
            jumpAudioSource.Play();
        }

        if (landed)
        {
            if (hardLand)
            {
                hardfallAudioSource.Play();
            }
            else
            {
                footstepHandler.Play();
            }

            landParticles.Emit((int)(velocity.y / -9f) + 2);
        }

        wasGrounded = isGrounded;
    }
}