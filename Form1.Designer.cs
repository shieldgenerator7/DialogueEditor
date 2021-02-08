namespace DialogueEditor
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dialoguePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conditionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlDialogue = new DisplayPanel();
            this.txtEdit = new System.Windows.Forms.TextBox();
            this.spltMain = new System.Windows.Forms.SplitContainer();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.tabCharacters = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCharacters = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCharacterName = new System.Windows.Forms.TextBox();
            this.btnUpdateCharacterName = new System.Windows.Forms.Button();
            this.grpCharacterFilters = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddCharacterFilter = new System.Windows.Forms.Button();
            this.txtCharacterFilters = new System.Windows.Forms.RichTextBox();
            this.tabVariables = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            this.pnlDialogue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltMain)).BeginInit();
            this.spltMain.Panel1.SuspendLayout();
            this.spltMain.Panel2.SuspendLayout();
            this.spltMain.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.tabCharacters.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.grpCharacterFilters.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1309, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.importToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(72, 36);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(377, 44);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(377, 44);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(377, 44);
            this.importToolStripMenuItem.Text = "&Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(377, 44);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(377, 44);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.moveToTopToolStripMenuItem,
            this.moveToBottomToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(75, 36);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dialoguePathToolStripMenuItem,
            this.conditionToolStripMenuItem,
            this.quoteToolStripMenuItem,
            this.actionToolStripMenuItem});
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(455, 44);
            this.addNewToolStripMenuItem.Text = "&Add New";
            // 
            // dialoguePathToolStripMenuItem
            // 
            this.dialoguePathToolStripMenuItem.Name = "dialoguePathToolStripMenuItem";
            this.dialoguePathToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.dialoguePathToolStripMenuItem.Size = new System.Drawing.Size(449, 44);
            this.dialoguePathToolStripMenuItem.Text = "&Dialogue Path";
            this.dialoguePathToolStripMenuItem.Click += new System.EventHandler(this.dialoguePathToolStripMenuItem_Click);
            // 
            // conditionToolStripMenuItem
            // 
            this.conditionToolStripMenuItem.Name = "conditionToolStripMenuItem";
            this.conditionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.conditionToolStripMenuItem.Size = new System.Drawing.Size(449, 44);
            this.conditionToolStripMenuItem.Text = "&Condition";
            this.conditionToolStripMenuItem.Click += new System.EventHandler(this.conditionToolStripMenuItem_Click);
            // 
            // quoteToolStripMenuItem
            // 
            this.quoteToolStripMenuItem.Name = "quoteToolStripMenuItem";
            this.quoteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.quoteToolStripMenuItem.Size = new System.Drawing.Size(449, 44);
            this.quoteToolStripMenuItem.Text = "&Quote";
            this.quoteToolStripMenuItem.Click += new System.EventHandler(this.quoteToolStripMenuItem_Click);
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(449, 44);
            this.actionToolStripMenuItem.Text = "&Action";
            this.actionToolStripMenuItem.Click += new System.EventHandler(this.actionToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Enabled = false;
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(455, 44);
            this.selectAllToolStripMenuItem.Text = "&Select All";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Enabled = false;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(455, 44);
            this.cutToolStripMenuItem.Text = "C&ut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Enabled = false;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(455, 44);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Enabled = false;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(455, 44);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Enabled = false;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(455, 44);
            this.deleteToolStripMenuItem.Text = "&Delete";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(452, 6);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Enabled = false;
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(455, 44);
            this.moveUpToolStripMenuItem.Text = "Move &Up";
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Enabled = false;
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(455, 44);
            this.moveDownToolStripMenuItem.Text = "&Move Down";
            // 
            // moveToTopToolStripMenuItem
            // 
            this.moveToTopToolStripMenuItem.Enabled = false;
            this.moveToTopToolStripMenuItem.Name = "moveToTopToolStripMenuItem";
            this.moveToTopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.moveToTopToolStripMenuItem.Size = new System.Drawing.Size(455, 44);
            this.moveToTopToolStripMenuItem.Text = "Move to &Top";
            // 
            // moveToBottomToolStripMenuItem
            // 
            this.moveToBottomToolStripMenuItem.Enabled = false;
            this.moveToBottomToolStripMenuItem.Name = "moveToBottomToolStripMenuItem";
            this.moveToBottomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.moveToBottomToolStripMenuItem.Size = new System.Drawing.Size(455, 44);
            this.moveToBottomToolStripMenuItem.Text = "Move to &Bottom";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(85, 36);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // pnlDialogue
            // 
            this.pnlDialogue.AutoScroll = true;
            this.pnlDialogue.AutoSize = true;
            this.pnlDialogue.BackColor = System.Drawing.Color.DarkGray;
            this.pnlDialogue.Controls.Add(this.txtEdit);
            this.pnlDialogue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDialogue.Location = new System.Drawing.Point(0, 0);
            this.pnlDialogue.Name = "pnlDialogue";
            this.pnlDialogue.Size = new System.Drawing.Size(1053, 865);
            this.pnlDialogue.TabIndex = 2;
            this.pnlDialogue.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDialogue_Paint);
            this.pnlDialogue.DoubleClick += new System.EventHandler(this.pnlDialogue_DoubleClick);
            this.pnlDialogue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlDialogue_MouseMove);
            // 
            // txtEdit
            // 
            this.txtEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtEdit.Location = new System.Drawing.Point(518, 83);
            this.txtEdit.Multiline = true;
            this.txtEdit.Name = "txtEdit";
            this.txtEdit.Size = new System.Drawing.Size(204, 393);
            this.txtEdit.TabIndex = 1;
            this.txtEdit.Text = "Jubilee: Hello there! I\'m Jubilee! What\'s your name? You\'re a new person! I\'ve ne" +
    "ver met you before! What\'s your name? What\'s your name?";
            this.txtEdit.Visible = false;
            // 
            // spltMain
            // 
            this.spltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spltMain.Location = new System.Drawing.Point(0, 40);
            this.spltMain.Name = "spltMain";
            // 
            // spltMain.Panel1
            // 
            this.spltMain.Panel1.Controls.Add(this.pnlDialogue);
            // 
            // spltMain.Panel2
            // 
            this.spltMain.Panel2.Controls.Add(this.tabInfo);
            this.spltMain.Size = new System.Drawing.Size(1309, 865);
            this.spltMain.SplitterDistance = 1053;
            this.spltMain.TabIndex = 3;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.tabCharacters);
            this.tabInfo.Controls.Add(this.tabVariables);
            this.tabInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInfo.Location = new System.Drawing.Point(0, 0);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(252, 865);
            this.tabInfo.TabIndex = 0;
            // 
            // tabCharacters
            // 
            this.tabCharacters.Controls.Add(this.flowLayoutPanel1);
            this.tabCharacters.Location = new System.Drawing.Point(8, 39);
            this.tabCharacters.Name = "tabCharacters";
            this.tabCharacters.Padding = new System.Windows.Forms.Padding(3);
            this.tabCharacters.Size = new System.Drawing.Size(236, 818);
            this.tabCharacters.TabIndex = 0;
            this.tabCharacters.Text = "Characters";
            this.tabCharacters.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.cmbCharacters);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.txtCharacterName);
            this.flowLayoutPanel1.Controls.Add(this.btnUpdateCharacterName);
            this.flowLayoutPanel1.Controls.Add(this.grpCharacterFilters);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(230, 812);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Characters";
            // 
            // cmbCharacters
            // 
            this.cmbCharacters.FormattingEnabled = true;
            this.cmbCharacters.Location = new System.Drawing.Point(3, 28);
            this.cmbCharacters.Name = "cmbCharacters";
            this.cmbCharacters.Size = new System.Drawing.Size(200, 33);
            this.cmbCharacters.TabIndex = 0;
            this.cmbCharacters.SelectedValueChanged += new System.EventHandler(this.cmbCharacters_SelectedValueChanged);
            this.cmbCharacters.Enter += new System.EventHandler(this.cmbCharacters_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Character Name";
            // 
            // txtCharacterName
            // 
            this.txtCharacterName.Location = new System.Drawing.Point(3, 92);
            this.txtCharacterName.Name = "txtCharacterName";
            this.txtCharacterName.Size = new System.Drawing.Size(200, 31);
            this.txtCharacterName.TabIndex = 3;
            this.txtCharacterName.TextChanged += new System.EventHandler(this.txtCharacterName_TextChanged);
            // 
            // btnUpdateCharacterName
            // 
            this.btnUpdateCharacterName.Location = new System.Drawing.Point(3, 129);
            this.btnUpdateCharacterName.Name = "btnUpdateCharacterName";
            this.btnUpdateCharacterName.Size = new System.Drawing.Size(106, 34);
            this.btnUpdateCharacterName.TabIndex = 4;
            this.btnUpdateCharacterName.Text = "Update";
            this.btnUpdateCharacterName.UseVisualStyleBackColor = true;
            this.btnUpdateCharacterName.Click += new System.EventHandler(this.btnUpdateCharacterName_Click);
            // 
            // grpCharacterFilters
            // 
            this.grpCharacterFilters.AutoSize = true;
            this.grpCharacterFilters.Controls.Add(this.flowLayoutPanel2);
            this.grpCharacterFilters.Location = new System.Drawing.Point(3, 169);
            this.grpCharacterFilters.MinimumSize = new System.Drawing.Size(200, 0);
            this.grpCharacterFilters.Name = "grpCharacterFilters";
            this.grpCharacterFilters.Size = new System.Drawing.Size(212, 108);
            this.grpCharacterFilters.TabIndex = 5;
            this.grpCharacterFilters.TabStop = false;
            this.grpCharacterFilters.Text = "Filters";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.btnAddCharacterFilter);
            this.flowLayoutPanel2.Controls.Add(this.txtCharacterFilters);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 27);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(206, 78);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // btnAddCharacterFilter
            // 
            this.btnAddCharacterFilter.AutoSize = true;
            this.btnAddCharacterFilter.Location = new System.Drawing.Point(3, 3);
            this.btnAddCharacterFilter.Name = "btnAddCharacterFilter";
            this.btnAddCharacterFilter.Size = new System.Drawing.Size(114, 35);
            this.btnAddCharacterFilter.TabIndex = 0;
            this.btnAddCharacterFilter.Text = "Filter";
            this.btnAddCharacterFilter.UseVisualStyleBackColor = true;
            this.btnAddCharacterFilter.Click += new System.EventHandler(this.btnAddCharacterFilter_Click);
            // 
            // txtCharacterFilters
            // 
            this.txtCharacterFilters.Enabled = false;
            this.txtCharacterFilters.Location = new System.Drawing.Point(3, 44);
            this.txtCharacterFilters.MinimumSize = new System.Drawing.Size(200, 4);
            this.txtCharacterFilters.Multiline = false;
            this.txtCharacterFilters.Name = "txtCharacterFilters";
            this.txtCharacterFilters.Size = new System.Drawing.Size(200, 31);
            this.txtCharacterFilters.TabIndex = 1;
            this.txtCharacterFilters.Text = "";
            // 
            // tabVariables
            // 
            this.tabVariables.Location = new System.Drawing.Point(8, 39);
            this.tabVariables.Name = "tabVariables";
            this.tabVariables.Padding = new System.Windows.Forms.Padding(3);
            this.tabVariables.Size = new System.Drawing.Size(236, 818);
            this.tabVariables.TabIndex = 1;
            this.tabVariables.Text = "Variables";
            this.tabVariables.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 905);
            this.Controls.Add(this.spltMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Dialogue Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlDialogue.ResumeLayout(false);
            this.pnlDialogue.PerformLayout();
            this.spltMain.Panel1.ResumeLayout(false);
            this.spltMain.Panel1.PerformLayout();
            this.spltMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltMain)).EndInit();
            this.spltMain.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.tabCharacters.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.grpCharacterFilters.ResumeLayout(false);
            this.grpCharacterFilters.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dialoguePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conditionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToBottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.SplitContainer spltMain;
        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage tabCharacters;
        private System.Windows.Forms.TabPage tabVariables;
        private System.Windows.Forms.ComboBox cmbCharacters;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCharacterName;
        private System.Windows.Forms.Button btnUpdateCharacterName;
        private System.Windows.Forms.GroupBox grpCharacterFilters;
        private System.Windows.Forms.Button btnAddCharacterFilter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RichTextBox txtCharacterFilters;
        public DisplayPanel pnlDialogue = new DisplayPanel();
        public System.Windows.Forms.TextBox txtEdit;
    }
}

