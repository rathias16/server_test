using System;
using System.Windows;
using System.Net.Sockets;

namespace client
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			my_client.Connect("127.0.0.1","Hello!");
			MessageBox.Show("start");
		}
		public void ShowError(Exception e)
		{
			MessageBox.Show(e.Message);
		}

		private void stopButton_Click(object sender, RoutedEventArgs e)
		{
			my_client mine = new my_client();

			mine.sentMessage("stop");
		}
	}

	public class my_client
	{
		//TcpClient tcpCliant;
		private static NetworkStream stream;
		//private string adress = "127.0.0.1";
		//private Int32 port = 2001;
		public static void Connect(String server, String message)
		{
			my_client mine = new my_client();
			try
			{
				// Create a TcpClient.
				// Note, for this client to work you need to have a TcpServer
				// connected to the same address as specified by the server, port
				// combination.
				Int32 port = 2001;
				TcpClient client = new TcpClient(server, port);

				// Get a client stream for reading and writing.

				stream = client.GetStream();

				// Send the message to the connected TcpServer.

				mine.sentMessage(message);

				

				Console.WriteLine("Sent: {0}", message);

				// Receive the TcpServer.response.

				// Buffer to store the response bytes.
				Byte[] data = new Byte[256];

				// String to store the response ASCII representation.
				String responseData = null;

				// Read the first batch of the TcpServer response bytes.
				Int32 bytes = stream.Read(data, 0, data.Length);
				responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
				Console.WriteLine("Received: {0}", responseData);

				// Close everything.
				stream.Close();
				client.Close();
			}
			catch (ArgumentNullException e)
			{
				Console.WriteLine("ArgumentNullException: {0}", e);
			}
			catch (SocketException e)
			{
				Console.WriteLine("SocketException: {0}", e);
			}
		}
		public int sentMessage(string message) {
			// Translate the passed message into ASCII and store it as a Byte array.
			Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
			MessageBox.Show("Send :"+message) ;
			if (message == "stop")
				return 1;
			stream.Write(data, 0, data.Length);
			return 0;
		}

	}

	
}
