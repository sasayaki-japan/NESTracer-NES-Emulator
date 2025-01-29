namespace NESTracer
{
    partial class Form_IO
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
            comboBox1 = new ComboBox();
            dataGridView_io = new DataGridView();
            label1 = new Label();
            button_rescan = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_io).BeginInit();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(66, 6);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(155, 23);
            comboBox1.TabIndex = 0;
            // 
            // dataGridView_io
            // 
            dataGridView_io.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_io.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView_io.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_io.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView_io.Location = new Point(12, 66);
            dataGridView_io.MultiSelect = false;
            dataGridView_io.Name = "dataGridView_io";
            dataGridView_io.RowHeadersVisible = false;
            dataGridView_io.RowTemplate.Height = 25;
            dataGridView_io.Size = new Size(209, 243);
            dataGridView_io.TabIndex = 1;
            dataGridView_io.CellContentClick += dataGridView_io_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(48, 15);
            label1.TabIndex = 2;
            label1.Text = "Joystick";
            // 
            // button_rescan
            // 
            button_rescan.Location = new Point(164, 37);
            button_rescan.Name = "button_rescan";
            button_rescan.Size = new Size(57, 23);
            button_rescan.TabIndex = 7;
            button_rescan.Text = "rescan";
            button_rescan.UseVisualStyleBackColor = true;
            button_rescan.Click += button_rescan_Click;
            // 
            // Form_IO
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(234, 321);
            Controls.Add(button_rescan);
            Controls.Add(label1);
            Controls.Add(dataGridView_io);
            Controls.Add(comboBox1);
            Name = "Form_IO";
            Text = "I/O View";
            FormClosing += Form_IO_FormClosing;
            Shown += Form_IO_Shown;
            ResizeEnd += Form_IO_ResizeEnd;
            ((System.ComponentModel.ISupportInitialize)dataGridView_io).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox comboBox1;
        private DataGridView dataGridView_io;
        private Label label1;
        private Button button_rescan;
    }
}