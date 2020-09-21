﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    /// <summary>
    /// Visual representation of a Quote
    /// </summary>
    public class Node
    {

        public Quote quote;
        public Vector position;
        public Vector pickupOffset;
        public Size size => (label.Visible) ? label.Size : textBox.Size;

        private static RichTextBox textBox;

        private Label label;

        public Node(Quote quote) : this(quote, Vector.zero) { }

        public Node(Quote quote, Vector position)
        {
            this.quote = quote;
            this.position = position;
            //
            this.label = new Label();
            this.label.AutoSize = true;
            this.label.BackColor = Color.FromArgb(53, 70, 127);
            this.label.Font = new System.Drawing.Font("Calibri", 12);
            this.label.ForeColor = Color.FromArgb(240, 240, 200);
            this.label.Location = position.toPoint();
            this.label.MaximumSize = new Size(200, 0);
            this.label.MinimumSize = new Size(100, 20);
            this.label.Size = new Size(100, 28);
            this.label.Text = quote.text;
            Managers.Form.Controls.Add(this.label);
        }

        public virtual Rectangle getRect()
        {
            return new Rectangle(
                position.x,
                position.y,
                size.Width,
                size.Height);
        }

        public void editNode(bool edit)
        {
            if (edit)
            {
                label.BringToFront();
                label.Hide();
                if (textBox == null)
                {
                    textBox = new RichTextBox();
                    textBox.Anchor = AnchorStyles.Left;
                    textBox.AutoSize = true;
                    textBox.Multiline = true;
                    textBox.ScrollBars = RichTextBoxScrollBars.None;
                    textBox.ForeColor = Color.FromArgb(53, 70, 127);
                    textBox.Font = new System.Drawing.Font("Calibri", 12);
                    textBox.BackColor = Color.FromArgb(240, 240, 200);
                    textBox.MaximumSize = new Size(200, 0);
                    textBox.MinimumSize = new Size(100, 20);
                    textBox.ContentsResized += rtb_ContentsResized;
                    //textBox.Size = new Size(100, 41);
                    Managers.Form.Controls.Add(textBox);
                    textBox.BringToFront();
                }
                textBox.Size = new Size(
                    200,
                    label.Size.Height + 10
                    );
                textBox.TextChanged -= acceptText;
                textBox.TextChanged += acceptText;
                textBox.Location = position.toPoint();
                textBox.Text = quote.text;
                textBox.Show();
                textBox.BringToFront();
                textBox.Focus();
            }
            else
            {
                textBox.TextChanged -= acceptText;
                textBox.Hide();
                label.Show();
            }
            Managers.Form.Refresh();
        }

        private void acceptText(object sender, EventArgs e)
        {
            quote.text = ((RichTextBox)sender).Text;
            label.Text = quote.text;
            Managers.Form.Refresh();
        }

        //2020-09-21: copied from https://stackoverflow.com/a/16607756/2336212
        private void rtb_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            ((RichTextBox)sender).Height = e.NewRectangle.Height + 5;
        }

        public void pickup(Vector pickupPos)
        {
            pickupOffset = position - pickupPos;
        }

        public virtual void moveTo(Vector pos, bool useOffset = true)
        {
            if (useOffset)
            {
                position = pos + pickupOffset;
            }
            else
            {
                position = pos;
            }
            label.Location = position.toPoint();
        }

        public static implicit operator Boolean(Node gameObjectSprite)
        {
            return gameObjectSprite != null;
        }

        public virtual int CompareTo(Node gos)
        {
            float thisSize = this.size.toVector().Magnitude;
            float goSize = gos.size.toVector().Magnitude;
            return (int)(this.size.toVector().Magnitude - gos.size.toVector().Magnitude);
        }

        public static bool operator <(Node a, Node b)
        {
            float aSize = a.size.toVector().Magnitude;
            float bSize = b.size.toVector().Magnitude;
            return aSize < bSize;
        }

        public static bool operator >(Node a, Node b)
        {
            float aSize = a.size.toVector().Magnitude;
            float bSize = b.size.toVector().Magnitude;
            return aSize > bSize;
        }
    }
}
