namespace BMS_Lifestyle
{
    partial class CustomerEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerEdit));
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.cmbName = new System.Windows.Forms.ComboBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtAadharNo = new System.Windows.Forms.TextBox();
            this.NewName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTAN_no = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCIN_no = new System.Windows.Forms.TextBox();
            this.txtpanno = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtgstno = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPhoneNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtContactPer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.Color.Black;
            this.txtEmail.Location = new System.Drawing.Point(130, 227);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(234, 21);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyUp);
            // 
            // cmbName
            // 
            this.cmbName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbName.DisplayMember = "CustName";
            this.cmbName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbName.ForeColor = System.Drawing.Color.Black;
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Location = new System.Drawing.Point(130, 18);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new System.Drawing.Size(234, 21);
            this.cmbName.TabIndex = 0;
            this.cmbName.ValueMember = "CustName";
            this.cmbName.SelectedIndexChanged += new System.EventHandler(this.cmbName_SelectedIndexChanged);
            this.cmbName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbName_KeyUp);
            // 
            // txtMobile
            // 
            this.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobile.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMobile.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobile.ForeColor = System.Drawing.Color.Black;
            this.txtMobile.Location = new System.Drawing.Point(130, 173);
            this.txtMobile.MaxLength = 10;
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(234, 21);
            this.txtMobile.TabIndex = 5;
            this.txtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMobile_KeyPress);
            this.txtMobile.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMobile_KeyUp);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Red;
            this.btnExit.Location = new System.Drawing.Point(576, 276);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(67, 25);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.ForeColor = System.Drawing.Color.Black;
            this.txtAddress.Location = new System.Drawing.Point(130, 125);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(234, 42);
            this.txtAddress.TabIndex = 4;
            this.txtAddress.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyUp);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnUpdate.Location = new System.Drawing.Point(649, 276);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(67, 25);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtDisplayName);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtAadharNo);
            this.panel1.Controls.Add(this.NewName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtTAN_no);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtCIN_no);
            this.panel1.Controls.Add(this.txtpanno);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.cmbState);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtgstno);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtPhoneNo);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtContactPer);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cmbName);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.txtMobile);
            this.panel1.Controls.Add(this.txtAddress);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Font = new System.Drawing.Font("Vani", 8.25F);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 272);
            this.panel1.TabIndex = 0;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDisplayName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplayName.ForeColor = System.Drawing.Color.Black;
            this.txtDisplayName.Location = new System.Drawing.Point(130, 71);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(234, 21);
            this.txtDisplayName.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(11, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(96, 13);
            this.label15.TabIndex = 38;
            this.label15.Text = "Display Name";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(386, 154);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 36;
            this.label14.Text = "Aadhar No.";
            // 
            // txtAadharNo
            // 
            this.txtAadharNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAadharNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAadharNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAadharNo.ForeColor = System.Drawing.Color.Black;
            this.txtAadharNo.Location = new System.Drawing.Point(468, 152);
            this.txtAadharNo.MaxLength = 14;
            this.txtAadharNo.Name = "txtAadharNo";
            this.txtAadharNo.Size = new System.Drawing.Size(212, 21);
            this.txtAadharNo.TabIndex = 13;
            this.txtAadharNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAadharNo_KeyPress);
            this.txtAadharNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAadharNo_KeyUp);
            this.txtAadharNo.Leave += new System.EventHandler(this.txtAadharNo_Leave);
            // 
            // NewName
            // 
            this.NewName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NewName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.NewName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewName.ForeColor = System.Drawing.Color.Black;
            this.NewName.Location = new System.Drawing.Point(130, 44);
            this.NewName.Name = "NewName";
            this.NewName.Size = new System.Drawing.Size(234, 21);
            this.NewName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "New Name";
            // 
            // txtTAN_no
            // 
            this.txtTAN_no.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTAN_no.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTAN_no.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTAN_no.ForeColor = System.Drawing.Color.Black;
            this.txtTAN_no.Location = new System.Drawing.Point(468, 98);
            this.txtTAN_no.MaxLength = 10;
            this.txtTAN_no.Name = "txtTAN_no";
            this.txtTAN_no.Size = new System.Drawing.Size(212, 21);
            this.txtTAN_no.TabIndex = 11;
            this.txtTAN_no.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTAN_no_KeyUp);
            this.txtTAN_no.Leave += new System.EventHandler(this.txtTAN_no_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(386, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "TAN No.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(386, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "CIN No.";
            // 
            // txtCIN_no
            // 
            this.txtCIN_no.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCIN_no.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCIN_no.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCIN_no.ForeColor = System.Drawing.Color.Black;
            this.txtCIN_no.Location = new System.Drawing.Point(468, 125);
            this.txtCIN_no.MaxLength = 21;
            this.txtCIN_no.Name = "txtCIN_no";
            this.txtCIN_no.Size = new System.Drawing.Size(212, 21);
            this.txtCIN_no.TabIndex = 12;
            this.txtCIN_no.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCIN_no_KeyUp);
            this.txtCIN_no.Leave += new System.EventHandler(this.txtCIN_no_Leave);
            // 
            // txtpanno
            // 
            this.txtpanno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpanno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtpanno.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpanno.ForeColor = System.Drawing.Color.Black;
            this.txtpanno.Location = new System.Drawing.Point(468, 44);
            this.txtpanno.MaxLength = 10;
            this.txtpanno.Name = "txtpanno";
            this.txtpanno.Size = new System.Drawing.Size(212, 21);
            this.txtpanno.TabIndex = 9;
            this.txtpanno.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtpanno_KeyUp);
            this.txtpanno.Leave += new System.EventHandler(this.txtpanno_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(386, 46);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "PAN No.";
            // 
            // cmbState
            // 
            this.cmbState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbState.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbState.ForeColor = System.Drawing.Color.Black;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(468, 70);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(212, 21);
            this.cmbState.TabIndex = 10;
            this.cmbState.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbState_KeyUp);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(386, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "State";
            // 
            // txtgstno
            // 
            this.txtgstno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtgstno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtgstno.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgstno.ForeColor = System.Drawing.Color.Black;
            this.txtgstno.Location = new System.Drawing.Point(468, 19);
            this.txtgstno.MaxLength = 15;
            this.txtgstno.Name = "txtgstno";
            this.txtgstno.Size = new System.Drawing.Size(212, 21);
            this.txtgstno.TabIndex = 8;
            this.txtgstno.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtgstno_KeyUp);
            this.txtgstno.Leave += new System.EventHandler(this.txtgstno_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(386, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "GST No.";
            // 
            // txtPhoneNo
            // 
            this.txtPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhoneNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPhoneNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNo.ForeColor = System.Drawing.Color.Black;
            this.txtPhoneNo.Location = new System.Drawing.Point(130, 200);
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.txtPhoneNo.Size = new System.Drawing.Size(234, 21);
            this.txtPhoneNo.TabIndex = 6;
            this.txtPhoneNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhoneNo_KeyPress);
            this.txtPhoneNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPhoneNo_KeyUp);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(11, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Phone No.";
            // 
            // txtContactPer
            // 
            this.txtContactPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContactPer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtContactPer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactPer.ForeColor = System.Drawing.Color.Black;
            this.txtContactPer.Location = new System.Drawing.Point(130, 98);
            this.txtContactPer.Name = "txtContactPer";
            this.txtContactPer.Size = new System.Drawing.Size(234, 21);
            this.txtContactPer.TabIndex = 3;
            this.txtContactPer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtContactPer_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(11, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Contact Person";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(11, 229);
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
            this.label5.Location = new System.Drawing.Point(11, 175);
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
            this.label3.Location = new System.Drawing.Point(11, 127);
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
            this.label2.Location = new System.Drawing.Point(11, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Customer Name";
            // 
            // CustomerEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(721, 304);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CustomerEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Modify";
            this.Load += new System.EventHandler(this.CustomerEdit_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.ComboBox cmbName;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        //private ATMDbDataSet aTMDbDataSet;
       // private ATM.ATMDbDataSetTableAdapters.CustomerTableAdapter customerTableAdapter;
        private System.Windows.Forms.TextBox txtContactPer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPhoneNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtgstno;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbState;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtpanno;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTAN_no;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCIN_no;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NewName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtAadharNo;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label label15;
    }
}