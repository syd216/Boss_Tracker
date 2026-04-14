namespace Boss_Tracker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            filterButton = new Button();
            clearfilterButton = new Button();
            bossnameLabel = new Label();
            playersLabel = new Label();
            filterCrystal = new Button();
            filterReport = new Button();
            updateCheckBox = new CheckBox();
            ElementAmount = new TextBox();
            excludeClearsCheckBox = new CheckBox();
            excludeSoloCheckBox = new CheckBox();
            soloCheckBox = new CheckBox();
            DownloadButton = new Button();
            jobtogglePanel = new SmoothScrollPanel();
            fakeBarToggleJob = new PictureBox();
            jobtoggleLabel = new Label();
            jobownerButton = new PictureBox();
            filterbossComboBox = new ComboBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            rightPanePB = new PictureBox();
            playertogglePanel = new SmoothScrollPanel();
            fakeBarTogglePlayer = new PictureBox();
            jobtogglePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fakeBarToggleJob).BeginInit();
            ((System.ComponentModel.ISupportInitialize)jobownerButton).BeginInit();
            tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rightPanePB).BeginInit();
            playertogglePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fakeBarTogglePlayer).BeginInit();
            SuspendLayout();
            // 
            // filterButton
            // 
            filterButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            filterButton.BackColor = SystemColors.ControlLightLight;
            filterButton.Font = new Font("Calibri", 9F, FontStyle.Bold);
            filterButton.Location = new Point(1004, 32);
            filterButton.Name = "filterButton";
            filterButton.Size = new Size(75, 23);
            filterButton.TabIndex = 1;
            filterButton.Text = "Filter";
            filterButton.UseVisualStyleBackColor = false;
            filterButton.Click += filterButton_Click;
            // 
            // clearfilterButton
            // 
            clearfilterButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            clearfilterButton.Font = new Font("Calibri", 9F, FontStyle.Bold);
            clearfilterButton.Location = new Point(1004, 456);
            clearfilterButton.Name = "clearfilterButton";
            clearfilterButton.Size = new Size(75, 23);
            clearfilterButton.TabIndex = 3;
            clearfilterButton.Text = "Clear";
            clearfilterButton.UseVisualStyleBackColor = true;
            clearfilterButton.Click += clearfilterButton_Click;
            // 
            // bossnameLabel
            // 
            bossnameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bossnameLabel.AutoSize = true;
            bossnameLabel.BackColor = Color.White;
            bossnameLabel.Font = new Font("Calibri", 9F, FontStyle.Bold);
            bossnameLabel.Location = new Point(788, 36);
            bossnameLabel.Name = "bossnameLabel";
            bossnameLabel.Size = new Size(63, 14);
            bossnameLabel.TabIndex = 5;
            bossnameLabel.Text = "Boss Name";
            // 
            // playersLabel
            // 
            playersLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            playersLabel.AutoSize = true;
            playersLabel.BackColor = Color.White;
            playersLabel.Font = new Font("Calibri", 9F, FontStyle.Bold);
            playersLabel.Location = new Point(788, 71);
            playersLabel.Name = "playersLabel";
            playersLabel.Size = new Size(43, 14);
            playersLabel.TabIndex = 6;
            playersLabel.Text = "Players";
            // 
            // filterCrystal
            // 
            filterCrystal.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            filterCrystal.Location = new Point(788, 497);
            filterCrystal.Name = "filterCrystal";
            filterCrystal.Size = new Size(75, 35);
            filterCrystal.TabIndex = 17;
            filterCrystal.Text = "Crystal Filter";
            filterCrystal.UseVisualStyleBackColor = true;
            filterCrystal.Visible = false;
            filterCrystal.Click += filterCrystal_Click;
            // 
            // filterReport
            // 
            filterReport.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            filterReport.Location = new Point(788, 456);
            filterReport.Name = "filterReport";
            filterReport.Size = new Size(75, 35);
            filterReport.TabIndex = 18;
            filterReport.Text = "Crystal Report";
            filterReport.UseVisualStyleBackColor = true;
            filterReport.Visible = false;
            filterReport.Click += filterReport_Click;
            // 
            // updateCheckBox
            // 
            updateCheckBox.AutoSize = true;
            updateCheckBox.BackColor = SystemColors.Window;
            updateCheckBox.Checked = true;
            updateCheckBox.CheckState = CheckState.Checked;
            updateCheckBox.Font = new Font("Calibri", 9F, FontStyle.Bold);
            updateCheckBox.Location = new Point(788, 391);
            updateCheckBox.Name = "updateCheckBox";
            updateCheckBox.Size = new Size(116, 18);
            updateCheckBox.TabIndex = 17;
            updateCheckBox.Text = "Check for Updates";
            updateCheckBox.UseVisualStyleBackColor = false;
            updateCheckBox.CheckedChanged += UpdateCheckButton_CheckedChanged;
            // 
            // ElementAmount
            // 
            ElementAmount.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ElementAmount.BackColor = SystemColors.Window;
            ElementAmount.BorderStyle = BorderStyle.None;
            ElementAmount.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ElementAmount.Location = new Point(1069, 542);
            ElementAmount.Name = "ElementAmount";
            ElementAmount.ReadOnly = true;
            ElementAmount.Size = new Size(21, 16);
            ElementAmount.TabIndex = 16;
            ElementAmount.Text = "70";
            ElementAmount.TextAlign = HorizontalAlignment.Center;
            // 
            // excludeClearsCheckBox
            // 
            excludeClearsCheckBox.AutoSize = true;
            excludeClearsCheckBox.BackColor = SystemColors.Window;
            excludeClearsCheckBox.Font = new Font("Calibri", 9F, FontStyle.Bold);
            excludeClearsCheckBox.Location = new Point(788, 367);
            excludeClearsCheckBox.Name = "excludeClearsCheckBox";
            excludeClearsCheckBox.Size = new Size(97, 18);
            excludeClearsCheckBox.TabIndex = 15;
            excludeClearsCheckBox.Text = "Exclude Clears";
            excludeClearsCheckBox.UseVisualStyleBackColor = false;
            excludeClearsCheckBox.CheckedChanged += ExcludeClearsButton_CheckedChanged;
            // 
            // excludeSoloCheckBox
            // 
            excludeSoloCheckBox.AutoSize = true;
            excludeSoloCheckBox.BackColor = SystemColors.Window;
            excludeSoloCheckBox.Font = new Font("Calibri", 9F, FontStyle.Bold);
            excludeSoloCheckBox.Location = new Point(788, 343);
            excludeSoloCheckBox.Name = "excludeSoloCheckBox";
            excludeSoloCheckBox.Size = new Size(88, 18);
            excludeSoloCheckBox.TabIndex = 3;
            excludeSoloCheckBox.Text = "Exclude Solo";
            excludeSoloCheckBox.UseVisualStyleBackColor = false;
            excludeSoloCheckBox.CheckedChanged += excludeSoloCheckBox_CheckedChanged;
            // 
            // soloCheckBox
            // 
            soloCheckBox.AutoSize = true;
            soloCheckBox.BackColor = SystemColors.Window;
            soloCheckBox.Font = new Font("Calibri", 9F, FontStyle.Bold);
            soloCheckBox.Location = new Point(788, 319);
            soloCheckBox.Name = "soloCheckBox";
            soloCheckBox.Size = new Size(103, 18);
            soloCheckBox.TabIndex = 1;
            soloCheckBox.Text = "Show Solo Only";
            soloCheckBox.UseVisualStyleBackColor = false;
            soloCheckBox.CheckedChanged += soloCheckBox_CheckedChanged;
            // 
            // DownloadButton
            // 
            DownloadButton.Font = new Font("Calibri", 9F, FontStyle.Bold);
            DownloadButton.Location = new Point(858, 422);
            DownloadButton.Name = "DownloadButton";
            DownloadButton.Size = new Size(139, 23);
            DownloadButton.TabIndex = 15;
            DownloadButton.Text = "Download Live CSVs";
            DownloadButton.UseVisualStyleBackColor = true;
            DownloadButton.Click += DownloadButton_Click;
            // 
            // jobtogglePanel
            // 
            jobtogglePanel.BackColor = SystemColors.Window;
            jobtogglePanel.BorderStyle = BorderStyle.FixedSingle;
            jobtogglePanel.Controls.Add(fakeBarToggleJob);
            jobtogglePanel.Location = new Point(858, 193);
            jobtogglePanel.Name = "jobtogglePanel";
            jobtogglePanel.Size = new Size(221, 113);
            jobtogglePanel.TabIndex = 13;
            // 
            // fakeBarToggleJob
            // 
            fakeBarToggleJob.BackgroundImage = Properties.Resources.FakeBar_;
            fakeBarToggleJob.BackgroundImageLayout = ImageLayout.Stretch;
            fakeBarToggleJob.Location = new Point(202, -2);
            fakeBarToggleJob.Name = "fakeBarToggleJob";
            fakeBarToggleJob.Size = new Size(18, 114);
            fakeBarToggleJob.TabIndex = 1;
            fakeBarToggleJob.TabStop = false;
            // 
            // jobtoggleLabel
            // 
            jobtoggleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            jobtoggleLabel.AutoSize = true;
            jobtoggleLabel.BackColor = Color.White;
            jobtoggleLabel.Font = new Font("Calibri", 9F, FontStyle.Bold);
            jobtoggleLabel.Location = new Point(788, 196);
            jobtoggleLabel.Name = "jobtoggleLabel";
            jobtoggleLabel.Size = new Size(28, 14);
            jobtoggleLabel.TabIndex = 12;
            jobtoggleLabel.Text = "Jobs";
            // 
            // jobownerButton
            // 
            jobownerButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            jobownerButton.BackColor = SystemColors.Window;
            jobownerButton.BorderStyle = BorderStyle.FixedSingle;
            jobownerButton.Image = Properties.Resources.Gear;
            jobownerButton.Location = new Point(830, 193);
            jobownerButton.Name = "jobownerButton";
            jobownerButton.Size = new Size(21, 21);
            jobownerButton.SizeMode = PictureBoxSizeMode.StretchImage;
            jobownerButton.TabIndex = 14;
            jobownerButton.TabStop = false;
            jobownerButton.Click += jobownerButton_Click;
            jobownerButton.MouseLeave += jobownerButton_MouseLeave;
            jobownerButton.MouseHover += jobownerButton_MouseHover;
            // 
            // filterbossComboBox
            // 
            filterbossComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            filterbossComboBox.FormattingEnabled = true;
            filterbossComboBox.Location = new Point(859, 32);
            filterbossComboBox.Name = "filterbossComboBox";
            filterbossComboBox.Size = new Size(139, 23);
            filterbossComboBox.TabIndex = 15;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(1, -2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(771, 566);
            tabControl1.TabIndex = 16;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.White;
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(763, 538);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Boss Parties";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.White;
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(763, 538);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Boss Crystals";
            // 
            // rightPanePB
            // 
            rightPanePB.BackgroundImage = Properties.Resources.RightPane;
            rightPanePB.BackgroundImageLayout = ImageLayout.Stretch;
            rightPanePB.Location = new Point(776, 21);
            rightPanePB.Name = "rightPanePB";
            rightPanePB.Size = new Size(318, 541);
            rightPanePB.TabIndex = 19;
            rightPanePB.TabStop = false;
            // 
            // playertogglePanel
            // 
            playertogglePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            playertogglePanel.BackColor = SystemColors.Window;
            playertogglePanel.BorderStyle = BorderStyle.FixedSingle;
            playertogglePanel.Controls.Add(fakeBarTogglePlayer);
            playertogglePanel.Location = new Point(858, 71);
            playertogglePanel.Name = "playertogglePanel";
            playertogglePanel.Size = new Size(221, 113);
            playertogglePanel.TabIndex = 11;
            // 
            // fakeBarTogglePlayer
            // 
            fakeBarTogglePlayer.BackgroundImage = Properties.Resources.FakeBar_;
            fakeBarTogglePlayer.BackgroundImageLayout = ImageLayout.Stretch;
            fakeBarTogglePlayer.Location = new Point(202, -1);
            fakeBarTogglePlayer.Name = "fakeBarTogglePlayer";
            fakeBarTogglePlayer.Size = new Size(18, 113);
            fakeBarTogglePlayer.TabIndex = 2;
            fakeBarTogglePlayer.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoScroll = true;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(1101, 566);
            Controls.Add(filterCrystal);
            Controls.Add(jobtogglePanel);
            Controls.Add(filterReport);
            Controls.Add(playertogglePanel);
            Controls.Add(clearfilterButton);
            Controls.Add(ElementAmount);
            Controls.Add(updateCheckBox);
            Controls.Add(DownloadButton);
            Controls.Add(jobownerButton);
            Controls.Add(tabControl1);
            Controls.Add(excludeClearsCheckBox);
            Controls.Add(filterbossComboBox);
            Controls.Add(excludeSoloCheckBox);
            Controls.Add(jobtoggleLabel);
            Controls.Add(soloCheckBox);
            Controls.Add(playersLabel);
            Controls.Add(filterButton);
            Controls.Add(bossnameLabel);
            Controls.Add(rightPanePB);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(1117, 2160);
            MinimumSize = new Size(1117, 605);
            Name = "Form1";
            Text = "Boss Tracker";
            jobtogglePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)fakeBarToggleJob).EndInit();
            ((System.ComponentModel.ISupportInitialize)jobownerButton).EndInit();
            tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)rightPanePB).EndInit();
            playertogglePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)fakeBarTogglePlayer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button filterButton;
        private Button clearfilterButton;
        private Label bossnameLabel;
        private Label playersLabel;
        private CheckBox soloCheckBox;
        private CheckBox excludeSoloCheckBox;
        private SmoothScrollPanel jobtogglePanel;
        private Label jobtoggleLabel;
        private PictureBox jobownerButton;
        private Button DownloadButton;
        private CheckBox excludeClearsCheckBox;
        private ComboBox filterbossComboBox;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button filterCrystal;
        private TextBox ElementAmount;
        private Button filterReport;
        private CheckBox updateCheckBox;
        private PictureBox rightPanePB;
        private PictureBox fakeBarToggleJob;
        private SmoothScrollPanel playertogglePanel;
        private PictureBox fakeBarTogglePlayer;
    }
}
