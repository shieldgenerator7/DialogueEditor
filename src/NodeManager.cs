﻿using System;
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

        public void createNode(DialoguePath path, Vector mousePos)
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
            path.quotes.Add(quote);
            Node node = new Node(quote, mousePos);
            nodes.Add(node);
            node.editNode(true);
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