namespace DBMSClinic.Tables
{
    partial class EditDoctor
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.comboBoxSpecialty = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMiddlename = new System.Windows.Forms.TextBox();
            this.textBoxFirstname = new System.Windows.Forms.TextBox();
            this.textBoxSurname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picturePhotoDoctor = new System.Windows.Forms.PictureBox();
            this.buttonUploadPhoto = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picturePhotoDoctor)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(321, 432);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(118, 41);
            this.buttonCancel.TabIndex = 26;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click_1);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(34, 432);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(118, 41);
            this.buttonAdd.TabIndex = 25;
            this.buttonAdd.Text = "Изменить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click_1);
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(22, 341);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(251, 28);
            this.comboBoxCategory.TabIndex = 24;
            // 
            // comboBoxSpecialty
            // 
            this.comboBoxSpecialty.FormattingEnabled = true;
            this.comboBoxSpecialty.Location = new System.Drawing.Point(22, 254);
            this.comboBoxSpecialty.Name = "comboBoxSpecialty";
            this.comboBoxSpecialty.Size = new System.Drawing.Size(251, 28);
            this.comboBoxSpecialty.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 308);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 22;
            this.label5.Text = "Категория";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "Специальность";
            // 
            // textBoxMiddlename
            // 
            this.textBoxMiddlename.Location = new System.Drawing.Point(111, 146);
            this.textBoxMiddlename.Name = "textBoxMiddlename";
            this.textBoxMiddlename.Size = new System.Drawing.Size(162, 27);
            this.textBoxMiddlename.TabIndex = 20;
            this.textBoxMiddlename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMiddlename_KeyPress);
            // 
            // textBoxFirstname
            // 
            this.textBoxFirstname.Location = new System.Drawing.Point(68, 87);
            this.textBoxFirstname.Name = "textBoxFirstname";
            this.textBoxFirstname.Size = new System.Drawing.Size(205, 27);
            this.textBoxFirstname.TabIndex = 19;
            this.textBoxFirstname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFirstname_KeyPress);
            // 
            // textBoxSurname
            // 
            this.textBoxSurname.Location = new System.Drawing.Point(109, 27);
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.Size = new System.Drawing.Size(164, 27);
            this.textBoxSurname.TabIndex = 18;
            this.textBoxSurname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSurname_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 146);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Отчество";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 87);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Имя";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Фамилия";
            // 
            // picturePhotoDoctor
            // 
            this.picturePhotoDoctor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picturePhotoDoctor.Image = global::DBMSClinic.Properties.Resources._3289576_individual_man_people_person_107097;
            this.picturePhotoDoctor.Location = new System.Drawing.Point(292, 22);
            this.picturePhotoDoctor.Margin = new System.Windows.Forms.Padding(5);
            this.picturePhotoDoctor.Name = "picturePhotoDoctor";
            this.picturePhotoDoctor.Size = new System.Drawing.Size(174, 213);
            this.picturePhotoDoctor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picturePhotoDoctor.TabIndex = 14;
            this.picturePhotoDoctor.TabStop = false;
            // 
            // buttonUploadPhoto
            // 
            this.buttonUploadPhoto.Location = new System.Drawing.Point(321, 254);
            this.buttonUploadPhoto.Name = "buttonUploadPhoto";
            this.buttonUploadPhoto.Size = new System.Drawing.Size(118, 50);
            this.buttonUploadPhoto.TabIndex = 27;
            this.buttonUploadPhoto.Text = "Обновить фото";
            this.buttonUploadPhoto.UseVisualStyleBackColor = true;
            this.buttonUploadPhoto.Click += new System.EventHandler(this.buttonUploadPhoto_Click);
            // 
            // EditDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 494);
            this.Controls.Add(this.buttonUploadPhoto);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.comboBoxCategory);
            this.Controls.Add(this.comboBoxSpecialty);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxMiddlename);
            this.Controls.Add(this.textBoxFirstname);
            this.Controls.Add(this.textBoxSurname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picturePhotoDoctor);
            this.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "EditDoctor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditDoctor";
            this.Load += new System.EventHandler(this.EditDoctor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePhotoDoctor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.ComboBox comboBoxSpecialty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMiddlename;
        private System.Windows.Forms.TextBox textBoxFirstname;
        private System.Windows.Forms.TextBox textBoxSurname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picturePhotoDoctor;
        private System.Windows.Forms.Button buttonUploadPhoto;
    }
}