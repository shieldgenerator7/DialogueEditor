using DialogueEditor.src;
using System;

public class Quote
{

	public string characterName;
	public string text;

	public DialoguePath path;

	public int Index => path.quotes.IndexOf(this);

	public Quote(string charName="", string txt="")
	{
		this.characterName = charName;
		this.text = txt;
	}
}
