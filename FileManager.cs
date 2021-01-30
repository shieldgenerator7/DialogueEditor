using DialogueEditor.src;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    public class FileManager
    {

        private string defaultFileName = null;
        public string DefaultFileName
        {
            get => defaultFileName;
            set
            {
                defaultFileName = value;
                updateTitleBar();
                sfd.FileName = defaultFileName;
                ofd.FileName = defaultFileName;
            }
        }

        private SaveFileDialog sfd;
        private OpenFileDialog ofd;

        private bool hasBeenSavedToFile = true;//true if this file has ever been saved to a file

        public FileManager()
        {
            //Save Dialog
            sfd = new SaveFileDialog();
            sfd.Filter = "Dialogue JSON Files (*.json)|*.json|All files|*.*";
            sfd.FileName = DefaultFileName;
            sfd.Title = "Save Dialogue";
            sfd.DefaultExt = ".json";
            //Open Dialog
            ofd = new OpenFileDialog();
            ofd.Filter = "Dialogue JSON Files (*.json)|*.json|All files|*.*";
            ofd.FileName = DefaultFileName;
            ofd.Title = "Open Dialogue";
            ofd.DefaultExt = ".json";
            //Default file
            DefaultFileName = DialogueEditor.Properties.Settings.Default.defaultFileName;
        }

        public void savePropertiesBeforeClose()
        {
            DialogueEditor.Properties.Settings.Default.defaultFileName = DefaultFileName;
            DialogueEditor.Properties.Settings.Default.Save();
            sfd.Dispose();
            ofd.Dispose();
        }

        public void saveFileWithDialog()
        {
            if (DefaultFileName != null && DefaultFileName != "")
            {
                sfd.FileName = DefaultFileName;
            }
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (sfd.FileName != null && sfd.FileName.ToLower().EndsWith(".json"))
                {
                    DefaultFileName = sfd.FileName;
                }
                saveFile(sfd.FileName);
            }
        }

        public void openFileWithDialog(bool append = false)
        {
            checkFileSaved();
            if (DefaultFileName != null && DefaultFileName != "")
            {
                ofd.FileName = DefaultFileName;
            }
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //Save current file first
                saveFile();
                //Open next file
                if (ofd.FileName != null && ofd.FileName.ToLower().EndsWith(".json"))
                {
                    DefaultFileName = ofd.FileName;
                }
                openFile(ofd.FileName, append);
            }
        }

        public void newFile()
        {
            checkFileSaved();
            Managers.Node.clear();
            Managers.Control.createQuote();
            DefaultFileName = null;
            updateTitleBar();
            hasBeenSavedToFile = false;
        }

        public bool saveFile(string filename = null)
        {
            if (filename == null || filename == "")
            {
                filename = DefaultFileName;
            }
            //If it's still null
            if (filename == null || filename == "")
            {
                //don't do anything
                return false;
            }
            DefaultFileName = filename;
            //2020-09-22: copied from https://stackoverflow.com/a/16921677/2336212
            using (StreamWriter file = new StreamWriter(sfd.OpenFile()))
            {
                string jsonString = JsonConvert.SerializeObject(Managers.Node.dialogueData, Formatting.Indented);

                file.Write(jsonString);
            }
            hasBeenSavedToFile = true;
            return true;
        }

        public bool openFile(string filename = null, bool append = false)
        {
            if (filename == null || filename == "")
            {
                filename = DefaultFileName;
            }
            //If it's still null,
            if (filename == null || filename == "")
            {
                //can't open the file
                return false;
            }
            //
            DefaultFileName = filename;
            //2020-09-22: copied from https://stackoverflow.com/a/13297964/2336212
            try
            {
                using (StreamReader r = new StreamReader(ofd.OpenFile()))
                {
                    string json = r.ReadToEnd();
                    try
                    {
                        //Open file from version 0.0.3 onward
                        DialogueData dialogueData = JsonConvert.DeserializeObject<DialogueData>(json);
                        Managers.Node.acceptInfoFromFile(dialogueData, append);
                    }
                    catch (JsonSerializationException jse)
                    {
                        //Open file from version 0.0.2
                        List<DialoguePath> dialogues = JsonConvert.DeserializeObject<List<DialoguePath>>(json);
                        Managers.Node.acceptInfoFromFile(new DialogueData(dialogues), append);
                    }
                }
            }
            catch (FileNotFoundException fnfe)
            {
                //don't do anything,
                //except unset the default file name
                DefaultFileName = null;
                //(tho it's possible you may want to keep it instead,
                //im not sure which is more user-friendly)
                return false;
            }
            hasBeenSavedToFile = true;
            return true;
        }

        private void checkFileSaved()
        {
            if (!hasBeenSavedToFile && !Managers.Node.empty)
            {
                saveFileWithDialog();
            }
            else
            {
                saveFile();
            }
        }

        private void updateTitleBar()
        {
            string title = "Untitled";
            if (DefaultFileName != null && DefaultFileName != "")
            {
                string[] split = DefaultFileName.Split('\\');
                title = split[split.Length - 1];
            }
            Managers.Form.Text = title + " - Dialogue Editor";
        }

    }
}
