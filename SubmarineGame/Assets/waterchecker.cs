using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterchecker : MonoBehaviour
{
    public bool isinwater;

    void Start()
    {
        //isinwater = true;
    }

    // Update is called once per frame
    void Update()
    {
    	if(this.transform.position.y >= -7)
    	{
    		this.GetComponent<Rigidbody2D>().gravityScale = 1;
        } else {
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
}
