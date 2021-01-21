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
        if (n is NodeDialogue dialogue)
        {
            layoutNode(dialogue);
        }
        if (n is NodeQuote quote)
        {
            layoutNode(quote);
        }
        else if (n is NodeCondition condition)
        {
            layoutNode(condition);
        }
        else if (n is NodeAction action)
        {
            layoutNode(action);
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
        nd.size.y += DisplayManager.BUFFER_WIDTH;
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
    private void layoutNode(NodeAction na)
    {
        na.position = cursor;
        int buffer = DisplayManager.BUFFER_WIDTH / 5;
        na.txtVariableName.position = na.position;
        na.txtVariableName.size = Managers.Display.measureString(na.txtVariableName);
        na.txtActionType.position = na.txtVariableName.position
            + Vector.right * (na.txtVariableName.size.x + buffer);
        na.txtActionType.size = Managers.Display.measureString(na.txtActionType);
        na.txtActionValue.position = na.txtActionType.position
            + Vector.right * (na.txtActionType.size.x + buffer);
        na.txtActionValue.size = Managers.Display.measureString(na.txtActionValue);
        na.size.x = DisplayManager.MAX_WIDTH;
        na.size.y = Math.Max(
            na.txtVariableName.size.y,
            Math.Max(
                na.txtActionType.size.y,
                na.txtActionValue.size.y
            ));
        cursor.y += na.size.y + DisplayManager.BUFFER_WIDTH;
    }
}
