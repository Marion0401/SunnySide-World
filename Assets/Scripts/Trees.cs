using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public GameObject treeCut;
    private bool isCollided;
    private int indexTree;
    
    private void OnMouseDown()
    {
        if (isCollided)
        {
            Instantiate(treeCut, gameObject.transform.position, Quaternion.identity);
            GameManager.instance.SendMessageTreeCut(indexTree);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isCollided = true;
        
        for (int index=0; index<Map.instance.listTrees.Count;index++)
        {
            
            if (Map.instance.listTrees[index].name == gameObject.name)
            {
                
                indexTree = index;
                

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollided = false;
    }
    

}
