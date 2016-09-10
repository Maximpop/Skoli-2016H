// Fig. 23.1: ChatServer.cs
// Set up a server that will receive a connection from a client, send a
// string to the client, chat with the client and close the connection.
using System;
using System.Windows.Forms;
using System.Threading;  
using System.Net;        
using System.Net.Sockets;
using System.IO;         

public partial class ChatServerForm : Form
{
   public ChatServerForm()
   {
      InitializeComponent();
   } // end constructor

   private Socket connection; // Socket for accepting a connection      
   private Thread readThread; // Thread for processing incoming messages
   private NetworkStream socketStream; // network data stream           
   private BinaryWriter writer; // facilitates writing to the stream    
   private BinaryReader reader; // facilitates reading from the stream  

   // initialize thread for reading
   private void ChatServerForm_Load( object sender, EventArgs e )
   {
      readThread = new Thread( new ThreadStart( RunServer ) );
      readThread.Start();                                     
   } // end method CharServerForm_Load

   // close all threads associated with this application
   private void ChatServerForm_FormClosing( object sender, 
      FormClosingEventArgs e )
   {         
      System.Environment.Exit( System.Environment.ExitCode );
   } // end method CharServerForm_FormClosing

   // delegate that allows method DisplayMessage to be called
   // in the thread that creates and maintains the GUI       
   private delegate void DisplayDelegate( string message );  

   // method DisplayMessage sets displayTextBox's Text property
   // in a thread-safe manner
   private void DisplayMessage( string message )
   {
      //senda server messages í messagebox.show
      //senda það sem notandinn sendir í streamwriter
      // if modifying displayTextBox is not thread safe
      if ( displayTextBox.InvokeRequired )
      {
         // use inherited method Invoke to execute DisplayMessage
         // via a delegate                                       
         Invoke( new DisplayDelegate( DisplayMessage ),          
            new object[] { message } );                          
      } // end if
      else // OK to modify displayTextBox in current thread
         displayTextBox.Text += message;//ATH ÞESSA LÍNU
   } // end method DisplayMessage

   // delegate that allows method DisableInput to be called 
   // in the thread that creates and maintains the GUI
   private delegate void DisableInputDelegate( bool value );

   // method DisableInput sets inputTextBox's ReadOnly property
   // in a thread-safe manner
   private void DisableInput( bool value )
   {
      // if modifying inputTextBox is not thread safe
      if ( inputTextBox.InvokeRequired )
      {
         // use inherited method Invoke to execute DisableInput
         // via a delegate                                     
         Invoke( new DisableInputDelegate( DisableInput ),     
            new object[] { value } );                          
      } // end if
      else // OK to modify inputTextBox in current thread
         inputTextBox.ReadOnly = value;
   } // end method DisableInput

   // send the text typed at the server to the client
   private void inputTextBox_KeyDown( object sender, KeyEventArgs e )
   {
      // send the text to the client
      try
      {         
         if ( e.KeyCode == Keys.Enter && inputTextBox.ReadOnly == false )
         {
                StreamReader reader = new StreamReader(inputTextBox.Text);
                displayTextBox.Text = reader.ReadToEnd();

                writer.Write(reader.ReadToEnd());

            // if the user at the server signaled termination
            // sever the connection to the client
            if ( inputTextBox.Text == "TERMINATE" )
               connection.Close();

            inputTextBox.Clear(); // clear the user’s input
         } // end if
      } // end try
      catch ( SocketException )
      {
         displayTextBox.Text += "\nError writing object";
      } // end catch
   } // end method inputTextBox_KeyDown

   // allows a client to connect; displays text the client sends
   public void RunServer()
   {
      TcpListener listener;
      int counter = 1;

      // wait for a client connection and display the text
      // that the client sends
      try
      {
         // Step 1: create TcpListener                    
         IPAddress local = IPAddress.Parse( "192.168.1.225" );
         listener = new TcpListener( local, 40000 );       

         // Step 2: TcpListener waits for connection request
         listener.Start();                                  

         // Step 3: establish connection upon client request
         while ( true )
         {
            MessageBox.Show( "Waiting for connection\r\n" );

            // accept an incoming connection     
            connection = listener.AcceptSocket();

            // create NetworkStream object associated with socket
            socketStream = new NetworkStream( connection );      

            // create objects for transferring data across stream
            writer = new BinaryWriter( socketStream );           
            reader = new BinaryReader( socketStream );

                MessageBox.Show( "Connection " + counter + " received.\r\n" );

            // inform client that connection was successfull  
            writer.Write( "SERVER>>> Connection successful" );

            DisableInput( false ); // enable inputTextBox

            string theReply = "";

            // Step 4: read string data sent from client
            do
            {
               try
               {   
                  // read the string sent to the server
                  theReply = reader.ReadString();

                        // display the message
                        MessageBox.Show( "\r\n" + theReply );
               } // end try
               catch ( Exception )
               {
                  // handle exception if error reading data
                  break;
               } // end catch
            } while ( theReply != "CLIENT>>> TERMINATE"  &&
               connection.Connected );

                MessageBox.Show( "\r\nUser terminated connection\r\n" );

            // Step 5: close connection  
            writer.Close();              
            reader.Close();              
            socketStream.Close();        
            connection.Close();          

            DisableInput( true ); // disable InputTextBox
            counter++;
         } // end while
      } // end try
      catch ( Exception error )
      {
         MessageBox.Show( error.ToString() );
      } // end catch
   } // end method RunServer
} // end class ChatServerForm

/**************************************************************************
 * (C) Copyright 1992-2006 by Deitel & Associates, Inc. and               *
 * Pearson Education, Inc. All Rights Reserved.                           *
 *                                                                        *
 * DISCLAIMER: The authors and publisher of this book have used their     *
 * best efforts in preparing the book. These efforts include the          *
 * development, research, and testing of the theories and programs        *
 * to determine their effectiveness. The authors and publisher make       *
 * no warranty of any kind, expressed or implied, with regard to these    *
 * programs or to the documentation contained in these books. The authors *
 * and publisher shall not be liable in any event for incidental or       *
 * consequential damages in connection with, or arising out of, the       *
 * furnishing, performance, or use of these programs.                     *
 *************************************************************************/