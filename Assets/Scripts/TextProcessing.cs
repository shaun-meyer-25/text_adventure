using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class TextProcessing 
{
	private GameController _controller;
	
	public TextProcessing(GameController controller)
	{
		_controller = controller;
	}
	
	// instead of doing this character by character, we should compose a list or dict of characters by color and go off that, 
	// not the original string, to avoid adding things like the </color> string 
	Dictionary<Tuple<int, int>, string> indicesOfColoredCharacters = new Dictionary<Tuple<int, int>, string>();
	
	public IEnumerator TypeSentence(string sentence)
	{
		
		for (int i = 0; i < sentence.ToCharArray().Length; i++) 
		{
			if (i < LookAheadForChar(i, sentence, '<'))
			{
				_controller.displayText.text += GetColoredChar(i, sentence);
			}
			else
			{
				_controller.displayText.text += sentence[i];
			}
			
			yield return new WaitForSeconds(.05f);
		}
	}

	private string GetColoredChar(int indexOfChar, string sentence)
	{
		string substring = sentence;
// 104, 126
		while (substring.Contains('<'))
		{
			string color = Regex.Match(substring,"(?<=color=)(.*?)(?=>)").Value;
			int startingColorIndex = substring.IndexOf('>') + 1;
			int endingColorIndex = LookAheadForChar(startingColorIndex, substring, '<');

			if (!indicesOfColoredCharacters.ContainsKey(new Tuple<int, int>(startingColorIndex, endingColorIndex)))
			{
				indicesOfColoredCharacters.Add(new Tuple<int, int>(startingColorIndex, endingColorIndex), color);
			}
			substring = substring.Substring(endingColorIndex + 7);
		}

		if (indicesOfColoredCharacters.Count > 0)
		{
			foreach (var kvp in indicesOfColoredCharacters)
			{
				if (Enumerable.Range(kvp.Key.Item1, kvp.Key.Item2).Contains(indexOfChar))
				{
					return "<color=" + kvp.Value + ">" + sentence[indexOfChar] + "</color>";
				}
			}
		}

		return "<color=" + _controller.currentColor + ">" + sentence[indexOfChar] + "</color>";
	}
	
	private int LookAheadForChar(int indexOfOpenBracket, string sentence, char c)
	{
		string slice = sentence.Substring(indexOfOpenBracket);
		return indexOfOpenBracket + slice.IndexOf(c);
	}
}
