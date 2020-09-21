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

    public static readonly Vector zero = new Vector(0, 0);

    public static readonly Vector up = new Vector(0, -1);

    public static readonly Vector down = new Vector(0, 1);

    public static readonly Vector left = new Vector(-1, 0);

    public static readonly Vector right = new Vector(1, 0);


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
