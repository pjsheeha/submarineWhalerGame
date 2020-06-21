using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class valuecalculator : MonoBehaviour
{
    public float value;
    private float movementvalue;
    public float sub = 90;

    void Start()
    {
        value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        movementvalue = this.transform.rotation.eulerAngles.z;
        value = Mathf.Abs(movementvalue-90 )/sub;
    }
}
