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
            this.label1 = new System.Windows.Forms.Label();
            this.searchTreeviewControlTest = new SearchTreeView.SearchTreeviewControl();
            this.dataGridViewSearch1 = new SearchTreeView.DataGridViewSearch();
            this.SuspendLayout();
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
            // searchTreeviewControlTest
            // 
            this.searchTreeviewControlTest.ExibirLogo = true;
            this.searchTreeviewControlTest.Location = new System.Drawing.Point(13, 13);
            this.searchTreeviewControlTest.Logo = global::SearchTreeView.Properties.Resources.SALARY;
            this.searchTreeviewControlTest.Name = "searchTreeviewControlTest";
            this.searchTreeviewControlTest.Size = new System.Drawing.Size(215, 425);
            this.searchTreeviewControlTest.TabIndex = 0;
            // 
            // dataGridViewSearch1
            // 
            this.dataGridViewSearch1.Location = new System.Drawing.Point(561, 35);
            this.dataGridViewSearch1.Name = "dataGridViewSearch1";
            this.dataGridViewSearch1.Size = new System.Drawing.Size(754, 450);
            this.dataGridViewSearch1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1373, 553);
            this.Controls.Add(this.dataGridViewSearch1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchTreeviewControlTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SearchTreeviewControl searchTreeviewControlTest;
        private System.Windows.Forms.Label label1;
        private DataGridViewSearch dataGridViewSearch1;
    }
}

