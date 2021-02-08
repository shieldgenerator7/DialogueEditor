using DialogueEditor.src;
using System;
using System.Linq;

public class CameraManager
{
    /// <summary>
    /// The top left position of the camera in world space
    /// </summary>
    Vector position = Vector.zero;
    float zoomScale = 1;
    /// <summary>
    /// The size of the camera viewing area (the size of the panel)
    /// </summary>
    Vector size = Vector.zero;

    public CameraManager(DisplayPanel panel)
    {
        panel.Resize += (obj, e) => size = new Vector(panel.Size);
        size = new Vector(panel.Size);
    }

    public void Zoom(float scale)
    {
        zoomScale = scale;
    }

    public Vector WorldToScreen(Vector pos)
    {
        return (pos - position) * zoomScale;
    }

    public Vector ScreenToWorld(Vector pos)
    {
        return (pos / zoomScale) + position;
    }

    public bool nodeOnScreen(Node n)
        => n.position.x + n.size.x >= position.x
        && position.x + size.x >= n.position.x
        && n.position.y + n.size.y >= position.y
        && position.y + size.y >= n.position.y;

    public void unscroll()
    {
        position = Vector.zero;
    }
    public void scroll(int dirX, int dirY)
    {
        int width = DisplayManager.MAX_WIDTH + DisplayManager.BUFFER_WIDTH * 3;
        position.x += dirX * width;
        if (position.x < 0)
        {
            position.x = 0;
        }
        int maxPosX = (Managers.Node.containers.Count - 1) * width;
        if (position.x > maxPosX)
        {
            position.x = maxPosX;
        }
        position.y += dirY * (50);
        if (position.y < 0)
        {
            position.y = 0;
        }
        int maxPosY = Managers.Node.containers.Max(c => c.size.y) - 50;
        if (position.y > maxPosY)
        {
            position.y = maxPosY;
        }
    }
}
