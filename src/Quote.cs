using DialogueEditor.src;
using System;

public class Quote
{

	public string characterName;
	public string text;

	public DialoguePath path;

	public Quote(string charName="TEST1", string txt="Text test 2.")
	{
		this.characterName = charName;
		this.text = txt;
	}
}
