using System;
using System.Drawing;

public class DisplayManager
{
	public DisplayManager()
	{
	}

	public void paint(Graphics g)
    {
		Managers.Node.containers.ForEach(c => c.paint(g));
    }
}
