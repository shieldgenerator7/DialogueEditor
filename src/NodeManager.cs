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
        public List<NodePanel> containers = new List<NodePanel>();

        /// <summary>
        /// Creates a UI Node and a Quote,
        /// Also creates a DialoguePath if none is provided
        /// </summary>
        /// <param name="path"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public NodeLabel createNode(DialoguePath path = null, int index = -1)
        {
            //If no path,
            if (path == null)
            {
                //create a path
                path = createContainerNode().path;
            }
            NodePanel container = containers.First(cn => cn.path == path);
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
                quote.characterName = quote.path.quotes[quote.Index - 2].characterName;
            }
            NodeLabel node = new NodeLabel(quote);
            container.Controls.Add(node);
            return node;
        }

        /// <summary>
        /// This method makes a UI node for the given quote in the given dialogue path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="quote"></param>
        /// <returns></returns>
        public NodeLabel createNode(DialoguePath path, Quote quote)
        {
            NodeLabel node = new NodeLabel(quote);
            NodePanel container = containers.First(cn => cn.path == path);
            container.Controls.Add(node);
            return node;
        }

        /// <summary>
        /// Creates a new node for each string in the given array
        /// Returns the last created node
        /// </summary>
        /// <param name="path"></param>
        /// <param name="textArray"></param>
        public NodeLabel createNodes(DialoguePath path, string[] textArray)
        {
            if (path == null)
            {
                path = createContainerNode().path;
            }
            NodeLabel lastNode = null;
            foreach (string text in textArray)
            {
                NodeLabel node = createNode(path);
                node.Text = text;
                lastNode = node;
            }
            return lastNode;
        }

        public NodePanel createContainerNode(DialoguePath path = null)
        {
            if (path == null)
            {
                path = new DialoguePath();
                dialogueData.dialogues.Add(path);
            }
            NodePanel container = new NodePanel(path);
            containers.Add(container);
            dialoguePanel.Controls.Add(container);
            return container;
        }

        public void acceptInfoFromFile(DialogueData dialogueData)
        {
            clearNodes();
            this.dialogueData = dialogueData;
            this.dialogueData.dialogues.ForEach(d => d.inflate());
            populateNodes(this.dialogueData.dialogues);
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
            containers.ForEach(cn => cn.Dispose());
            containers.Clear();
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
                    d.quotes.ForEach(
                        q => createNode(d, q)
                        );
                }
                );
        }
    }
}
