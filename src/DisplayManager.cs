using DialogueEditor.src;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

public class DisplayManager
{
    public const int MAX_WIDTH = 250;
    public const int BUFFER_WIDTH = 10;

    const int fontSize = 12;
    Font font = new Font("Ariel", fontSize);
    StringFormat stringFormat;
    Brush textBrush = new SolidBrush(Color.Black);

    Brush backBrush = new SolidBrush(Color.LightGray);

    public const int portraitSize = 50;

    Graphics g;

    Vector mapPos = Vector.zero;


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

    public void unscroll()
    {
        mapPos = Vector.zero;
    }
    public void scroll(int dirX, int dirY)
    {
        int width = MAX_WIDTH + BUFFER_WIDTH * 3;
        mapPos.x += dirX * width;
        if (mapPos.x < 0)
        {
            mapPos.x = 0;
        }
        int maxPosX = (Managers.Node.containers.Count - 1) * width;
        if (mapPos.x > maxPosX)
        {
            mapPos.x = maxPosX;
        }
        mapPos.y += dirY * (50);
        if (mapPos.y < 0)
        {
            mapPos.y = 0;
        }
        int maxPosY = Managers.Node.containers.Max(c => c.size.y) - 50;
        if (mapPos.y > maxPosY)
        {
            mapPos.y = maxPosY;
        }
    }

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
        else if (n is NodeCondition)
        {
            paintNode((NodeCondition)n);
        }
        else if (n is NodeAction)
        {
            paintNode((NodeAction)n);
        }
    }
    private void paintNode(NodeDialogue nd)
    {
        //Draw back
        g.FillRectangle(
            backBrush,
            nd.position.x - mapPos.x,
            nd.position.y - mapPos.y,
            nd.size.x,
            nd.size.y
            );
        //Draw nodes
        nd.Nodes.ForEach(n => paintNode(n));
    }
    private void paintNode(NodeQuote nq)
    {
        drawString(nq.textBox);
        if (nq.image == null)
        {
            nq.refreshImage();
        }
        if (nq.image != null)
        {
            g.DrawImage(
                nq.image,
                nq.position.x - mapPos.x,
                nq.position.y - mapPos.y,
                portraitSize,
                portraitSize
                );
        }
    }
    private void paintNode(NodeCondition nc)
    {
        drawString(nc.txtVariableName);
        drawString(nc.txtTestType);
        drawString(nc.txtTestValue);
    }
    private void paintNode(NodeAction na)
    {
        drawString(na.txtVariableName);
        drawString(na.txtActionType);
        drawString(na.txtActionValue);
    }
    #endregion

    #region Graphics Wrapper Methods
    public Vector measureString(TextDisplayable txt)
    {
        return measureString(txt.text, txt.maxWidth);
    }
    public Vector measureString(string text, int maxWidth = MAX_WIDTH)
    {
        return new Vector(g.MeasureString(text, font, maxWidth, stringFormat));
    }

    public void drawString(TextDisplayable txt)
    {
        drawString(txt.text, txt.position, txt.maxWidth);
    }
    public void drawString(string text, Vector position, int maxWidth = MAX_WIDTH)
    {
        RectangleF rectf = new RectangleF(
            position.x - mapPos.x,
            position.y - mapPos.y,
            maxWidth,
            2000
            );
        g.DrawString(text, font, textBrush, rectf, stringFormat);
    }
    #endregion
}
