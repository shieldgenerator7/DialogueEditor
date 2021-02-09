using DialogueEditor;
using DialogueEditor.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class ControlManager
{
    private Vector mousePos;
    private Vector mousePosWorld;
    /// <summary>
    /// The position of the mouse in screen coordinates
    /// </summary>
    public Vector MousePos
    {
        get => mousePos;
        set
        {
            mousePos = value;
            mousePosWorld = Managers.Camera.ScreenToWorld(mousePos);
        }
    }

    public ControlManager()
    {
    }

    /// <summary>
    /// Selects the given NodeDialogue or Node subtype.
    /// Appends if the SHIFT key is held down.
    /// </summary>
    /// <param name="node"></param>
    public void select(Node node)
    {
        //2020-09-27: copied from https://stackoverflow.com/a/973733/2336212
        Managers.Select.select(node, Control.ModifierKeys == Keys.Shift);
    }

    public void click()
    {
        Node n = Managers.Node.getNode(mousePosWorld);
        select(n);
    }

    public void doubleClick()
    {
        Node n = Managers.Node.getNode(mousePosWorld);
        Managers.Select.EditNode = n;
    }

    public DialoguePath createDialoguePath()
    {
        //Create a new quote with no path,
        //which will auto-create a new path with a new quote
        NodeQuote node = Managers.Node.createNodeQuote();
        select(node);
        Managers.Select.EditNode = node;
        return node.data.path;
    }

    public void createQuote()
    {
        bool anyPrevSelected = Managers.Select.processPrevSelectedNodes(
            n =>
            {
                DialoguePath path = n.Path;
                int index = (n is NodeQuote quote)
                    ? quote.quote.Index
                    : -1;
                NodeQuote node = Managers.Node.createNodeQuote(path, index);
                Managers.Select.select(node, true);
                Managers.Select.EditNode = node;
            });
        if (!anyPrevSelected)
        {
            //If nothing is selected, make a new dialogue path
            //(which will auto-create a new quote too)
            createDialoguePath();
        }
    }

    public void createCondition()
    {
        bool anyPrevSelected = Managers.Select.processPrevSelectedNodes(
            n =>
            {
                DialoguePath path = n.Path;
                NodeCondition node = Managers.Node.createNodeCondition(path);
                Managers.Select.select(node, true);
            }
            );
        if (!anyPrevSelected)
        {
            //If nothing is selected, make a new dialogue path
            //(which will auto-create a new quote too)
            DialoguePath path = createDialoguePath();
            Managers.Node.createNodeCondition(path);
        }
    }

    public void createAction()
    {
        bool anyPrevSelected = Managers.Select.processPrevSelectedNodes(
            n =>
            {
                DialoguePath path = n.Path;
                NodeAction node = Managers.Node.createNodeAction(path);
                Managers.Select.select(node, true);
            }
            );
        if (!anyPrevSelected)
        {
            //If nothing is selected, make a new dialogue path
            //(which will auto-create a new quote too)
            DialoguePath path = createDialoguePath();
            Managers.Node.createNodeAction(path);
        }
    }

    public void enterPressed()
    {
        if (Managers.Select.EditNode)
        {
            Managers.Select.EditNode = null;
        }
        else
        {
            NodeQuote activeNode = Managers.Select.getOne<NodeQuote>();
            if (activeNode != null)
            {
                Managers.Select.EditNode = null;
                //If it's the last in the path,
                DialoguePath path = activeNode.quote.path;
                if (activeNode.quote.Index == path.quotes.Count - 1)
                {
                    //Add new node at the end
                    NodeQuote newNode = Managers.Node.createNodeQuote(path);
                    select(newNode);
                    Managers.Select.EditNode = newNode;
                }
                //Else edit this node
                else
                {
                    Managers.Select.EditNode = activeNode;
                }
            }
        }
    }

    public void escapePressed()
    {
        Managers.Select.deselectAll();
    }

    /// <summary>
    /// Deletes the active control if it's a NodeLabel or NodePanel
    /// </summary>
    /// <returns>true if deleted, false if not deleted</returns>
    public bool deletePressed()
    {

        //Don't delete anything if a selected node is being edited
        if (!Managers.Select.EditNode)
        {
            bool canDeleteAll = true;
            List<NodeDialogue> dialogues = Managers.Select.getAll<NodeDialogue>();
            if (dialogues.Count > 0)
            {
                string titles = "";
                dialogues.ForEach(
                    d => titles += "\"" + ((NodeDialogue)d).path.title + "\", "
                    );
                titles = titles.Substring(0, titles.Length - 2);
                DialogResult dr = MessageBox.Show(
                    "Are you sure you want to delete these dialogue paths?\n" + titles,
                    "Delete?",
                    MessageBoxButtons.OKCancel
                    );
                if (dr != DialogResult.OK)
                {
                    canDeleteAll = false;
                }
            }
            if (canDeleteAll)
            {
                Managers.Select.processPrevSelectedNodes(n => Managers.Node.delete(n));
                return true;
            }
        }
        return false;
    }

    public void receiveInfoDump(DialoguePath path, string[] textArray)
    {
        NodeQuote lastNode = Managers.Node.createNodes(path, textArray);
        select(lastNode);
        Managers.Select.EditNode = lastNode;
    }
}

