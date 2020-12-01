using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Net;

namespace server
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

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Wait Start.");
			my_server.wait();
		}
	}
	class my_server {

		public static void wait()
		{
			TcpListener listener = null;
			try
			{
				//自身に接続するように設定
				Int32 port = 2001;
				IPAddress address = IPAddress.Parse("127.0.0.1");

				//サーバーの初期化
				listener = new TcpListener(address, port);

				//リクエストを受け付け開始
				listener.Start();

				Byte[] bytes = new Byte[256];
				string data = null;

				while (true)
				{

					TcpClient tcpClient = listener.AcceptTcpClient();
					MessageBox.Show("Connected.");

					data = null;
					NetworkStream stream = tcpClient.GetStream();
					int i;
					while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						data = Encoding.ASCII.GetString(bytes, 0, i);
						MessageBox.Show("Received :" + data);
						data = data.ToUpper();

						byte[] msg = Encoding.ASCII.GetBytes(data);
						stream.Write(msg, 0, msg.Length);
						MessageBox.Show("Sent : " + msg);
					}
					tcpClient.Close();
				}


			}
			catch (SocketException e)
			{
				MessageBox.Show(e.Message);
			}
			finally {
				listener.Stop();
			}

		}
	}
}

