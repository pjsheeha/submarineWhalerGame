using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcosystemManagement : MonoBehaviour
{
    int day = 0;
    float healFactor = 1.07f;
    float growthRate = 0.708f;

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
        thirdOrderPopulation = maxThirdOrderPopulation;
        secondOrderPopulation = maxSecondOrderPopulation;
        firstOrderPopulation = maxFirstOrderPopulation;
        primaryProducerPopulation = maxPrimaryProducerPopulation;
        whalePoopPopulation = maxWhalePoopPopulation;

        ecoCreate();
    }

    // Update is called once per frame
    void Update()
    {

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
        /// Find where in the array caller id is and eliminate it
    }

    void dayOver()
    {
        /// When the day is over the ecosystem must figure out which organisms need to go.

        //yesterdayData = todayData;
        previousThirdOrderPopulation = thirdOrderPopulation;
        previousSecondOrderPopulation = secondOrderPopulation;
        previousFirstOrderPopulation = firstOrderPopulation;
        previousPrimaryProducerPopulation = primaryProducerPopulation;
        previousWhalePoopPopulation = whalePoopPopulation;

        // Now we can modify the information we copied into previous*
    }

}