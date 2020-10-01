using DialogueEditor.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Managers.init(this);
            Managers.Node.dialoguePanel = this.pnlDialogue;
        }

        void refresh()
        {
            this.Invalidate();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                bool fileOpened = Managers.File.openFile();
                if (!fileOpened)
                {
                    Managers.File.newFile();
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                Managers.File.newFile();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Managers.File.saveFile();
            Managers.File.savePropertiesBeforeClose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //2020-09-21: copied from https://stackoverflow.com/a/54960523/2336212
            if (keyData == Keys.Return)
            {
                Managers.Control.enterPressed();
                refresh();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                Managers.Control.escapePressed();
                refresh();
                return true;
            }
            else if (keyData == Keys.Delete)
            {
                saveScroll();
                if (Managers.Control.deletePressed())
                {
                    refresh();
                    restoreScroll();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Use menus to make a new dialogue path.\n"
                + "Press ENTER to finish a quote. "
                + "If you're at the end, it will create a new quote.\n"
                + "Double-click a quote to edit it, press ENTER or ESC to finish.\n"
                + "You can paste (CTRL+V) pre-written lines into a quote. "
                + "It will automatically split it into more quotes if necessary.\n"
                + "If a quote is selected, it will have a green border.\n"
                + "Press DEL and the selected quote will be deleted. "
                + "This works on dialogue paths too.\n"
                + "Double-click above a quote to insert a quote before it. "
                );
        }

        private void dialoguePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Managers.Control.createDialoguePath();
            refresh();
        }

        private void quoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Managers.Control.createQuote();
            refresh();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool savedFile = Managers.File.saveFile();
            if (!savedFile)
            {
                Managers.File.saveFileWithDialog();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Managers.File.openFileWithDialog();
            refresh();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Managers.File.newFile();
        }

        private void conditionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Managers.Control.createCondition();
            refresh();
        }

        private void actionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Managers.Control.createAction();
            refresh();
        }

        Vector savedScrollPosition = Vector.zero;
        public void saveScroll()
        {
            savedScrollPosition.x = pnlDialogue.HorizontalScroll.Value;
            savedScrollPosition.y = pnlDialogue.VerticalScroll.Value;
        }
        public void restoreScroll()
        {
            pnlDialogue.HorizontalScroll.Value = savedScrollPosition.x;
            pnlDialogue.VerticalScroll.Value = savedScrollPosition.y;
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Managers.File.openFileWithDialog(true);
            refresh();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Managers.File.saveFileWithDialog();
        }

        private void cmbCharacters_Enter(object sender, EventArgs e)
        {
            updateCharacterList();
        }

        private void btnAddCharacterFilter_Click(object sender, EventArgs e)
        {
            string filters = txtCharacterFilters.Text + "," + (string)cmbCharacters.SelectedItem;
            //Trim whitespace and commas
            filters = filters.Trim();
            if (filters.StartsWith(","))
            {
                filters = filters.Substring(1);
            }
            if (filters.EndsWith(","))
            {
                filters = filters.Substring(0, filters.Length - 1);
            }
            filters = filters.Trim();
            //Update the textbox
            txtCharacterFilters.Text = filters;
            //Add the filters
            updateFilters();
        }
        private void updateCharacterList()
        {
            string character = (string)cmbCharacters.SelectedItem;
            List<string> chars = Managers.Node.dialogueData.Characters;
            chars.Sort();
            chars.Insert(0, "(All)");
            cmbCharacters.DataSource = chars;
            if (character != null && cmbCharacters.Items.Contains(character))
            {
                cmbCharacters.SelectedItem = character;
            }
            else
            {
                cmbCharacters.SelectedIndex = 0;
            }
        }
        private void updateFilters()
        {
            pnlDialogue.SuspendLayout();
            Managers.Node.filterCharacters(
                new List<string>(txtCharacterFilters.Text.Split(','))
                );
            pnlDialogue.ResumeLayout();
        }
    }
}
