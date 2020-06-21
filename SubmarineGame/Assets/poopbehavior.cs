using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poopbehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void eaten()
        {
            GameObject.Find("EcosystemManager").GetComponent<EcosystemManagement>().removeOrganism(this.gameObject ,1);
            
        }
    public void Die()
        {
            Debug.Log("dead");
            Destroy(gameObject);
        }
}
