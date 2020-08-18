namespace project_vniia
{
    partial class Form_protokol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_protokol));
            this.comboBox_number = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox_system = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.radioButton_PSI = new System.Windows.Forms.RadioButton();
            this.radioButton_PK = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // comboBox_number
            // 
            this.comboBox_number.FormattingEnabled = true;
            this.comboBox_number.Location = new System.Drawing.Point(15, 36);
            this.comboBox_number.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox_number.Name = "comboBox_number";
            this.comboBox_number.Size = new System.Drawing.Size(124, 21);
            this.comboBox_number.TabIndex = 8;
            this.comboBox_number.SelectedIndexChanged += new System.EventHandler(this.comboBox_number_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Номер системы:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(415, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Протокол";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(423, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(439, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Путь для сохранения Word файла: (при изменении не забудьте добавить имя файла)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 102);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(397, 20);
            this.textBox1.TabIndex = 13;
            // 
            // textBox_system
            // 
            this.textBox_system.Location = new System.Drawing.Point(144, 37);
            this.textBox_system.Name = "textBox_system";
            this.textBox_system.Size = new System.Drawing.Size(315, 20);
            this.textBox_system.TabIndex = 14;
            // 
            // radioButton_PSI
            // 
            this.radioButton_PSI.AutoSize = true;
            this.radioButton_PSI.Location = new System.Drawing.Point(15, 138);
            this.radioButton_PSI.Name = "radioButton_PSI";
            this.radioButton_PSI.Size = new System.Drawing.Size(176, 17);
            this.radioButton_PSI.TabIndex = 15;
            this.radioButton_PSI.TabStop = true;
            this.radioButton_PSI.Text = "Приемосдаточных испытаний";
            this.radioButton_PSI.UseVisualStyleBackColor = true;
            // 
            // radioButton_PK
            // 
            this.radioButton_PK.AutoSize = true;
            this.radioButton_PK.Location = new System.Drawing.Point(15, 162);
            this.radioButton_PK.Name = "radioButton_PK";
            this.radioButton_PK.Size = new System.Drawing.Size(177, 17);
            this.radioButton_PK.TabIndex = 16;
            this.radioButton_PK.TabStop = true;
            this.radioButton_PK.Text = "Производственного контроля";
            this.radioButton_PK.UseVisualStyleBackColor = true;
            // 
            // Form_protokol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 197);
            this.Controls.Add(this.radioButton_PK);
            this.Controls.Add(this.radioButton_PSI);
            this.Controls.Add(this.textBox_system);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox_number);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_protokol";
            this.Text = "Form_protokol";
            this.Load += new System.EventHandler(this.Form_protokol_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_number;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox_system;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RadioButton radioButton_PSI;
        private System.Windows.Forms.RadioButton radioButton_PK;
    }
}