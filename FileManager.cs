using DialogueEditor.src;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueEditor
{
    public class FileManager
    {

        public string defaultFileName = null;

        private readonly SaveFileDialog sfd;
        private readonly OpenFileDialog ofd;

        public FileManager()
        {
            //Save Dialog
            sfd = new SaveFileDialog();
            sfd.Filter = "Dialogue JSON Files (*.json)|*.json|All files|*.*";
            sfd.FileName = defaultFileName;
            sfd.Title = "Save Dialogue";
            sfd.DefaultExt = ".json";
            //Open Dialog
            ofd = new OpenFileDialog();
            ofd.Filter = "Dialogue JSON Files (*.json)|*.json|All files|*.*";
            ofd.FileName = defaultFileName;
            ofd.Title = "Open Dialogue";
            ofd.DefaultExt = ".json";
            //Default file
            defaultFileName = DialogueEditor.Properties.Settings.Default.defaultFileName;
        }

        public void savePropertiesBeforeClose()
        {
            DialogueEditor.Properties.Settings.Default.defaultFileName = defaultFileName;
            DialogueEditor.Properties.Settings.Default.Save();
        }

        public void saveFileWithDialog()
        {
            if (!String.IsNullOrEmpty(defaultFileName))
            {
                sfd.FileName = defaultFileName;
            }
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (sfd.FileName != null && sfd.FileName.ToLower().EndsWith(".json"))
                {
                    defaultFileName = sfd.FileName;
                }
                saveFile(sfd.FileName);
            }
        }

        public void openFileWithDialog(bool append = false)
        {
            if (!String.IsNullOrEmpty(defaultFileName))
            {
                ofd.FileName = defaultFileName;
            }
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //Save current file first
                saveFile();
                //Open next file
                if (ofd.FileName != null && ofd.FileName.ToLower().EndsWith(".json"))
                {
                    defaultFileName = ofd.FileName;
                }
                openFile(ofd.FileName, append);
            }
        }

        public void newFile()
        {
            Managers.Node.clear();
            Managers.Control.createQuote();
            defaultFileName = null;
        }

        public bool saveFile(string filename = null)
        {
            if (String.IsNullOrEmpty(filename))
            {
                filename = defaultFileName;
            }
            //If it's still null
            if (String.IsNullOrEmpty(filename))
            {
                //don't do anything
                return false;
            }
            sfd.FileName = filename;
            //2020-09-22: copied from https://stackoverflow.com/a/16921677/2336212
            using (StreamWriter file = new StreamWriter(sfd.OpenFile()))
            {
                string jsonString = JsonConvert.SerializeObject(Managers.Node.dialogueData, Formatting.Indented);

                file.Write(jsonString);
            }
            return true;
        }

        public bool openFile(string filename = null, bool append = false)
        {
            if (String.IsNullOrEmpty(filename))
            {
                filename = defaultFileName;
            }
            //If it's still null,
            if (String.IsNullOrEmpty(filename))
            {
                //can't open the file
                return false;
            }
            //
            ofd.FileName = filename;
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
                defaultFileName = null;
                //(tho it's possible you may want to keep it instead,
                //im not sure which is more user-friendly)
                return false;
            }
            return true;
        }

    }
}
