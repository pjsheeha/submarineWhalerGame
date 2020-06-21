using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatbehavior : MonoBehaviour
{
    public Transform target;
    public Transform movingObject;

    public float range = 15f;
    public float shootingrange = 10f;
    public float fireRate = 1f;
    public float speed = 2;
    public bool retreat;
    private float fireCountdown = 0f;
    private bool armed;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        retreat = false;
    }

    void UpdateTarget()
    {

    	GameObject[] whales = GameObject.FindGameObjectsWithTag("whale");
        GameObject Reseter = GameObject.Find("RetreatPoint");
    	float shortestDistance = Mathf.Infinity;
    	GameObject nearestEnemy = null;

    	foreach (GameObject whale in whales)
    	{
    		float distanceToEnemy = Vector3.Distance(transform.position, whale.transform.position);
    		if(distanceToEnemy < shortestDistance && retreat != true)
    		{
    			shortestDistance = distanceToEnemy;
    			nearestEnemy = whale;
    		}
    	}

    	if (nearestEnemy != null && shortestDistance <= range)
    	{
            //Debug.Log("Chasing");
            //armed = true;
    		target = nearestEnemy.transform;
            if(shortestDistance <= shootingrange)
            {
                armed = true;
                this.GetComponent<LineRenderer>().enabled = true;
            } else {
                armed = false;
                this.GetComponent<LineRenderer>().enabled = false;
            }
    	} else if (nearestEnemy == null || retreat == true){
            //armed = false;
            nearestEnemy = null;
            target = Reseter.transform;

        }

    }
    void boatattack()
    {
        //Debug.Log("Attacking");
        Vector3 sp = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
        Vector3 ep = new Vector3(target.position.x,target.position.y,target.position.z);
        //sp = this.transform.position;
        //ep = target;
        this.GetComponent<LineRenderer>().SetVertexCount(2);
        this.GetComponent<LineRenderer>().SetPosition(0, sp);
        this.GetComponent<LineRenderer>().SetPosition(1, ep);
        //Insert code here that tells the attacked whale that this boat attacked it.
    }

    void Update()
    {
        if(target == null)
        	return;
        movingObject.position = Vector3.MoveTowards(movingObject.position, target.position, Time.deltaTime*speed);
        if(armed == true)
        {
            boatattack();
        }
    }
}
