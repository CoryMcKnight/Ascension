using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour {
    int threshold;
    public Light lt;
    public bool moodLighting = false;
    public bool lightingUp = true;

    // Use this for initialization
    void Start()
    {
        threshold = Random.Range(10, 14);
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        //Toggle mood lighting on / off
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Toggles it off
            if (moodLighting)
            {
                lt.intensity = 0.0f;
                moodLighting = false;
            }
            //Toggles it on
            else
            {
                moodLighting = true;
            }
        }

        if (moodLighting)
        {
            //Met the threshold!
            if((lightingUp && (lt.intensity >= threshold)) || (!lightingUp && (lt.intensity <= threshold)))
            {
                threshold = Random.Range(3, 12  );
                if (lt.intensity < threshold) lightingUp = true;
                else lightingUp = false;
            }
            //Trying to get to the threshhold
            else if (lightingUp)
            {
                lt.intensity += 3 * Time.deltaTime;
            }
            else
            {
                lt.intensity -= 3 * Time.deltaTime;
            }
        }
    }
}

/*
    // Update is called once per frame
    void Update()
    {
        if (isChanging)
        {
            //Transformation is done, reset values
            if (lt.intensity >= 12)
            {
                lt.intensity = 12;
                changeSpeed = 1;
                isChanging = false;
            }
            else //Transform here
            {
                lt.intensity += changeSpeed * Time.deltaTime;
                changeSpeed += 0.5f;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            //Toggles it off
            if (moodLighting)
            {
                lt.intensity = 0.0f;
                moodLighting = false;
            }
            //Toggles it on
            else
            {
                isChanging = true;
                moodLighting = true;
            }
        }
        else if(moodLighting)
        {
            
        }
    }
}
*/
