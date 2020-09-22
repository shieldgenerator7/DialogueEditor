using DialogueEditor.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    /// <summary>
    /// Visual representation of a DialoguePath
    /// </summary>
    public class ContainerNode : Node
    {
        public readonly DialoguePath path;

        public int bufferTop = 30;//how much extra buffer to add to top
        public int bufferEdges = 10;

        //It doesn't store its children;
        //it's used for visual representation and editing

        private Size _size;
        public override Size size
        {
            get => _size;
            set
            {
                _size = value;
                if (label != null)
                {
                    label.Size = new Size(
                        _size.Width - (bufferEdges * 2),
                        bufferTop
                        );
                }
            }
        }

        public override Vector position { 
            get => base.position;
            set
            {
                base.position = value;
                if (label != null)
                {
                    label.Location = (position + new Vector(bufferEdges, bufferEdges)).toPoint();
                }
            }
        }

        public override bool Editing => false;

        public ContainerNode(DialoguePath path) : this(path, Vector.zero) { }

        public ContainerNode(DialoguePath path, Vector position)
        {
            //Label
            this.label = new Label();
            this.label.Text = "Dialogue Title";
            this.label.AutoSize = false;
            this.label.BackColor = System.Drawing.Color.LightGray;
            this.label.Font = new System.Drawing.Font("Calibri", 12);
            this.label.ForeColor = Color.Black;
            Managers.Form.Controls.Add(this.label);
            this.label.BringToFront();

            //Instance variables
            this.path = path;
            this.position = position;
        }

        public override void dispose()
        {
            Managers.Node.containers.Remove(this);
            Managers.Node.dialogues.Remove(path);
            Managers.Form.Controls.Remove(label);
            Managers.Node.nodes.ForEach(
                    n =>
                    {
                        if (n.quote.path == path)
                        {
                            n.dispose();
                        }
                    }
                );
            Managers.Node.nodes.RemoveAll(n => n.quote.path == path);
        }

    }
}
