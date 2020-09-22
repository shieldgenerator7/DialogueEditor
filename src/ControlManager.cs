using DialogueEditor;
using DialogueEditor.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class ControlManager
{
    public bool isMouseDown { get; private set; } = false;
    public Node selected { get; private set; } = null;
    public Node mousedOver { get; private set; } = null;
    public Vector mousePos { get; private set; }
    public bool isMouseHover { get; private set; } = false;
    public Vector origMousePos { get; private set; }

    public ControlManager()
    {
    }

    public void mouseDown()
    {
        this.isMouseDown = true;
        this.isMouseHover = false;
        origMousePos = mousePos;
        selected?.editNode(false);
        if (mousedOver)
        {
            selected = mousedOver;
        }
        selected?.pickup(mousePos);
    }

    public bool mouseMove(Vector mousePos)
    {
        this.mousePos = mousePos;
        if (isMouseDown)
        {
            if (!selected)
            {
                selected = mousedOver;
            }
            if (selected)
            {
                selected.moveTo(mousePos);
                selected.editNode(false);
            }
            return true;
        }
        else
        {
            Node prevMousedOver = mousedOver;
            mousedOver = null;
            if (!mousedOver)
            {
                mousedOver = Managers.Node.getNodeAtPosition(mousePos);
                mousedOver?.pickup(mousePos);
            }
            Cursor neededCursor = Cursor.Current;
            if (mousedOver)
            {
                neededCursor = Cursors.Hand;
            }
            else
            {
                neededCursor = Cursors.Default;
            }
            if (Cursor.Current != neededCursor)
            {
                Cursor.Current = neededCursor;
            }
            if (prevMousedOver != mousedOver)
            {
                return true;
            }
        }
        return false;
    }

    public void mouseUp()
    {
        this.isMouseDown = false;
        //if (selected)
        //{
        //    selected.editNode(false);
        //}
        //selected = null;
    }

    public void mouseHover()
    {
        this.isMouseHover = true;
    }

    public void mouseDoubleClick()
    {
        if (selected)
        {
            if (selected is ContainerNode)
            {
                Node node = Managers.Node.getIndexAtPosition(mousePos);
                if (node)
                {
                    Node newNode = Managers.Node.createNode(
                        node.quote.path,
                        node.quote.Index
                        );
                    selected = newNode;
                    newNode.editNode(true);
                }
            }
            else
            {
                selected.editNode(true);
            }
        }
        else
        {
        }
        //bool changedObjectState = false;
        //checkTrayDoubleClick(Managers.Command, mousePos);

        //Find an object to change its state
        //Node node = Managers.Node.getNodeAtPosition(mousePos);
        //if (node && node.gameObject.canChangeState() && node.gameObject.Permissions.canInteract)
        //{
        //    //changedObjectState = true;
        //    node.gameObject.changeState();
        //}
    }

    public void createDialoguePath()
    {
        //Create a new quote with no path,
        //which will auto-create a new path with a new quote
        selected = Managers.Node.createNode();
        selected.editNode(true);
    }

    public void createQuote()
    {
        //Create a new quote
        if (selected)
        {
            if (selected is ContainerNode)
            {
                ContainerNode container = (ContainerNode)selected;
                selected = Managers.Node.createNode(container.path);
                selected.editNode();
            }
            else
            {
                Node node = selected;
                selected = Managers.Node.createNode(
                    node.quote.path,
                    node.quote.Index
                    );
                selected.editNode();
            }
        }
        else
        {
            createDialoguePath();
        }
    }

    public void enterPressed()
    {
        if (selected)
        {
            selected.editNode(false);
            if (selected is ContainerNode)
            {
                //do nothing else
            }
            else
            {
                //If it's the last in the path,
                DialoguePath path = selected.quote.path;
                if (selected.quote == path.quotes[path.quotes.Count - 1])
                {
                    //Add new node at the end
                    selected = Managers.Node.createNode(selected.quote.path);
                    selected.editNode(true);
                }
            }
        }
    }

    public void escapePressed()
    {
        if (selected)
        {
            //If editing current node,
            if (selected.Editing)
            {
                //Stop editing it
                selected.editNode(false);
            }
            else
            {
                //Else just stop selecting it
                selected = null;
            }
        }
    }

    public bool deletePressed()
    {
        if (selected && !selected.Editing)
        {
            selected.dispose();
            Managers.Node.nodes.Remove(selected);
            selected = null;
            return true;
        }
        return false;
    }

    public void setMousedOver(object sender, EventArgs e)
    {
        mousedOver = ((NodeLabel)sender).node;
        Managers.Form.Refresh();
    }

    public void setSelected(object sender, EventArgs e)
    {
        selected?.editNode(false);
        selected = ((NodeLabel)sender).node;
        Managers.Form.Refresh();
    }

    public void processDoubleClick(object sender, EventArgs e)
    {
        selected?.editNode(false);
        selected = ((NodeLabel)sender).node;
        selected.editNode(true);
        Managers.Form.Refresh();
    }

    public void receiveInfoDump(DialoguePath path, string[] textArray)
    {
        Node lastNode = Managers.Node.createNodes(path, textArray);
        selected?.editNode(false);
        lastNode.editNode(true);
        selected = lastNode;
    }
}

