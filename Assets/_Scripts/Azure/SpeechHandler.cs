using System;
using System.Collections;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Obsolete("This is an obsolete class, use SpeechToTextHandler instead")]
public class SpeechHandler:MonoBehaviour
{
    [SerializeField] Button m_startListenBtn;
    [SerializeField] TextMeshProUGUI m_logTMP;
    private bool isListening = false;
    private SpeechRecognizer speechRecognizer;

    private void Start()
    {
        m_startListenBtn.onClick.AddListener(StartListener);
    }
    public void StartListener()
    {
        if (isListening) return;
        StartCoroutine(StartSpeechListener());
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            m_logTMP.text = "Starting";
            StartListener();
        }
    }


    // This example requires environment variables named "SPEECH_KEY" and "SPEECH_REGION"
    static string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
    static string speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");
    private void OutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
    {
        switch (speechRecognitionResult.Reason)
        {
            case ResultReason.RecognizedSpeech:
                m_logTMP.text = ($"RECOGNIZED: Text={speechRecognitionResult.Text}");
                break;
            case ResultReason.NoMatch:
                m_logTMP.text = ($"NOMATCH: Speech could not be recognized.");
                break;
            case ResultReason.Canceled:
                var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                m_logTMP.text = ($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    m_logTMP.text = ($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    m_logTMP.text += ($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    //m_logTMP.text = ($"CANCELED: Did you set the speech resource key and region values?");
                }
                break;
        }
    }
    /*
    async static Task StartSpeechListener()
    {
        var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
        speechConfig.SpeechRecognitionLanguage = "en-US";

        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

        Debug.Log("Speak into your microphone.");
        var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
        OutputSpeechRecognitionResult(speechRecognitionResult);
    }
     */
    private IEnumerator StartSpeechListener()
    {
        var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
        speechConfig.SpeechRecognitionLanguage = "en-US";

        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

        Debug.Log("Speak into your microphone.");
        isListening = true;
        m_startListenBtn.interactable = false;

        var operation = speechRecognizer.RecognizeOnceAsync();

        while (!operation.IsCompleted)
        {
            yield return null; // Wait for the operation to complete.
        }

        var speechRecognitionResult = operation.Result;
        OutputSpeechRecognitionResult(speechRecognitionResult);

        // Don't forget to clean up the recognizer.
        speechRecognizer.Dispose();
        isListening = false;
        m_startListenBtn.interactable = true;
    }
}