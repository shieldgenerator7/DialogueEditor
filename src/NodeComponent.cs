using DialogueEditor.src;
using System;

public abstract class NodeComponent : Node
{
    public override DialoguePath Path => data.path;
    public NodeComponent()
    {
    }

    public abstract DialogueComponent data { get; }
    public abstract string QuoteText { get; set; }

    /// <summary>
    /// Used to determine which types should be sorted before other types
    /// </summary>
    public abstract int OrderCode { get; }

    public abstract int CompareTo(Node n);
}
