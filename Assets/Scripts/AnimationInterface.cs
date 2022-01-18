using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInterface : MonoBehaviour
{
    [SerializeField] Animator Body;
    [SerializeField] Animator Hair;
    [SerializeField] Animator Tool;

    [Space(20)] [Header("State Bools")]
    public bool Idle = false;
    public bool Walk = false;
    public bool Chop = false;
    public bool Dig = false;
    public bool Attack = false;
    public bool Hurt = false;

    [Space(20)]
    [Header("Activation Bools")]
    public bool isMoving = false;
    public bool isChopping = false;
    public bool isDigging = false;
    public bool isAttacking = false;
    public bool gotHit = false;

    void SetBool(string boolName, bool state)
    {
        Body.SetBool(boolName, state);
        Hair.SetBool(boolName, state);
        Tool.SetBool(boolName, state);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetBool("isMoving", isMoving);
        SetBool("isChopping", isChopping);
        SetBool("isDigging", isDigging);
        SetBool("isAttacking", isAttacking);
        SetBool("gotHit", gotHit);

        Idle = Body.GetCurrentAnimatorStateInfo(0).IsTag("IDLE");
        Chop = Body.GetCurrentAnimatorStateInfo(0).IsTag("CHOP");
        Dig = Body.GetCurrentAnimatorStateInfo(0).IsTag("DIG");
        Walk = Body.GetCurrentAnimatorStateInfo(0).IsTag("WALK");
        Hurt = Body.GetCurrentAnimatorStateInfo(0).IsTag("HURT");
        Attack = Body.GetCurrentAnimatorStateInfo(0).IsTag("ATTACK");

        if (Chop) isChopping = false;
        if (Dig) isDigging = false;

        if (Hurt) gotHit = false;
        if (Attack) isAttacking = false;


    }
}
