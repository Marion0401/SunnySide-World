using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    private TreeCut treeCut;
    private bool isCollided;
    private int indexTree;

    private void Start()
    {
        treeCut = GetComponent<TreeCut>();
    }

    private void Update()
    {
        if (isCollided)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                Player.instance.animator.isChopping = true;
                treeCut.isHit = true;

                
            }
            //Instantiate(treeCut, gameObject.transform.position, Quaternion.identity);
            //GameManager.instance.SendMessageTreeCut(indexTree);
            //Destroy(gameObject);
        }
        if (treeCut.isCut == true)
        {
            Debug.Log("c est coupé");
            GameManager.instance.SendMessageTreeCut(indexTree);
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
                Debug.Log(index);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollided = false;
    }
    

}
