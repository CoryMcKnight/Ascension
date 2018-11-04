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
        conversationState = State.Commander;
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
            //Debug.Log("Talking to Maxwell");

            if (isPlayingVO == false && hello == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_MAXWELL_hello", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                hello = false;
            }
            else if (isPlayingVO == false && howAreYou == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_MAXWELL_howareyou", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                howAreYou = false;
            }
            else if (isPlayingVO == false && niceToMeetYou == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_MAXWELL_nicetomeetyou", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                niceToMeetYou = false;
            }
            else if (isPlayingVO == false && bye == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_MAXWELL_bye", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                bye = false;
            }
            else if (isPlayingVO == false && intro == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_MAXWELL_intro", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                intro = false;
            }
        }

        else if (conversationState == State.Commander)
        {
            //Commander VO lines and lighting
            //Debug.Log("Talking to Commander");

            if (isPlayingVO == false && hello == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_hello", Commander, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                hello = false;
            }
            else if (isPlayingVO == false && howAreYou == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_howareyou", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                howAreYou = false;
            }
            else if (isPlayingVO == false && niceToMeetYou == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_nicetomeetyou", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                niceToMeetYou = false;
            }
            else if (isPlayingVO == false && bye == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_bye", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                bye = false;
            }
            else if (isPlayingVO == false && intro == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_intro", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                intro = false;
            }

            // Backstory Dialogue
            else if (isPlayingVO == false && backstoryWhere == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_backstory_where", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                backstoryWhere = false;
            }
            else if (isPlayingVO == false && backstoryWho == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_backstory_who", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                backstoryWho = false;
            }

            // Narrative Dialogue
            else if (isPlayingVO == false && narrativeHouston == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_narrative_houston", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                narrativeHouston = false;
            }
            else if (isPlayingVO == false && narrativeTask == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_narrative_task", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                narrativeTask = false;
            }
            else if (isPlayingVO == false && narrativeWhen == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_narrative_when", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                narrativeWhen = false;
            }
            else if (isPlayingVO == false && narrativeWhere == true)
            {
                object myCookie = new object();
                isPlayingVO = true;
                AkSoundEngine.PostEvent("Play_VO_COMMANDER_narrative_where", Maxwell, (uint)AkCallbackType.AK_EndOfEvent, CheckWhenFinished, myCookie);
                narrativeWhere = false;
            }
        }

        else if (conversationState == State.Engineer)
        {
            //Engineer VO lines and lighting
            //Debug.Log("Talking to Engineer");
        }

        else if (conversationState == State.Doctor)
        {
            //Doctor VO lines and lighting
            //Debug.Log("Talking to Doctor");
        }

        else if (conversationState == State.Biologist)
        {
            //Biologist VO lines and lighting
            //Debug.Log("Talking to Biologist");
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
