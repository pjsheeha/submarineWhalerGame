using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class fadeText : MonoBehaviour
{
     byte alpha= 255;
    public int score = 0;
    public GameObject follower;
    TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (score > 0)
        {
            textMesh.text = "+ " + score;
            Vector3 m = Camera.main.WorldToScreenPoint(follower.transform.position);
            transform.position = new Vector3(m.x, m.y - (alpha / 10) + (75));
            textMesh.color = new Color32(255, 255, 255, alpha);
        }
        else
        {
            textMesh.text = "Not good framing";
            Vector3 m = Camera.main.WorldToScreenPoint(follower.transform.position);
            transform.position = new Vector3(m.x, m.y - (alpha / 10) + (75));
            textMesh.color = new Color32(255, 0, 0, alpha);
        }

    }
    private void FixedUpdate()
    {
        if (alpha > 0)
        {
            alpha -= 3;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
