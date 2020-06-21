using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMovementAI;

public class boatbehavior : MonoBehaviour
{
    public Transform target;
    public Transform movingObject;

    public GameObject victimwhale;
    public float range = 15f;
    public float shootingrange = 10f;
    public float fireRate = 1f;
    public float speed = 2;
    public bool retreat;
    private float fireCountdown = 0f;
    private bool armed;
    public bool catched;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        retreat = false;
        catched = false;
        armed = false;
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
        //SEARCH WHALES. STOP SEEKING WHALES IF WHALE IS CAUGHT.
    	if (nearestEnemy != null && shortestDistance <= range && catched == false)
    	{
    		target = nearestEnemy.transform;

        //RETREAT IF DAMAGED OR NO WHALES
    	} else if (nearestEnemy == null || retreat == true){
            nearestEnemy = null;
            target = Reseter.transform;
        }
        //CAUGHT WHALE IN SHOOTING RANGE.
        if(shortestDistance <= shootingrange && catched == false)
        {
            armed = true;
            catched = true; //STOPS SEARCH
            victimwhale = nearestEnemy;
            this.GetComponent<LineRenderer>().enabled = true;
            target = victimwhale.transform;
        } 
        if(catched == false || target == null || victimwhale == null)
        {
            armed = false;
            catched = false;
            victimwhale = null;
            target = nearestEnemy.transform;
            this.GetComponent<LineRenderer>().enabled = false;
        }

    }
    void boatattack()
    {
        //HARPOON
        Vector3 sp = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
        Vector3 ep = new Vector3(target.position.x,target.position.y,target.position.z);
        this.GetComponent<LineRenderer>().SetVertexCount(2);
        this.GetComponent<LineRenderer>().SetPosition(0, sp);
        this.GetComponent<LineRenderer>().SetPosition(1, ep);

        //Insert code here that tells the attacked whale that this boat attacked it.
        victimwhale.GetComponent<pursuewhale>().identifyboat(this.gameObject);
        //Debug.Log(victimwhale);
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
