using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    private Skelly_AnimationInterface skellyAnimation;
    private bool isCollided;
    private int indexSkeleton;
    public GameObject swordPrefab;
    private GameObject sword;
    public float positionSword;

    private void Start()
    {
        skellyAnimation = GetComponent<Skelly_AnimationInterface>();
        sword = Instantiate(swordPrefab, new Vector2(transform.position.x, transform.position.y + positionSword), Quaternion.identity);
        sword.SetActive(false);
    }

    private void Update()
    {
        if (isCollided)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {

                Player.instance.animator.isAttacking= true;
                skellyAnimation.gotHit = true;
                GameManager.instance.SendMessageSkeletonAttacked(indexSkeleton);


            }

        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        isCollided = true;
        sword.SetActive(true);

        for (int index = 0; index < Map.instance.listTrees.Count; index++)
        {

            if (Map.instance.listTrees[index].name == gameObject.name)
            {

                indexSkeleton = index;


            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollided = false;
        sword.SetActive(true);
    }

}

