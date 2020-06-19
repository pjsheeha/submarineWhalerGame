using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followSub : MonoBehaviour
{
    public GameObject mySub; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mySub.transform.position;
    }
}
