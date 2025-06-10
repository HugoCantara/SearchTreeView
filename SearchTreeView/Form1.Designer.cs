namespace SearchTreeView
{
    partial class Form1
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
            this.searchTreeviewControlTest = new SearchTreeView.SearchTreeviewControl();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchTreeviewControlTest
            // 
            this.searchTreeviewControlTest.ExibirLogo = true;
            this.searchTreeviewControlTest.Location = new System.Drawing.Point(13, 13);
            this.searchTreeviewControlTest.Logo = global::SearchTreeView.Properties.Resources.SALARY;
            this.searchTreeviewControlTest.Name = "searchTreeviewControlTest";
            this.searchTreeviewControlTest.Size = new System.Drawing.Size(215, 425);
            this.searchTreeviewControlTest.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(312, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchTreeviewControlTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SearchTreeviewControl searchTreeviewControlTest;
        private System.Windows.Forms.Label label1;
    }
}

