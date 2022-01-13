using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private bool isTurned=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isTurned==false)
            {
                transform.Rotate(0, 180, 0);
                isTurned = true;
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y);

        }



        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isTurned)
            {
                transform.Rotate(0, 180, 0);
                isTurned = false;

            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y);
        }

    }
}
