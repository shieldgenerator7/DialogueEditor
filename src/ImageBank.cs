using System;
using System.Collections.Generic;
using System.Drawing;

public class ImageBank
{
    private readonly Dictionary<string, Image> images = new Dictionary<string, Image>();
    public Image getImage(string filename)
    {
        if (images.ContainsKey(filename))
        {
            return images[filename];
        }
        else
        {
            images.Add(filename, Image.FromFile(filename));
            return images[filename];
        }
    }
}
