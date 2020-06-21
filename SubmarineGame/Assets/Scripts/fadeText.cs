using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class fadeText : MonoBehaviour
{
     byte alpha= 255;
    public int score = 0;
    public GameObject follower;
    public bool text = true;
    TextMeshProUGUI textMesh;
    Image im;
    // Start is called before the first frame update
    void Start()
    {
        if (text == true)
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }
        else
        {
            im = GetComponent<Image>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (score > 0)
        {
            if (text == true)
            {
                if (score != -1)
                {
                    textMesh.text = "+ " + score;
                    follower.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .3f);
                }
                else
                {
                    textMesh.text = "Already photographed.";

                }
                Vector3 m = Camera.main.WorldToScreenPoint(follower.transform.position);
                transform.position = new Vector3(m.x, m.y - (alpha / 10) + (75));
                textMesh.color = new Color32(255, 255, 255, alpha);
            }
            else
            {
                im.color = new Color32(255, 255, 255, alpha);
            }
        }
        else
        {
            if (text == true)
            {
                if (score !=-1)
                {
                    textMesh.text = "Not good framing";
                    follower.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .3f);

                }
                else
                {
                    textMesh.text = "Already photographed.";

                }
                Vector3 m = Camera.main.WorldToScreenPoint(follower.transform.position);
                transform.position = new Vector3(m.x, m.y - (alpha / 10) + (75));
                textMesh.color = new Color32(255, 0, 0, alpha);
            }
            else
            {
                im.color = new Color32(255, 255, 255, alpha);
            }
        }

    }
    private void FixedUpdate()
    {
        if (alpha > 0)
        {
            
            if (text == true)
            {
                alpha -= 3;

            }
            else
            {
                alpha -= 5;
            }
        }
        else
        {
            print("WTF");

            Destroy(this.gameObject);
        }
    }
}
