using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatbehavior : MonoBehaviour
{
    public Transform target;
    public Transform movingObject;

    public float range = 11f;
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
            armed = true;
    		target = nearestEnemy.transform;
    	} else if (nearestEnemy == null || retreat == true){
            armed = false;
            nearestEnemy = null;
            target = Reseter.transform;

        }

    }

    void Update()
    {
        if(target == null)
        	return;
        movingObject.position = Vector3.MoveTowards(movingObject.position, target.position, Time.deltaTime*speed);
    }
}
