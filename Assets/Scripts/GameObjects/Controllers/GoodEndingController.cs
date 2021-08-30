using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using Random = System.Random;

public class GoodEndingController : IController
{
	public bool DisabledButtons = true;

	private static int NUMBER_OF_OPTIONS = 4;
	private Dictionary<string, string> allPreferences;
	private List<string> undisplayedSentences = new List<string>();
	private TextProcessing _textProcessing;
	
	void Awake ()
	{
		_textProcessing = new TextProcessing(this, processingDelay);
		levelLoader = FindObjectOfType<LevelLoader>();
		isDaytime = true;
	}

	void Start ()
	{
		audio = GetComponent<AudioSource>();

		displayText.text = "";
		LogStringWithReturn("Tei is standing at the mouth of the cave. they see Ohm's blood on you and reach for you. you take them in an embrace.");
		LogStringWithReturn("you are safe. you are together. that dreadful thing will never touch your mind again.");		
		LogStringWithReturn("you hold each other in the brisk wind. you will return to your home. but for now, you hold each other.");
		DisplayLoggedText ();

		StartCoroutine(EndGame());
	}

	IEnumerator EndGame()
	{
		yield return new WaitForSeconds(30f);
		StartCoroutine(FadeAudioSource.StartFade(audio, .3f, 0));
		levelLoader.LoadScene("Credits");
	}
	
	public override void UpdateRoomChoices(Choice[] choices)
	{

	}

	public override void DisplayLoggedText ()
	{
		displayText.text = "";

		string logAsText = string.Join ("\n", actionLog.ToArray ());
		List<string> pastLog = new List<string>(logAsText.Split('\n'));
		while (logAsText.Length > 10000)
		{
			pastLog.RemoveRange(0, pastLog.Count / 2);
			logAsText = string.Join("\n", pastLog.ToArray());
		}
		foreach (var line in pastLog)
		{
			displayText.text += "\n<color=" + currentColor + ">" + line + "</color>";
		}

		string undisplayedLogAsText = string.Join ("\n\n", undisplayedSentences.ToArray ());
		while (undisplayedLogAsText.Length > 10000)
		{
			List<string> log = new List<string>(undisplayedLogAsText.Split('\n'));
			log.RemoveRange(0, log.Count / 2);
			undisplayedLogAsText = string.Join("\n", log.ToArray());
		}
		
		_textProcessing.StopTypingCoroutine();
		
		_textProcessing.DisplayText("\n" + undisplayedLogAsText);
		foreach (var sentence in undisplayedSentences)
		{
			actionLog.Add(sentence + "\n");
		}
		undisplayedSentences.Clear();
	}
	
	public override void LogStringWithReturn(string stringToAdd) {
		if (stringToAdd != "")
		{
			undisplayedSentences.Add(stringToAdd);
		}
	}

	public override void LoadRoomData()
	{
		ClearCollectionsForNewRoom ();
		UnpackRoom ();
		
	}
	
	public override void LoadRoomDataAndDisplayRoomText () {

	}

	void UnpackRoom()
	{

	}

	public override void PrepareObjectsToTakeOrExamine(Room currentRoom)
	{

	}

	public override string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun)
	{
		if (verbDictionary.ContainsKey(noun))
		{
			return verbDictionary[noun];
		}
		return "you can't " + verb + " " + noun;
	}

	void ClearCollectionsForNewRoom() {
		interactableItems.ClearCollections();
		interactionDescriptionsInRoom.Clear ();
		roomNavigation.ClearExits ();
	}

	public List<string> ActionNames()
	{
		List<string> actionNames = new List<string>();
		for (int i = 0; i < actions.Length; i++) {
			actionNames.Add(actions[i].keyword);
		}

		return actionNames;
	}

	public override List<string> ObservableChoiceNames()
	{
		List<string> observableChoiceNames = new List<string>();
		for (int i = 0; i < observableChoices.Length; i++)
		{
			observableChoiceNames.Add(observableChoices[i].keyword);
		}

		return observableChoiceNames;
	}
	
	public List<string> InteractChoiceNames()
	{
		List<string> interactChoiceNames = new List<string>();
		for (int i = 0; i < interactableChoices.Length; i++)
		{
			interactChoiceNames.Add(interactableChoices[i].keyword);
		}

		return interactChoiceNames;
	}

	private string DescribeTravelingCompanions(string combinedText)
	{
		return "";
	}

	public IEnumerator TypeSentence(Dictionary<int, Tuple<string, char>> charactersAndTheirColors )
	{
		for (int i = 0; i < charactersAndTheirColors.Count; i++)
		{
			Tuple<string, char> value = charactersAndTheirColors[i];
			displayText.text += "<color=" + value.Item1 + ">" + value.Item2 + "</color>";

			yield return new WaitForSeconds(processingDelay);
		}

	}
	
}
