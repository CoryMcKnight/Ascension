using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerS1 : MonoBehaviour {

    public bool isPlayingVO;

    //IBM Watson Intents
    [HideInInspector] public bool hello;
    [HideInInspector] public bool howAreYou;
    [HideInInspector] public bool bye;
    [HideInInspector] public bool niceToMeetYou;
    [HideInInspector] public bool intro;

    [HideInInspector] public bool backstoryWhere;
    [HideInInspector] public bool backstoryWho;

    [HideInInspector] public bool narrativeHouston;
    [HideInInspector] public bool narrativeTask;
    [HideInInspector] public bool narrativeWhen;
    [HideInInspector] public bool narrativeWhere;

    //Virtual characters
    public GameObject Maxwell;
    public GameObject Commander;
    public GameObject Engineer;
    public GameObject Doctor;
    public GameObject Biologist;

    public enum State //for allowing which character to respond when player is speaking
    {
        Maxwell,
        Commander,
        Engineer,
        Doctor,
        Biologist
    };
    public State conversationState;

    // Use this for initialization
    void Start () {
        isPlayingVO = false;
        conversationState = State.Maxwell;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            conversationState = State.Maxwell;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            conversationState = State.Commander;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            conversationState = State.Engineer;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            conversationState = State.Doctor;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            conversationState = State.Biologist;
        }

        if (conversationState == State.Maxwell)
        {
            //Maxwell VO lines and normal lighting
            Debug.Log("Talking to Maxwell");

            if (isPlayingVO == false && hello == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_MAXWELL_hello", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                hello = false;
            }
        }

        else if (conversationState == State.Commander)
        {
            //Commander VO lines and lighting
            Debug.Log("Talking to Commander");

            if (isPlayingVO == false && hello == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_hello", Commander, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                hello = false;
            }
        }

        else if (conversationState == State.Engineer)
        {
            //Engineer VO lines and lighting
            Debug.Log("Talking to Engineer");
        }

        else if (conversationState == State.Doctor)
        {
            //Doctor VO lines and lighting
            Debug.Log("Talking to Doctor");
        }

        else if (conversationState == State.Biologist)
        {
            //Biologist VO lines and lighting
            Debug.Log("Talking to Biologist");
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
}
