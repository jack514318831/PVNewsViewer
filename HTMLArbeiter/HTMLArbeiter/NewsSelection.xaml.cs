using HTMLArbeiter.Model;
using Microsoft.Win32;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        List<NewsModul> NachrichtList = new List<NewsModul>();
        List<NewsModul> selectedList = new List<NewsModul>();
        NewsModul selectedNews = new NewsModul();
        public NewsSelection(List<NewsModul> nachrichtList)
        {
            InitializeComponent();
            NachrichtList = nachrichtList;
            lb_data.ItemsSource = NachrichtList;
            //lb_SelectedData.ItemsSource = selectedList;
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
            selectedNews = (NewsModul)lb.SelectedItem;
            tbContent.Text = selectedNews.Content;
            tb_title.Text = selectedNews.Title;
            block_From.Text = selectedNews.Quelle;
            block_date.Text = selectedNews.date;
            block_cap.Text = selectedNews.Catagory;
            tBlockLink.Text = selectedNews.Link;
            //WebOutput.NavigateToString(selectedNews.Htmlstr);
            DragDrop.DoDragDrop(lb, lb.SelectedItem ,DragDropEffects.Copy);
        }

        private void lb_SelectedData_Drop(object sender, DragEventArgs e)
        {
            NewsModul nr = (NewsModul)e.Data.GetData(typeof(NewsModul));
            if (selectedList.Contains(nr)) return;
            selectedList.Add((NewsModul)e.Data.GetData(typeof(NewsModul)));
            lb_SelectedData.Items.Refresh();
            #region old
            //StackPanel panel = new StackPanel();
            //panel.Orientation = Orientation.Horizontal;
            //TextBlock tb = new TextBlock();
            //tb.FontSize = 12;
            //tb.Width = 240;
            //Button btn = new Button();
            //Button btnStufe = new Button();
            //btnStufe.Width = 40;
            //btnStufe.FontSize = 10;
            //btnStufe.Margin = new Thickness(3);
            //btnStufe.Content = "low";
            //btnStufe.Click += StufeChange;
            //btn.Width = 0;
            //btn.Content = e.Data.GetData(typeof(NewsModul));
            //ComboBox cb = new ComboBox();
            //cb.FontSize = 10;
            //cb.Items.Add("Framework");
            //cb.Items.Add("Markt");
            //cb.Items.Add("Company");
            //cb.Items.Add("Technology");
            //tb.Text = ((NewsModul)e.Data.GetData(typeof(NewsModul))).Title;
            //IAddChild container = panel;

            //container.AddChild(btn);
            //container.AddChild(tb);
            //container.AddChild(btnStufe);
            //container.AddChild(cb);
            //container = item;
            //container.AddChild(panel);
            //lb_SelectedData.Items.Add(item); 
            #endregion

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
            Process.Start("chrome", tBlockLink.Text);
            e.Handled = true;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            if (open.ShowDialog() == false) return;
            IWorkbook wk = new XSSFWorkbook();
            selectedList.Clear();
            foreach (var item in lb_SelectedData.Items)
            {
                selectedList.Add((NewsModul)item);
            }
            Functions.DataFunction.WriteDataToFile(wk, selectedList, "All");
            using (FileStream fs = new FileStream(open.FileName, FileMode.OpenOrCreate))
            {
                wk.Write(fs);
            }
        }

        private void btnExportAll_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            if (open.ShowDialog() == false) return;
            IWorkbook wk = new XSSFWorkbook();
            Functions.DataFunction.WriteDataToFile(wk, NachrichtList, "All");
            using (FileStream fs = new FileStream(open.FileName, FileMode.OpenOrCreate))
            {
                wk.Write(fs);
            }
        }

        private void lb_data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnImportExcel_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == false) return;
            IWorkbook wk = new XSSFWorkbook(open.FileName);
            ISheet sheet = wk.GetSheet("All");
            NachrichtList.Clear();
            for (int r = 1; r < sheet.LastRowNum; r++)
            {
                IRow row = sheet.GetRow(r);
                NachrichtList.Add(new NewsModul() {
                    Title = row.GetCell(0).StringCellValue,
                    Content = row.GetCell(1).StringCellValue,
                    date = row.GetCell(2).StringCellValue,
                    Link = row.GetCell(3).StringCellValue,
                    Catagory = row.GetCell(4).StringCellValue,
                    Quelle = row.GetCell(5).StringCellValue
                });
            }
            lb_data.ItemsSource = NachrichtList;
            lb_data.Items.Refresh();

        }

        private void lb_SelectedData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb == null) return;
            if (lb.SelectedItem == null) return;
            selectedNews = (NewsModul)lb.SelectedItem;
            tbContent.Text = selectedNews.Content;
            tb_title.Text = selectedNews.Title;
            block_From.Text = selectedNews.Quelle;
            block_date.Text = selectedNews.date;
            block_cap.Text = selectedNews.Catagory;
            tBlockLink.Text = selectedNews.Link;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NewsModul news = (NewsModul)lb_data.SelectedItem;
            if (news == null) return;
            //   selectedList.Add(news);
            lb_SelectedData.Items.Add(news);
            NachrichtList.Remove(news);
            lb_data.Items.Refresh();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            NewsModul news = (NewsModul)lb_SelectedData.SelectedItem;
            if (news == null) return;
            //selectedList.Remove(news);
            lb_SelectedData.Items.Remove(lb_SelectedData.SelectedItem);
            NachrichtList.Add(news);
            lb_data.Items.Refresh();
        }

        private void lb_data_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                NewsModul news = (NewsModul)lb_data.SelectedItem;
                if (news == null) return;
                //   selectedList.Add(news);
                lb_SelectedData.Items.Add(news);
                NachrichtList.Remove(news);
                lb_data.Items.Refresh();
            }
            if (e.Key == Key.Delete)
            {
                NewsModul news = (NewsModul)lb_data.SelectedItem;
                if (news == null) return;
                NachrichtList.Remove(news);
                lb_data.Items.Refresh();
            }
           

        }

        private void lb_SelectedData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D)
            {
                NewsModul news = (NewsModul)lb_SelectedData.SelectedItem;
                if (news == null) return;
                //selectedList.Remove(news);
                lb_SelectedData.Items.Remove(lb_SelectedData.SelectedItem);
                NachrichtList.Add(news);
                lb_data.Items.Refresh();        
            }

        }

      
    }
}
