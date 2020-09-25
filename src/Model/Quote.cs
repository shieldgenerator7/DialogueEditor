using Newtonsoft.Json;
using System;

[Serializable]
public class Quote
{

    public string characterName = "";
    public string text = "";
    public string imageFileName = "";

    [NonSerialized]
    public DialoguePath path;

    [JsonIgnore]
    public int Index => path.quotes.IndexOf(this);

    public Quote(string charName = "", string txt = "", string imageFileName = "")
    {
        this.characterName = charName;
        this.text = txt;
        this.imageFileName = imageFileName;
    }
}
