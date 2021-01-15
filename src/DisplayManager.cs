using DialogueEditor.src;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

public class DisplayManager
{
    const int MAX_WIDTH = 250;

    /// <summary>
    /// Where the next Node will be placed
    /// </summary>
    Vector cursor = new Vector(10, 10);

    Font font = new Font("Ariel", 15);
    StringFormat stringFormat;
    Brush textBrush = new SolidBrush(Color.Black);

    Graphics g;


    public DisplayManager()
    {
        //String Format
        stringFormat = new StringFormat(StringFormatFlags.NoClip);
        stringFormat.Alignment = StringAlignment.Near;
        stringFormat.Trimming = StringTrimming.None;
    }

    public void paint(Graphics g, Panel panel)
    {
        this.g = g;
        Vector mapPos = Vector.zero;
        Vector panelSize = new Vector(panel.Size);
        cursor = new Vector(10, 10);
        Managers.Node.containers
            .FindAll(n => nodeOnScreen(n, mapPos, panelSize))
            .ForEach(n => paintNode(n));
        this.g = null;
    }
    public bool nodeOnScreen(Node n, Vector mapPos, Vector screenSize)
        => n.position.x + n.Size.x >= mapPos.x
        && mapPos.x + screenSize.x >= n.position.x
        && n.position.y + n.Size.y >= mapPos.y
        && mapPos.y + screenSize.y >= n.position.y;

    #region Node-Specific Methods
    private void paintNode(Node n)
    {
        if (n is NodeDialogue)
        {
            paintNode((NodeDialogue)n);
        }
        else if (n is NodeQuote)
        {
            paintNode((NodeQuote)n);
        }
        //else if (n is NodeCondition)
        //{
        //    paintNode((NodeCondition)n);
        //}
        //else if (n is NodeAction)
        //{
        //    paintNode((NodeAction)n);
        //}
    }
    private void paintNode(NodeDialogue nd)
    {
        nd.position = cursor;
        nd.Nodes.ForEach(n => paintNode(n));
        cursor.x += MAX_WIDTH + 10;
        cursor.y = 10;
    }
    private void paintNode(NodeQuote nq)
    {
        nq.position = cursor;
        string text = nq.QuoteText;
        nq.Size = measureString(text);
        drawString(text, nq.position);
        cursor.y += nq.Size.y;
    }
    #endregion

    #region Graphics Wrapper Methods
    public Vector measureString(string text, int maxWidth = MAX_WIDTH)
    {
        return new Vector(g.MeasureString(text, font, maxWidth, stringFormat));
    }

    public void drawString(string text, Vector position)
    {
        RectangleF rectf = new RectangleF(position.x, position.y, MAX_WIDTH, 200);
        g.DrawString(text, font, textBrush, rectf, stringFormat);
    }
    #endregion
}
