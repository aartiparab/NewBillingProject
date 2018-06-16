namespace BMS_Lifestyle
{
    partial class Customer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Customer));
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.txtAadharNo = new System.Windows.Forms.TextBox();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtpanno = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtgstno = new System.Windows.Forms.TextBox();
            this.txtTAN_no = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCIN_no = new System.Windows.Forms.TextBox();
            this.txtPhoneNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtContactPer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtAdd = new System.Windows.Forms.TextBox();
            this.txtCust_Name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.Color.Black;
            this.txtEmail.Location = new System.Drawing.Point(130, 201);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(227, 21);
            this.txtEmail.TabIndex = 6;
            this.txtEmail.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtAadharNo);
            this.panel1.Controls.Add(this.txtDisplayName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtpanno);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.cmbState);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtgstno);
            this.panel1.Controls.Add(this.txtTAN_no);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtCIN_no);
            this.panel1.Controls.Add(this.txtPhoneNo);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtContactPer);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtMobile);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.txtAdd);
            this.panel1.Controls.Add(this.txtCust_Name);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 246);
            this.panel1.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(376, 152);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Aadhar No.";
            // 
            // txtAadharNo
            // 
            this.txtAadharNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAadharNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAadharNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAadharNo.ForeColor = System.Drawing.Color.Black;
            this.txtAadharNo.Location = new System.Drawing.Point(456, 148);
            this.txtAadharNo.MaxLength = 14;
            this.txtAadharNo.Name = "txtAadharNo";
            this.txtAadharNo.Size = new System.Drawing.Size(212, 21);
            this.txtAadharNo.TabIndex = 12;
            this.txtAadharNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAadharNo_KeyPress);
            this.txtAadharNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAadharNo_KeyUp);
            this.txtAadharNo.Leave += new System.EventHandler(this.txtAadharNo_Leave);
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDisplayName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplayName.ForeColor = System.Drawing.Color.Black;
            this.txtDisplayName.Location = new System.Drawing.Point(130, 46);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(227, 21);
            this.txtDisplayName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(15, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Display Name";
            // 
            // txtpanno
            // 
            this.txtpanno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpanno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtpanno.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpanno.ForeColor = System.Drawing.Color.Black;
            this.txtpanno.Location = new System.Drawing.Point(456, 46);
            this.txtpanno.MaxLength = 10;
            this.txtpanno.Name = "txtpanno";
            this.txtpanno.Size = new System.Drawing.Size(212, 21);
            this.txtpanno.TabIndex = 8;
            this.txtpanno.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtpanno_KeyUp);
            this.txtpanno.Leave += new System.EventHandler(this.txtpanno_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(376, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "PAN No.";
            // 
            // cmbState
            // 
            this.cmbState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbState.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbState.ForeColor = System.Drawing.Color.Black;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(456, 71);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(212, 21);
            this.cmbState.TabIndex = 9;
            this.cmbState.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbState_KeyUp);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(376, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "State";
            // 
            // txtgstno
            // 
            this.txtgstno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtgstno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtgstno.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgstno.ForeColor = System.Drawing.Color.Black;
            this.txtgstno.Location = new System.Drawing.Point(456, 20);
            this.txtgstno.MaxLength = 15;
            this.txtgstno.Name = "txtgstno";
            this.txtgstno.Size = new System.Drawing.Size(212, 21);
            this.txtgstno.TabIndex = 7;
            this.txtgstno.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtgstno_KeyUp);
            this.txtgstno.Leave += new System.EventHandler(this.txtgstno_Leave);
            // 
            // txtTAN_no
            // 
            this.txtTAN_no.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTAN_no.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTAN_no.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTAN_no.ForeColor = System.Drawing.Color.Black;
            this.txtTAN_no.Location = new System.Drawing.Point(456, 96);
            this.txtTAN_no.MaxLength = 10;
            this.txtTAN_no.Name = "txtTAN_no";
            this.txtTAN_no.Size = new System.Drawing.Size(212, 21);
            this.txtTAN_no.TabIndex = 10;
            this.txtTAN_no.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTAN_no_KeyUp);
            this.txtTAN_no.Leave += new System.EventHandler(this.txtTAN_no_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(376, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "TAN No.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(376, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "GST No.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(376, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "CIN No.";
            // 
            // txtCIN_no
            // 
            this.txtCIN_no.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCIN_no.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCIN_no.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCIN_no.ForeColor = System.Drawing.Color.Black;
            this.txtCIN_no.Location = new System.Drawing.Point(456, 122);
            this.txtCIN_no.MaxLength = 21;
            this.txtCIN_no.Name = "txtCIN_no";
            this.txtCIN_no.Size = new System.Drawing.Size(212, 21);
            this.txtCIN_no.TabIndex = 11;
            this.txtCIN_no.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCIN_no_KeyUp);
            this.txtCIN_no.Leave += new System.EventHandler(this.txtCIN_no_Leave);
            // 
            // txtPhoneNo
            // 
            this.txtPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhoneNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPhoneNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNo.ForeColor = System.Drawing.Color.Black;
            this.txtPhoneNo.Location = new System.Drawing.Point(130, 174);
            this.txtPhoneNo.MaxLength = 10;
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.txtPhoneNo.Size = new System.Drawing.Size(227, 21);
            this.txtPhoneNo.TabIndex = 5;
            this.txtPhoneNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPhoneNo_KeyUp);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(15, 176);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Phone No.";
            // 
            // txtContactPer
            // 
            this.txtContactPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactPer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtContactPer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactPer.ForeColor = System.Drawing.Color.Black;
            this.txtContactPer.Location = new System.Drawing.Point(130, 72);
            this.txtContactPer.Name = "txtContactPer";
            this.txtContactPer.Size = new System.Drawing.Size(227, 21);
            this.txtContactPer.TabIndex = 2;
            this.txtContactPer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtContactPer_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(15, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Contact Person";
            // 
            // txtMobile
            // 
            this.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobile.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMobile.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobile.ForeColor = System.Drawing.Color.Black;
            this.txtMobile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtMobile.Location = new System.Drawing.Point(130, 146);
            this.txtMobile.MaxLength = 10;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(227, 21);
            this.txtMobile.TabIndex = 4;
            this.txtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobile_KeyPress);
            this.txtMobile.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMobile_KeyUp);
            // 
            // txtAdd
            // 
            this.txtAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdd.ForeColor = System.Drawing.Color.Black;
            this.txtAdd.Location = new System.Drawing.Point(130, 98);
            this.txtAdd.Multiline = true;
            this.txtAdd.Name = "txtAdd";
            this.txtAdd.Size = new System.Drawing.Size(227, 42);
            this.txtAdd.TabIndex = 3;
            this.txtAdd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAdd_KeyUp);
            // 
            // txtCust_Name
            // 
            this.txtCust_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCust_Name.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCust_Name.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCust_Name.ForeColor = System.Drawing.Color.Black;
            this.txtCust_Name.Location = new System.Drawing.Point(130, 20);
            this.txtCust_Name.Name = "txtCust_Name";
            this.txtCust_Name.Size = new System.Drawing.Size(227, 21);
            this.txtCust_Name.TabIndex = 0;
            this.txtCust_Name.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCust_Name_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(15, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Email-ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(15, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Mobile No.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(15, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(15, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Customer Name";
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Red;
            this.btnExit.Location = new System.Drawing.Point(559, 255);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(67, 25);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(629, 255);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(67, 25);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Customer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(700, 285);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Customer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Add";
            this.Load += new System.EventHandler(this.Customer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAdd;
        private System.Windows.Forms.TextBox txtCust_Name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTAN_no;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCIN_no;
        private System.Windows.Forms.TextBox txtContactPer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPhoneNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtgstno;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtpanno;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtAadharNo;
    }
}