using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public GameObject treeCut;
    private bool isCollided;
    private void OnMouseDown()
    {
        if (isCollided)
        {
            Instantiate(treeCut, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isCollided = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollided = false;
    }
}
