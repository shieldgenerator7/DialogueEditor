using DialogueEditor.src;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

public class DisplayManager
{
    public const int MAX_WIDTH = 250;
    public const int BUFFER_WIDTH = 10;

    const int fontSize = 15;
    Font font = new Font("Ariel", fontSize);
    StringFormat stringFormat;
    Brush textBrush = new SolidBrush(Color.Black);

    Brush backBrush = new SolidBrush(Color.LightGray);

    public const int portraitSize = 50;

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
        Managers.Layout.layoutNodes();
        Vector mapPos = Vector.zero;
        Vector panelSize = new Vector(panel.Size);
        Managers.Node.containers
            .FindAll(n => nodeOnScreen(n, mapPos, panelSize))
            .ForEach(n => paintNode(n));
        this.g = null;
    }
    public bool nodeOnScreen(Node n, Vector mapPos, Vector screenSize)
        => n.position.x + n.size.x >= mapPos.x
        && mapPos.x + screenSize.x >= n.position.x
        && n.position.y + n.size.y >= mapPos.y
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
        //Draw back
        g.FillRectangle(backBrush, nd.position.x, nd.position.y, nd.size.x, nd.size.y);
        //Draw nodes
        nd.Nodes.ForEach(n => paintNode(n));
    }
    private void paintNode(NodeQuote nq)
    {
        string text = nq.QuoteText;
        drawString(text, nq.textBox.position, nq.textBox.maxWidth);
        if (nq.image == null)
        {
            nq.refreshImage();
        }
        if (nq.image != null)
        {
            g.DrawImage(nq.image, nq.position.x, nq.position.y, portraitSize, portraitSize);
        }
    }
    #endregion

    #region Graphics Wrapper Methods
    public Vector measureString(string text, int maxWidth = MAX_WIDTH)
    {
        return new Vector(g.MeasureString(text, font, maxWidth, stringFormat));
    }

    public void drawString(string text, Vector position, int maxWidth = MAX_WIDTH)
    {
        RectangleF rectf = new RectangleF(position.x, position.y, maxWidth, 2000);
        g.DrawString(text, font, textBrush, rectf, stringFormat);
    }
    #endregion
}
