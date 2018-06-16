namespace BMS_Lifestyle
{
    partial class Saleable_Item_Screen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Saleable_Item_Screen));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.EAC_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rack = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.qty1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bal_R_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sa_da_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_EacCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbwarehouse = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txt_EacCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbwarehouse);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(632, 428);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EAC_Code,
            this.s_name,
            this.rack,
            this.qty1,
            this.Bal_R_qty,
            this.R_qty,
            this.sa_da_qty,
            this.balqty});
            this.dataGridView1.Location = new System.Drawing.Point(4, 105);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.Size = new System.Drawing.Size(622, 313);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            // 
            // EAC_Code
            // 
            this.EAC_Code.HeaderText = "EAC Code";
            this.EAC_Code.Name = "EAC_Code";
            this.EAC_Code.ReadOnly = true;
            this.EAC_Code.Width = 120;
            // 
            // s_name
            // 
            this.s_name.HeaderText = "S Name";
            this.s_name.Name = "s_name";
            this.s_name.ReadOnly = true;
            this.s_name.Width = 185;
            // 
            // rack
            // 
            this.rack.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.rack.HeaderText = "Rack";
            this.rack.Name = "rack";
            // 
            // qty1
            // 
            this.qty1.HeaderText = "Qty";
            this.qty1.Name = "qty1";
            this.qty1.ReadOnly = true;
            this.qty1.Width = 70;
            // 
            // Bal_R_qty
            // 
            this.Bal_R_qty.HeaderText = "Bal Return Qty";
            this.Bal_R_qty.Name = "Bal_R_qty";
            this.Bal_R_qty.ReadOnly = true;
            // 
            // R_qty
            // 
            this.R_qty.HeaderText = "Rqty";
            this.R_qty.Name = "R_qty";
            this.R_qty.Visible = false;
            // 
            // sa_da_qty
            // 
            this.sa_da_qty.HeaderText = "sadaqty";
            this.sa_da_qty.Name = "sa_da_qty";
            this.sa_da_qty.Visible = false;
            // 
            // balqty
            // 
            this.balqty.HeaderText = "balqty";
            this.balqty.Name = "balqty";
            this.balqty.Visible = false;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(234, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Saleable Item";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_EacCode
            // 
            this.txt_EacCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_EacCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_EacCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EacCode.ForeColor = System.Drawing.Color.Black;
            this.txt_EacCode.Location = new System.Drawing.Point(234, 78);
            this.txt_EacCode.Name = "txt_EacCode";
            this.txt_EacCode.Size = new System.Drawing.Size(158, 21);
            this.txt_EacCode.TabIndex = 3;
            this.txt_EacCode.Leave += new System.EventHandler(this.txt_EacCode_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Scan Here";
            // 
            // cmbwarehouse
            // 
            this.cmbwarehouse.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbwarehouse.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbwarehouse.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbwarehouse.ForeColor = System.Drawing.Color.Black;
            this.cmbwarehouse.FormattingEnabled = true;
            this.cmbwarehouse.Location = new System.Drawing.Point(234, 51);
            this.cmbwarehouse.Name = "cmbwarehouse";
            this.cmbwarehouse.Size = new System.Drawing.Size(158, 21);
            this.cmbwarehouse.TabIndex = 1;
            this.cmbwarehouse.SelectedIndexChanged += new System.EventHandler(this.cmbwarehouse_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Warehouse";
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(500, 434);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "&Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(569, 434);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 25);
            this.button2.TabIndex = 3;
            this.button2.Text = "&Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Saleable_Item_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(639, 463);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Saleable_Item_Screen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Saleable_Item_Screen";
            this.Load += new System.EventHandler(this.Saleable_Item_Screen_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_EacCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbwarehouse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn EAC_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn s_name;
        private System.Windows.Forms.DataGridViewComboBoxColumn rack;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bal_R_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn sa_da_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn balqty;
    }
}