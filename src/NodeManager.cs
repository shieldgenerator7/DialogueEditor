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

        public Node createNode(DialoguePath path, Vector mousePos)
        {
            //If no path,
            if (path == null)
            {
                //create a path
                path = new DialoguePath();
                dialogues.Add(path);
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
                + new Vector(0,startNode.position.y + index * 30);
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
                path = new DialoguePath();
                dialogues.Add(path);
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

        public Node getNodeAtPosition(Vector mousePos)
        {
            foreach (Node gameObject in nodes)
            {
                if (nodeContainsPosition(gameObject, mousePos))
                {
                    return gameObject;
                }
            }
            return null;
        }

        private bool nodeContainsPosition(Node node, Vector pos)
        {
            Size size = node.size;
            float halfWidth = size.Width / 2;
            float halfHeight = size.Height / 2;
            Vector position = node.position;
            return pos.x >= position.x - halfWidth
                && pos.x <= position.x + halfWidth
                && pos.y >= position.y - halfHeight
                && pos.y <= position.y + halfHeight;
        }
    }
}
