using DialogueEditor.src;
using System;

public class LayoutManager
{

    /// <summary>
    /// Where the next Node will be placed
    /// </summary>
    Vector cursor = new Vector(DisplayManager.BUFFER_WIDTH, DisplayManager.BUFFER_WIDTH);

    public void layoutNodes()
    {
        cursor = new Vector(DisplayManager.BUFFER_WIDTH, DisplayManager.BUFFER_WIDTH);
        Managers.Node.containers.ForEach(c => layoutNode(c));
    }

    private void layoutNode(Node n)
    {
        if (n is NodeDialogue)
        {
            layoutNode((NodeDialogue)n);
        }
        if (n is NodeQuote)
        {
            layoutNode((NodeQuote)n);
        }
        else if (n is NodeCondition)
        {
            layoutNode((NodeCondition)n);
        }
    }
    private void layoutNode(NodeDialogue nd)
    {
        nd.position = cursor;
        cursor.x += DisplayManager.BUFFER_WIDTH;
        cursor.y += DisplayManager.BUFFER_WIDTH;
        nd.size = Vector.zero;
        //Set size
        nd.Nodes.ForEach(n =>
        {
            layoutNode(n);
            nd.size.x = Math.Max(nd.size.x, n.size.x);
            nd.size.y += n.size.y + DisplayManager.BUFFER_WIDTH;
        });
        nd.size.x += DisplayManager.BUFFER_WIDTH * 2;
        nd.size.y -= DisplayManager.BUFFER_WIDTH;
        //Update cursor
        cursor.x += nd.size.x;
        cursor.y = DisplayManager.BUFFER_WIDTH;
    }
    private void layoutNode(NodeQuote nq)
    {
        nq.position = cursor;
        int imageWidth = DisplayManager.portraitSize + DisplayManager.BUFFER_WIDTH;
        nq.textBox.position = nq.position + Vector.right * (imageWidth);
        nq.textBox.size = Managers.Display.measureString(nq.textBox);
        nq.size.x = DisplayManager.MAX_WIDTH;
        nq.size.y = Math.Max(nq.textBox.size.y, DisplayManager.portraitSize);
        cursor.y += nq.size.y + DisplayManager.BUFFER_WIDTH;
    }
    private void layoutNode(NodeCondition nc)
    {
        nc.position = cursor;
        int buffer = DisplayManager.BUFFER_WIDTH / 5;
        nc.txtVariableName.position = nc.position;
        nc.txtVariableName.size = Managers.Display.measureString(nc.txtVariableName);
        nc.txtTestType.position = nc.txtVariableName.position
            + Vector.right * (nc.txtVariableName.size.x + buffer);
        nc.txtTestType.size = Managers.Display.measureString(nc.txtTestType);
        nc.txtTestValue.position = nc.txtTestType.position
            + Vector.right * (nc.txtTestType.size.x + buffer);
        nc.txtTestValue.size = Managers.Display.measureString(nc.txtTestValue);
        nc.size.x = DisplayManager.MAX_WIDTH;
        nc.size.y = Math.Max(
            nc.txtVariableName.size.y,
            Math.Max(
                nc.txtTestType.size.y,
                nc.txtTestValue.size.y
            ));
        cursor.y += nc.size.y + DisplayManager.BUFFER_WIDTH;
    }
}
