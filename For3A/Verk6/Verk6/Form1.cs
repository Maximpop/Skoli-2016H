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
                do
                {
                    try
                    {
                        message = reader.ReadString();
                        if (message == "Please type in your PIN number or type CANCEL")
                        {
                            writer.Write(pinNumber);
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
            Console.ReadLine();
        }
        public static string md5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public Form1()
        {
            InitializeComponent();
            richTextBox1.Text = "Welcome\n\nPlease enter your Password";
        }
        string realPass = "b1946ac92492d2347c6235b4d2611184";

        private void button_enter_Click(object sender, EventArgs e)
        {
            if (realPass == md5(input_textbox.Text))
            {
                //kominn inn
            }
            else
            {
                Thread.Sleep(2147483647);
            }
        }
    }
}
