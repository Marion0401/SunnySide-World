using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly_AnimationInterface : MonoBehaviour
{
    Animator Anim;
    public int hitPoints = 3;
    float counter = 0;
    public float betterCounter = 0;
    [SerializeField] float deadTime = 2;
    int startHP;
    public bool refillLife = false;

    
    [Header("State Bools")]
    public bool Idle = false;
    public bool Walk = false;
    public bool Jump = false; 
    public bool Hurt = false;
    public bool Dying = false;
    public bool Dead = false;
    public bool Attack = false;
    

    
    [Header("Activation Bools")]
    public bool isWalking = false;
    public bool isJumping = false;
    public bool isAttacking = false;
    public bool gotHit = false;
    bool resurrect = false;
    bool isDead = false;
    

    void SetBool(string boolName, bool state)
    {
        Anim.SetBool(boolName, state);
    }

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        startHP = hitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        SetBool("isWalking", isWalking);
        SetBool("isJumping", isJumping);
        SetBool("gotHurt", gotHit);
        SetBool("isDead", isDead);
        SetBool("isAttacking", isAttacking);
        SetBool("Resurrect", resurrect);


        Idle = Anim.GetCurrentAnimatorStateInfo(0).IsTag("IDLE");
        Walk = Anim.GetCurrentAnimatorStateInfo(0).IsTag("WALK");
        Jump = Anim.GetCurrentAnimatorStateInfo(0).IsTag("JUMP");
        Hurt = Anim.GetCurrentAnimatorStateInfo(0).IsTag("HURT");
        Dying = Anim.GetCurrentAnimatorStateInfo(0).IsTag("DEATH");
        Dead = Anim.GetCurrentAnimatorStateInfo(0).IsTag("DEAD");
        Attack = Anim.GetCurrentAnimatorStateInfo(0).IsTag("ATTACK");


        if (refillLife)
        {
            refillLife = false;
            hitPoints = startHP;
        }


        if(resurrect)
        {
            hitPoints = startHP;
        }

        if (isDead)
        {
            counter += Time.deltaTime;
            if (counter >= 0.1f) { counter = 0; betterCounter += 0.1f; }

            if(betterCounter >= deadTime)
            {
                counter = 0;
                betterCounter = 0;
                isDead = false;
                resurrect = true;
            }
        }

        if (gotHit)
        {
            gotHit = false;
            if (hitPoints >= 0)
            {
                hitPoints--;
            }

            if(hitPoints == 0)
            {
                isDead = true;
            }
        }

        if (Jump) isJumping = false;

        if (Hurt) gotHit = false;

        if (Attack) isAttacking = false;

        if (Idle) resurrect = false;

    }
}
