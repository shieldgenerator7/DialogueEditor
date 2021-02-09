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
    readonly Font font = new Font("Ariel", fontSize);
    readonly StringFormat stringFormat;
    readonly Brush textBrush = new SolidBrush(Color.Black);
    readonly Brush backBrush = new SolidBrush(Color.LightGray);
    readonly Pen selectBrush = new Pen(Color.LimeGreen, 3);

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
        Vector panelSize = new Vector(panel.Size);
        Managers.Node.containers
            .FindAll(n => Managers.Camera.nodeOnScreen(n))
            .ForEach(n => paintNode(n));
        this.g = null;
    }

    #region Node-Specific Methods
    private void paintNode(Node n)
    {
        if (n is NodeDialogue dialogue)
        {
            paintNode(dialogue);
        }
        else if (n is NodeQuote quote)
        {
            paintNode(quote);
        }
        else if (n is NodeCondition condition)
        {
            paintNode(condition);
        }
        else if (n is NodeAction action)
        {
            paintNode(action);
        }
        if (Managers.Select.selected(n))
        {
            paintNodeBorder(n);
        }
    }
    private void paintNode(NodeDialogue nd)
    {
        //Draw back
        g.FillRectangle(
            backBrush,
            Managers.Camera.WorldToScreen(nd)
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
                Managers.Camera.WorldToScreen(nq.position, portraitSize)
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

    private void paintNodeBorder(Node n)
    {
        g.DrawRectangle(selectBrush, Managers.Camera.WorldToScreen(n));
    }

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
        RectangleF rectf = Managers.Camera.WorldToScreen(
            position,
            maxWidth,
            2000
            );
        g.DrawString(text, font, textBrush, rectf, stringFormat);
    }
    #endregion
}
