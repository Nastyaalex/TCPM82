﻿namespace project_vniia
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.but_peregruzka = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Add_Blocks_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поменятьСтрокиМестамиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заменитьНомерБДToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сложнаяФильтрацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчётПоТипамБДToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сборСистемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.переместитьМеждуПодразделениямиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.протоколToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.протоколToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dataGridView1.Size = new System.Drawing.Size(623, 605);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(643, 49);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(372, 605);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DisplayMember = "Номер БД";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(857, 673);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.ValueMember = "Номер БД";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(206, 676);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // but_peregruzka
            // 
            this.but_peregruzka.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.but_peregruzka.Location = new System.Drawing.Point(993, 667);
            this.but_peregruzka.Name = "but_peregruzka";
            this.but_peregruzka.Size = new System.Drawing.Size(33, 30);
            this.but_peregruzka.TabIndex = 9;
            this.but_peregruzka.Text = "bloki";
            this.but_peregruzka.UseVisualStyleBackColor = true;
            this.but_peregruzka.Visible = false;
            this.but_peregruzka.Click += new System.EventHandler(this.but_peregruzka_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.сохранитьToolStripMenuItem1,
            this.протоколToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1038, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_Blocks_ToolStripMenuItem,
            this.поменятьСтрокиМестамиToolStripMenuItem,
            this.заменитьНомерБДToolStripMenuItem,
            this.сложнаяФильтрацияToolStripMenuItem,
            this.отчётПоТипамБДToolStripMenuItem,
            this.сборСистемыToolStripMenuItem,
            this.переместитьМеждуПодразделениямиToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(70, 20);
            this.toolStripMenuItem1.Text = "Действия";
            // 
            // Add_Blocks_ToolStripMenuItem
            // 
            this.Add_Blocks_ToolStripMenuItem.Name = "Add_Blocks_ToolStripMenuItem";
            this.Add_Blocks_ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.Add_Blocks_ToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.Add_Blocks_ToolStripMenuItem.Text = "Добавить блоки";
            this.Add_Blocks_ToolStripMenuItem.Click += new System.EventHandler(this.добавитьБлокиToolStripMenuItem_Click);
            // 
            // поменятьСтрокиМестамиToolStripMenuItem
            // 
            this.поменятьСтрокиМестамиToolStripMenuItem.Name = "поменятьСтрокиМестамиToolStripMenuItem";
            this.поменятьСтрокиМестамиToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.поменятьСтрокиМестамиToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.поменятьСтрокиМестамиToolStripMenuItem.Text = "Поменять строки местами";
            this.поменятьСтрокиМестамиToolStripMenuItem.Click += new System.EventHandler(this.поменятьСтрокиМестамиToolStripMenuItem_Click);
            // 
            // заменитьНомерБДToolStripMenuItem
            // 
            this.заменитьНомерБДToolStripMenuItem.Name = "заменитьНомерБДToolStripMenuItem";
            this.заменитьНомерБДToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Q)));
            this.заменитьНомерБДToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.заменитьНомерБДToolStripMenuItem.Text = "Заменить номер БД";
            this.заменитьНомерБДToolStripMenuItem.Click += new System.EventHandler(this.заменитьНомерБДToolStripMenuItem_Click);
            // 
            // сложнаяФильтрацияToolStripMenuItem
            // 
            this.сложнаяФильтрацияToolStripMenuItem.Name = "сложнаяФильтрацияToolStripMenuItem";
            this.сложнаяФильтрацияToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.сложнаяФильтрацияToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.сложнаяФильтрацияToolStripMenuItem.Text = "Сложная фильтрация";
            this.сложнаяФильтрацияToolStripMenuItem.Click += new System.EventHandler(this.сложнаяФильтрацияToolStripMenuItem_Click);
            // 
            // отчётПоТипамБДToolStripMenuItem
            // 
            this.отчётПоТипамБДToolStripMenuItem.Name = "отчётПоТипамБДToolStripMenuItem";
            this.отчётПоТипамБДToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.отчётПоТипамБДToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.отчётПоТипамБДToolStripMenuItem.Text = "Отчёт по типам БД";
            this.отчётПоТипамБДToolStripMenuItem.Click += new System.EventHandler(this.отчётПоТипамБДToolStripMenuItem_Click);
            // 
            // сборСистемыToolStripMenuItem
            // 
            this.сборСистемыToolStripMenuItem.Name = "сборСистемыToolStripMenuItem";
            this.сборСистемыToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
            this.сборСистемыToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.сборСистемыToolStripMenuItem.Text = "Сбор системы";
            this.сборСистемыToolStripMenuItem.Click += new System.EventHandler(this.сборСистемыToolStripMenuItem_Click);
            // 
            // переместитьМеждуПодразделениямиToolStripMenuItem
            // 
            this.переместитьМеждуПодразделениямиToolStripMenuItem.Name = "переместитьМеждуПодразделениямиToolStripMenuItem";
            this.переместитьМеждуПодразделениямиToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
            this.переместитьМеждуПодразделениямиToolStripMenuItem.Size = new System.Drawing.Size(324, 22);
            this.переместитьМеждуПодразделениямиToolStripMenuItem.Text = "Переместить между подразделениями";
            this.переместитьМеждуПодразделениямиToolStripMenuItem.Click += new System.EventHandler(this.переместитьМеждуПодразделениямиToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem1
            // 
            this.сохранитьToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem});
            this.сохранитьToolStripMenuItem1.Name = "сохранитьToolStripMenuItem1";
            this.сохранитьToolStripMenuItem1.Size = new System.Drawing.Size(77, 20);
            this.сохранитьToolStripMenuItem1.Text = "Сохранить";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click_1);
            // 
            // протоколToolStripMenuItem
            // 
            this.протоколToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.протоколToolStripMenuItem1});
            this.протоколToolStripMenuItem.Name = "протоколToolStripMenuItem";
            this.протоколToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.протоколToolStripMenuItem.Text = "Протокол";
            // 
            // протоколToolStripMenuItem1
            // 
            this.протоколToolStripMenuItem1.Name = "протоколToolStripMenuItem1";
            this.протоколToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.протоколToolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.протоколToolStripMenuItem1.Text = "Протокол";
            this.протоколToolStripMenuItem1.Click += new System.EventHandler(this.протоколToolStripMenuItem1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 676);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Введите значение для фильтрации:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.Location = new System.Drawing.Point(328, 676);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 12;
            this.textBox2.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(993, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(655, 678);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(106, 17);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Зафиксировать";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(821, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Пути к файлам и папкам";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 711);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.but_peregruzka);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "project_vniia_TCPM82_1.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button but_peregruzka;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem Add_Blocks_ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem поменятьСтрокиМестамиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заменитьНомерБДToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolStripMenuItem сложнаяФильтрацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётПоТипамБДToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сборСистемыToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem переместитьМеждуПодразделениямиToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem протоколToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem протоколToolStripMenuItem1;
    }
}

