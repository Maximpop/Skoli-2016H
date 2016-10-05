partial class ChatClientForm
{
   /// <summary>
   /// Required designer variable.
   /// </summary>
   private System.ComponentModel.IContainer components = null;

   /// <summary>
   /// Clean up any resources being used.
   /// </summary>
   /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
   protected override void Dispose( bool disposing )
   {
      if (disposing && (components != null))
      {
         components.Dispose();
      }
      base.Dispose( disposing );
   }

   #region Windows Form Designer generated code

   /// <summary>
   /// Required method for Designer support - do not modify
   /// the contents of this method with the code editor.
   /// </summary>
   private void InitializeComponent()
   {
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_counter = new System.Windows.Forms.Label();
            this.label_word = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(12, 103);
            this.inputTextBox.MaxLength = 1;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(82, 20);
            this.inputTextBox.TabIndex = 0;
            this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Word";
            // 
            // label_counter
            // 
            this.label_counter.AutoSize = true;
            this.label_counter.Location = new System.Drawing.Point(183, 30);
            this.label_counter.Name = "label_counter";
            this.label_counter.Size = new System.Drawing.Size(0, 13);
            this.label_counter.TabIndex = 5;
            // 
            // label_word
            // 
            this.label_word.AutoSize = true;
            this.label_word.Location = new System.Drawing.Point(11, 65);
            this.label_word.Name = "label_word";
            this.label_word.Size = new System.Drawing.Size(35, 13);
            this.label_word.TabIndex = 6;
            this.label_word.Text = "label2";
            // 
            // ChatClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 311);
            this.Controls.Add(this.label_word);
            this.Controls.Add(this.label_counter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputTextBox);
            this.Name = "ChatClientForm";
            this.Text = "Chat Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatClientForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.TextBox inputTextBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label_counter;
    private System.Windows.Forms.Label label_word;
}

