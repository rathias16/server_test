using System;
using System.Windows;
using System.Net;
using System.Net.Sockets;

namespace client
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		my_client mine = null;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			IPCheck check = new IPCheck();
			check.Show();
			/*
			IPAddress[] address = new IPAddress[0];
			Array.Resize(ref address,address.Length+1);
			address[address.Length - 1] = IPAddress.Parse("127.0.0.1");
			mine = new my_client();
			mine.Connect(address, 2001);
			MessageBox.Show("start");
			mine.sentMessage("hello!");
			*/
		}
		public void ShowError(Exception e)
		{
			MessageBox.Show(e.Message);
		}

		private void stopButton_Click(object sender, RoutedEventArgs e)
		{
			if (mine != null) {
				mine.sentMessage("stop");
			}
		}
	}

	public class my_client
	{
		private NetworkStream stream;
		private TcpClient client;
		public void Connect(IPAddress[] server, Int32 port)
		{
			try
			{
				client = new TcpClient();
				client.Connect(server, port);
				stream = client.GetStream();
				
			}
			//error処理
			catch (ArgumentNullException e)
			{
				Console.WriteLine("ArgumentNullException: {0}", e);
			}
			catch (SocketException e)
			{
				Console.WriteLine("SocketException: {0}", e);
			}
		}

		public void EndConnect() {
			//client終了処理
			stream.Close();
			client.Close();

		}

		//messageを送る
		public void sentMessage(string message) {
			// Translate the passed message into ASCII and store it as a Byte array.
			Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
			MessageBox.Show("Send :"+message) ;									//message内容を表示
			stream.Write(data, 0, data.Length);
			readRecieveMessage();
			if (message == "stop") {
				EndConnect();
				MessageBox.Show("End");
			}
		}
		//responceされたデータの復元
		private void readRecieveMessage() {
			Byte[] data = new Byte[256];
			String responseData = null;

			Int32 bytes = stream.Read(data, 0, data.Length);
			responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);            //ASCIIとして復元
			Console.WriteLine("Received: {0}", responseData);                               //表示

		}

	}

	
}
