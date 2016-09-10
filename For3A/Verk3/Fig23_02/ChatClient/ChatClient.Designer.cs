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
      this.SuspendLayout();
      // 
      // inputTextBox
      // 
      this.inputTextBox.Location = new System.Drawing.Point( 13, 13 );
      this.inputTextBox.Name = "inputTextBox";
      this.inputTextBox.ReadOnly = true;
      this.inputTextBox.Size = new System.Drawing.Size( 267, 20 );
      this.inputTextBox.TabIndex = 0;
      this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler( this.inputTextBox_KeyDown );
      // 
      // displayTextBox
      // 
      this.displayTextBox.Location = new System.Drawing.Point( 13, 40 );
      this.displayTextBox.Multiline = true;
      this.displayTextBox.Name = "displayTextBox";
      this.displayTextBox.ReadOnly = true;
      this.displayTextBox.Size = new System.Drawing.Size( 267, 214 );
      this.displayTextBox.TabIndex = 1;
      // 
      // ChatClientForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 292, 266 );
      this.Controls.Add( this.displayTextBox );
      this.Controls.Add( this.inputTextBox );
      this.Name = "ChatClientForm";
      this.Text = "Chat Client";
      this.Load += new System.EventHandler( this.ChatClientForm_Load );
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.ChatClientForm_FormClosing );
      this.ResumeLayout( false );
      this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.TextBox inputTextBox;
   private System.Windows.Forms.TextBox displayTextBox;
}

