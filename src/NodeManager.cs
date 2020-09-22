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
        const int BUFFER_NODE = 10;
        const int BUFFER_CONTAINER = 20;

        public List<DialoguePath> dialogues = new List<DialoguePath>();
        public List<Node> nodes = new List<Node>();
        public List<ContainerNode> containers = new List<ContainerNode>();

        public Node createNode(DialoguePath path = null, int index = -1)
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

        public void layoutNodes()
        {
            //Layout dialogue paths
            Vector nextCorner = new Vector(BUFFER_CONTAINER, BUFFER_CONTAINER * 2);
            foreach (ContainerNode container in containers)
            {
                container.position = nextCorner;
                nextCorner += new Vector(
                    container.bufferEdges * 2 + 200 + BUFFER_CONTAINER,
                    0
                    );
            }
            //Layout quotes
            foreach (DialoguePath path in dialogues)
            {
                ContainerNode container = containers.First(cn => cn.path == path);
                Vector nextPos = container.position + new Vector(
                    container.bufferEdges,
                    container.bufferEdges + container.bufferTop + BUFFER_NODE
                    );
                for (int i = 0; i < path.quotes.Count; i++)
                {
                    Quote quote = path.quotes[i];
                    Node node = nodes.First(n => n.quote == quote);
                    node.position = nextPos;
                    nextPos.y += node.size.Height + BUFFER_NODE;
                }
                container.size = new Size(
                    200 + container.bufferEdges * 2,
                    nextPos.y - BUFFER_NODE + container.bufferEdges - container.position.y
                    );
            }
        }

        public Node getNodeAtPosition(Vector mousePos)
        {
            Node node = null;
            //Check normal nodes first
            node = nodes.FirstOrDefault(
                n => n.getRect().Contains(mousePos.toPoint())
                );
            if (node)
            {
                return node;
            }
            //Check containers second
            return containers.FirstOrDefault(
                cn => cn.getRect().Contains(mousePos.toPoint())
                );
        }
    }
}
