using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor.src
{
    public class NodeManager
    {
        public List<DialoguePath> dialogues = new List<DialoguePath>();
        public List<Node> nodes = new List<Node>();
        public List<ContainerNode> containers = new List<ContainerNode>();

        public Node createNode(DialoguePath path, Vector mousePos)
        {
            //If no path,
            if (path == null)
            {
                //create a path
                path = createContainerNode(mousePos).path;
            }
            //Add a node to the path
            Quote quote = new Quote();
            quote.path = path;
            path.quotes.Add(quote);
            Node node = new Node(quote, mousePos);
            nodes.Add(node);
            //Auto-place
            Node startNode = nodes.First(snode => snode.quote == path.quotes[0]);
            int index = path.quotes.IndexOf(quote);
            node.position = node.position
                + new Vector(0, startNode.position.y + index * 30);
            //
            return node;
        }

        /// <summary>
        /// Creates a new node for each string in the given array
        /// Returns the last created node
        /// </summary>
        /// <param name="path"></param>
        /// <param name="textArray"></param>
        public Node createNodes(DialoguePath path, string[] textArray)
        {
            if (path == null)
            {
                path = createContainerNode(Vector.zero).path;
            }
            Node lastNode = null;
            foreach (string text in textArray)
            {
                Node node = createNode(path, Vector.zero);
                node.quote.text = text;
                node.label.Text = text;
                lastNode = node;
            }
            return lastNode;
        }

        public ContainerNode createContainerNode(Vector mousePos)
        {
            DialoguePath path = new DialoguePath();
            dialogues.Add(path);
            ContainerNode container = new ContainerNode(path, mousePos);
            containers.Add(container);
            return container;
        }
        public Node getNodeAtPosition(Vector mousePos)
        {
            //Check normal nodes first
            foreach (Node node in nodes)
            {
                if (nodeContainsPosition(node, mousePos))
                {
                    return node;
                }
            }
            //Check containers second
            return containers.FirstOrDefault(
                cn => cn.getRect().Contains(mousePos.toPoint())
                );
        }

        private bool nodeContainsPosition(Node node, Vector pos)
        {
            Size size = node.size;
            Vector position = node.position;
            int buffer = 10;
            return pos.x >= position.x - buffer
                && pos.x <= position.x + size.Width + buffer
                && pos.y >= position.y - buffer
                && pos.y <= position.y + size.Height + buffer;
        }
    }
}
