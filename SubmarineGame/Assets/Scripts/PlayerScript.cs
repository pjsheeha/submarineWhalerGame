﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerScript : MonoBehaviour
{
    public float maxSpeed = 40;
    public GameObject textPop;
    public GameObject canvas;
    protected float rotInput = 0;
    protected float thrustInput = 0;
    public float rotSpeed = 320;
    protected Rigidbody2D plane_body;
    public GameObject flash;
    float delt = 0;
    string upda ="";
    protected float curThrust = 0;
    private float maxThrust = 1;
    public float acceleration = 10;
    protected Vector2 vector_up = new Vector2(0, 1);
    private Vector2 level_mins;
    protected Vector2 vector_down = new Vector2(0, -1);
    public bool camDown = false;
    public float level_width = 1000;
    public float level_height = 500;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI gasText;
    public TextMeshProUGUI updateM;

    public EcosystemManagement eco;
    public Water waterManager; 
    public LayerMask m_LayerMask;
    public GameObject exhaust;
    public float money = 0;
    public float gas = 90;

    float unit = 1;
    private void setupLevelBounds()
    {
        level_mins.x = -1 * level_width / 2;
        level_mins.y = -1 * level_height / 2;
        Vector3 p1 = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 p2 = Camera.main.ScreenToWorldPoint(Vector3.right);
        unit = Vector3.Distance(p1, p2);

    }
    // Start is called before the first frame update
    void Start()
    {
        setupLevelBounds();

        plane_body = GetComponent<Rigidbody2D>();

    }

    protected void handleFlash()
    {

        if (camDown)
        {

            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(Screen.width * unit, Screen.height * unit), 0, m_LayerMask);

            int i = 0;
            int j = 0;
            Collider2D[] colliderClosest = hitColliders;
            Collider2D moneyShot = new Collider2D();
            Collider2D okShot = new Collider2D();
            Collider2D mehShot = new Collider2D();
            Collider2D badShot = new Collider2D();
            float moneyShotD = 100;
            float okShotD = 100;
            float mehShotD = 100;
            float badShotD = 100;
            while (j < hitColliders.Length)
            {
                Vector3 vector1 = -transform.up;
                Vector3 vector2 = (transform.position - hitColliders[j].transform.position).normalized;
                float angle = Mathf.Acos(Vector3.Dot(vector2, vector1)) * Mathf.Rad2Deg;
                float dist = Vector3.Distance(transform.position, hitColliders[j].transform.position);
                if (angle < 25)
                {
                    if (dist < moneyShotD)
                    {
                        moneyShot = hitColliders[j];
                    }

                }
                else if (angle < 45)
                {
                    if (dist < okShotD)
                    {
                        okShot = hitColliders[j];
                    }
                }
                else if (angle < 90)
                {
                    if (dist < mehShotD)
                    {
                        mehShot = hitColliders[j];
                    }
                }
                else if (angle < 180)
                {
                    if (dist < badShotD)
                    {
                        badShot = hitColliders[j];
                    }
                }
                j++;

            }

            while (i < colliderClosest.Length)
            {
                if (colliderClosest[i] == moneyShot)
                {

                    print("W");
                    
                    GameObject myT = Instantiate(textPop, Camera.main.WorldToScreenPoint(colliderClosest[i].transform.position), Quaternion.identity, canvas.transform);
                    myT.GetComponent<fadeText>().follower = colliderClosest[i].gameObject;
                    if (moneyShot.GetComponent<valuecalculator>())
                    {
                        myT.GetComponent<fadeText>().score = Mathf.RoundToInt(moneyShot.GetComponent<valuecalculator>().value + ((moneyShot.GetComponent<valuecalculator>().value / 100) * 10) + (-transform.position.y));
                    }
                        money += myT.GetComponent<fadeText>().score;


                }
                if (colliderClosest[i] == okShot)
                {

                    GameObject myT = Instantiate(textPop, Camera.main.WorldToScreenPoint(colliderClosest[i].transform.position), Quaternion.identity, canvas.transform);
                    myT.GetComponent<fadeText>().follower = colliderClosest[i].gameObject;
                    if (okShot.GetComponent<valuecalculator>())
                    {
                        myT.GetComponent<fadeText>().score = Mathf.RoundToInt(okShot.GetComponent<valuecalculator>().value + ((okShot.GetComponent<valuecalculator>().value / 100) * 7)+(-transform.position.y));
                    }
                        money += myT.GetComponent<fadeText>().score;

                }
                if (colliderClosest[i] == mehShot)
                {

                    GameObject myT = Instantiate(textPop, Camera.main.WorldToScreenPoint(colliderClosest[i].transform.position), Quaternion.identity, canvas.transform);
                    myT.GetComponent<fadeText>().follower = colliderClosest[i].gameObject;
                    if (mehShot.GetComponent<valuecalculator>())
                    {
                        myT.GetComponent<fadeText>().score = Mathf.RoundToInt(mehShot.GetComponent<valuecalculator>().value + ((mehShot.GetComponent<valuecalculator>().value / 100) * 4)+(-transform.position.y));
                    }
                        money += myT.GetComponent<fadeText>().score;

                }
                if (colliderClosest[i] == badShot)
                {

                    GameObject myT = Instantiate(textPop, Camera.main.WorldToScreenPoint(colliderClosest[i].transform.position), Quaternion.identity, canvas.transform);
                    myT.GetComponent<fadeText>().follower = colliderClosest[i].gameObject;
                    myT.GetComponent<fadeText>().score = 0;


                }

                i++;

            }

            Instantiate(flash, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, canvas.transform);
            Debug.Log("d"); 

        }
    }

    // Update is called once per frame
    void Update()
    {
        rotInput = Input.GetAxisRaw("Horizontal");

        thrustInput = Input.GetAxisRaw("Vertical");
        camDown = Input.GetButtonDown("Fire1");
        moneyText.text = "$"+money.ToString();
        gasText.text =   Mathf.RoundToInt(gas).ToString();
        updateM.text = upda;
        if (upda != "")
        {
            if(delt < 2)
            {
                delt += Time.deltaTime;
            }
            else
            {
                delt = 0;
                upda = "";
            }
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "store")
        {
            print("Buy!");
            if (gas < 90 )
            {
                int change = (90 - gas) > 60 ? 60 : 90 - Mathf.RoundToInt(gas);
                print(eco.whalePopulation);
                int pay = Mathf.RoundToInt(9*6+100*(1 - eco.whalePopulation / eco.maxWhalePopulation));
                if (money - pay > 0)
                {
                    gas += change;

                    upda = "Added " + change + " gallons, paid $" + pay;
                    money -= pay;
                    delt = 0;

                }
                else
                {
                    upda = "Need more money!";
                    delt = 0;

                }


            }
            else
            {
                upda = "Full tank!";
                delt = 0;
            }
        }
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
        if (gas > 0)
        {
            handleMotion();
        }
        else
        {
            eco.checkToEndDay = true;
        }
        handleFlash();
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
                waterManager.GetComponent<Water>().Splash2(exhaust.transform.position, plane_body.velocity.magnitude);
                if (gas > 0)
                {
                    gas -= Time.fixedDeltaTime;
                }
            }
            else
            {

                curThrust = 0;
                plane_body.gravityScale = 3f;
                if (gas > 0)
                {
                    gas -= Time.fixedDeltaTime;
                }


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
        else
        {

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
