using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MS : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Vector3 startPos;
    public string state = "passive";
    Light sc;
    public Material Material1;
    public int startleCounter = 0;
    int weakenedCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        sc = gameObject.AddComponent<Light>();
        sc.type = LightType.Point;
        sc.intensity = 2f;
        sc.enabled = false;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < -50)
        {
            transform.position = startPos;
        }

        if (state == "weakened") //mouse has been weakened for a bit
        {
            sc.color = Color.red;
            weakenedCounter++;
            if (weakenedCounter >= 600)
            {
                state = "passive";
                weakenedCounter = 0;
                sc.color = Color.white;
                sc.enabled = false;
            }
        }
        if (state == "startled") //mouse has been startled
        {
            Debug.Log("YEEEOUCH!");
            if (startleCounter == 0)
            {
                Debug.Log("Startle counter begun!");
                m_Rigidbody.AddForce(new Vector3(0f, 150f, 0f), ForceMode.Force);
                sc.enabled = true;
            }
            startleCounter += 1;
            if (startleCounter >= 300)
            {
                state = "weakened";
                startleCounter = 0;
            }
            //Debug.Log(Material1);
            GameObject skin = gameObject.transform.Find("Maus").gameObject;
            skin.GetComponent<SkinnedMeshRenderer>().material = Material1;
        }
            
    }
}
