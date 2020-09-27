using DialogueEditor;
using DialogueEditor.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class ControlManager
{

    public ControlManager()
    {
    }

    public Control ActiveControl
    {
        get
        {
            Control activeControl = Managers.Form.ActiveControl;
            if (activeControl == null)
            {
                return null;
            }
            while (!(activeControl is Node)
                && !(activeControl is NodeDialogue)
                && activeControl.Parent != null)
            {
                activeControl = activeControl.Parent;
            }
            return activeControl;
        }
    }

    public Node ActiveNode
    {
        get
        {
            Control activeControl = ActiveControl;
            if (activeControl is Node)
            {
                return (Node)activeControl;
            }
            return null;
        }
    }

    public NodeQuote ActiveNodeQuote
    {
        get
        {
            Control activeControl = ActiveControl;
            if (activeControl is NodeQuote)
            {
                return (NodeQuote)activeControl;
            }
            return null;
        }
    }

    public NodeDialogue ActivePanel
    {
        get
        {
            Control activeControl = ActiveControl;
            if (activeControl is NodeDialogue)
            {
                return (NodeDialogue)activeControl;
            }
            return null;
        }
    }

    public DialoguePath createDialoguePath()
    {
        //Create a new quote with no path,
        //which will auto-create a new path with a new quote
        NodeQuote node = Managers.Node.createNodeQuote();
        node.Editing = true;
        return node.quote.path;
    }

    public void createQuote()
    {
        Control activeControl = ActiveControl;
        //Create a new quote
        if (activeControl != null)
        {
            NodeDialogue container = ActivePanel;
            if (container != null)
            {
                NodeQuote node = Managers.Node.createNodeQuote(container.path);
                node.Editing = true;
            }
            Node activeNode = ActiveNode;
            if (activeNode != null)
            {
                int index = (activeNode is NodeQuote)
                    ? ((NodeQuote)activeNode).quote.Index
                    : -1;
                NodeQuote node = Managers.Node.createNodeQuote(
                    activeNode.data.path,
                    index
                    );
                node.Editing = true;
            }
        }
        else
        {
            createDialoguePath();
        }
    }

    public void createCondition()
    {
        Control activeControl = ActiveControl;
        //Create a new quote
        if (activeControl != null)
        {
            NodeDialogue container = ActivePanel;
            if (container != null)
            {
                NodeCondition node = Managers.Node.createNodeCondition(container.path);
            }
            Node activeNode = ActiveNode;
            if (activeNode != null)
            {
                Node node = Managers.Node.createNodeCondition(
                    activeNode.data.path
                    );
            }
        }
        else
        {
            DialoguePath path = createDialoguePath();
            Managers.Node.createNodeCondition(path);
        }
    }

    public void createAction()
    {
        Control activeControl = ActiveControl;
        //Create a new quote
        if (activeControl != null)
        {
            NodeDialogue container = ActivePanel;
            if (container != null)
            {
                NodeAction node = Managers.Node.createNodeAction(container.path);
            }
            Node activeNode = ActiveNode;
            if (activeNode != null)
            {
                NodeAction node = Managers.Node.createNodeAction(
                    activeNode.data.path
                    );
            }
        }
        else
        {
            DialoguePath path = createDialoguePath();
            Managers.Node.createNodeCondition(path);
        }
    }

    public void enterPressed()
    {
        NodeQuote activeNode = ActiveNodeQuote;
        if (activeNode != null)
        {
            activeNode.Editing = false;
            //If it's the last in the path,
            DialoguePath path = activeNode.quote.path;
            if (activeNode.quote.Index == path.quotes.Count - 1)
            {
                //Add new node at the end
                NodeQuote newNode = Managers.Node.createNodeQuote(activeNode.quote.path);
                newNode.Editing = true;
            }
        }
    }

    public void escapePressed()
    {
        NodeQuote activeNode = ActiveNodeQuote;
        if (activeNode != null)
        {
            //Stop editing it
            activeNode.Editing = false;
        }
    }

    /// <summary>
    /// Deletes the active control if it's a NodeLabel or NodePanel
    /// </summary>
    /// <returns>true if deleted, false if not deleted</returns>
    public bool deletePressed()
    {
        Node activeNode = ActiveNode;
        if (activeNode != null)
        {
            bool canDelete = true;
            if (activeNode is NodeQuote)
            {
                //if the user is editing the node,
                if (((NodeQuote)activeNode).Editing){
                    //don't delete it
                    canDelete = false;
                }
            }
            if (canDelete)
            {
                activeNode.data.path.remove(activeNode.data);
                activeNode.Dispose();
                return true;
            }
        }
        NodeDialogue activePanel = ActivePanel;
        if (activePanel != null)
        {
            DialogResult dr = MessageBox.Show(
                "Are you sure you want to delete dialogue path \""
                + activePanel.path.title + "\"?",
                "Delete?",
                MessageBoxButtons.OKCancel
                );
            if (dr == DialogResult.OK)
            {
                Managers.Node.dialogueData.dialogues.Remove(activePanel.path);
                Managers.Node.containers.Remove(activePanel);
                activePanel.Dispose();
                return true;
            }
        }
        return false;
    }

    public void receiveInfoDump(DialoguePath path, string[] textArray)
    {
        NodeQuote lastNode = Managers.Node.createNodes(path, textArray);
        NodeQuote activeNode = ActiveNodeQuote;
        if (activeNode != null)
        {
            activeNode.Editing = false;
        }
        lastNode.Editing = true;
    }
}

