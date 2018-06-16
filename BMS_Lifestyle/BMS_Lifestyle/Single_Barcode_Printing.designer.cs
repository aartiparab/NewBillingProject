namespace BMS_Lifestyle
{
    partial class Single_Barcode_Printing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Single_Barcode_Printing));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtgroup = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtS_Name = new System.Windows.Forms.TextBox();
            this.txtEac_code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSname = new System.Windows.Forms.Label();
            this.lblPname = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtgroup);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.txtS_Name);
            this.panel1.Controls.Add(this.txtEac_code);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblSname);
            this.panel1.Controls.Add(this.lblPname);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(333, 136);
            this.panel1.TabIndex = 0;
            // 
            // txtgroup
            // 
            this.txtgroup.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtgroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtgroup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgroup.ForeColor = System.Drawing.Color.Black;
            this.txtgroup.Location = new System.Drawing.Point(92, 44);
            this.txtgroup.Name = "txtgroup";
            this.txtgroup.ReadOnly = true;
            this.txtgroup.Size = new System.Drawing.Size(217, 21);
            this.txtgroup.TabIndex = 7;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(92, 71);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(217, 21);
            this.txtName.TabIndex = 6;
            // 
            // txtS_Name
            // 
            this.txtS_Name.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtS_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtS_Name.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS_Name.ForeColor = System.Drawing.Color.Black;
            this.txtS_Name.Location = new System.Drawing.Point(92, 98);
            this.txtS_Name.Name = "txtS_Name";
            this.txtS_Name.ReadOnly = true;
            this.txtS_Name.Size = new System.Drawing.Size(217, 21);
            this.txtS_Name.TabIndex = 5;
            // 
            // txtEac_code
            // 
            this.txtEac_code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEac_code.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEac_code.Location = new System.Drawing.Point(92, 17);
            this.txtEac_code.Name = "txtEac_code";
            this.txtEac_code.Size = new System.Drawing.Size(177, 21);
            this.txtEac_code.TabIndex = 4;
            this.txtEac_code.TextChanged += new System.EventHandler(this.txtEac_code_TextChanged);
            this.txtEac_code.Leave += new System.EventHandler(this.txtEac_code_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Group";
            // 
            // lblSname
            // 
            this.lblSname.AutoSize = true;
            this.lblSname.Location = new System.Drawing.Point(18, 101);
            this.lblSname.Name = "lblSname";
            this.lblSname.Size = new System.Drawing.Size(56, 13);
            this.lblSname.TabIndex = 2;
            this.lblSname.Text = "S Name";
            // 
            // lblPname
            // 
            this.lblPname.AutoSize = true;
            this.lblPname.Location = new System.Drawing.Point(18, 74);
            this.lblPname.Name = "lblPname";
            this.lblPname.Size = new System.Drawing.Size(56, 13);
            this.lblPname.TabIndex = 1;
            this.lblPname.Text = "P Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "EAC Code";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(270, 147);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(67, 25);
            this.button4.TabIndex = 9;
            this.button4.Text = "&Print";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Red;
            this.btnExit.Location = new System.Drawing.Point(202, 147);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(67, 25);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Single_Barcode_Printing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(342, 176);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Single_Barcode_Printing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Single Barcode Printing";
            this.Load += new System.EventHandler(this.Single_Barcode_Printing_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtgroup;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtS_Name;
        private System.Windows.Forms.TextBox txtEac_code;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSname;
        private System.Windows.Forms.Label lblPname;
        private System.Windows.Forms.Label label1;
    }
}