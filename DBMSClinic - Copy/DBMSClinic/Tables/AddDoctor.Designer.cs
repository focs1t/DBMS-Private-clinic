namespace DBMSClinic.Tables
{
    partial class AddDoctor
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSurname = new System.Windows.Forms.TextBox();
            this.textBoxFirstname = new System.Windows.Forms.TextBox();
            this.textBoxMiddlename = new System.Windows.Forms.TextBox();
            this.picturePhotoDoctor = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxSpecialty = new System.Windows.Forms.ComboBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonUploadPhoto = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picturePhotoDoctor)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Фамилия";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Имя";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 138);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Отчество";
            // 
            // textBoxSurname
            // 
            this.textBoxSurname.Location = new System.Drawing.Point(113, 19);
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.Size = new System.Drawing.Size(164, 27);
            this.textBoxSurname.TabIndex = 4;
            this.textBoxSurname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSurname_KeyPress);
            // 
            // textBoxFirstname
            // 
            this.textBoxFirstname.Location = new System.Drawing.Point(72, 79);
            this.textBoxFirstname.Name = "textBoxFirstname";
            this.textBoxFirstname.Size = new System.Drawing.Size(205, 27);
            this.textBoxFirstname.TabIndex = 5;
            this.textBoxFirstname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFirstname_KeyPress);
            // 
            // textBoxMiddlename
            // 
            this.textBoxMiddlename.Location = new System.Drawing.Point(115, 138);
            this.textBoxMiddlename.Name = "textBoxMiddlename";
            this.textBoxMiddlename.Size = new System.Drawing.Size(162, 27);
            this.textBoxMiddlename.TabIndex = 6;
            this.textBoxMiddlename.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMiddlename_KeyPress);
            // 
            // picturePhotoDoctor
            // 
            this.picturePhotoDoctor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picturePhotoDoctor.Image = global::DBMSClinic.Properties.Resources._3289576_individual_man_people_person_107097;
            this.picturePhotoDoctor.Location = new System.Drawing.Point(296, 14);
            this.picturePhotoDoctor.Margin = new System.Windows.Forms.Padding(5);
            this.picturePhotoDoctor.Name = "picturePhotoDoctor";
            this.picturePhotoDoctor.Size = new System.Drawing.Size(174, 213);
            this.picturePhotoDoctor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picturePhotoDoctor.TabIndex = 0;
            this.picturePhotoDoctor.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Специальность";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 300);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Категория";
            // 
            // comboBoxSpecialty
            // 
            this.comboBoxSpecialty.FormattingEnabled = true;
            this.comboBoxSpecialty.Location = new System.Drawing.Point(26, 246);
            this.comboBoxSpecialty.Name = "comboBoxSpecialty";
            this.comboBoxSpecialty.Size = new System.Drawing.Size(251, 28);
            this.comboBoxSpecialty.TabIndex = 9;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(26, 333);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(251, 28);
            this.comboBoxCategory.TabIndex = 10;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(38, 424);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(118, 41);
            this.buttonAdd.TabIndex = 11;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(325, 424);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(118, 41);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonUploadPhoto
            // 
            this.buttonUploadPhoto.Location = new System.Drawing.Point(325, 246);
            this.buttonUploadPhoto.Name = "buttonUploadPhoto";
            this.buttonUploadPhoto.Size = new System.Drawing.Size(118, 50);
            this.buttonUploadPhoto.TabIndex = 13;
            this.buttonUploadPhoto.Text = "Загрузить фото";
            this.buttonUploadPhoto.UseVisualStyleBackColor = true;
            this.buttonUploadPhoto.Click += new System.EventHandler(this.buttonUploadPhoto_Click);
            // 
            // AddDoctor
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
            this.Name = "AddDoctor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddDoctor";
            this.Load += new System.EventHandler(this.AddDoctor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturePhotoDoctor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picturePhotoDoctor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSurname;
        private System.Windows.Forms.TextBox textBoxFirstname;
        private System.Windows.Forms.TextBox textBoxMiddlename;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxSpecialty;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonUploadPhoto;
    }
}