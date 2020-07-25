using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using UnityEngine.Events;

[Serializable]
public class KeywordEvent
{
    public string keyword;
    public UnityEvent keywordAction;
}

public class KeywordScript : MonoBehaviour
{
    public KeywordEvent[] keywordActions;

    private KeywordRecognizer m_Recognizer;
    private Dictionary<string, UnityEvent> keywords = new Dictionary<string, UnityEvent>();

    void Start()
    {
        foreach (var keywordAction in keywordActions)
        {
            keywords.Add(keywordAction.keyword, keywordAction.keywordAction);
        }
        m_Recognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        WindowsVoice.Speak("Initiating " + args.text);	
        keywords[args.text].Invoke();
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("Timestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("Duration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        Debug.Log(builder.ToString());
    }
}