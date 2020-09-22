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

        public int bufferTop
            => (Editing) ? textBox.Size.Height : label.Size.Height;
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
            }
        }

        public override Vector position
        {
            get => base.position;
            set
            {
                base.position = value;
                if (label != null)
                {
                    label.Location = (position + new Vector(bufferEdges, bufferEdges)).toPoint();
                    if (Editing)
                    {
                        textBox.Location = label.Location;
                    }
                }
            }
        }

        public override string Text
        {
            get => path.title;
            set
            {
                path.title = value;
                label.Text = value;
            }
        }

        public ContainerNode(DialoguePath path) : this(path, Vector.zero) { }

        public ContainerNode(DialoguePath path, Vector position)
        {
            //Instance variables
            this.path = path;
            this.position = position;
            setupLabel();
        }

        protected override void setupLabel()
        {
            base.setupLabel();
            this.label.Text = path.title;
            this.label.BackColor = System.Drawing.Color.LightGray;
            this.label.ForeColor = Color.Black;
            this.position = position;
        }

        public override void editNode(bool edit)
        {
            base.editNode(edit);
            if (edit)
            {
                textBox.Text = path.title;
            }
            //Automatically update the size of the textbox / label
            this.position = this.position;
        }

        protected override void acceptText(object sender, EventArgs e)
        {
            string text = ((RichTextBox)sender).Text;
            //Normal procedure
            path.title = text;
            label.Text = text;
            size = size;
            Managers.Form.Refresh();
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
