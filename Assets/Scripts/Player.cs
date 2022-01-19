using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    
    public static Player instance;
    private Vector3 mousePosition;
    private Vector3 direction;
    public AnimationInterface animator;
    public bool isTurned = false;
    // Start is called before the first frame update
   

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        animator = GetComponent<AnimationInterface>();
    }
    // Update is called once per frame
    void Update()
    {
        animator.isMoving = false;

        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);            
            direction = mousePosition;
            direction.z = 0;
            GameManager.instance.SendNewPosition(direction.x, direction.y);
            if (mousePosition.x < transform.position.x && isTurned==false)
            {
                
                transform.Rotate(new Vector3 (0, 180, 0));
                isTurned = true;

            }

            else if (mousePosition.x > transform.position.x && isTurned == true)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                isTurned = false;
            }


        }
        if (transform.position != direction)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            animator.isMoving = true;

        }
    }



    


}
