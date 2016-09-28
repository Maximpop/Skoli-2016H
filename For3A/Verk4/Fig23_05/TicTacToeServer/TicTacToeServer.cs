// Fig. 23.5: TicTacToeServer.cs
// This class maintains a game of Tic-Tac-Toe for two
// client applications.
using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

public partial class TicTacToeServerForm : Form
{
    public TicTacToeServerForm()
    {
        InitializeComponent();
    } // end constructor

    private byte[] board; // the local representation of the game board  
    public Player[] players; // two Player objects                      
    private Thread[] playerThreads; // Threads for client interaction    
    private TcpListener listener; // listen for client connection        
    public int currentPlayer; // keep track of whose turn it is         
    private Thread getPlayers; // Thread for acquiring client connections
    internal bool disconnected = false; // true if the server closes
    public bool draw = false;

    // initialize variables and thread for receiving clients
    private void TicTacToeServerForm_Load(object sender, EventArgs e)
    {
        board = new byte[9];
        players = new Player[2];
        playerThreads = new Thread[2];
        currentPlayer = 0;

        // accept connections on a different thread         
        getPlayers = new Thread(new ThreadStart(SetUp));
        getPlayers.Start();
    } // end method TicTacToeServerForm_Load

    // notify Players to stop Running
    private void TicTacToeServerForm_FormClosing(object sender,
       FormClosingEventArgs e)
    {
        disconnected = true;
        System.Environment.Exit(System.Environment.ExitCode);
    } // end method TicTacToeServerForm_FormClosing

    // delegate that allows method DisplayMessage to be called
    // in the thread that creates and maintains the GUI       
    private delegate void DisplayDelegate(string message);

    // method DisplayMessage sets displayTextBox's Text property
    // in a thread-safe manner
    internal void DisplayMessage(string message)
    {
        // if modifying displayTextBox is not thread safe
        if (displayTextBox.InvokeRequired)
        {
            // use inherited method Invoke to execute DisplayMessage
            // via a delegate                                       
            Invoke(new DisplayDelegate(DisplayMessage),
               new object[] { message });
        } // end if
        else // OK to modify displayTextBox in current thread
            displayTextBox.Text += message;
    } // end method DisplayMessage

    // accepts connections from 2 players
    public void SetUp()
    {
        DisplayMessage("Waiting for players...\r\n");

        // set up Socket                                           
        listener =
           new TcpListener(IPAddress.Parse("127.0.0.1"), 50000);
        listener.Start();

        // accept first player and start a player thread              
        players[0] = new Player(listener.AcceptSocket(), this, 0);
        playerThreads[0] =
           new Thread(new ThreadStart(players[0].Run));
        playerThreads[0].Start();

        // accept second player and start another player thread       
        players[1] = new Player(listener.AcceptSocket(), this, 1);
        playerThreads[1] =
           new Thread(new ThreadStart(players[1].Run));
        playerThreads[1].Start();

        // let the first player know that the other player has connected
        lock (players[0])
        {
            players[0].threadSuspended = false;
            Monitor.Pulse(players[0]);
        } // end lock                                                   
    } // end method SetUp

    // determine if a move is valid
    public bool ValidMove(int location, int player)
    {
        // prevent another thread from making a move
        lock (this)
        {
            // while it is not the current player's turn, wait
            while (player != currentPlayer)
                Monitor.Wait(this);

            // if the desired square is not occupied
            if (!IsOccupied(location))
            {
                // set the board to contain the current player's mark
                board[location] = (byte)(currentPlayer == 0 ?
                   'X' : 'O');

                // set the currentPlayer to be the other player
                currentPlayer = (currentPlayer + 1) % 2;

                // notify the other player of the move                
                players[currentPlayer].OtherPlayerMoved(location);

                // alert the other player that it's time to move
                Monitor.Pulse(this);
                return true;
            } // end if
            else
                return false;
        } // end lock
    } // end method ValidMove

    // determines whether the specified square is occupied
    public bool IsOccupied(int location)
    {
        if (board[location] == 'X' || board[location] == 'O')
            return true;
        else
            return false;
    } // end method IsOccupied

