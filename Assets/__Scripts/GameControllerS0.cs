using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerS0 : MonoBehaviour {

    public bool isPlayingVO;
    public bool hasCountedDown;

    public GameObject Lights1;
    public GameObject Lights2;
    public GameObject Lights3;

    public GameObject Jets;
    public GameObject Title;
    public GameObject Shuttle;

    private bool hasPlayedIntroVO = false;
    private bool hasTurnedOnLights = false;
    private bool hasStartedShuttle = false;
    private bool isPlayingLights1 = false;
    private bool isPlayingLights2 = false;
    private bool isPlayingLights3 = false;
    private bool isPlayingIgnition = false;
    private bool isPlayingLiftoff = false;

    // Use this for initialization
    void Start () {
        isPlayingVO = false;
        hasCountedDown = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isPlayingVO == false && hasPlayedIntroVO == false)
        {
            object myCookie = new object();
            isPlayingVO = true;
            AkSoundEngine.PostEvent("Play_VO_JPLENGINEER_S0_intro", gameObject, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
            hasPlayedIntroVO = true;
        }

        else if (isPlayingVO == false && hasPlayedIntroVO == true && hasTurnedOnLights == false && hasCountedDown == true)
        {
            StartCoroutine(TurnOnLights());
        }

        else if (isPlayingVO == false && hasPlayedIntroVO == true && hasTurnedOnLights == true && hasCountedDown == true && hasStartedShuttle == false)
        {
            StartCoroutine(StartShuttle());
        }
    }

    void CheckWhenFinished(object in_cookie, AkCallbackType in_type, object in_info)
    {

        if (in_type == AkCallbackType.AK_EndOfEvent)
        {
            AkEventCallbackInfo info = (AkEventCallbackInfo)in_info; //Then do stuff.
            isPlayingVO = false;
        }
    }

    IEnumerator TurnOnLights()
    {
        Lights1.SetActive(true);
        if (isPlayingLights1 == false)
        {
            AkSoundEngine.PostEvent("Play_Lights_Rafter_On", Lights1);
            isPlayingLights1 = true;
        }
        yield return new WaitForSeconds(3);
        Lights2.SetActive(true);
        if (isPlayingLights2 == false)
        {
            AkSoundEngine.PostEvent("Play_Lights_Rafter_On", Lights2);
            isPlayingLights2 = true;
        }
        yield return new WaitForSeconds(2);
        Lights3.SetActive(true);
        if (isPlayingLights3 == false)
        {
            AkSoundEngine.PostEvent("Play_Lights_Rafter_On", Lights3);
            isPlayingLights3 = true;
        }
        hasTurnedOnLights = true;
        if (isPlayingIgnition == false)
        {
            AkSoundEngine.PostEvent("Play_Shuttle_Engine_Ignition_01", Shuttle);
            isPlayingIgnition = true;
        }
    }

    IEnumerator StartShuttle()
    {
        Jets.SetActive(true);
        Title.SetActive(true);
        Shuttle.GetComponent<ShuttleUp>().enabled = true;
        if (isPlayingLiftoff == false)
        {
            AkSoundEngine.PostEvent("Play_Shuttle_Liftoff_01", Shuttle);
            isPlayingLiftoff = true;
        }
        yield return new WaitForSeconds(3);
        Jets.SetActive(false);
    }
}
