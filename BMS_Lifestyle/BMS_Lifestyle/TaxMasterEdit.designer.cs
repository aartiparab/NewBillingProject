namespace BMS_Lifestyle
{
    partial class TaxMasterEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaxMasterEdit));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtnewName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtigsttax = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtsgsttax = new System.Windows.Forms.TextBox();
            this.txtcgsttax = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtVatPer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtnewName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.txtigsttax);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtsgsttax);
            this.panel1.Controls.Add(this.txtcgsttax);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtVatPer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 190);
            this.panel1.TabIndex = 0;
            // 
            // txtnewName
            // 
            this.txtnewName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnewName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtnewName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnewName.ForeColor = System.Drawing.Color.Black;
            this.txtnewName.Location = new System.Drawing.Point(124, 44);
            this.txtnewName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtnewName.Name = "txtnewName";
            this.txtnewName.Size = new System.Drawing.Size(133, 21);
            this.txtnewName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "New Tax Name";
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.Color.Black;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(124, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(133, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtigsttax
            // 
            this.txtigsttax.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtigsttax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtigsttax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtigsttax.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtigsttax.ForeColor = System.Drawing.Color.Black;
            this.txtigsttax.Location = new System.Drawing.Point(124, 152);
            this.txtigsttax.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtigsttax.Name = "txtigsttax";
            this.txtigsttax.ReadOnly = true;
            this.txtigsttax.Size = new System.Drawing.Size(102, 21);
            this.txtigsttax.TabIndex = 5;
            this.txtigsttax.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtigsttax_KeyUp);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(16, 154);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 13);
            this.label14.TabIndex = 85;
            this.label14.Text = "IGST Tax";
            // 
            // txtsgsttax
            // 
            this.txtsgsttax.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtsgsttax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsgsttax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsgsttax.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsgsttax.ForeColor = System.Drawing.Color.Black;
            this.txtsgsttax.Location = new System.Drawing.Point(124, 125);
            this.txtsgsttax.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtsgsttax.Name = "txtsgsttax";
            this.txtsgsttax.ReadOnly = true;
            this.txtsgsttax.Size = new System.Drawing.Size(102, 21);
            this.txtsgsttax.TabIndex = 4;
            this.txtsgsttax.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtsgsttax_KeyUp);
            // 
            // txtcgsttax
            // 
            this.txtcgsttax.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtcgsttax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcgsttax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcgsttax.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcgsttax.ForeColor = System.Drawing.Color.Black;
            this.txtcgsttax.Location = new System.Drawing.Point(124, 98);
            this.txtcgsttax.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtcgsttax.Name = "txtcgsttax";
            this.txtcgsttax.ReadOnly = true;
            this.txtcgsttax.Size = new System.Drawing.Size(102, 21);
            this.txtcgsttax.TabIndex = 3;
            this.txtcgsttax.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtcgsttax_KeyUp);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(16, 127);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 13);
            this.label15.TabIndex = 82;
            this.label15.Text = "SGST Tax";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(16, 100);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 81;
            this.label13.Text = "CGST Tax";
            // 
            // txtVatPer
            // 
            this.txtVatPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVatPer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVatPer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVatPer.ForeColor = System.Drawing.Color.Black;
            this.txtVatPer.Location = new System.Drawing.Point(124, 71);
            this.txtVatPer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtVatPer.Name = "txtVatPer";
            this.txtVatPer.Size = new System.Drawing.Size(102, 21);
            this.txtVatPer.TabIndex = 2;
            this.txtVatPer.TextChanged += new System.EventHandler(this.txtVatPer_TextChanged);
            this.txtVatPer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVatPer_KeyPress);
            this.txtVatPer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtVatPer_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(16, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tax %";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tax Name";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Red;
            this.btnExit.Location = new System.Drawing.Point(140, 196);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(67, 25);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(210, 196);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(67, 25);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Update";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TaxMasterEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(281, 225);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "TaxMasterEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tax Modify";
            this.Load += new System.EventHandler(this.TaxMasterEdit_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtVatPer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        // private ATMDbDataSet6 aTMDbDataSet6;
       // private ATM.ATMDbDataSet6TableAdapters.TaxTableAdapter taxTableAdapter;
        private System.Windows.Forms.TextBox txtigsttax;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtsgsttax;
        private System.Windows.Forms.TextBox txtcgsttax;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtnewName;
        private System.Windows.Forms.Label label2;

    }
}