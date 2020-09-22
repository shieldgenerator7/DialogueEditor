using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


public class DisplayPanel : Panel
{
    public List<Keys> pressedKeys = new List<Keys>();

    public Vector mapPos = new Vector(0, 0);//the panel position in which to start drawing the grid

    bool mouseDown = false;
    public Vector lastMousePosition = new Vector(0, 0);//the position of the mouse at the last mouse event, panel coordinates
    public Vector firstMousePosition = new Vector(0, 0);//the position of the mouse at the first mouse event of the click, panel coordinates


    public RGB drawColor;
    public Color DrawColor
    {
        set
        {
            drawColor = ColorToRGB(value);
        }
    }
    Dictionary<Color, Brush> colorBrushes = new Dictionary<Color, Brush>();
    Pen lighten = new Pen(Color.FromArgb(100, 255, 255, 255), 1);
    Pen darken = new Pen(Color.FromArgb(100, 0, 0, 0), 1);


    public DisplayPanel() : base()
    {
        this.DoubleBuffered = true;
    }

    void updatePixelAtPosition(MouseEventArgs e, bool forceRedraw = false)
    {
        updatePixelAtPosition(e.X, e.Y, forceRedraw);
    }

    public void updatePixelAtPosition(int ex, int ey, bool forceRedraw = false)
    {
        if (forceRedraw || ex != lastMousePosition.x || ey != lastMousePosition.y)
        {
            RGB rgb = drawColor;
            updatePixelAtPosition(ex, ey, rgb);
            if (ex != lastMousePosition.x || ey != lastMousePosition.y)
            {
                lastMousePosition.x = ex;
                lastMousePosition.y = ey;
            }
            Invalidate();
        }
    }

    public static RGB ColorToRGB(Color color)
    {
        return new RGB(
            color.R,
            color.G,
            color.B
            );
    }

    public static Color RGBToColor(RGB rgb)
    {
        if (!rgb.isValid())
        {
            return Color.White;
        }
        return Color.FromArgb(
            rgb.red,
            rgb.green,
            rgb.blue
            );
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        g.Clear(Color.DarkGray);
        if (Managers.Initialized)
        {
            Managers.Node.layoutNodes();
            Managers.Display.displayDialoguePaths(g);
            Managers.Display.displayRectangles(g);
        }
    }

    Brush getBrush(Color color)
    {
        if (color == null)
        {
            color = Color.Red;
        }
        Brush brush;
        if (colorBrushes.ContainsKey(color))
        {
            brush = colorBrushes[color];
        }
        else
        {
            brush = new SolidBrush(color);
            colorBrushes.Add(color, brush);
        }
        return brush;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        mouseDown = true;
        lastMousePosition.x = e.X;
        lastMousePosition.y = e.Y;
        firstMousePosition.x = e.X;
        firstMousePosition.y = e.Y;
        Focus();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (mouseDown)
        {
        }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        mouseDown = false;
    }
    public delegate void OnPixelClicked(Color pixelColor);
    public OnPixelClicked onPixelClicked;

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);
        //PixelSize += Math.Sign(e.Delta) * 2;
        Invalidate();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (!pressedKeys.Contains(e.KeyCode))
        {
            pressedKeys.Add(e.KeyCode);
        }
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        base.OnKeyUp(e);
        pressedKeys.Remove(e.KeyCode);
    }
}
