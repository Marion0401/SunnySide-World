using UnityEngine;

public class PlayerV2 : MonoBehaviour
{
    [SerializeField] public float speed = 1;
    [SerializeField] [Range(0, 1f)] public float inputThreshold = 0.75f;
    public bool facingRight = true;
    public bool isMoving = false;
    public bool chopping = false;
    public bool idle = false;
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

       
        Body.SetBool("isMoving", isMoving);
        Hair.SetBool("isMoving", isMoving);
        Hand.SetBool("isMoving", isMoving);
        

        // Réseau
        //==================================

        if (isMoving != Body.GetBool("isMoving"))
        {
            //Send("MoveAnim", isMoving);
        }

        if(moved)
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

    // Update is called once per frame
    void Update()
    {
        idle = Body.GetCurrentAnimatorStateInfo(0).IsTag("IDLE");
        if (chopping) { Chop(false); chopping = false; }


        noMoveAction = false;
        if(idle)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                noMoveAction = true;
                Chop(true);
                chopping = true;
            } 
            else
            {
                noMoveAction = false;
            }

        }
        




        if(!noMoveAction) Move();



    }
}
