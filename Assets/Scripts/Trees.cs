using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    private TreeCut treeCut;
    private bool isCollided;
    private int indexTree;
    public GameObject axePrefab;
    private GameObject axe;
    public float positionAxe;

    private void Start()
    {
        treeCut = GetComponent<TreeCut>();
        axe = Instantiate(axePrefab, new Vector2(transform.position.x, transform.position.y + positionAxe), Quaternion.identity);
        axe.SetActive(false);
    }

    private void Update()
    {
        if (isCollided)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                Player.instance.animator.isChopping = true;
                treeCut.isHit = true;
                GameManager.instance.SendMessageTreeCut(indexTree);


            }
           
        }
        if (treeCut.hitPoints == 0)
        {
            axe.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        isCollided = true;
        axe.SetActive(true);


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
        axe.SetActive(false);

    }


}
