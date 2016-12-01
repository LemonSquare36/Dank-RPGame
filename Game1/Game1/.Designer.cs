namespace RPGame
{
    partial class CrashForm
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
            handle.CrashFileRemove();
            base.Dispose(disposing);
            handle.MainClose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Email = new System.Windows.Forms.Button();
            this.TextBox = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Email
            // 
            this.Email.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Email.Location = new System.Drawing.Point(22, 208);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(134, 54);
            this.Email.TabIndex = 0;
            this.Email.Text = "Send Email of Crash";
            this.Email.UseVisualStyleBackColor = true;
            this.Email.Click += new System.EventHandler(this.Email_Click);
            // 
            // TextBox
            // 
            this.TextBox.BackColor = System.Drawing.SystemColors.Window;
            this.TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox.Location = new System.Drawing.Point(12, 12);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "TextBox";
            this.TextBox.ReadOnly = true;
            this.TextBox.Size = new System.Drawing.Size(237, 107);
            this.TextBox.TabIndex = 1;
            this.TextBox.Text = "The Application didnt close correctly last time. Would you like to send an Email " +
    "about this?";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(2, 142);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(195, 20);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "To: wSilentWEntertainment@gmail.com";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(203, 142);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(236, 137);
            this.textBox4.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(263, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Write the Email here";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Location = new System.Drawing.Point(266, 30);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(173, 40);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "*You will need to restart the Application after this";
            // 
            // CrashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 305);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.Email);
            this.Name = "CrashForm";
            this.Text = "CrashForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Email;
        private System.Windows.Forms.TextBox TextBox;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}