using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;

namespace ConsoleATMServer
{
    class Program
    {
        private Socket connection; // Socket for accepting a connection
        private int counter = 0; // count the number of clients connected
        private string magicPIN = "5678";
        private int port = 8190;
        
        static void Main(string[] args)
        {
            new Program().Run();
        }

        void Run()
        {
            new Thread(new ThreadStart(RunServer)).Start();
        }

        public void RunServer()
        {
            Thread readThread; // Thread for processing incoming messages
            bool done = false;

            TcpListener listener;
            try
            {
                // Step 1: create TcpListener
                //IPAddress local = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                Console.WriteLine("Waiting for connection ...");

                while (!done)
                {
                    // This is where the server sits and waits for clients
                    connection = listener.AcceptSocket();
                    counter++;
                    Console.WriteLine("Starting a new client, numbered " + counter);
                    // Start a new thread for a client
                    readThread = new Thread(new ThreadStart(GetMessages));
                    readThread.Start();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Port " + port + " may be busy. Try another.");
            }

        }

        public void GetMessages()
        {
             Socket socket = connection;
             int count = counter;
             NetworkStream socketStream = null;
             BinaryWriter writer = null;
             BinaryReader reader = null;

            try
            {
                // establish the communication streams
                socketStream = new NetworkStream(socket);
                reader = new BinaryReader(socketStream);
                writer = new BinaryWriter(socketStream);
                writer.Write("Connection successful.\n");
                string message = null;

                bool okay = false;
                for (int tries = 0; tries < 3 && !okay; tries++)
                {
                    writer.Write("Please type in your PIN number or type CANCEL");
                    message = reader.ReadString();
                    Console.WriteLine("Client " + count + ":" + message);
                    okay = true;
                    switch (message)
                    {
                        case "CANCEL":
                            writer.Write("Transaction halted. Goodbye.");
                            break;
                        default:
                            if (message == magicPIN)
                            {
                                writer.Write("Please start your transactions.");
                                // Transaction
                                break;
                            }
                            else
                            {
                                writer.Write("Incorrect PIN. Try again.");
                                okay = false;
                            }
                            break;
                    }
                }
                writer.Write("Simulation complete. Thanks.");
                
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }
            finally
            {
                reader.Close();
                writer.Close();
                socketStream.Close();
                socket.Close();
            }

        }
    }
}
