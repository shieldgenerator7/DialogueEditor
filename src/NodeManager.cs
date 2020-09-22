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
        public const int BUFFER_NODE = 10;
        public const int BUFFER_CONTAINER = 20;

        public List<DialoguePath> dialogues { get; private set; } = new List<DialoguePath>();
        public List<Node> nodes = new List<Node>();
        public List<ContainerNode> containers = new List<ContainerNode>();

        /// <summary>
        /// Creates a UI Node and a Quote,
        /// Also creates a DialoguePath if none is provided
        /// </summary>
        /// <param name="path"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Node createNode(DialoguePath path = null, int index = -1)
        {
            //If no path,
            if (path == null)
            {
                //create a path
                path = createContainerNode().path;
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
                quote.characterName = quote.path.quotes[quote.Index - 2].characterName;
            }
            Node node = new Node(quote);
            nodes.Add(node);
            return node;
        }

        /// <summary>
        /// This method makes a UI node for the given quote in the given dialogue path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="quote"></param>
        /// <returns></returns>
        public Node createNode(DialoguePath path, Quote quote)
        {
            Node node = new Node(quote);
            nodes.Add(node);
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
                path = createContainerNode().path;
            }
            Node lastNode = null;
            foreach (string text in textArray)
            {
                Node node = createNode(path);
                node.Text = text;
                lastNode = node;
            }
            return lastNode;
        }

        public ContainerNode createContainerNode(DialoguePath path = null)
        {
            if (path == null)
            {
                path = new DialoguePath();
                dialogues.Add(path);
            }
            ContainerNode container = new ContainerNode(path);
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

        /// <summary>
        /// Returns the index of the node directly below the mousePos
        /// This assumes the mousePos is between two nodes in the same path
        /// </summary>
        /// <param name="mousePos"></param>
        /// <returns></returns>
        public Node getIndexAtPosition(Vector mousePos)
        {
            Size size = new Size(200, BUFFER_NODE);
            Point pos = mousePos.toPoint();
            Rectangle selectArea = new Rectangle(0, 0, 200, BUFFER_NODE);
            return nodes.FirstOrDefault(n =>
            {
                selectArea.X = n.position.x;
                selectArea.Y = n.position.y - BUFFER_NODE;
                return selectArea.Contains(pos);
            });
        }

        public void acceptInfoFromFile(List<DialoguePath> dialogues)
        {
            clearNodes();
            this.dialogues = dialogues;
            this.dialogues.ForEach(d => d.inflate());
            populateNodes(this.dialogues);
            Managers.Control.deselect();
        }

        /// <summary>
        /// Clears all UI nodes
        /// </summary>
        public void clearNodes()
        {
            nodes.ForEach(n => n.disposeLabel());
            nodes.Clear();
            containers.ForEach(cn => cn.disposeLabel());
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
