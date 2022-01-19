using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : MonoBehaviour
{
    public static OtherPlayer instance;
    public float speed;
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
                Debug.Log("longueur" + GameManager.arrayOtherPlayerDestination.Length);
                Debug.Log(GameManager.arrayOtherPlayerDestination[i]);
                //Debug.Log("destination" + GameManager.arrayOtherPlayerDestination[i]);
                //Debug.Log(GameManager.arrayOtherPlayerGameObject[i].transform.position);

                if (GameManager.arrayOtherPlayerDestination[i] != null)
                {
                    if (GameManager.arrayOtherPlayerDestination[i] != GameManager.arrayOtherPlayerGameObject[i].transform.position)
                    {

                        GameObject player = GameManager.arrayOtherPlayerGameObject[i];
                        player.gameObject.transform.position = Vector3.MoveTowards(player.transform.position, GameManager.arrayOtherPlayerDestination[i], speed * Time.deltaTime);

                    }
                }
            }

        }
    }
    public void ChangePositionOtherPlayer(int nbPlayer, float posX, float posY)
    {

        GameManager.arrayOtherPlayerDestination[nbPlayer] = new Vector3(posX, posY, 0);
        //Debug.Log(posX.ToString() + " " + posY.ToString());
        


    }

}
