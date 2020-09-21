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
        selected = mousedOver;
        selected?.pickup(mousePos);
    }

    public bool mouseMove(Vector mousePosWorld)
    {
        this.mousePos = mousePosWorld;
        if (isMouseDown)
        {
            if (selected)
            {
                selected.moveTo(mousePosWorld);
            }
            return true;
        }
        else
        {
            Node prevMousedOver = mousedOver;
            mousedOver = null;
            if (!mousedOver)
            {
                mousedOver = Managers.Node.getNodeAtPosition(mousePosWorld);
                mousedOver?.pickup(mousePosWorld);
            }
            Cursor neededCursor = Cursor.Current;
            if (mousedOver)
            {
                neededCursor = Cursors.SizeAll;
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
        if (selected)
        {
        }
        selected = null;
    }

    public void mouseHover()
    {
        this.isMouseHover = true;
    }

    public void mouseDoubleClick()
    {
        if (selected)
        {
            selected?.pickup(mousePos);
        }
        else
        {
            //Create a new one
            Managers.Node.createNode(null, mousePos);
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
}

