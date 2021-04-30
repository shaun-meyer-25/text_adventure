using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class TextProcessing 
{
	private IController _controller;
	private IEnumerator typeSentence;
	
	public TextProcessing(IController controller, float processingDelay)
	{
		_controller = controller;
		//typeSentence = TypeSentence(processingDelay);
	}

	Dictionary<Tuple<int, int>, string> indicesOfColoredCharacters = new Dictionary<Tuple<int, int>, string>();
	Dictionary<int, Tuple<string, char>> charactersAndTheirColors = new Dictionary<int, Tuple<string, char>>();
	List<Tuple<int, int>> rangesOfMarkupCharacters = new List<Tuple<int, int>>();

	public void DisplayText(string sentence)
	{
		ClearCollections();
		PopulateList(sentence);
		PopulateColoredCharDict(sentence);
		PopulateCharactersAndTheirColors(sentence);
		_controller.StartCoroutine("TypeSentence", charactersAndTheirColors);
	}
	public void StopTypingCoroutine()
	{
		_controller.StopCoroutine("TypeSentence");
	}

	private void PopulateCharactersAndTheirColors(string sentence)
	{
		int indexOfFinalString = 0;
		bool indexOutsideMarkupRange;

		for (int i = 0; i < sentence.ToCharArray().Length; i++)
		{

			indexOutsideMarkupRange = true;
			foreach (var range in rangesOfMarkupCharacters)
			{
				if (Enumerable.Range(range.Item1, range.Item2 - range.Item1 + 1).Contains(i))
				{
					indexOutsideMarkupRange = false;
				}
			}

			if (indexOutsideMarkupRange)
			{
				charactersAndTheirColors.Add(indexOfFinalString, new Tuple<string, char>(GetColorOfChar(i, sentence), sentence[i]));
				indexOfFinalString += 1;
			}
		}
	}
	
	private string GetColorOfChar(int indexOfChar, string sentence)
	{
		if (indicesOfColoredCharacters.Count > 0)
		{
			foreach (var kvp in indicesOfColoredCharacters)
			{
				if (Enumerable.Range(kvp.Key.Item1, kvp.Key.Item2 - kvp.Key.Item1 + 1).Contains(indexOfChar))
				{
					return kvp.Value;
				}
			}
		}

		return _controller.currentColor;
	}

	private void PopulateColoredCharDict(string sentence)
	{
		int startingIndex = 0;  
		int endingIndex = 0;

		while (LookAheadForChar(endingIndex + 1, sentence, '<') != -1)
		{
			string color = Regex.Match(sentence.Substring(endingIndex),"(?<=color=)(.*?)(?=>)").Value;
			int startingColorIndex = LookAheadForChar(endingIndex + 1, sentence, '>') + 1;
			int endingColorIndex = LookAheadForChar(startingColorIndex, sentence, '<');

			if (!indicesOfColoredCharacters.ContainsKey(new Tuple<int, int>(startingColorIndex, endingColorIndex)))
			{
				indicesOfColoredCharacters.Add(new Tuple<int, int>(startingColorIndex, endingColorIndex), color);
			}

			endingIndex = endingColorIndex + 7;
		}
	}

	private void PopulateList(string sentence)
	{
		int startingIndex = 0;
		int endingIndex = 0;

		while (LookAheadForChar(endingIndex + 1, sentence, '<') != -1)
		{
			startingIndex = LookAheadForChar(startingIndex + 1, sentence, '<');
			endingIndex = LookAheadForChar(endingIndex + 1, sentence, '>');
			
			rangesOfMarkupCharacters.Add(new Tuple<int, int>(startingIndex, endingIndex));
		}
	}
	
	public static int LookAheadForChar(int indexOfOpenBracket, string sentence, char c)
	{
		string slice = sentence.Substring(indexOfOpenBracket);
		if (slice.IndexOf(c) == -1)
		{
			return -1;
		} 
		return indexOfOpenBracket + slice.IndexOf(c);
	}

	private void ClearCollections()
	{
		indicesOfColoredCharacters = new Dictionary<Tuple<int, int>, string>();
		charactersAndTheirColors = new Dictionary<int, Tuple<string, char>>();
		rangesOfMarkupCharacters = new List<Tuple<int, int>>();

	}
}
