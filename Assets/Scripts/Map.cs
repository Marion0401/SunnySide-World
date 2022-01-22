using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map instance;
    public List<GameObject> listTrees = new List<GameObject>();
    public GameObject treeCut; 
    

    private void Awake()
    {
        instance = this;
    }

    public void cutTreeWithIndex(int index)
    {
        Debug.Log("je dois couper l'arbe" + index);
        GameObject tree = Map.instance.listTrees[index];
        tree.GetComponent<TreeCut>().hitPoints = 1;
        tree.GetComponent<TreeCut>().isHit = true;
        //Instantiate(treeCut, new Vector2(tree.transform.position.x, tree.transform.position.y), Quaternion.identity);
        //Destroy(tree);
    }

    public void playerCutTreeWithIndex(int indexTree, int numberPlayer)
    {
        GameManager.arrayOtherPlayerGameObject[numberPlayer].GetComponent<AnimationInterface>().isChopping = true;
        GameObject tree = Map.instance.listTrees[indexTree];
        tree.GetComponent<TreeCut>().isHit =true;
    }
}
