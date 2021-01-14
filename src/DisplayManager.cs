using DialogueEditor.src;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

public class DisplayManager
{
    const int MAX_WIDTH = 200;

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
        nd.Nodes.ForEach(n => paintNode(n));
    }
    private void paintNode(NodeQuote nq)
    {
        string text = nq.QuoteText;
        nq.Size = measureString(text);
        drawString(text, nq.position);
    }
    #endregion

    #region Graphics Wrapper Methods
    public Vector measureString(string text, int maxWidth = MAX_WIDTH)
    {
        return new Vector(g.MeasureString(text, font, maxWidth, stringFormat));
    }

    public void drawString(string text, Vector position)
    {
        g.DrawString(text, font, textBrush, position, stringFormat);
    }
    #endregion
}
