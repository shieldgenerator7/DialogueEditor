using DialogueEditor.src;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

public class DisplayManager
{
    const int MAX_WIDTH = 250;
    const int BUFFER_WIDTH = 10;

    /// <summary>
    /// Where the next Node will be placed
    /// </summary>
    Vector cursor = new Vector(BUFFER_WIDTH, BUFFER_WIDTH);

    const int fontSize = 15;
    Font font = new Font("Ariel", fontSize);
    StringFormat stringFormat;
    Brush textBrush = new SolidBrush(Color.Black);

    Brush backBrush = new SolidBrush(Color.LightGray);

    const int portraitSize = 50;

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
        cursor = new Vector(BUFFER_WIDTH, BUFFER_WIDTH);
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
        nd.position = cursor;
        cursor.x += BUFFER_WIDTH;
        cursor.y += BUFFER_WIDTH;
        nd.size = Vector.zero;
        //Set size
        nd.Nodes.ForEach(n =>
        {
            sizeNode(n);
            nd.size.x = Math.Max(nd.size.x, n.size.x);
            nd.size.y += n.size.y + BUFFER_WIDTH;
        });
        nd.size.x += BUFFER_WIDTH * 2;
        nd.size.y -= BUFFER_WIDTH;
        //Draw back
        g.FillRectangle(backBrush, nd.position.x, nd.position.y, nd.size.x, nd.size.y);
        //Draw nodes
        nd.Nodes.ForEach(n => paintNode(n));
        //Update cursor
        cursor.x += nd.size.x;
        cursor.y = BUFFER_WIDTH;
    }
    private void paintNode(NodeQuote nq)
    {
        nq.position = cursor;
        string text = nq.QuoteText;
        Vector textPosition = nq.position + Vector.right * (portraitSize + BUFFER_WIDTH);
        int imageWidth = portraitSize + BUFFER_WIDTH;
        int textWidth = MAX_WIDTH - imageWidth;
        drawString(text, textPosition, textWidth);
        if (nq.image == null)
        {
            nq.refreshImage();
        }
        if (nq.image != null)
        {
            g.DrawImage(nq.image, nq.position.x, nq.position.y, portraitSize, portraitSize);
        }
        cursor.y += nq.size.y + BUFFER_WIDTH;
    }

    private void sizeNode(Node n)
    {
        if (n is NodeQuote)
        {
            sizeNode((NodeQuote)n);
        }
    }
    private void sizeNode(NodeQuote nq)
    {
        string text = nq.QuoteText;
        int imageWidth = portraitSize + BUFFER_WIDTH;
        int textWidth = MAX_WIDTH - imageWidth;
        nq.size = measureString(text, textWidth);
        nq.size.x += imageWidth;
        nq.size.y = Math.Max(nq.size.y, portraitSize);
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
