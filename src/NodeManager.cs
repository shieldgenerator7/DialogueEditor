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
            node.position.y = startNode.position.y + index * 30;
            //
            node.editNode(true);
            return node;
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
