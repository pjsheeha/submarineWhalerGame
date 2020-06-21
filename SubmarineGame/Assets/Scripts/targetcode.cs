using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetcode : MonoBehaviour
{
    // Start is called before the first frame update
    public string predator;
    public string prey;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == predator)
        {
            if(GameObject.Find("Whale").GetComponent<FollowBehavior>().behavior == 2)
            {
                //Destroy(gameObject);
            }
        }
    }
}
