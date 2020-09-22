using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    public class FileManager
    {

        public string filename = null;

        private SaveFileDialog sfd;

        public FileManager()
        {
            sfd = new SaveFileDialog();
            sfd.Filter = "Dialogue JSON Files (*.json)|*.json|All files|*.*";
            sfd.FileName = filename;
            sfd.Title = "Save Dialogue";
            sfd.DefaultExt = ".json";
        }

        public void saveFile()
        {
            if (filename == null)
            {
                sfd.FileName = filename;
                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    MessageBox.Show("Here's where the file would be saved.");
                }
            }
        }

        public void openFile()
        {

        }

    }
}
