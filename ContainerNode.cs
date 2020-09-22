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

        public ContainerNode(DialoguePath path) : this(path, Vector.zero) { }

        public ContainerNode(DialoguePath path, Vector position)
        {
            this.path = path;
            this.position = position;
            //Label
            this.label = new Label();
            this.label.Text = "Dialogue";
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.Location = position.toPoint();
            this.label.Size = new System.Drawing.Size(70, 25);
            this.label.Font = new System.Drawing.Font("Calibri", 12);
            this.label.ForeColor = Color.FromArgb(240, 240, 200);
            Managers.Form.Controls.Add(this.label);
            this.label.BringToFront();
        }

    }
}
