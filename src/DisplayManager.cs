using DialogueEditor;
using DialogueEditor.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class DisplayManager
{
    int BRUSH_THICKNESS = 4;

    Pen selectPen;
    Pen overPen;
    Pen deletePen;
    Pen anchorPen;
    Pen changePen;
    Pen createPen;
    Pen containerPen;

    Brush rectBrush;
    Brush textBrush;
    Brush containerBrush;

    Font font;

    public Vector origin = Vector.zero;

    public DisplayManager()
    {
        selectPen = new Pen(Color.FromArgb(131, 239, 76), BRUSH_THICKNESS);
        overPen = new Pen(Color.FromArgb(255, 255, 255), BRUSH_THICKNESS);
        deletePen = new Pen(Color.FromArgb(247, 70, 70), BRUSH_THICKNESS);
        anchorPen = new Pen(Color.FromArgb(52, 175, 0), BRUSH_THICKNESS);
        changePen = new Pen(Color.FromArgb(137, 206, 255), BRUSH_THICKNESS);
        createPen = new Pen(Color.FromArgb(255, 230, 107), BRUSH_THICKNESS);
        containerPen = new Pen(Color.Black);
        //
        rectBrush = new SolidBrush(Color.FromArgb(53, 70, 127));
        textBrush = new SolidBrush(Color.FromArgb(240, 240, 200));
        containerBrush = new SolidBrush(Color.LightGray);
        //
        FontFamily fontFamily = new FontFamily("Microsoft Sans Serif");
        font = new Font(
            fontFamily,
            20,
            FontStyle.Regular,
            GraphicsUnit.Pixel
            );
    }

    public void displayDialoguePaths(Graphics graphics)
    {
        foreach (ContainerNode container in Managers.Node.containers)
        {
            graphics.FillRectangle(containerBrush, container.getRect());
            drawRectangle(graphics, containerPen, container);
        }
    }

    public void displayRectangles(Graphics graphics)
    {
        displayRectangles(
            graphics,
            Managers.Control.selected,
            Managers.Control.mousedOver,
            Managers.Control.mousePos,
            Managers.Control.isMouseDown
            );
    }

    public void displayRectangles(Graphics graphics, Node selected, Node mousedOver, Vector mousePos, bool mouseDown)
    {
        if (selected)
        {
            drawRectangle(graphics, selectPen, selected);
        }
        if (mousedOver)
        {
            drawRectangle(graphics, overPen, mousedOver);
        }
    }
    private void drawRectangle(Graphics graphics, Pen pen, Node node)
    {
        graphics.DrawRectangle(
            pen,
            convertToScreen(node.getRect())
            );
    }

    public void displayDescription(Graphics graphics)
    {
        displayDescription(
            graphics,
            Managers.Control.mousedOver,
            Managers.Control.mousePos
            );
    }

    public void displayDescription(Graphics graphics, Node mousedOver, Vector mousePos)
    {
        mousePos = convertToScreen(mousePos);
        //Object Description
        if (mousedOver)
        {
            graphics.DrawString(mousedOver.quote.text,
                font,
                new SolidBrush(Color.Black),
                mousePos.x - 5,
                mousePos.y - 25
                );
        }
    }

    public Vector convertToScreen(Vector vWorld)
    {
        return vWorld + origin;
    }

    public Vector convertToWorld(Vector vScreen)
    {
        return vScreen - origin;
    }

    public Rectangle convertToScreen(Rectangle rWorld)
        => new Rectangle(
            rWorld.X + (int)origin.x,
            rWorld.Y + (int)origin.y,
            rWorld.Width,
            rWorld.Height
            );

    public Rectangle convertToWorld(Rectangle rScreen)
        => new Rectangle(
            rScreen.X - (int)origin.x,
            rScreen.Y - (int)origin.y,
            rScreen.Width,
            rScreen.Height
            );
}
