using System;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;

[Serializable] public class StringEvent : UnityEvent<string> { }

[Serializable]
public class SemanticEvents
{
    public string semantics;
    public StringEvent semanticEvent;
}
public class GrammarScript : MonoBehaviour {

    public string m_GrammarFile;

    public SemanticEvents[] semanticEvents;
    
    private GrammarRecognizer m_Recognizer;
    private Dictionary<string, UnityEvent<string>> semantics = new Dictionary<string, UnityEvent<string>>();

    void Start()
    {
        foreach (var semantic in semanticEvents)
        {
            semantics.Add(semantic.semantics, semantic.semanticEvent);
        }
        m_Recognizer = new GrammarRecognizer(Application.streamingAssetsPath + m_GrammarFile);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("Timestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("Duration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        foreach (var semantic in args.semanticMeanings)
        {            
            foreach (var value in semantic.values)
            {
                semantics[semantic.key].Invoke(value);
                builder.AppendFormat("Semantic Meaning: {0} - {1}{2} ", semantic.key, value, Environment.NewLine);
            }
            Debug.Log(builder.ToString());
        }
    }
}