    // determines if the game is over
    public bool GameOver()
    {
        // place code here to test for a winner of the game
        int count = 0;
        foreach (var item in board)
        {
            if (item == 0)
                count++;
        }

        if (board[0] == board[1] && board[1] == board[2] && board[0] != 0)
            return true;
        else if (board[3] == board[4] && board[4] == board[5] && board[3] != 0)
            return true;
        else if (board[6] == board[7] && board[7] == board[8] && board[6] != 0)
            return true;
        if (board[0] == board[3] && board[3] == board[6] && board[0] != 0)
            return true;
        else if (board[1] == board[4] && board[4] == board[7] && board[1] != 0)
            return true;
        else if (board[2] == board[5] && board[5] == board[8] && board[2] != 0)
            return true;
        else if (board[0] == board[4] && board[4] == board[8] && board[0] != 0)
            return true;
        else if (board[2] == board[4] && board[4] == board[6] && board[2] != 0)
            return true;
        else if (count == 0)
        {
            draw = true;
            return true;
        }
        else
            return false;
    } // end method GameOver
} // end class TicTacToeServerForm

// class Player represents a tic-tac-toe player
public class Player
{
    internal Socket connection; // Socket for accepting a connection    
    private NetworkStream socketStream; // network data stream          
    private TicTacToeServerForm server; // reference to server          
    private BinaryWriter writer; // facilitates writing to the stream   
    private BinaryReader reader; // facilitates reading from the stream 
    private int number; // player number                                
    private char mark; // player’s mark on the board                    
    internal bool threadSuspended = true; // if waiting for other player

    // constructor requiring Socket, TicTacToeServerForm and int 
    // objects as arguments
    public Player(Socket socket, TicTacToeServerForm serverValue,
       int newNumber)
    {
        mark = (newNumber == 0 ? 'X' : 'O');
        connection = socket;
        server = serverValue;
        number = newNumber;

        // create NetworkStream object for Socket      
        socketStream = new NetworkStream(connection);

        // create Streams for reading/writing bytes
        writer = new BinaryWriter(socketStream);
        reader = new BinaryReader(socketStream);
    } // end constructor

    // signal other player of move
    public void OtherPlayerMoved(int location)
    {
        // signal that opponent moved                     
        writer.Write("Opponent moved.");
        writer.Write(location); // send location of move
    } // end method OtherPlayerMoved

    // allows the players to make moves and receive moves
    // from the other player
    public void Run()
    {
        bool done = false;

        // display on the server that a connection was made
        server.DisplayMessage("Player " + (number == 0 ? 'X' : 'O')
           + " connected\r\n");

        // send the current player's mark to the client
        writer.Write(mark);

        // if number equals 0 then this player is X,                
        // otherwise O must wait for X's first move                 
        writer.Write("Player " + (number == 0 ?
           "X connected.\r\n" : "O connected, please wait.\r\n"));

        // X must wait for another player to arrive
        if (mark == 'X')
        {
            writer.Write("Waiting for another player.");

            // wait for notification from server that another 
            // player has connected
            lock (this)
            {
                while (threadSuspended)
                    Monitor.Wait(this);
            } // end lock               

            writer.Write("Other player connected. Your move.");
        } // end if

        // play game
        while (!done)
        {
            // wait for data to become available
            while (connection.Available == 0)
            {
                Thread.Sleep(1000);

                if (server.disconnected)
                    return;
            } // end while

            // receive data                   
            int location = reader.ReadInt32();

            // if the move is valid, display the move on the
            // server and signal that the move is valid
            if (server.ValidMove(location, number))
            {
                server.DisplayMessage("loc: " + location + "\r\n");
                writer.Write("Valid move.");
            } // end if
            else // signal that the move is invalid
                writer.Write("Invalid move, try again.");

            // if game is over, set done to true to exx it while loop
            if (server.GameOver())
            {
                done = true;
                string message;

                if (server.draw)
                {
                    message = "Draw!";
                }
                else
                {
                    message = (server.currentPlayer == 0 ? 'O' : 'X').ToString() + " wins!";
                }
                server.DisplayMessage("Game over " + message);
                server.players[0].writer.Write("Game over " + message);
                server.players[1].writer.Write("Game over " + message);
            }
        } // end while loop

        // close the socket connection
        writer.Close();
        reader.Close();
        socketStream.Close();
        connection.Close();
    } // end method Run
} // end class Player