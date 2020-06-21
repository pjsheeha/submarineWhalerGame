using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityMovementAI;

public class boatbehavior : MonoBehaviour
{
    public Transform target;
    public Transform movingObject;
    GameObject player;
    public GameObject victimwhale;
    public float range = 15f;
    public float shootingrange = 10f;
    public float fireRate = 1f;
    public float speed = 2;
    public bool retreat;
    private float fireCountdown = 0f;
    private bool armed;
    public bool catched;
    private bool dead;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        retreat = false;
        catched = false;
        armed = false;
        player = GameObject.Find("player");
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
    	if (nearestEnemy != null && shortestDistance <= range && catched == false && dead == false)
    	{
    		target = nearestEnemy.transform;
            Debug.Log("I see a whale");

        //RETREAT IF DAMAGED OR NO WHALES
    	} else if (nearestEnemy == null || retreat == true){
            nearestEnemy = null;
            target = Reseter.transform;
            this.GetComponent<LineRenderer>().enabled = false;

            Debug.Log("No whales");

        }
        //CAUGHT WHALE IN SHOOTING RANGE.
        if(shortestDistance <= shootingrange && catched == false && dead == false)
        {
            armed = true;
            catched = true; //STOPS SEARCH
            victimwhale = nearestEnemy;
            this.GetComponent<LineRenderer>().enabled = true;
            target = victimwhale.transform;
            FindObjectOfType<AudioManager>().Play("harpoon");
            Debug.Log("I shoot a whale");
        } 
        if(catched == false || target == null || victimwhale == null)
        {
            armed = false;
            catched = false;
            victimwhale = null;
            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform;
                this.GetComponent<LineRenderer>().enabled = false;
                Debug.Log("I got whale find more");
            }
            else
            {
                target = player.transform;
            }
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

        if(dead == false)
        {
            movingObject.position = Vector3.MoveTowards(movingObject.position, target.position, Time.deltaTime*speed);
        }
        if(armed == true && dead == false)
        {
            boatattack();
        }
         Vector3 pos = transform.position;
         pos.z = -3;
         transform.position = pos;
         if(this.transform.position.y <= -60)
         {
            Destroy(gameObject);
         }
    }
    void OnTriggerEnter2D(Collider2D coll)
        {
            if(coll.gameObject.tag == "Player")
            {
                Debug.Log("Destroyed");
                catched = false;
                dead = true;
                this.GetComponent<CircleCollider2D>().enabled = false;
                this.GetComponent<BoxCollider2D>().enabled = false;
                this.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
                victimwhale.GetComponent<pursuewhale>().boatdestroyed();
                FindObjectOfType<AudioManager>().Play("splash");
                FindObjectOfType<AudioManager>().Play("great");
            }
        }
}
