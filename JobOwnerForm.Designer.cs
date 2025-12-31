namespace Boss_Tracker
{
    partial class JobOwnerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobOwnerForm));
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("UD Digi Kyokasho N-B", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label1.Location = new Point(3, 254);
            label1.Name = "label1";
            label1.Size = new Size(631, 14);
            label1.TabIndex = 0;
            label1.Text = "Note: Setting anything here will cause the filter to ignore the simplified job filter in the main window";
            // 
            // JobOwnerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(648, 273);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "JobOwnerForm";
            Text = "Job Owner Config";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
    }
}