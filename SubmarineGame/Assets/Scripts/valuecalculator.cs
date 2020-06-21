using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class valuecalculator : MonoBehaviour
{
    public float value;
    public float fixedvalue;
    private float movementvalue;
    public bool doesflips;

    void Start()
    {
        value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(doesflips == true)
        {
            movementvalue = this.transform.rotation.eulerAngles.z;
            value = Mathf.Abs(movementvalue - fixedvalue);
        } else {
            value = fixedvalue;
        }
    }
}
