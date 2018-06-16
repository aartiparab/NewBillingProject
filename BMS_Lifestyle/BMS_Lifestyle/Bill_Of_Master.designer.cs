namespace BMS_Lifestyle
{
    partial class Bill_Of_Master
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bill_Of_Master));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtkitname = new System.Windows.Forms.ComboBox();
            this.txtTotalQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dtpBOM = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEacCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtskucode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_EacCode = new System.Windows.Forms.TextBox();
            this.eaccode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stkqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txt_EacCode);
            this.panel1.Controls.Add(this.txtkitname);
            this.panel1.Controls.Add(this.txtTotalQty);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.dtpBOM);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtEacCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 430);
            this.panel1.TabIndex = 0;
            // 
            // txtkitname
            // 
            this.txtkitname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtkitname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtkitname.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtkitname.ForeColor = System.Drawing.Color.Black;
            this.txtkitname.FormattingEnabled = true;
            this.txtkitname.Location = new System.Drawing.Point(85, 14);
            this.txtkitname.Name = "txtkitname";
            this.txtkitname.Size = new System.Drawing.Size(259, 21);
            this.txtkitname.TabIndex = 0;
            this.txtkitname.SelectedIndexChanged += new System.EventHandler(this.txtkitname_SelectedIndexChanged);
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtTotalQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotalQty.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalQty.ForeColor = System.Drawing.Color.Black;
            this.txtTotalQty.Location = new System.Drawing.Point(434, 401);
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.ReadOnly = true;
            this.txtTotalQty.Size = new System.Drawing.Size(115, 22);
            this.txtTotalQty.TabIndex = 4;
            this.txtTotalQty.TabStop = false;
            this.txtTotalQty.Text = "0";
            this.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(362, 405);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Total Qty";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.eaccode,
            this.sname,
            this.qty1,
            this.stkqty,
            this.balqty});
            this.dataGridView1.Location = new System.Drawing.Point(5, 95);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Size = new System.Drawing.Size(544, 300);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            // 
            // dtpBOM
            // 
            this.dtpBOM.CustomFormat = "dd/MM/yyyy";
            this.dtpBOM.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBOM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBOM.Location = new System.Drawing.Point(454, 11);
            this.dtpBOM.Name = "dtpBOM";
            this.dtpBOM.Size = new System.Drawing.Size(95, 21);
            this.dtpBOM.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(411, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Date";
            // 
            // txtEacCode
            // 
            this.txtEacCode.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtEacCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEacCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEacCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEacCode.ForeColor = System.Drawing.Color.Black;
            this.txtEacCode.Location = new System.Drawing.Point(85, 41);
            this.txtEacCode.Name = "txtEacCode";
            this.txtEacCode.ReadOnly = true;
            this.txtEacCode.Size = new System.Drawing.Size(177, 21);
            this.txtEacCode.TabIndex = 1;
            this.txtEacCode.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "EAC Code";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kit Name ";
            // 
            // txtskucode
            // 
            this.txtskucode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtskucode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtskucode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtskucode.ForeColor = System.Drawing.Color.Black;
            this.txtskucode.Location = new System.Drawing.Point(78, 438);
            this.txtskucode.Name = "txtskucode";
            this.txtskucode.ReadOnly = true;
            this.txtskucode.Size = new System.Drawing.Size(23, 21);
            this.txtskucode.TabIndex = 5;
            this.txtskucode.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 442);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "SKU Code";
            this.label3.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(492, 438);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "&Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(419, 438);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 25);
            this.button2.TabIndex = 1;
            this.button2.Text = "&Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Scan Here";
            // 
            // txt_EacCode
            // 
            this.txt_EacCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_EacCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_EacCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EacCode.ForeColor = System.Drawing.Color.Black;
            this.txt_EacCode.Location = new System.Drawing.Point(85, 68);
            this.txt_EacCode.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_EacCode.MaxLength = 10000;
            this.txt_EacCode.Name = "txt_EacCode";
            this.txt_EacCode.Size = new System.Drawing.Size(177, 21);
            this.txt_EacCode.TabIndex = 26;
            this.txt_EacCode.Leave += new System.EventHandler(this.txt_EacCode_Leave);
            // 
            // eaccode
            // 
            this.eaccode.HeaderText = "EAC Code";
            this.eaccode.Name = "eaccode";
            this.eaccode.ReadOnly = true;
            this.eaccode.Width = 150;
            // 
            // sname
            // 
            this.sname.HeaderText = "SName";
            this.sname.Name = "sname";
            this.sname.ReadOnly = true;
            this.sname.Width = 250;
            // 
            // qty1
            // 
            this.qty1.HeaderText = "Quantity";
            this.qty1.Name = "qty1";
            this.qty1.ReadOnly = true;
            // 
            // stkqty
            // 
            this.stkqty.HeaderText = "stkqty";
            this.stkqty.Name = "stkqty";
            this.stkqty.ReadOnly = true;
            this.stkqty.Visible = false;
            // 
            // balqty
            // 
            this.balqty.HeaderText = "balqty";
            this.balqty.Name = "balqty";
            this.balqty.ReadOnly = true;
            this.balqty.Visible = false;
            // 
            // Bill_Of_Master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(563, 465);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtskucode);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Bill_Of_Master";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bill Of Material";
            this.Load += new System.EventHandler(this.Bill_Of_Master_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtskucode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEacCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpBOM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtTotalQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox txtkitname;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_EacCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn eaccode;
        private System.Windows.Forms.DataGridViewTextBoxColumn sname;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty1;
        private System.Windows.Forms.DataGridViewTextBoxColumn stkqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn balqty;
    }
}