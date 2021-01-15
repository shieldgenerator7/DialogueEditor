using DialogueEditor;
using DialogueEditor.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class ControlManager
{
    private List<Node> selectedNodes = new List<Node>();
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
        select(node, Control.ModifierKeys == Keys.Shift);
    }

    /// <summary>
    /// Selects the given NodeDialogue or Node subtype
    /// </summary>
    /// <param name="node"></param>
    /// <param name="append">true to add to list, false to overwrite it</param>
    public void select(Node node, bool append)
    {
        if (node is NodeDialogue || node is Node)
        {
            if (!append)
            {
                //selectedNodes.ForEach(c => c.BackColor = Managers.Colors.unselectColor);
                selectedNodes.Clear();
            }
            if (!selectedNodes.Contains(node))
            {
                //node.BackColor = Managers.Colors.selectColor;
                selectedNodes.Add(node);
            }
        }
    }

    public void deselect(Node node)
    {
        //node.BackColor = Managers.Colors.unselectColor;
        selectedNodes.Remove(node);
        if (node is NodeQuote)
        {
            ((NodeQuote)node).Editing = false;
        }
    }

    /// <summary>
    /// Deselect everything
    /// </summary>
    public void deselectAll()
    {
        for (int i = selectedNodes.Count - 1; i >= 0; i--)
        {
            deselect(selectedNodes[i]);
        }
    }

    public void processSelectedNodes(Action<Node> action)
    {
        selectedNodes.ForEach(n => action(n));
    }
        
    public DialoguePath createDialoguePath()
    {
        //Create a new quote with no path,
        //which will auto-create a new path with a new quote
        NodeQuote node = Managers.Node.createNodeQuote();
        node.Editing = true;
        select(node);
        return node.quote.path;
    }

    public void createQuote()
    {
        List<Node> prevSelected = new List<Node>(selectedNodes);
        deselectAll();
        prevSelected.ForEach(
            c =>
            {
                DialoguePath path = (c is NodeDialogue)
                    ? path = ((NodeDialogue)c).path
                    : path = c.data.path;
                int index = (c is NodeQuote)
                    ? ((NodeQuote)c).quote.Index
                    : -1;
                NodeQuote node = Managers.Node.createNodeQuote(path, index);
                node.Editing = true;
                select(node, true);
            }
            );
        if (prevSelected.Count == 0)
        {
            //If nothing is selected, make a new dialogue path
            //(which will auto-create a new quote too)
            createDialoguePath();
        }
    }

    public void createCondition()
    {
        List<Node> prevSelected = new List<Node>(selectedNodes);
        deselectAll();
        prevSelected.ForEach(
            c =>
            {
                DialoguePath path = (c is NodeDialogue)
                    ? path = ((NodeDialogue)c).path
                    : path = ((Node)c).data.path;
                NodeCondition node = Managers.Node.createNodeCondition(path);
                select(node, true);
            }
            );
        if (prevSelected.Count == 0)
        {
            //If nothing is selected, make a new dialogue path
            //(which will auto-create a new quote too)
            DialoguePath path = createDialoguePath();
            Managers.Node.createNodeCondition(path);
        }
    }

    public void createAction()
    {
        List<Node> prevSelected = new List<Node>(selectedNodes);
        deselectAll();
        prevSelected.ForEach(
            c =>
            {
                DialoguePath path = (c is NodeDialogue)
                    ? path = ((NodeDialogue)c).path
                    : path = ((Node)c).data.path;
                NodeAction node = Managers.Node.createNodeAction(path);
                select(node, true);
            }
            );
        if (prevSelected.Count == 0)
        {
            //If nothing is selected, make a new dialogue path
            //(which will auto-create a new quote too)
            DialoguePath path = createDialoguePath();
            Managers.Node.createNodeAction(path);
        }
    }

    public void enterPressed()
    {
        NodeQuote activeNode = (NodeQuote)selectedNodes.FirstOrDefault(
            c => c is NodeQuote
            );
        if (activeNode != null)
        {
            activeNode.Editing = false;
            //If it's the last in the path,
            DialoguePath path = activeNode.quote.path;
            if (activeNode.quote.Index == path.quotes.Count - 1)
            {
                //Add new node at the end
                NodeQuote newNode = Managers.Node.createNodeQuote(path);
                newNode.Editing = true;
                select(newNode);
            }
        }
    }

    public void escapePressed()
    {
        deselectAll();
    }

    /// <summary>
    /// Deletes the active control if it's a NodeLabel or NodePanel
    /// </summary>
    /// <returns>true if deleted, false if not deleted</returns>
    public bool deletePressed()
    {

        //Don't delete anything if a selected node is being edited
        if (!selectedNodes.Any(c => c is NodeQuote && ((NodeQuote)c).Editing))
        {
            bool canDeleteAll = true;
            List<Node> dialogues = selectedNodes.FindAll(n => n is NodeDialogue);
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
                selectedNodes.ForEach(n => Managers.Node.delete(n));
                selectedNodes.Clear();
                return true;
            }
        }
        return false;
    }

    public void receiveInfoDump(DialoguePath path, string[] textArray)
    {
        NodeQuote lastNode = Managers.Node.createNodes(path, textArray);
        lastNode.Editing = true;
        select(lastNode);
    }
}

