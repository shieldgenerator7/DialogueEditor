using System;
using System.Drawing;

public struct Vector
{
    public int x;
    public int y;

    public Vector(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector(Vector vector) : this(vector.x, vector.y) { }
    public Vector(Point point) : this(point.X, point.Y) { }
    public Vector(Size size) : this(size.Width, size.Height) { }

    public float Magnitude
    {
        get
        {
            return (float)Math.Sqrt((x * x) + (y * y));
        }
    }

    public static Vector zero
    {
        get => new Vector(0, 0);
    }
    public static Vector up
    {
        get => new Vector(0, -1);
    }
    public static Vector down
    {
        get => new Vector(0, 1);
    }
    public static Vector left
    {
        get => new Vector(-1, 0);
    }
    public static Vector right
    {
        get => new Vector(1, 0);
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }

    public static Vector operator -(Vector a)
        => new Vector(-a.x, -a.y);

    public static Vector operator +(Vector a, Vector b)
        => new Vector(a.x + b.x, a.y + b.y);

    public static Vector operator -(Vector a, Vector b)
        => new Vector(a.x - b.x, a.y - b.y);

    public static Vector operator *(Vector v, int f)
        => new Vector(v.x * f, v.y * f);

    public static Vector operator /(Vector v, int f)
        => new Vector(v.x / f, v.y / f);

    public static bool operator <(Vector a, Vector b)
        => a.Magnitude < b.Magnitude;

    public static bool operator >(Vector a, Vector b)
        => a.Magnitude > b.Magnitude;

    public static bool operator ==(Vector a, Vector b)
    {
        bool aNull = ReferenceEquals(a, null);
        bool bNull = ReferenceEquals(b, null);
        if (aNull || bNull)
        {
            return aNull == bNull;
        }
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(Vector a, Vector b)
    {
        bool aNull = ReferenceEquals(a, null);
        bool bNull = ReferenceEquals(b, null);
        if (aNull || bNull)
        {
            return aNull != bNull;
        }
        return a.x != b.x || a.y != b.y;
    }
}
