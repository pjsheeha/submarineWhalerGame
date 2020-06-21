using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cruisemove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int mult = 1;
    void Start()
    {
        speed = 0.95f;
    }

    // Update is called once per frame
    void Update()
    {

    	Vector3 position = this.transform.position;
        this.transform.position += -transform.right * speed * mult * Time.deltaTime;

        if (transform.position.x < -60)
        {
            mult = -1;
        }
        else if (transform.position.x > 60)
        {
            mult = 1;


        }
    }
}
