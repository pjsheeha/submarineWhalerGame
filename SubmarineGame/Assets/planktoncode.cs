using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planktoncode : MonoBehaviour
{
 
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "whale")
        {
            if(GameObject.Find("Whale").GetComponent<FollowBehavior>().behavior == 1)
            {
                //Destroy(gameObject);
            }
        }
    }
}
