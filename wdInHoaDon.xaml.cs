using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace HoaDon
{
	/// <summary>
	/// Interaction logic for wdInHoaDon.xaml
	/// </summary>
	public partial class wdInHoaDon : Window
	{
		Model.HoaDon _HoaDon = new Model.HoaDon();
		public wdInHoaDon(object o, bool InHoaDon = false)
		{
			InitializeComponent();
			if (o is Model.HoaDon)
			{
				var hoaDon = o as Model.HoaDon;
				DataContext = Helpers.DatabaseHandler.CaiDat();
				stKhachHang.DataContext = stBangThongSoThiLuc.DataContext = _HoaDon = hoaDon;
				if (hoaDon.SoDoLapTheoYeuCau)
				{
					txbSoDoLapTheoYeuCau.Visibility = Visibility.Visible;
					txbSoDoLapTheoYeuCau.Text = "*Số độ lắp theo yêu cầu";
				}
				if (!hoaDon.HienThiThiLuc) stBangThongSoThiLuc.Visibility = Visibility.Collapsed;
				txbTongCong.Text = "Tổng cộng:    "+String.Format("{0:#,0}", hoaDon.KhachHang.Tien) + ",000 VNĐ";
				if (hoaDon.DatTruoc)
				{ 
					txbDatTruoc.Text ="Đặt trước: " + String.Format("{0:#,0}", hoaDon.TienDatTruoc) + ",000 VNĐ" +
						". Còn lại: " + String.Format("{0:#,0}", (hoaDon.KhachHang.Tien - hoaDon.TienDatTruoc)) + ",000 VNĐ";
				}
				rbtnA5_Checked(null, null);
			}
			else root.Child = o as UIElement;
			
			if (InHoaDon)
				Button_Click_1(null, null);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			scr.ScrollToVerticalOffset(0);
			Helpers.MiscHelper.Print(root);
		}

		private void rbtnA5_Checked(object sender, RoutedEventArgs e)
		{
			imgLogo.Visibility = Visibility.Visible;
			root.Width = 550;
			root.MinHeight = 780;
			txbHeader1.FontSize = txbHeader2.FontSize = 22;
			root.SetValue(TextElement.FontSizeProperty, 12d);
			txbNgay.Inlines.Clear();
			txbNgay.Inlines.Add(new Run("Ngày " + _HoaDon.KhachHang.Ngay%100 + " tháng " + _HoaDon.KhachHang.Ngay/100 % 100 + " năm " + _HoaDon.KhachHang.Ngay / 10000)
			{ FontWeight = FontWeights.Bold });
			txbNgay.Inlines.Add(new Run(Environment.NewLine + "       Nhân viên bán hàng" + Environment.NewLine
				+ Environment.NewLine + Environment.NewLine + Environment.NewLine));
			grA5.Visibility = Visibility.Visible;
			grK80.Visibility = Visibility.Collapsed;
			stMatHangs.Children.Clear();
			foreach (var item in _HoaDon.MatHangs)
			{
				Grid gr = new Grid();
				gr.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30, GridUnitType.Pixel) });
				gr.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
				gr.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
				gr.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.7, GridUnitType.Star) });
				gr.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.7, GridUnitType.Star) });
				var lb = new Label() { Content = (item.ID + 1) + "", BorderThickness = new Thickness(1, 0, 0, 1), Padding = new Thickness(0, 5, 0, 5) };
				Grid.SetColumn(lb, 0);
				gr.Children.Add(lb);
				lb = new Label() { Content = item.TenMatHang, BorderThickness = new Thickness(1, 0, 0, 1), Padding = new Thickness(0, 5, 0, 5) };
				Grid.SetColumn(lb, 1);
				gr.Children.Add(lb);
				lb = new Label() { Content = item.QuyCach, BorderThickness = new Thickness(1, 0, 0, 1), Padding = new Thickness(0, 5, 0, 5) };
				Grid.SetColumn(lb, 2);
				gr.Children.Add(lb);
				lb = new Label()
				{
					Content = String.IsNullOrEmpty(item.TrietKhau) || item.TrietKhau.Contains("%") ?
					item.TrietKhau : String.Format("{0:#,0}", item.TrietKhau) + ",000",
					BorderThickness = new Thickness(1, 0, 0, 1)
				};
				Grid.SetColumn(lb, 3);
				gr.Children.Add(lb);
				lb = new Label() { Content = String.Format("{0:#,0}", item.GiaTien) + ",000", BorderThickness = new Thickness(1, 0, 1, 1) };
				Grid.SetColumn(lb, 4);
				gr.Children.Add(lb);
				stMatHangs.Children.Add(gr);
			}
		}

		private void rbtnk57_Checked(object sender, RoutedEventArgs e)
		{
			imgLogo.Visibility = Visibility.Collapsed;
			root.Width = 250;
			root.MinHeight = 300;
			root.SetValue(TextElement.FontSizeProperty, 8d);
			txbHeader1.FontSize = txbHeader2.FontSize = 14;
			grA5.Visibility = Visibility.Collapsed;
			grK80.Visibility = Visibility.Visible;
			txbNgay.Inlines.Clear();
			txbNgay.Inlines.Add(new Run("Ngày " + _HoaDon.KhachHang.Ngay % 100 + " tháng " + _HoaDon.KhachHang.Ngay / 100 % 100 + " năm " + _HoaDon.KhachHang.Ngay / 10000)
			{ FontWeight = FontWeights.Bold });
			stMatHangs.Children.Clear();
			foreach (var item in _HoaDon.MatHangs)
			{
				Grid gr = new Grid();
				gr.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20, GridUnitType.Pixel) });
				gr.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
				gr.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.25, GridUnitType.Star) });
				gr.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.4, GridUnitType.Star) });
				var lb = new Label() { Content = (item.ID + 1) + "", BorderThickness = new Thickness(1, 0, 0, 1) };
				Grid.SetColumn(lb, 0);
				gr.Children.Add(lb);
				lb = new Label() {HorizontalContentAlignment = HorizontalAlignment.Left, 
					Content = item.TenMatHang, BorderThickness = new Thickness(1, 0, 0, 1) };
				if (!String.IsNullOrEmpty(item.QuyCach))
					lb.Content += Environment.NewLine + item.QuyCach;
				Grid.SetColumn(lb, 1);
				gr.Children.Add(lb);
				lb = new Label()
				{
					Content = String.IsNullOrEmpty(item.TrietKhau) || item.TrietKhau.Contains("%") ?
					item.TrietKhau : String.Format("{0:#,0}", item.TrietKhau) + ",000",
					BorderThickness = new Thickness(1, 0, 0, 1)
				};
				Grid.SetColumn(lb, 2);
				gr.Children.Add(lb);
				lb = new Label() { Content = String.Format("{0:#,0}", item.GiaTien) + ",000", BorderThickness = new Thickness(1, 0, 1, 1) };
				Grid.SetColumn(lb, 3);
				gr.Children.Add(lb);
				stMatHangs.Children.Add(gr);
			}
		}

		private void rbtnk80_Checked(object sender, RoutedEventArgs e)
		{
			rbtnk57_Checked(null, null);
			root.Width = 330;
			root.SetValue(TextElement.FontSizeProperty, 10d);
			txbHeader1.FontSize = txbHeader2.FontSize = 18;
		}
	}
}
