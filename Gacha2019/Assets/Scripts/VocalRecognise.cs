using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VocalRecognise : MonoBehaviour
{
	private KeywordRecognizer keywordRecognizer;
	private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
		actions.Add("Bonjour je m'appelle sabri", Forward);
		actions.Add("up", Up);
		actions.Add("down", Down);
		actions.Add("Shazam", Back);

		keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
		keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
		keywordRecognizer.Start();

	}

	private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
	{
		Debug.Log(speech.text);
		actions[speech.text].Invoke();
	}

	// Ultime avec la voix (Mot) 

    private void Forward()
	{
		transform.Translate(1, 0, 0);
	}
	private void Back()
	{
		transform.Translate(-1, 0, 0);
	}
	private void Up()
	{
		transform.Translate(0, 1, 0);
	}
	private void Down()
	{
		transform.Translate(0, -1, 0);
	}
}
