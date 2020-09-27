
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
    }

    /// <summary>
    /// Returns a list of the characters in this dialogue path
    /// </summary>
    [JsonIgnore]
    public List<string> Characters
        => quotes.Select(q => q.characterName).Distinct().ToList();
}
