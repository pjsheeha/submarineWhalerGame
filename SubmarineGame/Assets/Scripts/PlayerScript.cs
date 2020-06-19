using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float maxSpeed = 40;

    protected float rotInput = 0;
    protected float thrustInput = 0;
    public float rotSpeed = 320;
    protected Rigidbody2D plane_body;
    protected float curThrust = 0;
    private float maxThrust = 1;
    public float acceleration = 10;
    protected Vector2 vector_up = new Vector2(0, 1);
    private Vector2 level_mins;
    protected Vector2 vector_down = new Vector2(0, -1);

    public float level_width = 1000;
    public float level_height = 500;

    private void setupLevelBounds()
    {
        level_mins.x = -1 * level_width / 2;
        level_mins.y = -1 * level_height / 2;

    }
    // Start is called before the first frame update
    void Start()
    {
        setupLevelBounds();

        plane_body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        rotInput = Input.GetAxisRaw("Horizontal");

        thrustInput = Input.GetAxisRaw("Vertical");


    }
    public float getWaterLevel()
    {
        return level_mins.y + getWaterLevelHeight();
    }

    public float getWaterLevelHeight()
    {
        return 251;// level_height * water_level;
    }
    private void FixedUpdate()
    {
        handleMotion();
    }

    public Vector2 getAimDir()
    {
        return plane_body.transform.up;
    }
    protected void handleMotion()
    {
        if (rotInput != 0)
        {
            var new_rotation = plane_body.rotation - rotSpeed * GetRotPower() * Time.fixedDeltaTime * rotInput;

            new_rotation = normalizeAngle(new_rotation);
            plane_body.MoveRotation(new_rotation);
        }
        //plane_body.transform.GetChild(0).transform.localEulerAngles = new Vector3(0, plane_body.rotation * -1, 0);
        if (IsAccelerating())
        {
            if (plane_body.transform.position.y < this.getWaterLevel())
            {
                curThrust = Mathf.Min(curThrust + 2.3f * Time.fixedDeltaTime, maxThrust);
                plane_body.gravityScale = 0;
                plane_body.AddForce(getAimDir() * acceleration * curThrust, ForceMode2D.Impulse);
            }
            else
            {
                print(curThrust);

                curThrust = 0;
                plane_body.gravityScale = 3f;

            }
        }
        else
        {

            curThrust = 0;
            plane_body.gravityScale = 3f;
        }
        if (plane_body.transform.position.y < this.getWaterLevel())
        {

            plane_body.AddForce(vector_up * 30, ForceMode2D.Force);
            var clamped_vel = plane_body.velocity.magnitude;

            clamped_vel = Mathf.Clamp(clamped_vel, 0, maxSpeed);

            plane_body.velocity = getVelDir() * clamped_vel;
        }
        else{
            print("Wah wah wooie");

            //plane_body.gravityScale = plane_body.gravityScale+10;

        }



    }
    protected virtual float GetRotPower()
    {
        if (IsAccelerating())
            return 0.3f;
        else
            return 1;
    }

    public bool IsAccelerating()
    {
        return thrustInput > 0;
    }
    protected float normalizeAngle(float ang)
    {
        ang = (ang + 180) % 360;
        if (ang < 0)
            ang += 360;
        return ang - 180;
    }
    public Vector2 getVelDir()
    {
        return plane_body.velocity.normalized;
    }

}
