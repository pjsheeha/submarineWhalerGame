using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class valuecalculator : MonoBehaviour
{
    public float value;
    public float fixedvalue;
    public float movementvalue;
    public float sub = 90;
    public bool doesflips;
    public bool snapped = false;


    void Start()
    {
        value = 0;
    }

    // Update is called once per frame
    void Update()
    {

        movementvalue = Mathf.Abs(this.transform.rotation.z-90)/sub;
        if(doesflips == true)
        {
            movementvalue = this.transform.rotation.eulerAngles.z;
            value = Mathf.Abs(movementvalue - fixedvalue)/sub;
        } else {
            value = fixedvalue/sub;
        }
    }
}
