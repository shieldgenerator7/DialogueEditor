using System;

public struct TextDisplayable
{
    public string text;
    public int maxWidth;
    public Vector position;
    public Vector size;
    public TextDisplayable(string text, int maxWidth)
    {
        this.text = text;
        this.maxWidth = maxWidth;
        this.position = Vector.zero;
        this.size = new Vector(25, maxWidth);
    }
}
