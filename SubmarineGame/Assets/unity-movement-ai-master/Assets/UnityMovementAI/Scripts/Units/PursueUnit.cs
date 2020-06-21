using UnityEngine;

namespace UnityMovementAI
{

    public class PursueUnit : MonoBehaviour
    {
        public MovementAIRigidbody target;
        public float range = 11f;
        public float iwanttoeat; 
        //0 = idle. switch to wander. deactivate pursue.
        //1 = eat. eat krill. activate pursue. deactivate wander. 
        //2 = hotspot. track hotstpot
        public string whatsitsprey;
        public string whatsitshotspot;
        public bool isinwater;
        public float minspeed;
        public float maxspeed;
        int victims = 0; 

        SteeringBasics steeringBasics;
        Pursue pursue;

        void Start()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            pursue = GetComponent<Pursue>();
            //CHANGE
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
            this.GetComponent<SteeringBasics>().maxVelocity = Random.Range(minspeed, maxspeed);
        }
        //CHANGE
        void UpdateTarget()
        {

            GameObject[] enemies2 = GameObject.FindGameObjectsWithTag(whatsitsprey);
            GameObject[] hotspots = GameObject.FindGameObjectsWithTag(whatsitshotspot);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy2 = null;
            GameObject nearesthotspot = null;

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
            foreach (GameObject coral in hotspots)
            {
                float distanceToCoral = Vector3.Distance(transform.position, coral.transform.position);
                if(distanceToCoral < shortestDistance)
                {
                    shortestDistance = distanceToCoral;
                    nearesthotspot = coral;
                }
            }
            //SEEK TARGETS
            if (nearestEnemy2 != null && shortestDistance <= range && iwanttoeat == 1 && victims > 0)
            {
                //EAT
                target = nearestEnemy2.GetComponent<MovementAIRigidbody>();
                //Activate Pursue
                this.GetComponent<PursueUnit>().enabled = true;
                //Deactivate Wander
                this.GetComponent<Wander2>().enabled = false;
                this.GetComponent<Wander2Unit>().enabled = false;

            } else if (nearestEnemy2 == null || iwanttoeat == 2)
            {
                //HOTSPOT or NO FOOD LEFT
                target = nearesthotspot.GetComponent<MovementAIRigidbody>();
                //Activate Pursue
                this.GetComponent<PursueUnit>().enabled = true;
                //Deactivate Wander
                this.GetComponent<Wander2>().enabled = false;
                this.GetComponent<Wander2Unit>().enabled = false;

            } else if (iwanttoeat == 0)
            {
                //Activate Wander
                this.GetComponent<Wander2>().enabled = true;
                this.GetComponent<Wander2Unit>().enabled = true;
                //Deactivate Pursue
                this.GetComponent<PursueUnit>().enabled = false;
            }
            isinwater = false;
        }
        public void addTargetHomework()
        {
            Debug.Log("Got assigned homework #goals");
            victims++;
            iwanttoeat = 1;
        }
        public void Die()
        {
            Debug.Log("dead");
            Destroy(gameObject);
        }
        void FixedUpdate()
        {
                Vector3 accel = pursue.GetSteering(target);
            
                steeringBasics.Steer(accel);
                steeringBasics.LookWhereYoureGoing();

                if(victims > 0)
                {
                    iwanttoeat = 1;
                } else {
                    iwanttoeat = 0;
                }
        }
        public void eaten()
        {
            GameObject.Find("EcosystemManager").GetComponent<EcosystemManagement>().removeOrganism(this.gameObject ,1);
            
        }
        void OnTriggerEnter2D(Collider2D coll)
        {
            if(coll.gameObject.tag == whatsitsprey && victims > 0)
            {
                coll.GetComponent<PursueUnit>().eaten();
            }
        }
    }
}
