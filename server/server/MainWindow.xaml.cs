using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
				

				//サーバーの初期化
				listener = new TcpListener(IPAddress.Any, port);

				//リクエストを受け付け開始
				listener.Start();

				Byte[] bytes = new Byte[256];
				string data = null;
				
				TcpClient tcpClient = new TcpClient();

				while (true)											//起動している限りclientを待機
				{
					if (tcpClient.Connected == false)
					{
						tcpClient = listener.AcceptTcpClient(); //clientを受け付け
						MessageBox.Show("Connected.");                      //接続完了
					}
					data = null;										//clientから受け取るデータ
					NetworkStream stream = tcpClient.GetStream();
					int i;

					//ここからデータのやりとり
					while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						data = Encoding.ASCII.GetString(bytes, 0, i);
						MessageBox.Show("Received :" + data);		//受け取ったデータを表示
						//data = data.ToUpper();						//データを大文字にする

						byte[] msg = Encoding.ASCII.GetBytes(data);
						stream.Write(msg, 0, msg.Length);			//送り返す
						//MessageBox.Show("Sent : " + msg);           //送り返したメッセージを表示
						
					}
					
					//データのやりとりここまで
					if ("stop" == data)
					{
						tcpClient.Close();                              //clientとの接続を切る
						tcpClient = new TcpClient();
						MessageBox.Show("End Conection." );
					}
				}


			}
			catch (SocketException e)								//通信エラーが発生した場合
			{
				MessageBox.Show(e.Message);							//表示
			}
			finally {
				listener.Stop();									//サーバーを停止
			}

		}
	}
}

