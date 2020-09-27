using DialogueEditor;
using DialogueEditor.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class ControlManager
{
    private List<Control> selectedNodes = new List<Control>();
    public ControlManager()
    {
    }

    /// <summary>
    /// Selects the given NodeDialogue or Node subtype.
    /// Appends if the SHIFT key is held down.
    /// </summary>
    /// <param name="control"></param>
    public void select(Control control)
    {
        //2020-09-27: copied from https://stackoverflow.com/a/973733/2336212
        select(control, Control.ModifierKeys == Keys.Shift);
    }

    /// <summary>
    /// Selects the given NodeDialogue or Node subtype
    /// </summary>
    /// <param name="control"></param>
    /// <param name="append">true to add to list, false to overwrite it</param>
    public void select(Control control, bool append)
    {
        if (control is NodeDialogue || control is Node)
        {
            if (!append)
            {
                selectedNodes.ForEach(c => c.BackColor = Managers.Colors.unselectColor);
                selectedNodes.Clear();
            }
            if (!selectedNodes.Contains(control))
            {
                control.BackColor = Managers.Colors.selectColor;
                selectedNodes.Add(control);
            }
        }
    }

    public void deselect(Control control)
    {
        control.BackColor = Managers.Colors.unselectColor;
        selectedNodes.Remove(control);
    }

    public void processSelectedNodes(Action<Control> action)
    {
        selectedNodes.ForEach(c => action(c));
    }

    public Control ActiveControl => selectedNodes.FirstOrDefault();

    public Node ActiveNode => (Node)selectedNodes.FirstOrDefault(c => c is Node);

    public NodeQuote ActiveNodeQuote => (NodeQuote)selectedNodes.FirstOrDefault(c => c is NodeQuote);

    public NodeDialogue ActivePanel => (NodeDialogue)selectedNodes.FirstOrDefault(c => c is NodeDialogue);

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
        //Create a new quote
        NodeDialogue container = ActivePanel;
        if (container != null)
        {
            NodeQuote node = Managers.Node.createNodeQuote(container.path);
            node.Editing = true;
            return;
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
            return;
        }
        //Else
        createDialoguePath();
    }

    public void createCondition()
    {
        //Create a new condition
        NodeDialogue container = ActivePanel;
        if (container != null)
        {
            NodeCondition node = Managers.Node.createNodeCondition(container.path);
            return;
        }
        Node activeNode = ActiveNode;
        if (activeNode != null)
        {
            Node node = Managers.Node.createNodeCondition(
                activeNode.data.path
                );
            return;
        }
        //Else
        DialoguePath path = createDialoguePath();
        Managers.Node.createNodeCondition(path);
    }

    public void createAction()
    {
        //Create a new action
        NodeDialogue container = ActivePanel;
        if (container != null)
        {
            NodeAction node = Managers.Node.createNodeAction(container.path);
            return;
        }
        Node activeNode = ActiveNode;
        if (activeNode != null)
        {
            NodeAction node = Managers.Node.createNodeAction(
                activeNode.data.path
                );
            return;
        }
        //Else
        DialoguePath path = createDialoguePath();
        Managers.Node.createNodeAction(path);
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
                if (((NodeQuote)activeNode).Editing)
                {
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

