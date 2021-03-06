﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public class NodeManager
    {
        public const int BUFFER_NODE = 10;
        public const int BUFFER_CONTAINER = 20;

        public DialogueData dialogueData { get; private set; } = new DialogueData();
        public Panel dialoguePanel;
        public List<NodeDialogue> containers = new List<NodeDialogue>();

        /// <summary>
        /// Creates a UI Node and a Quote,
        /// Also creates a DialoguePath if none is provided
        /// </summary>
        /// <param name="path"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public NodeQuote createNodeQuote(DialoguePath path = null, int index = -1)
        {
            NodeDialogue container;
            //If no path,
            if (path == null)
            {
                //create a path
                container = createContainerNode();
                path = container.path;
            }
            else
            {
                container = containers.First(cn => cn.path == path);
            }
            //Add a node to the path
            Quote quote = new Quote();
            quote.path = path;
            if (index < 0)
            {
                path.quotes.Add(quote);
            }
            else
            {
                path.quotes.Insert(index, quote);
            }
            if (quote.Index >= 2)
            {
                Quote prevQuote = quote.path.quotes[quote.Index - 2];
                quote.characterName = prevQuote.characterName;
                quote.imageFileName = prevQuote.imageFileName;
            }
            NodeQuote node = new NodeQuote(quote);
            container.AddNode(node);
            return node;
        }

        /// <summary>
        /// This method makes a UI node for the given quote in the given dialogue path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="quote"></param>
        /// <returns></returns>
        public NodeQuote createNodeQuote(DialoguePath path, Quote quote)
        {
            NodeQuote node = new NodeQuote(quote);
            NodeDialogue container = containers.First(cn => cn.path == path);
            container.AddNode(node);
            return node;
        }

        /// <summary>
        /// Creates a new node for each string in the given array
        /// Returns the last created node
        /// </summary>
        /// <param name="path"></param>
        /// <param name="textArray"></param>
        public NodeQuote createNodes(DialoguePath path, string[] textArray)
        {
            if (path == null)
            {
                path = createContainerNode().path;
            }
            NodeQuote lastNode = null;
            foreach (string text in textArray)
            {
                NodeQuote node = createNodeQuote(path);
                node.Text = text;
                lastNode = node;
            }
            return lastNode;
        }

        public NodeCondition createNodeCondition(DialoguePath path = null, Condition condition = null)
        {
            //If no path,
            if (path == null)
            {
                //create a path
                path = createContainerNode().path;
            }
            NodeDialogue container = containers.First(cn => cn.path == path);
            //Add a node to the path
            if (condition == null)
            {
                //Find template
                Condition template = null;
                if (containers.Count > 0)
                {
                    List<NodeDialogue> templateContainers = containers
                       .Where(c => c.path.conditions.Count > 0).ToList();
                    if (templateContainers.Count > 0)
                    {
                        template = templateContainers.Last()
                            .path.conditions[0];
                    }
                }
                //Create condition
                if (template != null)
                {
                    condition = new Condition(
                        template.variableName,
                        template.testType,
                        template.testValue + 1
                        );
                }
                else
                {
                    condition = new Condition(dialogueData.Variables.Last());
                }
                condition.path = path;
                path.conditions.Add(condition);
            }
            NodeCondition node = new NodeCondition(condition);
            container.AddNode(node);
            return node;
        }

        public NodeAction createNodeAction(DialoguePath path = null, Action action = null)
        {
            //If no path,
            if (path == null)
            {
                //create a path
                path = createContainerNode().path;
            }
            NodeDialogue container = containers.First(cn => cn.path == path);
            //Add a node to the path
            if (action == null)
            {
                //Set default variable name 
                string defaultVarName = dialogueData.Variables.Last();
                if (path.conditions.Count > 0)
                {
                    defaultVarName = path.conditions[0].variableName;
                }
                //Create new action
                action = new Action(defaultVarName);
                action.path = path;
                path.actions.Add(action);
            }
            NodeAction node = new NodeAction(action);
            container.AddNode(node);
            return node;
        }

        public NodeDialogue createContainerNode(DialoguePath path = null)
        {
            if (path == null)
            {
                path = new DialoguePath();
                dialogueData.dialogues.Add(path);
            }
            NodeDialogue container = new NodeDialogue(path);
            containers.Add(container);
            dialoguePanel.Controls.Add(container);
            return container;
        }

        public void acceptInfoFromFile(DialogueData dialogueData, bool append = false)
        {
            Managers.Form.saveScroll();
            dialoguePanel.SuspendLayout();
            //
            clearNodes();
            if (append)
            {
                this.dialogueData.append(dialogueData);
            }
            else
            {
                this.dialogueData = dialogueData;
            }
            this.dialogueData.dialogues.ForEach(d => d.inflate());
            populateNodes(this.dialogueData.dialogues);
            //
            dialoguePanel.ResumeLayout();
            Managers.Form.restoreScroll();
        }

        public void clear()
        {
            clearNodes();
            Managers.Node.dialogueData = new DialogueData();
        }

        /// <summary>
        /// Clears all UI nodes
        /// </summary>
        public void clearNodes()
        {
            Managers.Control.deselectAll();
            containers.ForEach(cn => cn.Dispose());
            containers.Clear();
        }

        /// <summary>
        /// Delete the given NodeDialogue or Node
        /// </summary>
        /// <param name="c"></param>
        public void delete(Control c)
        {
            if (c is Node)
            {
                Node node = (Node)c;
                node.data.path.remove(node.data);
                node.Dispose();
            }
            else if (c is NodeDialogue)
            {
                NodeDialogue container = (NodeDialogue)c;
                dialogueData.dialogues.Remove(container.path);
                containers.Remove(container);
                container.Dispose();
            }
        }

        /// <summary>
        /// Creates nodes for the dialogue paths and quotes in the list
        /// </summary>
        /// <param name="dialogues"></param>
        public void populateNodes(List<DialoguePath> dialogues)
        {
            dialogues.ForEach(
                d =>
                {
                    createContainerNode(d);
                    d.conditions.ForEach(
                        c => createNodeCondition(d, c)
                        );
                    d.quotes.ForEach(
                        q => createNodeQuote(d, q)
                        );
                    d.actions.ForEach(
                        a => createNodeAction(d, a)
                        );
                }
                );
        }

        public void filterCharacters(List<string> characters)
        {
            List<DialoguePath> filteredPaths = dialogueData.dialogues.Where(
                d => d.allCharactersPresent(characters)
                ).ToList();
            containers.ForEach(
                cn =>
                {
                    if (filteredPaths.Contains(cn.path))
                    {
                        cn.Show();
                    }
                    else
                    {
                        cn.Hide();
                    }
                }
                );
        }

        public void setDefaultImageFileName(string character, string imageFileName)
        {
            if (imageFileName == null || imageFileName == "")
            {
                return;
            }
            dialogueData.dialogues.ForEach(
                d =>
                {
                    d.quotes.ForEach(
                        q =>
                        {
                            if (q.characterName == character)
                            {
                                if (q.imageFileName == null || q.imageFileName == "")
                                {
                                    q.imageFileName = imageFileName;
                                }
                            }
                        }
                        );
                }
                );
            refreshNodeImages();
        }

        public void refreshNodeImages()
        {
            containers.ForEach(
                container =>
                {
                    foreach (Control c in container.Controls)
                    {
                        if (c is NodeQuote)
                        {
                            ((NodeQuote)c).refreshImage();
                        }
                    }
                }
                );
        }

        public bool empty
            => containers.Count == 0 || containers.All(container => container.empty());
    }
}
