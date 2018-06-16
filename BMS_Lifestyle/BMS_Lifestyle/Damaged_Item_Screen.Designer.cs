namespace BMS_Lifestyle
{
    partial class Damaged_Item_Screen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Damaged_Item_Screen));
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.EAC_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rack = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.qty1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bal_R_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sa_da_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_EacCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbwarehouse = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txt_EacCode);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cmbwarehouse);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(631, 428);
            this.panel2.TabIndex = 2;
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
            this.dataGridView1.Location = new System.Drawing.Point(3, 110);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.Size = new System.Drawing.Size(622, 313);
            this.dataGridView1.TabIndex = 7;
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
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(238, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 23);
            this.label4.TabIndex = 5;
            this.label4.Text = "Damaged Item";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_EacCode
            // 
            this.txt_EacCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_EacCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EacCode.ForeColor = System.Drawing.Color.Black;
            this.txt_EacCode.Location = new System.Drawing.Point(238, 83);
            this.txt_EacCode.Name = "txt_EacCode";
            this.txt_EacCode.Size = new System.Drawing.Size(158, 21);
            this.txt_EacCode.TabIndex = 3;
            this.txt_EacCode.Leave += new System.EventHandler(this.txt_EacCode_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Scan Here";
            // 
            // cmbwarehouse
            // 
            this.cmbwarehouse.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbwarehouse.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbwarehouse.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbwarehouse.ForeColor = System.Drawing.Color.Black;
            this.cmbwarehouse.FormattingEnabled = true;
            this.cmbwarehouse.Location = new System.Drawing.Point(238, 56);
            this.cmbwarehouse.Name = "cmbwarehouse";
            this.cmbwarehouse.Size = new System.Drawing.Size(158, 21);
            this.cmbwarehouse.TabIndex = 1;
            this.cmbwarehouse.SelectedIndexChanged += new System.EventHandler(this.cmbwarehouse_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Warehouse";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(566, 433);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 25);
            this.button2.TabIndex = 5;
            this.button2.Text = "&Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(494, 433);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "&Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Damaged_Item_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(637, 463);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Damaged_Item_Screen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damaged_Item_Screen";
            this.Load += new System.EventHandler(this.Damaged_Item_Screen_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_EacCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbwarehouse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
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