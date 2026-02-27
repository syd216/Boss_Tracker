namespace Boss_Tracker
{
    partial class CrystalReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrystalReportForm));
            CrystalFlowPanel = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // CrystalFlowPanel
            // 
            CrystalFlowPanel.AutoScroll = true;
            CrystalFlowPanel.Dock = DockStyle.Fill;
            CrystalFlowPanel.Location = new Point(0, 0);
            CrystalFlowPanel.Margin = new Padding(0);
            CrystalFlowPanel.Name = "CrystalFlowPanel";
            CrystalFlowPanel.RightToLeft = RightToLeft.No;
            CrystalFlowPanel.Size = new Size(918, 503);
            CrystalFlowPanel.TabIndex = 0;
            CrystalFlowPanel.WrapContents = false;
            // 
            // CrystalReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(918, 503);
            Controls.Add(CrystalFlowPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "CrystalReportForm";
            Text = "Crystal Report";
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel CrystalFlowPanel;
    }
}