using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightBehavior : MonoBehaviour {
    public Light lt;
    public bool moodLighting = false;
    public bool isChanging = false;


    // Use this for initialization
    void Start()
    {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isChanging)
        {
            if(lt.intensity <= 0)
            {
                lt.intensity = 0;
                isChanging = false;
            }
            else
            {
                lt.intensity -= 3 * Time.deltaTime;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            //Toggles it off
            if (moodLighting)
            {
                lt.intensity = 1.0f;
                moodLighting = false;
            }
            //Toggles it on
            else
            {
                isChanging = true;
                moodLighting = true;
            }
        }
    }
}
