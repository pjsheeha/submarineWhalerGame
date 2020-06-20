using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cruisemove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void Start()
    {
        speed = 0.95f;
    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 position = this.transform.position;
        this.transform.position += -transform.right * speed * Time.deltaTime;
    }
}
