﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMovementAI
{
	public class pursuewhale : MonoBehaviour
	{
	    public MovementAIRigidbody target;
	    public float range = 11f;
	    public float iwanttoeat; 
	    public float eatnumber;
	    public float minspeed;
	    public float maxspeed;

	    SteeringBasics steeringBasics;
	    Pursue pursue;
	    void Start()
	    {
	        steeringBasics = GetComponent<SteeringBasics>();
	        pursue = GetComponent<Pursue>();
	        //CHANGE
	        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	        iwanttoeat = 0;
	        target = GameObject.Find("cruiser").GetComponent<MovementAIRigidbody>();
	        this.GetComponent<SteeringBasics>().maxVelocity = Random.Range(minspeed, maxspeed);
	        Debug.Log(this.GetComponent<SteeringBasics>().maxVelocity);
	    }
	    void UpdateTarget()
	    {

	        GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("Fish");
	        float shortestDistance = Mathf.Infinity;
	        GameObject nearestEnemy2 = null;

	        //FIND ALL TARGETS AND DISTANCE
	        foreach (GameObject enem in enemies2)
	        {
	            float distanceToEnemy = Vector3.Distance(transform.position, enem.transform.position);
	            if(distanceToEnemy < shortestDistance)
	            {
	                shortestDistance = distanceToEnemy;
	                nearestEnemy2 = enem;
	            }
	        }
	        //SEEK TARGETS
	        //Attack all Fish
	        if (nearestEnemy2 != null && shortestDistance <= range && iwanttoeat == 1  && eatnumber < 0)
	        {
	            target = nearestEnemy2.GetComponent<MovementAIRigidbody>();
	            //Cruise
	        } else if (nearestEnemy2 == null || iwanttoeat == 0)
	        {
	            target = GameObject.Find("cruiser").GetComponent<MovementAIRigidbody>();
	        }
	    }
	    public void addTargetHomework()
        {
           iwanttoeat = 1;
        }
        public void Die()
        {
            Debug.Log("dead");
			
        }
	    public void huntedByShip()
	    {
	    	Debug.Log("Hunted Oh shuit");
			GameObject.Find("EcosystemManager").GetComponent<EcosystemManagement>().removeOrganism(this.gameObject ,1);
		}

	    // Update is called once per frame
	    void FixedUpdate()
	    {
	        Vector3 accel = pursue.GetSteering(target);
	        this.GetComponent<FollowBehavior>().behavior = iwanttoeat; 

	        steeringBasics.Steer(accel);
	        steeringBasics.LookWhereYoureGoing();
	    }
	}
}
