using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EcosystemManagement : MonoBehaviour
{
    int day = 0;
    int globalWarmth = 0;
    public float timerDay = 0f;
    float healFactor = 1.07f;
    float growthRate = 0.708f;
    bool dayStarted = false;
    public float dayLengthSeconds = 300f;

    public GameObject thirdOrderInstance;
    public GameObject secondOrderInstance;
    public GameObject firstOrderInstance;
    public GameObject primaryProducerInstance;
    public GameObject whalePoop;

    GameObject[] thirdOrderConsumers;
    GameObject[] secondOrderConsumers;
    GameObject[] firstOrderConsumers;
    GameObject[] primaryProducers;
    GameObject[] whalePoops;

    int maxThirdOrderPopulation = 3;
    int maxSecondOrderPopulation = 9; //*3
    int maxFirstOrderPopulation = 27; //*3
    int maxPrimaryProducerPopulation = 81; //*3
    int maxWhalePoopPopulation = 3;

    int thirdOrderPopulation, secondOrderPopulation, firstOrderPopulation, primaryProducerPopulation, whalePoopPopulation;
    int previousThirdOrderPopulation, previousSecondOrderPopulation, previousFirstOrderPopulation, previousPrimaryProducerPopulation, previousWhalePoopPopulation;

    // Start is called before the first frame update
    void Start()
    {
        previousThirdOrderPopulation = thirdOrderPopulation = maxThirdOrderPopulation;
        previousSecondOrderPopulation = secondOrderPopulation = maxSecondOrderPopulation;
        previousFirstOrderPopulation = firstOrderPopulation = maxFirstOrderPopulation;
        previousPrimaryProducerPopulation = primaryProducerPopulation = maxPrimaryProducerPopulation;
        previousWhalePoopPopulation = whalePoopPopulation = maxWhalePoopPopulation;

        ecoCreate();
        dayStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(dayStarted)
            timerDay -= Time.deltaTime;

        if ((timerDay <= 0) && dayStarted)
        {
            dayStarted = false;
            dayOver();
        }
    }

    void dayStart()
    {
        day++;
        dayStarted = true;
        timerDay = dayLengthSeconds;
    }

    void ecoCreate()
    {
        /// Instantiate all elements on the ecosystem    
        for (int i = 0; i < thirdOrderPopulation; i++)
        {
            thirdOrderConsumers[i] = Instantiate(thirdOrderInstance, transform.position, transform.rotation);
            // also poop
            whalePoops[i] = Instantiate(whalePoop, transform.position, transform.rotation);
        }
        for (int i = 0; i < secondOrderPopulation; i++)
        {
            secondOrderConsumers[i] = Instantiate(secondOrderInstance, transform.position, transform.rotation);
        }
        for (int i = 0; i < firstOrderPopulation; i++)
        {
            firstOrderConsumers[i] = Instantiate(firstOrderInstance, transform.position, transform.rotation);
        }
        for (int i = 0; i < primaryProducerPopulation; i++)
        {
            primaryProducers[i] = Instantiate(primaryProducerInstance, transform.position, transform.rotation);
        }
    }

    // An organism/poop will call this to request the sweet release of death
    void removeOrganism( GameObject callerId )
    {
        /// Find where in the array caller id is, eliminate it
        /// and adjust array order and size
    }

    void dayOver()
    {
        /// When the day is over the ecosystem must figure out which organisms need to go.
        /// We assume that the current populations were already updated in removeOrganism(). 
        /// Right now we have the old information stored in previous.

        float thirdOrderPercentage = (float)thirdOrderPopulation / (float)maxThirdOrderPopulation;
        float secondOrderPercentage = (float)secondOrderPopulation / (float)maxSecondOrderPopulation;
        float firstOrderPercentage = (float)firstOrderPopulation / (float)maxFirstOrderPopulation;
        float primaryProducerPercentage = (float)primaryProducerPopulation / (float)maxPrimaryProducerPopulation;
        float whalePoopPercentage = (float)whalePoopPopulation / (float)maxWhalePoopPopulation;

        float newThirdOrderPercentage = thirdOrderPercentage - ((1 - secondOrderPercentage) * growthRate);
        float newSecondOrderPercentage = secondOrderPercentage - ((1 - firstOrderPercentage) * growthRate);
        float newFirstOrderPercentage = firstOrderPercentage - ((1 - primaryProducerPercentage) * growthRate);
        float newPrimaryProducerPercentage = primaryProducerPercentage - ((1 - whalePoopPercentage) * growthRate);
        float newWhalePoopPercentage = thirdOrderPercentage;

        // Ok, now that we've fiigured out the percentage we proceed to convert this values to ceiled populations
        thirdOrderPopulation = (int)Mathf.Ceil(newThirdOrderPercentage * maxThirdOrderPopulation);
        secondOrderPopulation = (int)Mathf.Ceil(newSecondOrderPercentage * maxSecondOrderPopulation);
        firstOrderPopulation = (int)Mathf.Ceil(newFirstOrderPercentage * maxFirstOrderPopulation);
        primaryProducerPopulation = (int)Mathf.Ceil(newPrimaryProducerPercentage * maxPrimaryProducerPopulation);
        whalePoopPopulation = (int)Mathf.Ceil(newWhalePoopPercentage * maxWhalePoopPopulation);

        // By this part its possible for difference in whole numbers to exist previousPopulation y todayPopulation.
        // We have to order our organisms what to eat tomorrow.

        /// All of this must be sent to a method all of these organisms have. And the individually will have to decide hoe to deal with this eat queue.
        //if(less whales) throw corpse into beach
        if ((previousThirdOrderPopulation - thirdOrderPopulation) > 0)
        {
            ///
        }
        //if(less fish) order whales in my whale array to eat this amount of fish
        if ((previousSecondOrderPopulation - secondOrderPopulation) > 0)
        {
            int feed = previousSecondOrderPopulation - secondOrderPopulation;
            int index = 0;
            while (feed > 0)
            {
                firstOrderConsumers[index].addTargetHomework();
                index++;
                index = index % firstOrderConsumers.Length;
                feed--;
            }
        }
        //if(less krill) order fish to eat this amount of krill
        if ((previousFirstOrderPopulation - firstOrderPopulation) > 0)
        {
            int feed = previousFirstOrderPopulation - firstOrderPopulation;
            int index = 0;
            while (feed > 0)
            {
                secondOrderConsumers[index].addTargetHomework();
                index++;
                index = index % secondOrderConsumers.Length;
                feed--;
            }
        }
        //if(less plankton) order krill to eat plankton
        if ((previousPrimaryProducerPopulation - primaryProducerPopulation) > 0)
        {
            int feed = previousPrimaryProducerPopulation - primaryProducerPopulation;
            int index = 0;
            while (feed > 0)
            {
                firstOrderConsumers[index].addTargetHomework();
                index++;
                index = index % firstOrderConsumers.Length;
                feed--;
            }
        }
        // NOT NECCESARY FOR NOW: if(less whale poop) order plankton to eat whale poop


        // Last thing that must be done in this function
        //yesterdayData = todayData;
        previousThirdOrderPopulation = thirdOrderPopulation;
        previousSecondOrderPopulation = secondOrderPopulation;
        previousFirstOrderPopulation = firstOrderPopulation;
        previousPrimaryProducerPopulation = primaryProducerPopulation;
        previousWhalePoopPopulation = whalePoopPopulation;
    }

}