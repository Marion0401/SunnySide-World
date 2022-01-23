using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public GameObject shovelPrefab;
    private GameObject shovel;
    public float positionShovel;
    private CarrotGrowth carrotGrowth;
    private bool isCollided;
    private int indexCarrot;
    private bool isDiggingForCarrot = false;
    private void Start()
    {
        carrotGrowth = GetComponent<CarrotGrowth>();
        shovel = Instantiate(shovelPrefab, new Vector2(transform.position.x, transform.position.y + positionShovel), Quaternion.identity);
        shovel.SetActive(false);
    }

    private void Update()
    {
        if (isCollided)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {

                Player.instance.animator.isDigging = true;
                isDiggingForCarrot=true;
                
                GameManager.instance.SendMessageCarrot(indexCarrot);


            }

        }

        if (isDiggingForCarrot)
        {
            if(Player.instance.animator.isDigging == false)
            {
                carrotGrowth.gotHarvested = true;
                isDiggingForCarrot = false;
                shovel.SetActive(false);
            }
        }

        

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        isCollided = true;
        shovel.SetActive(true);
        for (int index = 0; index < Map.instance.listCarrots.Count; index++)
        {

            if (Map.instance.listCarrots[index].name == gameObject.name)
            {

                indexCarrot = index;


            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shovel.SetActive(false);
        isCollided = false;
    }
}
