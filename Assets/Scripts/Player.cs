using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    
    public static Player instance;
    private Vector3 mousePosition;
    private Vector3 direction;
    // Start is called before the first frame update
   

    private void Awake()
    {
        instance = this;

    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);            
            direction = mousePosition;
            direction.z = 0;
            GameManager.instance.SendNewPosition(direction.x, direction.y);
        }     
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);        
    }



    


}
