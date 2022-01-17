using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCut : MonoBehaviour
{
    [SerializeField] Sprite trunkFull;
    [SerializeField] Sprite trunkCut;
    [Space(5)]

    [SerializeField] int maxHit = 3;
    [SerializeField] Animator treetop;
    public int hitPoints = 3;

    [SerializeField] float timeBeforeRespawn = 10f;
    public float counter = 0;
    public bool counting = false;

    public bool isCut = false;
    public bool isHit = true;
    public bool isHittable = false;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = maxHit;
        GetComponent<SpriteRenderer>().sprite = trunkFull;
    }

    public void TreeHasBeenHit()
    {
        isHit = true;



    }

    // Update is called once per frame
    void Update()
    {
        isHittable = treetop.GetCurrentAnimatorStateInfo(0).IsTag("IDLE");

        //
        // isHit = (Input.GetKeyDown(KeyCode.Space)) ? true : false;

        

        if (isHit && isHittable && hitPoints == 1)
        {
            isCut = true;
            treetop.SetBool("isCut", true);
            hitPoints = 0;
            

            Color32 color = treetop.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            treetop.GetComponent<SpriteRenderer>().color = color;
            counting = true;
            GetComponent<SpriteRenderer>().sprite = trunkCut;

            treetop.SetFloat("RegrowSpeed", 0.0f);
        }

        treetop.SetBool("Hit", isHit);
        if (isHit && isHittable && hitPoints > 1)
        {
            isHit = false;

            hitPoints--;
        }

        if (counting)
        {
            isHit = false;
            counter += Time.deltaTime;
            if (counter >= timeBeforeRespawn)
            {
                treetop.GetComponent<SpriteRenderer>().color = Color.white;
                GetComponent<SpriteRenderer>().sprite = trunkFull;


                counter = 0;
                counting = false;
                treetop.SetBool("isCut", false);
                hitPoints = maxHit;
                treetop.SetFloat("RegrowSpeed", 0.8f);
            }
        }

    }
}
