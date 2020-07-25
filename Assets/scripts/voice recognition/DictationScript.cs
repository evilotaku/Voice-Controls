using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
//using UnitySpeechToText.Services;

public class DictationScript : MonoBehaviour
{
    [SerializeField]
    private Text m_Hypotheses;

    [SerializeField]
    public Text m_Recognitions;

    private DictationRecognizer m_DictationRecognizer;

    //public ChatBot bot;

    //SpeechToTextResult m_LastResult;

    void Start()
    {
        m_DictationRecognizer = new DictationRecognizer(ConfidenceLevel.High, DictationTopicConstraint.Dictation);

        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            m_Recognitions.text += text + "\n";
            //bot.SendChat(text);

        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
            m_Hypotheses.text += text + "\n";
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };

       // m_LastResult = new SpeechToTextResult("", false);
        m_DictationRecognizer.Start();
    }
}