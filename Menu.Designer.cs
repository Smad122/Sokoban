namespace Sokoban
{
    partial class Menu
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
            Start = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            Path = new System.Windows.Forms.Button();
            ChangeName = new System.Windows.Forms.Button();
            textBox2 = new System.Windows.Forms.TextBox();
            textBox3 = new System.Windows.Forms.TextBox();
            listView1 = new System.Windows.Forms.ListView();
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // Start
            // 
            Start.Cursor = System.Windows.Forms.Cursors.Hand;
            Start.Location = new System.Drawing.Point(288, 48);
            Start.Name = "Start";
            Start.Size = new System.Drawing.Size(203, 69);
            Start.TabIndex = 0;
            Start.Text = "Start";
            Start.UseVisualStyleBackColor = true;
            Start.Click += Start_Click;
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new System.Drawing.Point(542, 48);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Your path";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(212, 27);
            textBox1.TabIndex = 1;

            // 
            // Path
            // 
            Path.Cursor = System.Windows.Forms.Cursors.Hand;
            Path.Location = new System.Drawing.Point(542, 88);
            Path.Name = "Path";
            Path.Size = new System.Drawing.Size(212, 29);
            Path.TabIndex = 2;
            Path.Text = "Change path to map";
            Path.UseVisualStyleBackColor = true;
            Path.Click += button1_Click;
            // 
            // ChangeName
            // 
            ChangeName.Cursor = System.Windows.Forms.Cursors.Hand;
            ChangeName.Location = new System.Drawing.Point(36, 88);
            ChangeName.Name = "ChangeName";
            ChangeName.Size = new System.Drawing.Size(211, 29);
            ChangeName.TabIndex = 3;
            ChangeName.Text = "Set name";
            ChangeName.UseVisualStyleBackColor = true;
            ChangeName.Click += ChangeName_Click;
            // 
            // textBox2
            // 
            textBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            textBox2.Location = new System.Drawing.Point(36, 48);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "Write your name here";
            textBox2.Size = new System.Drawing.Size(211, 27);
            textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
            textBox3.Location = new System.Drawing.Point(258, 12);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new System.Drawing.Size(266, 27);
            textBox3.TabIndex = 5;
            textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listView1
            // 
            listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            listView1.Location = new System.Drawing.Point(180, 188);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(407, 250);
            listView1.TabIndex = 6;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = System.Windows.Forms.View.List;

            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(338, 165);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(94, 20);
            label1.TabIndex = 7;
            label1.Text = "Leaderboard";
            // 
            // Menu
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(label1);
            Controls.Add(listView1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(ChangeName);
            Controls.Add(Path);
            Controls.Add(textBox1);
            Controls.Add(Start);
            Name = "Menu";
            Text = "Sokoban";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Path;
        private System.Windows.Forms.Button ChangeName;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
    }
}