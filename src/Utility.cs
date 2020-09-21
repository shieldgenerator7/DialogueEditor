using System;
using System.Drawing;

public static class Utility
{
    public static Vector toVector(this Point point)
    {
        return new Vector(point);
    }

    public static Vector toVector(this Size size)
    {
        return new Vector(size);
    }

    public static Point toPoint(this Vector vector)
    {
        return new Point(vector.x,vector.y);
    }

    public static Size toSize(this Vector vector)
    {
        return new Size(vector.x, vector.y);
    }
}
