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
            while (!(activeControl is NodeLabel)
                && !(activeControl is NodePanel)
                && activeControl.Parent != null)
            {
                activeControl = activeControl.Parent;
            }
            return activeControl;
        }
    }

    public NodeLabel ActiveNode
    {
        get
        {
            Control activeControl = ActiveControl;
            if (activeControl is NodeLabel)
            {
                return (NodeLabel)activeControl;
            }
            return null;
        }
    }

    public NodePanel ActivePanel
    {
        get
        {
            Control activeControl = ActiveControl;
            if (activeControl is NodePanel)
            {
                return (NodePanel)activeControl;
            }
            return null;
        }
    }

    public void createDialoguePath()
    {
        //Create a new quote with no path,
        //which will auto-create a new path with a new quote
        NodeLabel node = Managers.Node.createNode();
        node.Editing = true;
    }

    public void createQuote()
    {
        Control activeControl = ActiveControl;
        //Create a new quote
        if (activeControl != null)
        {
            NodePanel container = ActivePanel;
            if (container != null)
            {
                NodeLabel node = Managers.Node.createNode(container.path);
                node.Editing = true;
            }
            NodeLabel activeNode = ActiveNode;
            if (activeNode != null)
            {
                NodeLabel node = Managers.Node.createNode(
                    activeNode.quote.path,
                    activeNode.quote.Index
                    );
                node.Editing = true;
            }
        }
        else
        {
            createDialoguePath();
        }
    }

    public void enterPressed()
    {
        NodeLabel activeNode = ActiveNode;
        if (activeNode != null)
        {
            activeNode.Editing = false;
            //If it's the last in the path,
            DialoguePath path = activeNode.quote.path;
            if (activeNode.quote.Index == path.quotes.Count - 1)
            {
                //Add new node at the end
                NodeLabel newNode = Managers.Node.createNode(activeNode.quote.path);
                newNode.Editing = true;
            }
        }
    }

    public void escapePressed()
    {
        NodeLabel activeNode = ActiveNode;
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
        NodeLabel activeNode = ActiveNode;
        if (activeNode != null)
        {
            if (!activeNode.Editing)
            {
                activeNode.Dispose();
                activeNode.Parent.Controls.Remove(activeNode);
                return true;
            }
        }
        NodePanel activePanel = ActivePanel;
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
                Managers.Node.containers.Remove(activePanel);
                activePanel.Parent.Controls.Remove(activePanel);
                activePanel.Dispose();
                return true;
            }
        }
        return false;
    }

    public void receiveInfoDump(DialoguePath path, string[] textArray)
    {
        NodeLabel lastNode = Managers.Node.createNodes(path, textArray);
        NodeLabel activeNode = ActiveNode;
        if (activeNode != null)
        {
            activeNode.Editing = false;
        }
        lastNode.Editing = true;
    }
}

