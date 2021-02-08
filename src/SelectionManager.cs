﻿using DialogueEditor.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

public class SelectionManager
{
    private readonly List<Node> selectedNodes = new List<Node>();
    private Node editingNode = null;//the node that's currently being edited
    public Node EditNode
    {
        get => editingNode;
        set
        {
            editingNode = value;
            openEditNode(editingNode != null);
        }
    }

    private TextBox txtEdit;

    public SelectionManager(TextBox txtEdit)
    {
        this.txtEdit = txtEdit;
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
        if (node == EditNode)
        {
            EditNode = null;
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

    public T getOne<T>() where T : Node
        => (T)selectedNodes.FirstOrDefault(n => n is T);

    public List<T> getAll<T>() where T : Node
       => selectedNodes.FindAll(n => n is T).ConvertAll(n => (T)n);

    public void processSelectedNodes(Action<Node> action)
    {
        selectedNodes.ForEach(n => action(n));
    }

    /// <summary>
    /// Deselects all nodes, then processes the now-previously selected nodes.
    /// </summary>
    /// <param name="action"></param>
    /// <returns>True: there was at least 1 previously selected node</returns>
    public bool processPrevSelectedNodes(Action<Node> action)
    {
        List<Node> prevSelected = new List<Node>(selectedNodes);
        deselectAll();
        selectedNodes.ForEach(n => action(n));
        return prevSelected.Count > 0;
    }

    private void openEditNode(bool open)
    {
        if (open)
        {
            txtEdit.Location = EditNode.position;
            txtEdit.Size = EditNode.size;
            txtEdit.Visible = true;
            if (EditNode is NodeQuote nq)
            {
                txtEdit.Text = nq.QuoteText;
            }
        }
        else
        {
            txtEdit.Visible = false;
        }
    }
}
