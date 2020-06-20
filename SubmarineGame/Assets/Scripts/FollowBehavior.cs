using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : MonoBehaviour
{
   
    public float behavior; 
    //0 = cruise
    //1 = plankton
    //2 = targets
    //3 = flip

    void Start()
    {
       behavior = 0;
    }

    void Update()
    {

    }
}