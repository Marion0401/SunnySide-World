using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotGrowth : MonoBehaviour
{
    [SerializeField] GameObject smallCarrot;
    [SerializeField] GameObject bigCarrot;
    [SerializeField] int level = 2;
    [SerializeField] int harvestLevel = 1;
    [SerializeField] bool isHarvestable = true;
    public bool gotHarvested = false;
    public bool respawning = false;

    private float counter = 0;
    private float betterCounter = 0;

    private bool smallCarrotOn = false;
    private bool bigCarrotOn = false;
    
    [SerializeField] float timeToFull = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        isHarvestable = (level >= harvestLevel);
        
        if (gotHarvested && !isHarvestable) { gotHarvested = false; }

        if(gotHarvested)
        {
            level = 0;
            counter = 0;
            betterCounter = 0;

            gotHarvested = false;
            
            smallCarrot.SetActive(false);
            smallCarrotOn = false;

            bigCarrot.SetActive(false);
            bigCarrotOn = false;
        }
        
        if(level < 2)
        {
            counter += Time.deltaTime;
            if (counter >= 0.1f)
            {
                counter = 0;
                betterCounter += 0.1f;
            }
        }

        if ((level == 0) && (betterCounter >= (timeToFull / 2)) && !smallCarrotOn)
        {
            smallCarrotOn = true;
            smallCarrot.SetActive(true);
            level++;
        }

        
        if ((level == 1) && (betterCounter >= timeToFull) && !bigCarrotOn)
        {
            bigCarrotOn = true;
            bigCarrot.SetActive(true);

            smallCarrot.SetActive(false);
            smallCarrotOn = false;

            level++;

            counter = 0;
            betterCounter = 0;
        }


    }
}
