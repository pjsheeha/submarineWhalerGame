using UnityEngine;

namespace UnityMovementAI
{

    public class PursueUnit : MonoBehaviour
    {
        public MovementAIRigidbody target;
        public float range = 11f;
        public float iwanttoeat; 

        SteeringBasics steeringBasics;
        Pursue pursue;

        void Start()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            pursue = GetComponent<Pursue>();
            //CHANGE
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }
        //CHANGE
        void UpdateTarget()
        {

            GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("Target");
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
            if (nearestEnemy2 != null && shortestDistance <= range)
            {
                target = nearestEnemy2.GetComponent<MovementAIRigidbody>();

            } else if (nearestEnemy2 == null)
            {
                target = this.GetComponent<MovementAIRigidbody>();
            }
        }

        void FixedUpdate()
        {
            Vector3 accel = pursue.GetSteering(target);
            

            steeringBasics.Steer(accel);
            steeringBasics.LookWhereYoureGoing();
        }
    }
}
