namespace ScriptIn14Days
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox2 = new ScriptIn14Days.Tools.RichTextBoxEx();
            this.richTextBox1 = new ScriptIn14Days.Tools.RichTextBoxEx();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Line";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(95, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Word";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(176, 164);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Expression";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(257, 164);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Run";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Input Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox2.HideSelection = false;
            this.richTextBox2.Location = new System.Drawing.Point(12, 211);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.NumberAlignment = System.Drawing.StringAlignment.Center;
            this.richTextBox2.NumberBackground1 = System.Drawing.SystemColors.ControlLight;
            this.richTextBox2.NumberBackground2 = System.Drawing.SystemColors.Window;
            this.richTextBox2.NumberBorder = System.Drawing.SystemColors.ControlDark;
            this.richTextBox2.NumberBorderThickness = 1F;
            this.richTextBox2.NumberColor = System.Drawing.Color.DarkGray;
            this.richTextBox2.NumberFont = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.NumberLeadingZeroes = false;
            this.richTextBox2.NumberLineCounting = ScriptIn14Days.Tools.RichTextBoxEx.LineCounting.CRLF;
            this.richTextBox2.NumberPadding = 2;
            this.richTextBox2.ShowLineNumbers = true;
            this.richTextBox2.Size = new System.Drawing.Size(394, 100);
            this.richTextBox2.TabIndex = 6;
            this.richTextBox2.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.AcceptsTab = true;
            this.richTextBox1.Font = new System.Drawing.Font("SimSun", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 29);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.NumberAlignment = System.Drawing.StringAlignment.Center;
            this.richTextBox1.NumberBackground1 = System.Drawing.Color.PowderBlue;
            this.richTextBox1.NumberBackground2 = System.Drawing.Color.Gainsboro;
            this.richTextBox1.NumberBorder = System.Drawing.SystemColors.ActiveCaptionText;
            this.richTextBox1.NumberBorderThickness = 1F;
            this.richTextBox1.NumberColor = System.Drawing.Color.DimGray;
            this.richTextBox1.NumberFont = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.NumberLeadingZeroes = false;
            this.richTextBox1.NumberLineCounting = ScriptIn14Days.Tools.RichTextBoxEx.LineCounting.CRLF;
            this.richTextBox1.NumberPadding = 2;
            this.richTextBox1.ShowLineNumbers = true;
            this.richTextBox1.Size = new System.Drawing.Size(394, 120);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "i=3;\nj=4;\nkk=fun name(i,j){i+j;}\nif(i)\n{\ni=2;\nkk(3,9);\n}\nkk(3,9);\nsum=0;\nwhile(i)" +
    "\n{\ni--;\nsum=sum+i;\n}\nsum;";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 323);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ScriptIn14Days.Tools.RichTextBoxEx richTextBox1;
        private ScriptIn14Days.Tools.RichTextBoxEx richTextBox2;
    }
}

