/**
* Copyright 2015 IBM Corp. All Rights Reserved.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.DataTypes;
using System.Collections.Generic;
using UnityEngine.UI;
using IBM.Watson.DeveloperCloud.Services.Conversation.v1;
using FullSerializer;
using IBM.Watson.DeveloperCloud.Connection;
using UnityEngine.SceneManagement;

public class SpeechRecognitionS1 : MonoBehaviour
{
    [SerializeField]
    private fsSerializer _serializer = new fsSerializer();
    private SpeechToText _speechToText;
    private Conversation _conversation;

    private string stt_username = "0e10c402-b934-453f-9b72-d6c50f563ecf";
    private string stt_password = "kib31vffQPR6";
    private string stt_apiKey = "OW3xqNu9Q6lkNfaKlDbOnkiuNN6m81RAI_C11XNx8YDt";
    // Change stt_url if different from below
    private string stt_url = "https://stream.watsonplatform.net/speech-to-text/api";

    private string convo_username = "1ea45dae-87c5-4f95-823e-3c5126612c52";
    private string convo_password = "Y1Qxm4XMOWVd";
    // Change convo_url if different from below
    private string convo_url = "https://gateway.watsonplatform.net/assistant/api";
    // Change  _conversationVersionDate if different from below
    private string _conversationVersionDate = "2018-09-13";
    private string convo_workspaceId = "5794a517-db45-4cb9-8834-e74b86f76f7e";

    private int _recordingRoutine = 0;
    private string _microphoneID = null;
    private AudioClip _recording = null;
    private int _recordingBufferSize = 1;
    private int _recordingHZ = 22050;

    GameControllerS1 gameControllerS1;


    void Start()
    {
        LogSystem.InstallDefaultReactors();

        //  Create credential and instantiate service
        //Credentials stt_credentials = new Credentials(stt_username, stt_password, stt_url); //crystal creds
        Credentials stt_credentials = new Credentials(stt_apiKey, stt_url); //mari creds
        Credentials convo_credentials = new Credentials(convo_username, convo_password, convo_url);

        _speechToText = new SpeechToText(stt_credentials);
        _conversation = new Conversation(convo_credentials);
        _conversation.VersionDate = _conversationVersionDate;
        Active = true;

        StartRecording();

        gameControllerS1 = GameObject.Find("GameController").GetComponent<GameControllerS1>();

    }

    public bool Active
    {
        get { return _speechToText.IsListening; }
        set
        {
            if (value && !_speechToText.IsListening)
            {
                _speechToText.DetectSilence = true;
                _speechToText.EnableWordConfidence = true;
                _speechToText.EnableTimestamps = true;
                _speechToText.SilenceThreshold = 0.01f;
                _speechToText.MaxAlternatives = 0;
                _speechToText.EnableInterimResults = true;
                _speechToText.OnError = OnError;
                _speechToText.InactivityTimeout = -1;
                _speechToText.ProfanityFilter = false;
                _speechToText.SmartFormatting = true;
                _speechToText.SpeakerLabels = false;
                _speechToText.WordAlternativesThreshold = null;
                _speechToText.StartListening(OnRecognize, OnRecognizeSpeaker);
            }
            else if (!value && _speechToText.IsListening)
            {
                _speechToText.StopListening();
            }
        }
    }

    private void StartRecording()
    {
        if (_recordingRoutine == 0)
        {
            UnityObjectUtil.StartDestroyQueue();
            _recordingRoutine = Runnable.Run(RecordingHandler());
        }
    }

    private void StopRecording()
    {
        if (_recordingRoutine != 0)
        {
            Microphone.End(_microphoneID);
            Runnable.Stop(_recordingRoutine);
            _recordingRoutine = 0;
        }
    }

    private void OnError(string error)
    {
        Active = false;

        Log.Debug("ExampleStreaming.OnError()", "Error! {0}", error);
    }

    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleConversation.OnFail()", "Error received: {0}", error.ToString());
    }

    private IEnumerator RecordingHandler()
    {
        Log.Debug("ExampleStreaming.RecordingHandler()", "devices: {0}", Microphone.devices);
        _recording = Microphone.Start(_microphoneID, true, _recordingBufferSize, _recordingHZ);
        yield return null;      // let _recordingRoutine get set..

        if (_recording == null)
        {
            StopRecording();
            yield break;
        }

        bool bFirstBlock = true;
        int midPoint = _recording.samples / 2;
        float[] samples = null;

        while (_recordingRoutine != 0 && _recording != null)
        {
            int writePos = Microphone.GetPosition(_microphoneID);
            if (writePos > _recording.samples || !Microphone.IsRecording(_microphoneID))
            {
                Log.Error("ExampleStreaming.RecordingHandler()", "Microphone disconnected.");

                StopRecording();
                yield break;
            }

            if ((bFirstBlock && writePos >= midPoint)
              || (!bFirstBlock && writePos < midPoint))
            {
                // front block is recorded, make a RecordClip and pass it onto our callback.
                samples = new float[midPoint];
                _recording.GetData(samples, bFirstBlock ? 0 : midPoint);

                AudioData record = new AudioData();
                record.MaxLevel = Mathf.Max(Mathf.Abs(Mathf.Min(samples)), Mathf.Max(samples));
                record.Clip = AudioClip.Create("Recording", midPoint, _recording.channels, _recordingHZ, false);
                record.Clip.SetData(samples, 0);

                _speechToText.OnListen(record);

                bFirstBlock = !bFirstBlock;
            }
            else
            {
                // calculate the number of samples remaining until we ready for a block of audio,
                // and wait that amount of time it will take to record.
                int remaining = bFirstBlock ? (midPoint - writePos) : (_recording.samples - writePos);
                float timeRemaining = (float)remaining / (float)_recordingHZ;

                yield return new WaitForSeconds(timeRemaining);
            }

        }

        yield break;
    }

    private void OnRecognize(SpeechRecognitionEvent result)
    {
        if (result != null && result.results.Length > 0)
        {
            foreach (var res in result.results)
            {
                foreach (var alt in res.alternatives)
                {
                    if (res.final && alt.confidence > 0)
                    {
                        string text = alt.transcript;
                        Debug.Log("Result: " + text + " Confidence: " + alt.confidence);
                        _conversation.Message(OnMessage, OnFail, convo_workspaceId, text);
                    }
                }

                if (res.keywords_result != null && res.keywords_result.keyword != null)
                {
                    foreach (var keyword in res.keywords_result.keyword)
                    {
                        Log.Debug("ExampleStreaming.OnRecognize()", "keyword: {0}, confidence: {1}, start time: {2}, end time: {3}", keyword.normalized_text, keyword.confidence, keyword.start_time, keyword.end_time);
                    }
                }

                if (res.word_alternatives != null)
                {
                    foreach (var wordAlternative in res.word_alternatives)
                    {
                        Log.Debug("ExampleStreaming.OnRecognize()", "Word alternatives found. Start time: {0} | EndTime: {1}", wordAlternative.start_time, wordAlternative.end_time);
                        foreach (var alternative in wordAlternative.alternatives)
                            Log.Debug("ExampleStreaming.OnRecognize()", "\t word: {0} | confidence: {1}", alternative.word, alternative.confidence);
                    }
                }
            }
        }
    }

    void OnMessage(object resp, Dictionary<string, object> customData)
    {
        if (!gameControllerS1.isPlayingVO)
        {
            //  Convert resp to fsdata

            fsData fsdata = null;
            fsResult r = _serializer.TrySerialize(resp.GetType(), resp, out fsdata);
            if (!r.Succeeded)
                throw new WatsonException(r.FormattedMessages);

            //  Convert fsdata to MessageResponse
            MessageResponse messageResponse = new MessageResponse();
            object obj = messageResponse;
            r = _serializer.TryDeserialize(fsdata, obj.GetType(), ref obj);
            if (!r.Succeeded)
                throw new WatsonException(r.FormattedMessages);

            if (resp != null && (messageResponse.intents.Length > 0 || messageResponse.entities.Length > 0))
            {
                string intent = messageResponse.intents[0].intent;
                Debug.Log("Intent: " + intent);

                if (intent == "Hello")
                {
                    Debug.Log("Hello");
                    gameControllerS1.hello = true;
                    //AkSoundEngine.PostEvent("Play_VO_MAXWELL_hello", gameObject);

                    foreach (RuntimeEntity entity in messageResponse.entities)
                    {
                    }
                }
                if (intent == "HowAreYou")
                {
                    Debug.Log("How are you");
                    gameControllerS1.howAreYou = true;
                    //AkSoundEngine.PostEvent("Play_VO_MAXWELL_howareyou", gameObject);

                    foreach (RuntimeEntity entity in messageResponse.entities)
                    {
                    }
                }
                if (intent == "NiceToMeetYou")
                {
                    Debug.Log("Nice to meet you");
                    gameControllerS1.niceToMeetYou = true;
                    //AkSoundEngine.PostEvent("Play_VO_MAXWELL_nicetomeetyou", gameObject);

                    foreach (RuntimeEntity entity in messageResponse.entities)
                    {
                    }
                }
                else if (intent == "Bye")
                {
                    Debug.Log("Bye");
                    gameControllerS1.bye = true;
                    //AkSoundEngine.PostEvent("Play_VO_MAXWELL_bye", gameObject);

                    foreach (RuntimeEntity entity in messageResponse.entities)
                    {
                    }
                }
                else if (intent == "Intro")
                {
                    Debug.Log("Intro");
                    gameControllerS1.intro = true;
                    //AkSoundEngine.PostEvent("Play_VO_MAXWELL_intro", gameObject);

                    foreach (RuntimeEntity entity in messageResponse.entities)
                    {
                    }
                }
                else if (intent == "Backstory_Where")
                {
                    Debug.Log("Backstory where");
                    gameControllerS1.backstoryWhere = true;
                    foreach (RuntimeEntity entity in messageResponse.entities)
                    {
                    }
                }
                else if (intent == "Backstory_Who")
                {
                    Debug.Log("Backstory who");
                    gameControllerS1.backstoryWho = true;
                    foreach (RuntimeEntity entity in messageResponse.entities)
                    {
                    }
                }
                else if (intent == "Narrative_Houston")
                {
                    Debug.Log("Narrative Houston");
                    gameControllerS1.narrativeHouston = true;
                    foreach (RuntimeEntity entity in messageResponse.entities)
                    {
                    }
                }
                else if (intent == "Narrative_Task")
                {
                    Debug.Log("Narrative task");
                    gameControllerS1.narrativeTask = true;
                    foreach (RuntimeEntity entity in messageResponse.entities)
                    {
                    }
                }
                else if (intent == "Narrative_When")
                {
                    Debug.Log("Narrative when");
                    gameControllerS1.narrativeWhen = true;
                    foreach (RuntimeEntity entity in messageResponse.entities)
                    {
                    }
                }
                else if (intent == "Narrative_Where")
                {
                    Debug.Log("Narrative where");
                    gameControllerS1.narrativeWhere = true;
                    foreach (RuntimeEntity entity in messageResponse.entities)
                    {
                    }
                }
            }
            else
            {
                Debug.Log("Anything else");
                //AkSoundEngine.PostEvent("Play_PR_VO_001300_MAXWELL_lift_off_01", gameObject);

                foreach (RuntimeEntity entity in messageResponse.entities)
                {
                }
            }
        }

        else
        {
            Debug.Log("Stop talking over me.");
        }
    }

    private void OnRecognizeSpeaker(SpeakerRecognitionEvent result)
    {
        if (result != null)
        {
            foreach (SpeakerLabelsResult labelResult in result.speaker_labels)
            {
                Log.Debug("ExampleStreaming.OnRecognize()", string.Format("speaker result: {0} | confidence: {3} | from: {1} | to: {2}", labelResult.speaker, labelResult.from, labelResult.to, labelResult.confidence));
            }
        }
    }
}