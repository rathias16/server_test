using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Collections.ObjectModel;

namespace client
{
	/// <summary>
	/// IPCheck.xaml の相互作用ロジック
	/// </summary>
	/// 
	public partial class IPCheck : Window
	{
		public ObservableCollection<IPList> Data { get; set; }
		MainWindow main;
		public ObservableCollection<string> selected = new ObservableCollection<string>();
		public IPCheck(MainWindow main)
		{
			this.main = main;
			GetIPList();
			InitializeComponent();
			listBox.ItemsSource = Data;
		}


		private void Button_Click(object sender, RoutedEventArgs e)
		{
			foreach (IPList item in Data) {
				if (item.isSelect)
				{
					Console.WriteLine("Add");
					selected.Add(item.ip);
				}
			}
			Console.WriteLine(selected.Count);
			
			main.StartConnect(this);
		}
		public void GetIPList()
		{
			//取得したIPアドレスを保管
			string tmp;
			Data = new ObservableCollection<IPList>();

			using (StreamReader sr = new StreamReader(
				"../../IPData.txt", Encoding.GetEncoding("utf-8")))
			{
				while ((tmp = sr.ReadLine()) != null)
				{
					//line.Append(tmp);
					IPList i = new IPList();
					i.ip = tmp;
					i.isSelect = false;
					Data.Add(i);
					Console.WriteLine(i.ip);
				}
			}
		}
	}

	public class IPList {
		public string ip { get; set; }
		public bool isSelect { get; set; }
	}

}
