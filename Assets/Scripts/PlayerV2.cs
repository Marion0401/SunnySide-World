using UnityEngine;

public class PlayerV2 : MonoBehaviour
{
    [SerializeField] public float speed = 1;
    [SerializeField] [Range(0, 1f)] public float inputThreshold = 0.75f;
    public bool facingRight = true;
    public bool isMoving = false;
    public bool chopping = false;
    public bool digging = false;
    public bool hit = false;


    public bool idle = false;
    public bool chop = false;
    public bool dig = false;
    public bool walk = false;
    public bool hurt = false;



    public bool noMoveAction = false;
    [Space(5)]
    [SerializeField] Animator Body;
    [SerializeField] Animator Hair;
    [SerializeField] Animator Hand;


    // Start is called before the first frame update
    void Start()
    {

    }

    private bool Move()
    {
        bool moved = true;
        
        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");


        

        //print(xInput.ToString() + "," + yInput.ToString());

        float tempSpeed = speed;

        if (xInput != 0 && yInput != 0)
            speed /= 1.4142135f;



        if (Mathf.Abs(yInput) > inputThreshold)
        {
            if (yInput > inputThreshold)
            {
                transform.position += (Vector3)(Vector2.up * 0.01f * speed);
            }
            else
            {
                transform.position -= (Vector3)(Vector2.up * 0.01f * speed);
            }
            isMoving = true;
        }

        if(xInput !=0)
        {
            if (xInput > 0)
            {
                if (!facingRight)
                {
                    transform.Rotate(0, 180, 0);
                }
                facingRight = true;
            }
            else
            {
                if (facingRight)
                {
                    transform.Rotate(0, 180, 0);
                }
                facingRight = false;
            }
            isMoving = true;
        }


        if (Mathf.Abs(xInput) > inputThreshold)
        {
            if (xInput > inputThreshold)
            {
                transform.position += (Vector3)(Vector2.right * 0.01f * speed);
            }
            else
            {
                transform.position -= (Vector3)(Vector2.right * 0.01f * speed);
            }
        }

        if (Mathf.Abs(yInput) ==0 && Mathf.Abs(xInput) ==0)
        {
            isMoving = false;
            moved = false;
        }

        speed = tempSpeed;

       
       

        // Réseau
        //==================================

        if (isMoving != Body.GetBool("isMoving"))
        {
            //Send("MoveAnim", isMoving);

            Body.SetBool("isMoving", isMoving);
            Hair.SetBool("isMoving", isMoving);
            Hand.SetBool("isMoving", isMoving);

        }

        if (moved)
        {
            //Send("Moved", x, y, z, facingRight);
        }

        //==================================


        return moved;
    }

    private void Chop(bool state)
    {
        Body.SetBool("isChopping", state);
        Hair.SetBool("isChopping", state);
        Hand.SetBool("isChopping", state);


        // Réseau
        //==========================

            //Send("Chop", state);

        //==========================

    }

    private void Dig(bool state)
    {
        Body.SetBool("isDigging", state);
        Hair.SetBool("isDigging", state);
        Hand.SetBool("isDigging", state);


        // Réseau
        //==========================

        //Send("Dig", state);

        //==========================

    }


    private void Hurt(bool state)
    {
        Body.SetBool("gotHit", state);
        Hair.SetBool("gotHit", state);
        Hand.SetBool("gotHit", state);


        // Réseau
        //==========================

        //Send("Hurt", state);

        //==========================
    }

    private void AnimSync()
    {
        if (Body.GetCurrentAnimatorStateInfo(0).normalizedTime == 0)
            Hair.Play(Hair.GetCurrentAnimatorStateInfo(0).fullPathHash, 0,0);

        if (Body.GetCurrentAnimatorStateInfo(0).normalizedTime == 0)
            Hand.Play(Hand.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        //AnimSync();

        idle = Body.GetCurrentAnimatorStateInfo(0).IsTag("IDLE");
        chop = Body.GetCurrentAnimatorStateInfo(0).IsTag("CHOP");
        dig = Body.GetCurrentAnimatorStateInfo(0).IsTag("DIG");
        walk = Body.GetCurrentAnimatorStateInfo(0).IsTag("WALK");
        hurt = Body.GetCurrentAnimatorStateInfo(0).IsTag("HURT");

        if (chopping) { Chop(false); chopping = false; }

        if (digging) { Dig(false); digging = false; }

        if (hit) { Hurt(false); hit = false; }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Hurt(true);
            hit = true;
        }




        noMoveAction = (chop || dig || hurt);



        if((idle || walk) && !hurt)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Chop(true);
                chopping = true;
            } 

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Dig(true);
                digging = true;
            }

        }
        




        if(!noMoveAction && ! chopping && ! digging && !hurt) Move();


        
    }
}
