using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    public static OtherPlayer instance;
    public float speed;
    private AnimationInterface animator;
    private GameObject player;
    private void Awake()
    {
        instance = this;
    }

    
    private void Update()
    {
        for (int i = 0; i < GameManager.arrayOtherPlayerDestination.Length; i++)
        {
            if (i != GameManager.nbMainPlayer)
            {
                
                if (GameManager.arrayOtherPlayerDestination[i] != null)
                {
                    if (GameManager.arrayOtherPlayerDestination[i] != GameManager.arrayOtherPlayerGameObject[i].transform.position)
                    {

                        player = GameManager.arrayOtherPlayerGameObject[i];


                        if (GameManager.arrayOtherPlayerDestination[i].x < player.transform.position.x && player.GetComponent<OtherPlayerInformation>().isTurned == false)
                        {

                            player.transform.Rotate(new Vector3(0, 180, 0));
                            player.GetComponent<OtherPlayerInformation>().isTurned = true;

                        }

                        else if (GameManager.arrayOtherPlayerDestination[i].x > player.transform.position.x && player.GetComponent<OtherPlayerInformation>().isTurned == true)
                        {
                            player.transform.Rotate(new Vector3(0, 180, 0));
                            player.GetComponent<OtherPlayerInformation>().isTurned = false;
                        }


                        player.gameObject.transform.position = Vector3.MoveTowards(player.transform.position, GameManager.arrayOtherPlayerDestination[i], speed * Time.deltaTime);
                        animator = player.GetComponent<AnimationInterface>();
                        animator.isMoving = true;


                    }
                    else
                    {
                        animator.isMoving = false;
                    }
                }
            }

        }
    }
    
    public void ChangePositionOtherPlayer(int nbPlayer, float posX, float posY)
    {

        GameManager.arrayOtherPlayerDestination[nbPlayer] = new Vector3(posX, posY, -5);
        
        


    }

}
