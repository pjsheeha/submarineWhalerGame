using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Timeline;
using UnityMovementAI;

public class EcosystemManagement : MonoBehaviour
{
    int day = 0;
    public bool checkToEndDay = false;
    public bool checkToKillWhale = false;
    int globalWarmth = 0;
    public float timerDay = 0f;
    float healFactor = 1.07f;
    float growthRate = 0.708f;
    bool dayStarted = false;
    bool canSignalWhales = false;
    bool canSignalFish = false;
    bool canSignalKrill = false;
    bool canSignalPlankton = false;
    public float dayLengthSeconds = 300f;

    public GameObject WhaleInstance;
    public GameObject FishInstance;
    public GameObject KrillInstance;
    public GameObject PlanktonInstance;
    public GameObject WhalePoopInstance;

    List<GameObject> whales = new List<GameObject>();
    List<GameObject> fishes = new List<GameObject>();
    List<GameObject> krills = new List<GameObject>();
    List<GameObject> planktons = new List<GameObject>();
    List<GameObject> whalePoops = new List<GameObject>();

    int maxWhalePopulation = 3;
    int maxFishPopulation = 9; //*3
    int maxKrillPopulation = 27; //*3
    int maxPlanktonPopulation = 81; //*3
    int maxWhalePoopPopulation = 3;

    public int whalePopulation, fishPopulation, krillPopulation, planktonPopulation, whalePoopPopulation;
    int previousWhalePopulation, previousFishPopulation, previousKrillPopulation, previousPlanktonPopulation, previousWhalePoopPopulation;

    int fishThatMustBeEaten = 0;
    int krillThatMustBeEaten = 0;
    int planktonThatMustBeEaten = 0;
    int whalePoopThatMustBeEaten = 0;



    // Start is called before the first frame update
    void Start()
    {

        previousWhalePopulation = whalePopulation = maxWhalePopulation;
        previousFishPopulation = fishPopulation = maxFishPopulation;
        previousKrillPopulation = krillPopulation = maxKrillPopulation;
        previousPlanktonPopulation = planktonPopulation = maxPlanktonPopulation;
        previousWhalePoopPopulation = whalePoopPopulation = maxWhalePoopPopulation;

        ecoCreate();
        dayStart();
    
    }

