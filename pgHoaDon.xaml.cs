using System.Windows;
using System.Windows.Controls;
using HoaDon.Model;

namespace HoaDon
{
	public partial class pgHoaDon : Page
	{
		Model.HoaDon hoaDon;
		MatHang matHang;
		public pgHoaDon()
		{
			InitializeComponent();
			Click_btnLamMoi(null, null);
			var caiDat = Helpers.DatabaseHandler.CaiDat();
			var khoHang = Helpers.DatabaseHandler.KhoHang();
			cbBaoHanh.ItemsSource = Helpers.MiscHelper.GetListFromString(caiDat.BaoHanh);
			cbQuaTang.ItemsSource = Helpers.MiscHelper.GetListFromString(caiDat.QuaTang);
			cbDiaChiKhachHang.ItemsSource = Helpers.MiscHelper.GetListFromString(caiDat.DiaChiKhachHang);
			cbTenMatHang.ItemsSource = Helpers.MiscHelper.GetListFromString(khoHang.TenMatHang);
			cbQuyCach.ItemsSource = Helpers.MiscHelper.GetListFromString(khoHang.QuyCachGiaTien);
			dtpNgay.SelectedDate = System.DateTime.Today;
		}
		private void Click_btnLamMoi(object sender, RoutedEventArgs e)
		{
			stThongSoTrongKinh.DataContext = DataContext = hoaDon = new Model.HoaDon();
			stMatHang.DataContext = matHang = new MatHang();
		}
		private void Click_btnThem(object sender, RoutedEventArgs e)
		{
			if (hoaDon.ThongSoTrongKinh)
			{
				if (!System.String.IsNullOrEmpty(matHang.QuyCach)) matHang.QuyCach += System.Environment.NewLine;
				matHang.QuyCach += "P: " + hoaDon.MatPhai
								+ System.Environment.NewLine + "T: " + hoaDon.MatTrai;
			}
			matHang.ID = lbMatHangs.Items.Count;
			hoaDon.MatHangs.Add(matHang);
			stMatHang.DataContext = matHang = new MatHang();
		}
		private void Click_btnXoa(object sender, RoutedEventArgs e)
		{
			if (lbMatHangs.SelectedItems.Count > 0)
			{
				hoaDon.MatHangs.Remove(lbMatHangs.SelectedItem as MatHang);
			}
		}
		private void Click_btnLuuKhachHang(object sender, RoutedEventArgs e)
		{
			CapNhatKhachHang();
			if(Helpers.DatabaseHandler.ThemKhachHang(hoaDon.KhachHang) && MessageBox.Show("Khách hàng đã được lưu. Bạn có muốn in hóa đơn không?", "", MessageBoxButton.OKCancel)
				== MessageBoxResult.OK)
			{
				wdInHoaDon wd = new wdInHoaDon(hoaDon, true);
				wd.Show();
			}
		}
		private void Click_btnInHoaDon(object sender, RoutedEventArgs e)
		{
			CapNhatKhachHang();
			wdInHoaDon wd = new wdInHoaDon(hoaDon);
			wd.Show();
		}
		void CapNhatKhachHang()
		{
			hoaDon.KhachHang.Ngay = Helpers.MiscHelper.FormatDate(dtpNgay.SelectedDate ?? System.DateTime.Today);
			hoaDon.KhachHang.Tien = 0;
			foreach (var item in hoaDon.MatHangs)
			{
				hoaDon.KhachHang.Tien += item.GiaTien ?? 0;
				if (System.Text.RegularExpressions.Regex.IsMatch(item.TrietKhau ?? "", @"^\d+%$"))
				{
					int? i = int.Parse(item.TrietKhau.Remove(item.TrietKhau.Length - 1));
					hoaDon.KhachHang.Tien -= item.GiaTien * i / 100;
				}
				else if (System.Text.RegularExpressions.Regex.IsMatch(item.TrietKhau ?? "", @"^\d+$"))
				{
					hoaDon.KhachHang.Tien -= int.Parse(item.TrietKhau);
				}
			}
			hoaDon.KhachHang.Data = Helpers.MiscHelper.SerializeToJson(hoaDon);
		}

