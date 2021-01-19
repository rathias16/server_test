using System;
using System.Net.Sockets;
using System.Net;

namespace server
{
	class my_server
	{

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

				while (true)                                            //起動している限りclientを待機
				{
					if (tcpClient.Connected == false)
					{
						tcpClient = listener.AcceptTcpClient(); //clientを受け付け
						MessageBox.Show("Connected.");                      //接続完了
					}
					data = null;                                        //clientから受け取るデータ
					NetworkStream stream = tcpClient.GetStream();
					int i;

					//ここからデータのやりとり
					while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						data = Encoding.ASCII.GetString(bytes, 0, i);
						MessageBox.Show("Received :" + data);       //受け取ったデータを表示
						byte[] msg = Encoding.ASCII.GetBytes(data);
						stream.Write(msg, 0, msg.Length);           //送り返す

					}

					//データのやりとりここまで
					if ("stop\n" == data)
					{
						tcpClient.Close();                              //clientとの接続を切る
						tcpClient = new TcpClient();
						MessageBox.Show("End Conection.");
					}
				}


			}
			catch (SocketException e)                               //通信エラーが発生した場合
			{
				MessageBox.Show(e.Message);                         //表示
			}
			finally
			{
				listener.Stop();                                    //サーバーを停止
			}

		}
	}
}