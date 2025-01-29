namespace NESTracer
{
    partial class Form_IO_Setting
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
            label1 = new Label();
            button_cancel = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            button_clear = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 18);
            label1.Name = "label1";
            label1.Size = new Size(269, 15);
            label1.TabIndex = 0;
            label1.Text = "Press the button for the device you want to set up";
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(186, 40);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(75, 23);
            button_cancel.TabIndex = 1;
            button_cancel.Text = "cancel";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button1_Click_1;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            // 
            // button_clear
            // 
            button_clear.Location = new Point(64, 40);
            button_clear.Name = "button_clear";
            button_clear.Size = new Size(75, 23);
            button_clear.TabIndex = 2;
            button_clear.Text = "clear";
            button_clear.UseVisualStyleBackColor = true;
            button_clear.Click += button_clear_Click;
            // 
            // Form_IO_Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(303, 75);
            Controls.Add(button_clear);
            Controls.Add(button_cancel);
            Controls.Add(label1);
            Name = "Form_IO_Setting";
            Text = "button select";
            Shown += Form_IO_Setting_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button_cancel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button button_clear;
    }
}