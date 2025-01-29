namespace NESTracer
{
    partial class Form_Registry
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
            groupBox1 = new GroupBox();
            dataGridView_cpu = new DataGridView();
            groupBox2 = new GroupBox();
            dataGridView_ppu = new DataGridView();
            dataGridView_apu = new DataGridView();
            groupBox3 = new GroupBox();
            dataGridView_cartrig = new DataGridView();
            groupBox4 = new GroupBox();
            groupBox5 = new GroupBox();
            dataGridView_call_stack = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_cpu).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_ppu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_apu).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_cartrig).BeginInit();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_call_stack).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dataGridView_cpu);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(213, 545);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "cpu";
            // 
            // dataGridView_cpu
            // 
            dataGridView_cpu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_cpu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView_cpu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_cpu.Location = new Point(6, 22);
            dataGridView_cpu.Name = "dataGridView_cpu";
            dataGridView_cpu.ReadOnly = true;
            dataGridView_cpu.RowHeadersVisible = false;
            dataGridView_cpu.RowTemplate.Height = 25;
            dataGridView_cpu.Size = new Size(200, 517);
            dataGridView_cpu.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView_ppu);
            groupBox2.Location = new Point(231, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(271, 545);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "ppu";
            // 
            // dataGridView_ppu
            // 
            dataGridView_ppu.AllowUserToAddRows = false;
            dataGridView_ppu.AllowUserToDeleteRows = false;
            dataGridView_ppu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_ppu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView_ppu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_ppu.Location = new Point(6, 22);
            dataGridView_ppu.Name = "dataGridView_ppu";
            dataGridView_ppu.ReadOnly = true;
            dataGridView_ppu.RowHeadersVisible = false;
            dataGridView_ppu.RowTemplate.Height = 25;
            dataGridView_ppu.Size = new Size(259, 517);
            dataGridView_ppu.TabIndex = 1;
            // 
            // dataGridView_apu
            // 
            dataGridView_apu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_apu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView_apu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_apu.Location = new Point(6, 22);
            dataGridView_apu.Name = "dataGridView_apu";
            dataGridView_apu.ReadOnly = true;
            dataGridView_apu.RowHeadersVisible = false;
            dataGridView_apu.RowTemplate.Height = 25;
            dataGridView_apu.Size = new Size(237, 517);
            dataGridView_apu.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dataGridView_apu);
            groupBox3.Location = new Point(508, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(249, 545);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "apu";
            // 
            // dataGridView_cartrig
            // 
            dataGridView_cartrig.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_cartrig.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView_cartrig.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_cartrig.Location = new Point(6, 22);
            dataGridView_cartrig.Name = "dataGridView_cartrig";
            dataGridView_cartrig.ReadOnly = true;
            dataGridView_cartrig.RowHeadersVisible = false;
            dataGridView_cartrig.RowTemplate.Height = 25;
            dataGridView_cartrig.Size = new Size(200, 124);
            dataGridView_cartrig.TabIndex = 0;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(dataGridView_cartrig);
            groupBox4.Location = new Point(763, 12);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(213, 159);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "cartrige";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(dataGridView_call_stack);
            groupBox5.Location = new Point(764, 177);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(212, 142);
            groupBox5.TabIndex = 5;
            groupBox5.TabStop = false;
            groupBox5.Text = "call stack";
            // 
            // dataGridView_call_stack
            // 
            dataGridView_call_stack.AllowUserToAddRows = false;
            dataGridView_call_stack.AllowUserToDeleteRows = false;
            dataGridView_call_stack.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_call_stack.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView_call_stack.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_call_stack.Location = new Point(5, 22);
            dataGridView_call_stack.MultiSelect = false;
            dataGridView_call_stack.Name = "dataGridView_call_stack";
            dataGridView_call_stack.ReadOnly = true;
            dataGridView_call_stack.RowHeadersVisible = false;
            dataGridView_call_stack.RowTemplate.Height = 25;
            dataGridView_call_stack.Size = new Size(200, 110);
            dataGridView_call_stack.TabIndex = 33;
            // 
            // Form_Registry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(985, 565);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form_Registry";
            Text = "Register View";
            FormClosing += Form_Registry_FormClosing;
            Shown += Form_Registry_Shown;
            ResizeEnd += Form_Registry_ResizeEnd;
            Paint += Form_Registry_Paint;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_cpu).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_ppu).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_apu).EndInit();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_cartrig).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_call_stack).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBox1;
        private DataGridView dataGridView_cpu;
        private GroupBox groupBox2;
        private DataGridView dataGridView_ppu;
        private DataGridView dataGridView_apu;
        private GroupBox groupBox3;
        private DataGridView dataGridView_cartrig;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private DataGridView dataGridView_call_stack;
    }
}