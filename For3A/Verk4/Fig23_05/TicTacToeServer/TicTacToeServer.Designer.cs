partial class TicTacToeServerForm
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
      this.displayTextBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // displayTextBox
      // 
      this.displayTextBox.Location = new System.Drawing.Point( 13, 13 );
      this.displayTextBox.Multiline = true;
      this.displayTextBox.Name = "displayTextBox";
      this.displayTextBox.ReadOnly = true;
      this.displayTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.displayTextBox.Size = new System.Drawing.Size( 267, 241 );
      this.displayTextBox.TabIndex = 0;
      // 
      // TicTacToeServerForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 292, 266 );
      this.Controls.Add( this.displayTextBox );
      this.Name = "TicTacToeServerForm";
      this.Text = "Tic Tac Toe Server";
      this.Load += new System.EventHandler( this.TicTacToeServerForm_Load );
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.TicTacToeServerForm_FormClosing );
      this.ResumeLayout( false );
      this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.TextBox displayTextBox;
}

