using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncClient_Task2
{
    public partial class ClientForm : Form
    {
        // The port number for the remote device.  
        private const int port = 11000;
        string Message = "";
        // ManualResetEvent instances signal completion.  
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        private ManualResetEvent sendDone = new ManualResetEvent(false);
        private ManualResetEvent receiveDone = new ManualResetEvent(false);

        // Create a TCP/IP socket.  

        Socket MyClient;


        // The response from the remote device.  
        private String response = String.Empty;
        public ClientForm()
        {
            InitializeComponent();
        }

        private void StartClient()
        {
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // The name of the
                // remote device is "host.contoso.com".  
                /* IPHostEntry ipHostInfo = Dns.GetHostEntry("host.contoso.com");
                 IPAddress ipAddress = ipHostInfo.AddressList[0];*/
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

                MyClient = new Socket(IPAddress.Parse("127.0.0.1").AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                MyClient.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), MyClient);
                connectDone.WaitOne();

                // Send test data to the remote device.  
                Send(MyClient, $"{Message} <EOF>{Environment.NewLine}");
                sendDone.WaitOne();

                // Receive the response from the remote device.  
                Receive(MyClient);
                receiveDone.WaitOne();

                // Write the response to the console.  
                SetTextSafe($"Response received : {response}{Environment.NewLine}");
                connectDone.Reset();
                sendDone.Reset();
                receiveDone.Reset();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                SetTextSafe($"Socket connected to {client.RemoteEndPoint.ToString()}{Environment.NewLine}");

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    StateObject.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (StateObject.sb.Length > 1)
                    {
                        response = StateObject.sb.ToString();                   
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                SetTextSafe(Message);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        void SetTextSafe(string newText)
        {
            if (Screen_txbx.InvokeRequired)
                Screen_txbx.BeginInvoke(new Action<string>((s) => Screen_txbx.Text += s), newText);
            else
                Screen_txbx.Text += newText;
        }

        private void Date_btn_Click(object sender, EventArgs e)
        {
            Screen_txbx.Text = "";
            StateObject.sb.Clear();
            Message = "Date";
            StartClient();
            Message = "";
            //CloseSockets();
        }

        private void Time_btn_Click(object sender, EventArgs e)
        {
            Screen_txbx.Text = "";
            StateObject.sb.Clear();
            Message = "Time";
            StartClient();
            Message = "";
            //CloseSockets();
        }

        void CloseSockets()
        {
            if(MyClient != null)
            {
                // Release the socket.  
                MyClient.Shutdown(SocketShutdown.Both);
                MyClient.Close();
            }
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseSockets();
        }
    }
}
