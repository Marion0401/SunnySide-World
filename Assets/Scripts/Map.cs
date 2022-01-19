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
        
        GameObject tree = Map.instance.listTrees[index];
        Instantiate(treeCut, new Vector2(tree.transform.position.x, tree.transform.position.y), Quaternion.identity);
        Destroy(tree);
    }

    
}
