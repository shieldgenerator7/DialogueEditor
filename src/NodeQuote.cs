﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor.src
{
    public class NodeQuote : Node
    {
        public const int SIZE_PICTURE = 43;
        public const int WIDTH_LABEL = 200;

        public readonly Quote quote;

        private RichTextBox textBox;
        private PictureBox pictureBox;
        private PictureBox imgVoiceLine;
        private ToolTip toolTip;
        private static OpenFileDialog ofdPicture;
        private static OpenFileDialog ofdVoiceLine;

        public string QuoteText
        {
            get =>
                (
                    (quote.characterName != "" && quote.characterName != null)
                        ? quote.characterName + ": "
                        : ""
                )
                + quote.text;
            set
            {
                if (value.Contains(":"))
                {
                    int index = value.IndexOf(':');
                    quote.characterName = value.Substring(0, index).Trim();
                    if (index < value.Length - 1)
                    {
                        quote.text = value.Substring(index + 1).Trim();
                    }
                    else
                    {
                        quote.text = "";
                    }
                }
                else
                {
                    quote.text = value;
                }
            }
        }

        private bool _editing = false;
        public bool Editing
        {
            get => _editing;
            set
            {
                _editing = value;
                if (_editing)
                {
                    textBox.BackColor = Managers.Colors.textBackColor;
                    textBox.ForeColor = Managers.Colors.textForeColor;
                    textBox.BorderStyle = BorderStyle.Fixed3D;
                    //
                    textBox.Focus();
                    textBox.SelectionStart = textBox.Text.Length;
                    textBox.SelectionLength = 0;
                }
                else
                {
                    textBox.BackColor = Managers.Colors.labelBackColor;
                    textBox.ForeColor = Managers.Colors.labelForeColor;
                    textBox.BorderStyle = BorderStyle.None;
                }
                textBox.ReadOnly = !_editing;
            }
        }

        public override int OrderCode => 2;
        public string SortString => "" + quote.Index;

        public NodeQuote(Quote quote) : base(quote)
        {
            this.quote = quote;

            if (ofdPicture == null)
            {
                ofdPicture = new OpenFileDialog();
                ofdPicture.Filter = "PNG Files (*.png)|*.png|All files (*.*)|*.*";
                ofdPicture.Title = "Choose picture";
            }

            if (ofdVoiceLine == null)
            {
                ofdVoiceLine = new OpenFileDialog();
                ofdVoiceLine.Filter = "MP3 Files (*.mp3)|*.mp3|WAV Files (*.wav)|*.wav|All files (*.*)|*.*";
                ofdVoiceLine.Title = "Choose voice line";
            }

            toolTip = new ToolTip();

            // Panel (self) properties
            AutoSize = true;
            AutoScroll = false;
            Location = new System.Drawing.Point(3, 3);
            Size = new System.Drawing.Size(280, 80);

            // PictureBox properties
            pictureBox = new PictureBox();
            this.Controls.Add(pictureBox);
            refreshImage();
            pictureBox.BackColor = System.Drawing.Color.LightGray;
            pictureBox.Location = new System.Drawing.Point(0, 0);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox.BackgroundImage = DialogueEditor.Properties.Resources.defaultQuoteImage;
            pictureBox.Size = new System.Drawing.Size(SIZE_PICTURE, SIZE_PICTURE);
            pictureBox.DoubleClick += selectPicture;
            pictureBox.Click += (sender, e) => Managers.Control.select(this);

            // imgVoiceLine properties
            imgVoiceLine = new PictureBox();
            this.Controls.Add(imgVoiceLine);
            refreshVoiceLine();
            imgVoiceLine.BackColor = System.Drawing.Color.LightGray;
            imgVoiceLine.Location = new System.Drawing.Point(0, SIZE_PICTURE + NodeManager.BUFFER_NODE);
            imgVoiceLine.SizeMode = PictureBoxSizeMode.Zoom;
            imgVoiceLine.BackgroundImageLayout = ImageLayout.Stretch;
            imgVoiceLine.BackgroundImage = DialogueEditor.Properties.Resources.noVoiceLine;
            imgVoiceLine.Size = new System.Drawing.Size(SIZE_PICTURE, SIZE_PICTURE);
            imgVoiceLine.DoubleClick += selectVoiceLine;
            imgVoiceLine.Click += (sender, e) => Managers.Control.select(this);

            // TextBox properties
            textBox = new RichTextBox();
            this.Controls.Add(textBox);
            textBox.AutoSize = true;
            textBox.Font = new Font("Calibri", 12);
            textBox.Location = new System.Drawing.Point(SIZE_PICTURE + 5, 0);
            textBox.MinimumSize = new Size(WIDTH_LABEL, SIZE_PICTURE);
            textBox.MaximumSize = new Size(WIDTH_LABEL, 0);
            textBox.Size = new Size(WIDTH_LABEL, 96);
            textBox.ScrollBars = RichTextBoxScrollBars.None;
            textBox.Text = QuoteText;
            //Event Listeners
            textBox.ContentsResized += rtb_ContentsResized;
            textBox.TextChanged += acceptText;
            textBox.DoubleClick += (sender, e) => Editing = !Editing;
            textBox.Click += (sender, e) => Managers.Control.select(this);
            //
            Editing = false;
        }

        //2020-09-21: copied from https://stackoverflow.com/a/16607756/2336212
        private void rtb_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            ((RichTextBox)sender).Height = e.NewRectangle.Height + 5;
        }

        protected void acceptText(object sender, EventArgs e)
        {
            string sentText = ((RichTextBox)sender).Text;
            if (sentText.Contains('\n'))
            {
                //Special processing,
                //Create new nodes
                string[] split = sentText.Split('\n');
                sentText = split[0];
                List<string> splitList = new List<string>(split);
                splitList.RemoveAt(0);
                split = splitList.ToArray();
                Managers.Control.receiveInfoDump(quote.path, split);
                textBox.Text = sentText;
            }
            //Normal procedure
            sentText = sentText.Trim();
            if (sentText.StartsWith(":"))
            {
                sentText = sentText.Substring(1).Trim();
            }
            this.QuoteText = sentText;
        }

        private void selectPicture(object sender, EventArgs e)
        {
            if (this.quote.imageFileName != null && this.quote.imageFileName != "")
            {
                if (File.Exists(this.quote.imageFileName))
                {
                    ofdPicture.FileName = this.quote.imageFileName;
                }
            }
            //Open file dialog
            DialogResult dr = ofdPicture.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.quote.imageFileName = ofdPicture.FileName;
                refreshImage();
                Managers.Node.setDefaultImageFileName(
                    quote.characterName,
                    quote.imageFileName
                    );
            }
        }

        public void refreshImage()
        {
            if (this.quote.imageFileName != null && this.quote.imageFileName.EndsWith(".png"))
            {
                try
                {
                    pictureBox.Image = Image.FromFile(this.quote.imageFileName);
                    toolTip.SetToolTip(pictureBox, this.quote.imageFileName);
                }
                catch (FileNotFoundException fnfe)
                {
                    toolTip.SetToolTip(pictureBox, "Image not found: " + this.quote.imageFileName);
                }
            }
            else
            {
                toolTip.SetToolTip(pictureBox, "No image chosen");
            }
        }
        private void selectVoiceLine(object sender, EventArgs e)
        {
            if (this.quote.voiceLineFileName != null && this.quote.voiceLineFileName != "")
            {
                if (File.Exists(this.quote.voiceLineFileName))
                {
                    ofdVoiceLine.FileName = this.quote.voiceLineFileName;
                }
            }
            //Open file dialog
            DialogResult dr = ofdVoiceLine.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.quote.voiceLineFileName = ofdVoiceLine.FileName;
                refreshVoiceLine();
            }
        }

        public void refreshVoiceLine()
        {
            string filename = this.quote.voiceLineFileName;
            if (filename != null && filename != "")
            {
                imgVoiceLine.Image = DialogueEditor.Properties.Resources.setVoiceLine;
                toolTip.SetToolTip(imgVoiceLine, filename);
            }
            else
            {
                imgVoiceLine.Image = DialogueEditor.Properties.Resources.noVoiceLine;
                toolTip.SetToolTip(imgVoiceLine, "No voice line chosen");
            }
        }

        public static void disposeDialogs()
        {
            ofdPicture.Dispose();
            ofdVoiceLine.Dispose();
        }

        public override int CompareTo(Node n)
            => this.quote.Index - ((NodeQuote)n).quote.Index;

        public static bool operator <(NodeQuote a, NodeQuote b)
            => a.quote.Index < b.quote.Index;


        public static bool operator >(NodeQuote a, NodeQuote b)
            => a.quote.Index > b.quote.Index;

    }
}
