using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcosystemManagement : MonoBehaviour
{
    int day = 0;
    float healFactor = 1.07f;
    float growthRate = 0.708f;

    GameObject[] thirdOrderConsumers;
    GameObject[] secondOrderConsumers;
    GameObject[] firstOrderConsumers;
    GameObject[] primaryProducers;
    GameObject[] whalePoop;

    float thirdOrderPopulation = 1;
    float secondOrderPopulation = 1;
    float firstOrderPopulation = 1;
    float primaryProducerPopulation = 1;

    int howManyThirdOrderConsumers = 0;
    int howManySecondOrderConsumers = 0;
    int howManyFirstOrderConsumers = 0;
    int howManyPrimaryProducers = 0;

    // Start is called before the first frame update
    void Start()
    {
        howManyThirdOrderConsumers = 5;
        howManySecondOrderConsumers = howManyThirdOrderConsumers * 10;
        howManyFirstOrderConsumers = howManySecondOrderConsumers * 5;
        howManyPrimaryProducers = howManyFirstOrderConsumers * 20;
        ecoCreate();
    }

// Update is called once per frame
void Update()
{

}

void ecoCreate()
{
    for (int i = 0; i < howManyThirdOrderConsumers; i++)
    {
        thirdOrderConsumers[i] = Instantiate(objWhale);
        // also poop
        whalePoop[i] = Instantiate(objWhalePoop);
    }
    for (int i = 0; i < howManySecondOrderConsumers; i++)
    {
        secondOrderConsumers[i] = Instantiate(objFish);
    }
    for (int i = 0; i < howManyFirstOrderConsumers; i++)
    {
        firstOrderConsumers[i] = Instantiate(objKrill);
    }
    for (int i = 0; i < howManyFirstOrderConsumers; i++)
    {
        firstOrderConsumers[i] = Instantiate(objKrill;
    }
}
}