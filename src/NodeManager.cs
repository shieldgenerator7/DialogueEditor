using System;
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
            //If no path,
            if (path == null)
            {
                //create a path
                path = createContainerNode().path;
            }
            NodeDialogue container = containers.First(cn => cn.path == path);
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
                node.QuoteText = text;
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

        public Node getNode(Vector pos)
        {
            NodeDialogue nd = containers.FirstOrDefault(nd2 => nd2.overlap(pos));
            if (nd)
            {
                Node n = nd.Nodes.FirstOrDefault(n2 => n2.overlap(pos));
                if (n)
                {
                    return n;
                }
                return nd;
            }
            return null;
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
            containers.Clear();
        }

        /// <summary>
        /// Delete the given NodeDialogue or Node
        /// </summary>
        /// <param name="n"></param>
        public void delete(Node n)
        {
            //2021-01-14: TODO
            if (n is NodeComponent node)
            {
                node.data.path.remove(node.data);
                containers.Find(nd => nd.Nodes.Contains(node)).RemoveNode(node);
            }
            else if (n is NodeDialogue container)
            {
                dialogueData.dialogues.Remove(container.path);
                containers.Remove(container);
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
            containers.Clear();
            populateNodes(filteredPaths);
            dialoguePanel.Invalidate();
        }

        public void setDefaultImageFileName(string character, string imageFileName)
        {
            if (String.IsNullOrEmpty(imageFileName))
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
                                if (String.IsNullOrEmpty(q.imageFileName))
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
                    foreach (Node n in container.Nodes)
                    {
                        if (n is NodeQuote quote)
                        {
                            quote.refreshImage();
                        }
                    }
                }
                );
        }
    }
}