		private void cbQuyCach_DropDownClosed(object sender, System.EventArgs e)
		{
			string str = cbQuyCach.Text;
			if (!System.String.IsNullOrEmpty(str) && System.Text.RegularExpressions.Regex.IsMatch(str, @".*|\d+"))
			{
				var arr = str.Split('|');
				if (arr.Length == 2)
				{
					cbQuyCach.Text = arr[0];
					txbGiaTien.Text = arr[1];
					matHang.GiaTien = int.Parse(arr[1]);
				}
			}
		}

		private void cbTenKhachHang_TextChanged(object sender, TextChangedEventArgs e)
		{
			var comboBox = sender as ComboBox;
			if (!System.String.IsNullOrEmpty(comboBox.Text) && comboBox.Text.Length >= 3)
			{
				comboBox.ItemsSource = Helpers.DatabaseHandler.KhachHang(
					"where ten like '%" + comboBox.Text + "%' or sdt like '%" + comboBox.Text + "%'");
				comboBox.IsDropDownOpen = true;
			}
			else
			{
				comboBox.IsDropDownOpen = false;
			}
		}

		private void txbTenKhachHang_TextChanged(object sender, TextChangedEventArgs e)
		{
			var tb = sender as TextBox;
			if (tb.Text.Length >= 3)
			{
				lbGoiYTenKhachHang.ItemsSource = Helpers.DatabaseHandler.KhachHang(
					" where ten like \"%" + tb.Text + "%\"");
				if (lbGoiYTenKhachHang.Items.Count == 0)
					return;
				pupGoiYTenKhachHang.IsOpen = true;
			}
			else
			{
				pupGoiYTenKhachHang.IsOpen = false;
			}
		}

		private void txbSoDienThoai_TextChanged(object sender, TextChangedEventArgs e)
		{
			var tb = sender as TextBox;
			if (tb.Text.Length >= 3)
			{
				lbGoiYSDT.ItemsSource = Helpers.DatabaseHandler.KhachHang(
					" where sdt like \"%" + tb.Text + "%\"");
				if (lbGoiYSDT.Items.Count == 0)
					return;
				pupGoiYSDT.IsOpen = true;
			}
			else
			{
				pupGoiYSDT.IsOpen = false;
			}
		}

		private void lbGoiYTenKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (lbGoiYTenKhachHang.SelectedItem != null)
			{
				var kh = lbGoiYTenKhachHang.SelectedItem as KhachHang;
				stThongSoTrongKinh.DataContext = DataContext = hoaDon = Helpers.MiscHelper.DeserializeFromJson(kh.Data);
				dtpNgay.SelectedDate = Helpers.MiscHelper.ReverseFormatDate(kh.Ngay);
				stMatHang.DataContext = matHang = new MatHang();
				pupGoiYTenKhachHang.IsOpen = pupGoiYSDT.IsOpen = false;
			}
		}

		private void lbGoiYSDT_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (lbGoiYSDT.SelectedItem != null)
			{
				var kh = lbGoiYSDT.SelectedItem as KhachHang;
				stThongSoTrongKinh.DataContext = DataContext = hoaDon = Helpers.MiscHelper.DeserializeFromJson(kh.Data);
				dtpNgay.SelectedDate = Helpers.MiscHelper.ReverseFormatDate(kh.Ngay);
				stMatHang.DataContext = matHang = new MatHang();
				pupGoiYTenKhachHang.IsOpen = pupGoiYSDT.IsOpen = false;
			}
		}

		private void txbGiaTien_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void txbTenKhachHang_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			if (e.Text.Contains("\"")) 
				e.Handled = true;
		}

		private void Click_btnHomNay(object sender, RoutedEventArgs e)
		{
			dtpNgay.SelectedDate = System.DateTime.Today;
		}
	}
}
