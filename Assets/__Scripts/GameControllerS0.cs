using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerS0 : MonoBehaviour {

    public bool isPlayingVO;

    public GameObject Lights1;
    public GameObject Lights2;
    public GameObject Lights3;

    public GameObject Jets;
    public GameObject Title;
    public GameObject Shuttle;

    private bool hasPlayedIntroVO = false;
    private bool hasTurnedOnLights = false;
    private bool hasStartedShuttle = false;

	// Use this for initialization
	void Start () {
        isPlayingVO = false;
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

        else if (isPlayingVO == false && hasPlayedIntroVO == true && hasTurnedOnLights == false)
        {
            StartCoroutine(TurnOnLights());
        }

        else if (isPlayingVO == false && hasPlayedIntroVO == true && hasTurnedOnLights == true && hasStartedShuttle == false)
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
        yield return new WaitForSeconds(3);
        Lights2.SetActive(true);
        yield return new WaitForSeconds(2);
        Lights3.SetActive(true);
        hasTurnedOnLights = true;
    }

    IEnumerator StartShuttle()
    {
        Jets.SetActive(true);
        Title.SetActive(true);
        Shuttle.GetComponent<ShuttleUp>().enabled = true;
        yield return new WaitForSeconds(3);
        Jets.SetActive(false);
    }
}
