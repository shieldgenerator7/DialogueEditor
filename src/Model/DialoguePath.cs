
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class DialoguePath
{
    public string title = "Dialogue Title";
    public List<Condition> conditions = new List<Condition>();
    public List<Quote> quotes = new List<Quote>();
    public List<Action> actions = new List<Action>();

    /// <summary>
    /// Restores temp variables after being read in
    /// </summary>
    public void inflate()
    {
        conditions.ForEach(
            c => c.path = this
            );
        quotes.ForEach(
            q => q.path = this
            );
        actions.ForEach(
            a => a.path = this
            );
    }

    /// <summary>
    /// Returns a list of the characters in this dialogue path
    /// </summary>
    [JsonIgnore]
    public List<string> Characters
        => quotes.Select(q => q.characterName).Distinct().ToList();

    /// <summary>
    /// Returns a list of the variables checked or modified in this dialogue path
    /// </summary>
    [JsonIgnore]
    public List<string> Variables
    {
        get
        {
            List<string> vars = conditions.Select(c => c.variableName).ToList();
            vars.AddRange(actions.Select(a => a.variableName).ToList());
            return vars.Distinct().ToList();
        }
    }

    /// <summary>
    /// Returns true if all the required characters are in this dialogue path.
    /// Allows for extra characters not mentioned
    /// </summary>
    /// <param name="requiredCharacters"></param>
    /// <returns></returns>
    public bool allCharactersPresent(List<string> requiredCharacters)
    {
        List<string> chars = Characters;
        foreach (string chr in requiredCharacters)
        {
            //If one required character is not in this dialogue path,
            if (!chars.Contains(chr))
            {
                //Then not all are present
                return false;
            }
        }
        //All characters present
        return true;
    }

    public void remove(DialogueComponent dc)
    {
        if (dc is Condition condition)
        {
            conditions.Remove(condition);
        }
        else if (dc is Quote quote)
        {
            quotes.Remove(quote);
        }
        else if (dc is Action action)
        {
            actions.Remove(action);
        }
    }
}
