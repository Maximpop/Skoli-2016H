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
            this.displayTextBox = new System.Windows.Forms.TextBox();
            this.bt_textUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(20, 20);
            this.inputTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.ReadOnly = true;
            this.inputTextBox.Size = new System.Drawing.Size(398, 26);
            this.inputTextBox.TabIndex = 0;
            this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTextBox_KeyDown);
            // 
            // displayTextBox
            // 
            this.displayTextBox.Location = new System.Drawing.Point(20, 62);
            this.displayTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.displayTextBox.Multiline = true;
            this.displayTextBox.Name = "displayTextBox";
            this.displayTextBox.ReadOnly = true;
            this.displayTextBox.Size = new System.Drawing.Size(398, 327);
            this.displayTextBox.TabIndex = 1;
            // 
            // bt_textUpdate
            // 
            this.bt_textUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.bt_textUpdate.Location = new System.Drawing.Point(61, 397);
            this.bt_textUpdate.Name = "bt_textUpdate";
            this.bt_textUpdate.Size = new System.Drawing.Size(329, 59);
            this.bt_textUpdate.TabIndex = 2;
            this.bt_textUpdate.Text = "Send updated text to server";
            this.bt_textUpdate.UseVisualStyleBackColor = true;
            this.bt_textUpdate.Click += new System.EventHandler(this.bt_textUpdate_Click);
            // 
            // ChatClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 478);
            this.Controls.Add(this.bt_textUpdate);
            this.Controls.Add(this.displayTextBox);
            this.Controls.Add(this.inputTextBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ChatClientForm";
            this.Text = "Chat Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatClientForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.TextBox inputTextBox;
   private System.Windows.Forms.TextBox displayTextBox;
    private System.Windows.Forms.Button bt_textUpdate;
}

