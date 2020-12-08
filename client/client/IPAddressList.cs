using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace client
{
	public class IPAddressList
	{
		public ObservableCollection<string> Data { get; }
		public IPAddressList() {
			string[] line = new string[0];
			string tmp;

			using (StreamReader sr = new StreamReader(
				"C:/Users/hi420/Desktop/server_test/client/client/IPData.txt", Encoding.GetEncoding("utf-8")))
			{
				while ((tmp = sr.ReadLine()) != null){
					line.Append(tmp);
				}
			}

			for (int i=0;i<line.Length;i++) {
				Console.WriteLine(line[i]);
			}
		}
	}
}
