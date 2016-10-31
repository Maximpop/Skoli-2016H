using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

namespace Verk6
{
    public partial class Form1 : Form
    {
        private NetworkStream output;
        private BinaryWriter writer;
        private BinaryReader reader;
        private string message = "";
        private string pinNumber = "";
        static int port = 8190;
        bool pressed = false;
        void Run()
        {
            new Thread(new ThreadStart(Connect)).Start();
        }
        void Connect()
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient();
                client.Connect("localhost", port);
                output = client.GetStream();
                writer = new BinaryWriter(output);
                reader = new BinaryReader(output);
                int value = 0;
                string item = null;
                string textboxHolder = null;
                do
                {
                    try
                    {
                        message = reader.ReadString();
                        if (message == "Please type in your PIN number or type CANCEL")
                        {
                            while (!pressed)
                            {
                                //wait for the button te be pressed(in theory)
                            }
                            try
                            {
                                value = Convert.ToInt32(input_textbox.Text);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("The pin must be only numbers");
                                MessageBox.Show("fegit");
                                Application.Exit();
                            }
                            MessageBox.Show("Pass 1");
                            writer.Write(value);
                            MessageBox.Show("Pass 2");
                            item = reader.ReadString();
                            MessageBox.Show("Pass 3");
                            textboxHolder = richTextBox1.Text;
                            MessageBox.Show("Pass 4");
                            richTextBox1.Text = (textboxHolder + "\n " + item);
                            MessageBox.Show("Pass 5");
                        }
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.ToString());
                        System.Environment.Exit(System.Environment.ExitCode);
                    }
                } while (message != "Simulation complete. Thanks.");

            } // end try
            catch (Exception error)
            {
                // handle exception if error in establishing connection
                Console.WriteLine(error.ToString());
                System.Environment.Exit(System.Environment.ExitCode);
            } // end catch
            finally
            {
                reader.Close();
                writer.Close();
                output.Close();
                client.Close();
            }
        }

        public Form1()
        {
            InitializeComponent();
            richTextBox1.Text = "Welcome\n\nPlease enter your Password\n";
            Run();
        }

        private void button_enter_Click(object sender, EventArgs e)
        {
            pressed = true;


        }
    }
}
