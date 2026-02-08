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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            filterButton = new Button();
            clearfilterButton = new Button();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            ExcludeClearsButton = new CheckBox();
            DownloadButton = new Button();
            ElementAmount = new Label();
            excludeSoloCheckBox = new CheckBox();
            soloCheckBox = new CheckBox();
            label3 = new Label();
            playertogglePanel = new Panel();
            jobtogglePanel = new Panel();
            jobtoggleLabel = new Label();
            jobownerButton = new PictureBox();
            filterbossComboBox = new ComboBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            imageList1 = new ImageList(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)jobownerButton).BeginInit();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // filterButton
            // 
            filterButton.BackColor = SystemColors.ControlLightLight;
            filterButton.Font = new Font("Calibri", 9F, FontStyle.Bold);
            filterButton.Location = new Point(1034, 11);
            filterButton.Name = "filterButton";
            filterButton.Size = new Size(75, 23);
            filterButton.TabIndex = 1;
            filterButton.Text = "Filter";
            filterButton.UseVisualStyleBackColor = false;
            filterButton.Click += filterButton_Click;
            // 
            // clearfilterButton
            // 
            clearfilterButton.Font = new Font("Calibri", 9F, FontStyle.Bold);
            clearfilterButton.Location = new Point(1034, 41);
            clearfilterButton.Name = "clearfilterButton";
            clearfilterButton.Size = new Size(75, 23);
            clearfilterButton.TabIndex = 3;
            clearfilterButton.Text = "Clear";
            clearfilterButton.UseVisualStyleBackColor = true;
            clearfilterButton.Click += clearfilterButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 9F, FontStyle.Bold);
            label1.Location = new Point(817, 15);
            label1.Name = "label1";
            label1.Size = new Size(63, 14);
            label1.TabIndex = 5;
            label1.Text = "Boss Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Calibri", 9F, FontStyle.Bold);
            label2.Location = new Point(747, 46);
            label2.Name = "label2";
            label2.Size = new Size(43, 14);
            label2.TabIndex = 6;
            label2.Text = "Players";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(ExcludeClearsButton);
            panel1.Controls.Add(DownloadButton);
            panel1.Controls.Add(ElementAmount);
            panel1.Controls.Add(excludeSoloCheckBox);
            panel1.Controls.Add(soloCheckBox);
            panel1.Controls.Add(label3);
            panel1.Font = new Font("UD Digi Kyokasho N-B", 9F, FontStyle.Bold);
            panel1.Location = new Point(817, 449);
            panel1.Name = "panel1";
            panel1.Size = new Size(292, 117);
            panel1.TabIndex = 8;
            // 
            // ExcludeClearsButton
            // 
            ExcludeClearsButton.AutoSize = true;
            ExcludeClearsButton.Font = new Font("Calibri", 9F, FontStyle.Bold);
            ExcludeClearsButton.Location = new Point(4, 51);
            ExcludeClearsButton.Name = "ExcludeClearsButton";
            ExcludeClearsButton.Size = new Size(97, 18);
            ExcludeClearsButton.TabIndex = 15;
            ExcludeClearsButton.Text = "Exclude Clears";
            ExcludeClearsButton.UseVisualStyleBackColor = true;
            // 
            // DownloadButton
            // 
            DownloadButton.Font = new Font("Calibri", 9F, FontStyle.Bold);
            DownloadButton.Location = new Point(3, 89);
            DownloadButton.Name = "DownloadButton";
            DownloadButton.Size = new Size(152, 23);
            DownloadButton.TabIndex = 15;
            DownloadButton.Text = "Download Live CSVs";
            DownloadButton.UseVisualStyleBackColor = true;
            DownloadButton.Click += DownloadButton_Click;
            // 
            // ElementAmount
            // 
            ElementAmount.AutoSize = true;
            ElementAmount.BorderStyle = BorderStyle.FixedSingle;
            ElementAmount.Font = new Font("Calibri", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ElementAmount.Location = new Point(242, 100);
            ElementAmount.Name = "ElementAmount";
            ElementAmount.Size = new Size(15, 16);
            ElementAmount.TabIndex = 4;
            ElementAmount.Text = "0";
            // 
            // excludeSoloCheckBox
            // 
            excludeSoloCheckBox.AutoSize = true;
            excludeSoloCheckBox.Font = new Font("Calibri", 9F, FontStyle.Bold);
            excludeSoloCheckBox.Location = new Point(4, 27);
            excludeSoloCheckBox.Name = "excludeSoloCheckBox";
            excludeSoloCheckBox.Size = new Size(88, 18);
            excludeSoloCheckBox.TabIndex = 3;
            excludeSoloCheckBox.Text = "Exclude Solo";
            excludeSoloCheckBox.UseVisualStyleBackColor = true;
            // 
            // soloCheckBox
            // 
            soloCheckBox.AutoSize = true;
            soloCheckBox.Font = new Font("Calibri", 9F, FontStyle.Bold);
            soloCheckBox.Location = new Point(4, 3);
            soloCheckBox.Name = "soloCheckBox";
            soloCheckBox.Size = new Size(103, 18);
            soloCheckBox.TabIndex = 1;
            soloCheckBox.Text = "Show Solo Only";
            soloCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Font = new Font("Calibri", 9F, FontStyle.Bold);
            label3.Location = new Point(242, -1);
            label3.Name = "label3";
            label3.Size = new Size(49, 16);
            label3.TabIndex = 0;
            label3.Text = "Settings";
            // 
            // playertogglePanel
            // 
            playertogglePanel.AutoSize = true;
            playertogglePanel.BorderStyle = BorderStyle.FixedSingle;
            playertogglePanel.Location = new Point(889, 45);
            playertogglePanel.Name = "playertogglePanel";
            playertogglePanel.Size = new Size(139, 21);
            playertogglePanel.TabIndex = 11;
            // 
            // jobtogglePanel
            // 
            jobtogglePanel.AutoSize = true;
            jobtogglePanel.BorderStyle = BorderStyle.FixedSingle;
            jobtogglePanel.Location = new Point(889, 78);
            jobtogglePanel.Name = "jobtogglePanel";
            jobtogglePanel.Size = new Size(139, 21);
            jobtogglePanel.TabIndex = 13;
            // 
            // jobtoggleLabel
            // 
            jobtoggleLabel.AutoSize = true;
            jobtoggleLabel.Font = new Font("Calibri", 9F, FontStyle.Bold);
            jobtoggleLabel.Location = new Point(747, 79);
            jobtoggleLabel.Name = "jobtoggleLabel";
            jobtoggleLabel.Size = new Size(28, 14);
            jobtoggleLabel.TabIndex = 12;
            jobtoggleLabel.Text = "Jobs";
            // 
            // jobownerButton
            // 
            jobownerButton.BorderStyle = BorderStyle.FixedSingle;
            jobownerButton.Image = Properties.Resources.Gear;
            jobownerButton.Location = new Point(1034, 78);
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
            filterbossComboBox.FormattingEnabled = true;
            filterbossComboBox.Location = new Point(890, 12);
            filterbossComboBox.Name = "filterbossComboBox";
            filterbossComboBox.Size = new Size(139, 23);
            filterbossComboBox.TabIndex = 15;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(16, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(785, 558);
            tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(777, 530);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Boss Parties";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(777, 534);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Boss Crystals";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1124, 586);
            Controls.Add(tabControl1);
            Controls.Add(filterbossComboBox);
            Controls.Add(jobownerButton);
            Controls.Add(jobtogglePanel);
            Controls.Add(jobtoggleLabel);
            Controls.Add(playertogglePanel);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(clearfilterButton);
            Controls.Add(filterButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Boss Tracker";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)jobownerButton).EndInit();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button filterButton;
        private Button clearfilterButton;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private CheckBox soloCheckBox;
        private Label label3;
        private Panel playertogglePanel;
        private CheckBox excludeSoloCheckBox;
        private Panel jobtogglePanel;
        private Label jobtoggleLabel;
        private Label ElementAmount;
        private PictureBox jobownerButton;
        private Button DownloadButton;
        private CheckBox ExcludeClearsButton;
        private ComboBox filterbossComboBox;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ImageList imageList1;
    }
}
