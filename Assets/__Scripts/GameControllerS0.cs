using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerS0 : MonoBehaviour {

    public bool isPlayingVO;
    public bool hasCountedDown;
    public bool hasLiftedOff;
    public GameObject speechRecognition;
    public GameObject Maxwell;

    public string levelName;

    public GameObject Lights1;
    public GameObject Lights2;
    public GameObject Lights3;

    public GameObject Jets;
    public GameObject Title;
    public GameObject Shuttle;

    private bool hasPlayedIntroVO = false;
    private bool hasPlayedMaxwellVO = false;
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
        hasLiftedOff = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (isPlayingVO == true) //turn off speech recognition when VO is playing
        {
            speechRecognition.GetComponent<SpeechRecognition>().enabled = false;
        }

        if (isPlayingVO == false) //turn on speech recognition when VO is not playing
        {
            speechRecognition.GetComponent<SpeechRecognition>().enabled = true;
        }

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

        else if (isPlayingVO == false && hasPlayedIntroVO == true && hasTurnedOnLights == true && hasCountedDown == true && hasPlayedMaxwellVO == false && hasStartedShuttle == false)
        {
            object myCookie = new object();
            isPlayingVO = true;
            AkSoundEngine.PostEvent("Play_PR_VO_001300_MAXWELL_when_youre_ready_to_01", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
            hasPlayedMaxwellVO = true;
        }

        else if (isPlayingVO == false && hasPlayedIntroVO == true && hasTurnedOnLights == true && hasCountedDown == true && hasPlayedMaxwellVO == true && hasLiftedOff == true && hasStartedShuttle == false)
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
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(levelName);

    }
}
