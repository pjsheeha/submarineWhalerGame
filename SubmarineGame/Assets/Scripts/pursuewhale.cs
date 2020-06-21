using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMovementAI
{
	public class pursuewhale : MonoBehaviour
	{
		private GameObject boater; 
	    public MovementAIRigidbody target;
	    public float range = 11f;
	    public float iwanttoeat; 
	    public float eatnumber;
	    public float minspeed;
	    public float maxspeed;
	    private bool caught;
	    private bool dead;

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
	        //Debug.Log(this.GetComponent<SteeringBasics>().maxVelocity);
	        caught = false;
	        dead = false;

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
	        if (nearestEnemy2 != null && shortestDistance <= range && iwanttoeat == 1  && eatnumber < 0 && caught == false)
	        {
	            target = nearestEnemy2.GetComponent<MovementAIRigidbody>();
	            //Cruise
	        } else if (nearestEnemy2 == null || iwanttoeat == 0)
	        {
	        	if(caught == false)
	        	{
	            target = GameObject.Find("cruiser").GetComponent<MovementAIRigidbody>();
	        	}
	        }
	    }
	    public void addTargetHomework()
        {
           iwanttoeat = 1;
        }
        public void Die()
        {
        	FindObjectOfType<AudioManager>().Play("eat");
            Destroy(gameObject);
			if (boater != null)
			{
				boater.GetComponent<boatbehavior>().catched = false;
				boater = null;
			}
			
        }
	    public void huntedByShip()
	    {
	    	Debug.Log("Hunted Oh shit");
			GameObject.Find("EcosystemManager").GetComponent<EcosystemManagement>().removeOrganism(this.gameObject ,1);
			
		}
		public void identifyboat(GameObject capturer)
		{
			boater = capturer;
			caught = true;
			//Debug.Log(boater);
			reeledin();
		}
		void reeledin()
		{
			if(boater != null)
			{
				//Insert code to tell whale to go to that specific boat and die.
				target = boater.GetComponent<MovementAIRigidbody>();
				//Disable colliders and all movement behavior.
				this.GetComponent<CircleCollider2D>().enabled = false;
				this.GetComponent<waterchecker>().enabled = false;
				this.GetComponent<SteeringBasics>().maxVelocity = 0.5f;

			    if(this.transform.position.y >= 1)
		        {
		        	dead = true;
		        }
	    	}	
		}

	    // Update is called once per frame
	    void FixedUpdate()
	    {
	        Vector3 accel = pursue.GetSteering(target);
	        this.GetComponent<FollowBehavior>().behavior = iwanttoeat; 

	        steeringBasics.Steer(accel);
	        steeringBasics.LookWhereYoureGoing();
	        if(dead == true)
	        {
	        	huntedByShip();
	        }
	    }
	}
}
