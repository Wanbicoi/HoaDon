using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ClosedXML.Excel;

namespace HoaDon.Helpers
{
	public static class MiscHelper
	{
		public static string SerializeToJson(Model.HoaDon hoaDon)
		{
			try
			{
				return Newtonsoft.Json.JsonConvert.SerializeObject(hoaDon);
			}
			catch (Exception e)
			{
				MessageBox.Show("Lỗi: " + e.Message);
				return "";
			}
		}
		public static Model.HoaDon DeserializeFromJson(string obj)
		{
			try
			{
				return Newtonsoft.Json.JsonConvert.DeserializeObject<Model.HoaDon>(obj);
			}
			catch (Exception e)
			{
				MessageBox.Show("Lỗi: " + e.Message);
				return new Model.HoaDon();
			}
		}
	
		public static List<string> GetListFromString(string str)
		{
			return str.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
		}
		public static string GetComputerID()
		{
			var mbs = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
			var mbsList = mbs.Get();
			string s = "";
			foreach (ManagementObject mo in mbsList)
			{
				s = mo["ProcessorID"].ToString();
			}
			return s;
		}
		public static string CreateMD5(string input)
		{
			// Use input string to calculate MD5 hash
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
			{
				byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				// Convert the byte array to hexadecimal string
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("X2"));
				}
				return sb.ToString().ToLower();
			}
		}
		public static int FormatDate(DateTime date)
		{
			return date.Year * 10000 + date.Month * 100 + date.Day;
		}
		public static DateTime ReverseFormatDate(int date)
		{
			return new DateTime(date / 10000, date /100 % 100, date % 100);
		}

		public static void ExportToExcel(List<Model.KhachHang> khachHangs)
		{
			var workbook = new XLWorkbook();
			workbook.AddWorksheet("sheetName");
			var ws = workbook.Worksheet("sheetName");
			ws.ColumnWidth = 20;
			ws.Cell("A1").Value = "Tên";
			ws.Cell("B1").Value = "Số điện thoại";
			ws.Cell("C1").Value = "Ngày mua hàng";
			ws.Cell("D1").Value = "Thành tiền";
			int row = 2;
			foreach (var item in khachHangs)
			{
				ws.Cell("A" + row.ToString()).Value = item.Ten;
				ws.Cell("B" + row.ToString()).Value = item.SDT;
				ws.Cell("C" + row.ToString()).Value = Helpers.MiscHelper.ReverseFormatDate(item.Ngay).ToShortDateString();
				ws.Cell("D" + row.ToString()).Value = item.Tien;
				row++;
			}
			var save = new Microsoft.Win32.SaveFileDialog();
			save.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
			if (save.ShowDialog() == true)
			{
				try
				{
					workbook.SaveAs(save.FileName);
					System.Diagnostics.Process.Start(save.FileName);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message);
				}
			}
		}
		public static void Print(Visual v)
		{
			PrintDialog dlg = new PrintDialog();
			if (dlg.ShowDialog() == true)
			{
				System.Printing.LocalPrintServer localPrintServer = new LocalPrintServer();
				System.Printing.PrintQueue defaultPrintQueue = localPrintServer.DefaultPrintQueue;
				PrintTicket defaultPrintTicket = defaultPrintQueue.DefaultPrintTicket;
				defaultPrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA5);
				dlg.PrintTicket = defaultPrintTicket;
				dlg.PrintVisual(v, "In hóa đơn.");
			}
		}
		public static UIElement DeSerializeXAML(string s)
		{
			try
			{
				var stringReader = new System.IO.StringReader(s);
				var xmlReader = System.Xml.XmlReader.Create(stringReader);
				var i = System.Windows.Markup.XamlReader.Load(xmlReader);
				return i as UIElement;
			}
			catch (System.Windows.Markup.XamlParseException ex)
			{
				MessageBox.Show("Lỗi: Không xem được khách hàng này!" + ex.Message);
				return new UIElement();
			}
		}
	}
}
