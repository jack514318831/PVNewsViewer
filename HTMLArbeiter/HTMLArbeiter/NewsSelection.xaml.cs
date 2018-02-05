using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static CrawlerLib.Crawler;

namespace HTMLArbeiter
{
    /// <summary>
    /// Interaktionslogik für NewsSelection.xaml
    /// </summary>
    public partial class NewsSelection : Window
    {
        public NewsSelection()
        {
            InitializeComponent();
        }

        List<nachricht> NachrichtList = new List<nachricht>();
        List<nachricht> selectedList = new List<nachricht>();
        nachricht selectedNews = new nachricht();
        public NewsSelection(List<nachricht> nachrichtList)
        {
            InitializeComponent();
            NachrichtList = nachrichtList;
            lb_data.ItemsSource = NachrichtList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridModul.DataContext = selectedNews;
        }

        private void lb_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb == null) return;
            if (lb.SelectedItem == null) return;
            selectedNews = (nachricht)lb.SelectedItem;
            tbContent.Text = selectedNews.Content;
            tb_title.Text = selectedNews.Title;
            block_date.Text = selectedNews.date;
            block_cap.Text = selectedNews.Catagory;
            tBlockLink.Text = selectedNews.Link;
            DragDrop.DoDragDrop(lb, lb.SelectedItem ,DragDropEffects.Copy);
        }

        private void lb_SelectedData_Drop(object sender, DragEventArgs e)
        {
            nachricht nr = (nachricht)e.Data.GetData(typeof(nachricht));
            if (selectedList.Contains(nr)) return;
            selectedList.Add((nachricht)e.Data.GetData(typeof(nachricht)));
            ListBoxItem item = new ListBoxItem();
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;
            TextBlock tb = new TextBlock();
            tb.Width = 180;
            Button btn = new Button();
            Button btnStufe = new Button();
            btnStufe.Width = 40;
            btnStufe.Margin = new Thickness(3);
            btnStufe.Content = "low";
            btnStufe.Click += StufeChange;
            btn.Width = 0;
            btn.Content = e.Data.GetData(typeof(nachricht));
            ComboBox cb = new ComboBox();
            cb.Items.Add("Legal Framework");
            cb.Items.Add("Company Infomation");
            tb.Text = ((nachricht)e.Data.GetData(typeof(nachricht))).Title;
            IAddChild container = panel;
            
            container.AddChild(btn);
            container.AddChild(tb);
            container.AddChild(btnStufe);
            container.AddChild(cb);
            container = item;
            container.AddChild(panel);
            lb_SelectedData.Items.Add(item);

        }

        private void StufeChange(object sender, RoutedEventArgs e)
        {
            string str = ((Button)sender).Content.ToString();
            if (str == "low")
            { ((Button)sender).Content = "middle"; }
            else if (str == "middle")
            { ((Button)sender).Content = "high"; }
            else
            { ((Button)sender).Content = "low"; }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start("chrome", e.Uri.ToString());
            e.Handled = true;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
