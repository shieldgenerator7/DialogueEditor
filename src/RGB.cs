using System;

public struct RGB
{
    public int red;
    public int green;
    public int blue;

    public RGB(int r, int g, int b)
    {
        this.red = r;
        this.green = g;
        this.blue = b;
    }

    public static RGB white = new RGB(255, 255, 255);
    public static RGB black = new RGB(0, 0, 0);

    public static RGB nullRGB = new RGB(-1, -1, -1);
    public static RGB tempRGB = new RGB(-2, -2, -2);

    public bool isValid()
    {
        //Cast to boolean ensures all values are between 0 and 255, inclusive
        return this==true;
    }

    public override string ToString()
    {
        return "RGB: (" + red + ", " + green + ", " + blue + ")";
    }

    public override bool Equals(object obj)
    {
        return this == (RGB)obj;
    }

    public override int GetHashCode()
    {
        return red * 1000000 + green * 1000 + blue * 1;
    }

    public static bool operator ==(RGB a, RGB b)
    {
        return a.red == b.red && a.green == b.green && a.blue == b.blue;
    }
    public static bool operator !=(RGB a, RGB b)
    {
        return a.red != b.red || a.green != b.green || a.blue != b.blue;
    }

    public static implicit operator bool(RGB a)
    {
        return a.red >= 0 && a.red <= 255
            && a.green >= 0 && a.green <= 255
            && a.blue >= 0 && a.blue <= 255;
    }
}