    void debugTesting()
    {
        if (checkToEndDay)
        {
            checkToEndDay = false;
            timerDay = 0.1;
        }

        if(checkToKillWhale)
        {
            checkToKillWhale = false;
            whales[0].GetComponent<pursuewhale>().huntedByShip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        debugTesting();

        if (dayStarted)
            timerDay -= Time.deltaTime;

        if ((timerDay <= 0) && dayStarted)
        {
            timerDay = 0;
            dayStarted = false;
            dayOver();
        }

        if ((timerDay < dayLengthSeconds) && canSignalWhales)
        {
            canSignalWhales = false;
            signalWhales(fishThatMustBeEaten);
        }
        if ((timerDay < dayLengthSeconds * 3 / 4) && canSignalFish)
        {
            canSignalFish = false;
            signalFish(krillThatMustBeEaten);
        }
        if ((timerDay < dayLengthSeconds * 1 / 2) && canSignalKrill)
        {
            canSignalKrill = false;
            signalKrill(planktonThatMustBeEaten);
        }
        if ((timerDay < dayLengthSeconds * 1 / 4) && canSignalPlankton)
        {
            canSignalPlankton = false;
            signalPlankton(whalePoopThatMustBeEaten);
        }
    }

    void dayStart()
    {
        day++;
        dayStarted = true;
        canSignalWhales = true;
        canSignalFish = true;
        canSignalKrill = true;
        canSignalPlankton = true;
        timerDay = dayLengthSeconds;
    }

    void ecoCreate()
    {
        /// Instantiate all elements on the ecosystem    
        for (int i = 0; i < whalePopulation; i++)
        {
            whales.Add(Instantiate(WhaleInstance, transform.position, transform.rotation));
            // also poop
            whalePoops.Add(Instantiate(WhalePoopInstance, transform.position, transform.rotation));
        }
        for (int i = 0; i < fishPopulation; i++)
        {
            fishes.Add(Instantiate(FishInstance, transform.position, transform.rotation));
        }
        for (int i = 0; i < krillPopulation; i++)
        {
            krills.Add(Instantiate(KrillInstance, transform.position, transform.rotation));
        }
        for (int i = 0; i < planktonPopulation; i++)
        {
            planktons.Add(Instantiate(PlanktonInstance, transform.position, transform.rotation));
        }
    }

    /**
     * callerId: The organism/poop that requests the sweet release of death
     * reason:
     * 0: unknown
     * 1: hunted
     * 2: eaten
     */
    void removeOrganism(GameObject callerId, int reason)
    {
        removeCallerIdFromList(callerId, reason);
    }

    /// Find where in the array caller id is, eliminate it
    int removeCallerIdFromList(GameObject callerId, int reason)
    {
        int ret = 0;
        if (whales.Contains(callerId))
        {
            whales.Remove(callerId);
            if (reason == 1)
                whalePopulation--;
            callerId.GetComponent<death>().Die();
        }
        else if (fishes.Contains(callerId))
        {
            fishes.Remove(callerId);
            if (reason == 1)
                fishPopulation--;
            if (reason == 2)
            {
                fishThatMustBeEaten--;
                fishPopulation--;
            }
            callerId.GetComponent<death>().Die();
        }
        else if (krills.Contains(callerId))
        {
            krills.Remove(callerId);
            if (reason == 1)
                krillPopulation--;
            if (reason == 2)
            {
                krillThatMustBeEaten--;
                krillPopulation--;
            }
            callerId.GetComponent<death>().Die();
        }
        else if (planktons.Contains(callerId))
        {
            planktons.Remove(callerId);
            if (reason == 1)
                planktonPopulation--;
            if (reason == 2)
            {
                planktonThatMustBeEaten--;
                planktonPopulation--;
            }
            callerId.GetComponent<death>().Die();
        }
        else if (whalePoops.Contains(callerId))
        {
            whalePoops.Remove(callerId);
            if (reason == 1)
                whalePoopPopulation--;
            if (reason == 2)
            {
                whalePoopThatMustBeEaten--;
                whalePoopPopulation--;
            }
            callerId.GetComponent<death>().Die();
        }
        else
        {
            ret = 1; //not found
        }
        return ret;
    }

    /**
     * This is called in case organisms that MustBeEaten where not in fact eaten
     */
    void removeDeathMarkedOrganisms()
    {
        while (fishThatMustBeEaten > 0)
        {
            removeOrganism(fishes[0], 2);
        }
        while (krillThatMustBeEaten > 0)
        {
            removeOrganism(krills[0], 2);
        }
        while (planktonThatMustBeEaten > 0)
        {
            removeOrganism(planktons[0], 2);
        }
        while (whalePoopThatMustBeEaten > 0)
        {
            removeOrganism(whalePoops[0], 2);
        }
    }

    void dayOver()
    {
        removeDeathMarkedOrganisms();
        /// When the day is over the ecosystem must figure out which organisms need to go.
        /// We assume that the current populations were already updated in removeOrganism(). 
        /// Right now we have the old information stored in previous.

        float whalePercentage = (float)whalePopulation / (float)maxWhalePopulation;
        float fishPercentage = (float)fishPopulation / (float)maxFishPopulation;
        float krillPercentage = (float)krillPopulation / (float)maxKrillPopulation;
        float planktonPercentage = (float)planktonPopulation / (float)maxPlanktonPopulation;
        float whalePoopPercentage = (float)whalePoopPopulation / (float)maxWhalePoopPopulation;

        float newWhalePercentage = whalePercentage - ((1 - fishPercentage) * growthRate);
        float newFishPercentage = fishPercentage - ((1 - krillPercentage) * growthRate);
        float newKrillPercentage = krillPercentage - ((1 - planktonPercentage) * growthRate);
        float newPlanktonPercentage = planktonPercentage - ((1 - whalePoopPercentage) * growthRate);
        float newWhalePoopPercentage = whalePercentage;

        // Ok, now that we've fiigured out the percentage we proceed to convert this values to ceiled populations
        whalePopulation = (int)Mathf.Ceil(newWhalePercentage * maxWhalePopulation);
        fishPopulation = (int)Mathf.Ceil(newFishPercentage * maxFishPopulation);
        krillPopulation = (int)Mathf.Ceil(newKrillPercentage * maxKrillPopulation);
        planktonPopulation = (int)Mathf.Ceil(newPlanktonPercentage * maxPlanktonPopulation);
        whalePoopPopulation = (int)Mathf.Ceil(newWhalePoopPercentage * maxWhalePoopPopulation);

        // By this part its possible for difference in whole numbers to exist in previousPopulation and todayPopulation.
        // We have to order our organisms what to eat tomorrow.

        /// All of this must be sent to a method all of these organisms have. And they individually will have to decide how to deal with this eat queue.
        // This will not be decided here, but in the update method. What we do here is decide how many organisms will die
        //if(less whales) throw corpse into beach
        /*not yet implemented*/

        fishThatMustBeEaten = previousFishPopulation - fishPopulation;
        krillThatMustBeEaten = previousKrillPopulation - krillPopulation;
        planktonThatMustBeEaten = previousPlanktonPopulation - planktonPopulation;
        whalePoopThatMustBeEaten = previousWhalePoopPopulation - whalePoopPopulation;

        // Last thing that must be done in this function
        // yesterdayData = todayData;
        previousWhalePopulation = whalePopulation;
        previousFishPopulation = fishPopulation;
        previousKrillPopulation = krillPopulation;
        previousPlanktonPopulation = planktonPopulation;
        previousWhalePoopPopulation = whalePoopPopulation;
    }

    void signalWhales(int amountToFeed)
    {

        //if(less fish) order whales in my whale array to eat this amount of fish
        if ((amountToFeed) > 0)
        {
            int feed = amountToFeed;
            int index = 0;
            while (feed > 0)
            {
                whales[index].GetComponent<homeworkgiver>().addTargetHomework();
                index++;
                index = index % whales.Count;
                feed--;
            }
        }
    }

    void signalFish(int amountToFeed)
    {
        //if(less krill) order fish to eat this amount of krill
        if ((amountToFeed) > 0)
        {
            int feed = amountToFeed;
            int index = 0;
            while (feed > 0)
            {
                fishes[index].GetComponent<PursueUnit>().addTargetHomework();
                index++;
                index = index % fishes.Count;
                feed--;
            }
        }
    }

    void signalKrill(int amountToFeed)
    {

        //if(less plankton) order krill to eat plankton
        if ((amountToFeed) > 0)
        {
            int feed = amountToFeed;
            int index = 0;
            while (feed > 0)
            {
                krills[index].GetComponent<homeworkgiver>().addTargetHomework();
                index++;
                index = index % krills.Count;
                feed--;
            }
        }
    }

    void signalPlankton(int amountToFeed)
    {
        //if(less whale poop) order plankton to eat whale poop
        if ((amountToFeed) > 0)
        {
            int feed = amountToFeed;
            int index = 0;
            while (feed > 0)
            {
                planktons[index].GetComponent<homeworkgiver>().addTargetHomework();
                index++;
                index = index % planktons.Count;
                feed--;
            }
        }
    }

}