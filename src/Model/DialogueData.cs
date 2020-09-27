using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class DialogueData
{
    public List<DialoguePath> dialogues;

    public DialogueData(List<DialoguePath> dialogues = null)
    {
        this.dialogues = dialogues;
        if (this.dialogues == null)
        {
            this.dialogues = new List<DialoguePath>();
        }
    }

    public DialoguePath getDialoguePath(string title)
    {
        return dialogues.FirstOrDefault(d => d.title == title);
    }

    public List<DialoguePath> getDialoguePaths(List<string> characters)
    {
        return dialogues.FindAll(d => d.allCharactersPresent(characters));
    }

    /// <summary>
    /// Returns a list of all the characters in all the dialogue paths
    /// </summary>
    [JsonIgnore]
    public List<string> Characters
    {
        get
        {
            List<string> chars = new List<string>();
            dialogues.ForEach(d => chars.AddRange(d.Characters));
            return chars.Distinct().ToList();
        }
    }

    /// <summary>
    /// Returns a list of all the variables checked or modified in all dialogue paths
    /// </summary>
    [JsonIgnore]
    public List<string> Variables
    {
        get
        {
            List<string> vars = new List<string>();
            dialogues.ForEach(d => vars.AddRange(d.Variables));
            if (vars.Count == 0)
            {
                vars.Add("var1");
            }
            return vars.Distinct().ToList();
        }
    }
}
