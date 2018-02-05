using CrawlerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using static CrawlerLib.Crawler;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Xml.Linq;
using Microsoft.Win32;
using HTMLArbeiter.Functions;
using System.Windows.Markup;
using System.Text.RegularExpressions;

namespace HTMLArbeiter
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<WebModul> ModulList = new List<WebModul>();
        List<nachricht> NachrichtList = new List<nachricht>();
        public WebModul wm { get; set; }
        public WebModul.UrlModel wmUrl { get; set; }
        public ManualResetEvent ThreadDone = new ManualResetEvent(false);
        IWorkbook wk = new XSSFWorkbook();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ModulList = WebModul.GetFromXML();
            cbTarget.ItemsSource = ModulList;
            wm = ModulList.ElementAt(0);
            if (wm == null) return;
            cbTarget.SelectedItem = wm;
            SPCatagory.Children.Clear();
            foreach (WebModul.UrlModel url in wm.Urls)
            {
                CheckBox cb = new CheckBox();
                cb.Content = url.CatName;
                IAddChild container = this.SPCatagory;
                container.AddChild(cb);
            }
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            #region alt
            //if (wm == null) return;
            //string urlt1 = wm.Urls.First().URLT1;
            //string urlt2 = "";
            //string urlt3 = "";
            //string starturlstr = wm.StartURL;
            //string rLink = wm.RegularLink;
            //string rTitle = wm.RegularTitel;
            //string rContent = wm.RegularContent;
            //string rDate = wm.RegularDate;

            //int max = int.Parse( wm.Max);
            //int min = int.Parse(wm.Min);
            //int step = int.Parse(wm.Step);

            //SaveFileDialog savefile = new SaveFileDialog();
            //savefile.Filter = "Excel Files (*.xlsx)| *.xlsx";
            //if (savefile.ShowDialog() != true)
            //{
            //    MessageBox.Show("No path Choosed!");
            //    return;
            //}

            //path = savefile.FileName;

            //Thread WriteDataThread = new Thread(() =>
            //{
            //    for(int i =0;i<=max;i++)
            //    {
            //        DataFunction.WriteDataToFile(path, urlt1, urlt2, urlt3, starturlstr, rLink, rTitle, rDate, rContent, i, "", 0);
            //    }
            //});
            //WriteDataThread.IsBackground = true;
            //WriteDataThread.Start(); 
            #endregion
            SaveFileDialog open = new SaveFileDialog();
            open.Filter = "Excel (*.xlsx)|*.xlsx";
            if (open.ShowDialog() == false) return;

            List<string> selectedlist = new List<string>();
            int NewsCount = txtNewsCount.Text.Equals(string.Empty) ? 0 : int.Parse(txtNewsCount.Text);
            WebModul newsModel = wm.Clone() as WebModul;

            foreach(var item in SPCatagory.Children)
            {
                if((item as CheckBox).IsChecked==true)
                selectedlist.Add((item as CheckBox).Content.ToString());
            }

            IEnumerable<WebModul.UrlModel> list = from url in wm.Urls
                                                  where selectedlist.Contains(url.CatName)
                                                  select url;
            newsModel.Urls = list.ToList();

            pbProcess.Maximum = NewsCount;
            pbProcess.Minimum = 0;

            Thread oThread = new Thread(() =>
            {
                DataFunction.GetNews(ref wk, newsModel, NewsCount, this);
                using (FileStream fs = new FileStream(open.FileName, FileMode.OpenOrCreate))
                {
                    wk.Write(fs);
                }
            });
            oThread.IsBackground = true;
            oThread.Start();

        }

        public void SetProgressbarValue(double i, string infostr)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    pbProcess.Value = i;
                    tblockText.Text = infostr;
                }));
               
                return;
            }
        }

        private void cbTarget_DropDownClosed(object sender, EventArgs e)
        {
            wm = (sender as ComboBox).SelectedItem as WebModul;
            if (wm == null) return;
            gridData.DataContext = wm;
            SPCatagory.Children.Clear();
            foreach (WebModul.UrlModel url in wm.Urls)
            {
                CheckBox cb = new CheckBox();
                cb.Content = url.CatName;
                IAddChild container = this.SPCatagory;
                container.AddChild(cb);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Thread oThread = new Thread(() => {
                XMLEdit editF = new XMLEdit(ModulList);
                editF.ShowDialog();

                System.Windows.Threading.Dispatcher.Run();

            });
            oThread.SetApartmentState(ApartmentState.STA);
            oThread.Start();
        }

        private void btnRegexHelper_Click(object sender, RoutedEventArgs e)
        {
            Thread oThread = new Thread(()=> {
                RegexHelper helper = new RegexHelper();
                helper.ShowDialog();

                System.Windows.Threading.Dispatcher.Run();

            });
            oThread.SetApartmentState(ApartmentState.STA);
            oThread.Start();
        }

        private void cbCatagory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WebModul.UrlModel urlmodel = (sender as ComboBox).SelectedItem as WebModul.UrlModel;
            if (urlmodel == null) return;
        }

        private void btnGetAllCat_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            open.Filter = "Excel (*.xlsx)|*.xlsx";
            if (open.ShowDialog() == false) return;
            
            int NewsCount = txtNewsCount.Text.Equals(string.Empty) ? 0 : int.Parse(txtNewsCount.Text);

            pbProcess.Maximum = NewsCount;
            pbProcess.Minimum = 0;

            Thread oThread = new Thread(() =>
            {
                foreach (WebModul modul in ModulList)
                {
                    DataFunction.GetNews(ref wk, modul, NewsCount,this);
                }
                using (FileStream fs = new FileStream(open.FileName, FileMode.OpenOrCreate))
                {
                    wk.Write(fs);
                }
            });
            oThread.IsBackground = true;
            oThread.Start();
        }

        private void btnGetDataToTool_Click(object sender, RoutedEventArgs e)
        {
            List<string> selectedlist = new List<string>();
            int NewsCount = txtNewsCount.Text.Equals(string.Empty) ? 0 : int.Parse(txtNewsCount.Text);
            WebModul newsModel = wm.Clone() as WebModul;

            foreach (var item in SPCatagory.Children)
            {
                if ((item as CheckBox).IsChecked == true)
                    selectedlist.Add((item as CheckBox).Content.ToString());
            }

            IEnumerable<WebModul.UrlModel> list = from url in wm.Urls
                                                  where selectedlist.Contains(url.CatName)
                                                  select url;
            newsModel.Urls = list.ToList();

            pbProcess.Maximum = NewsCount;
            pbProcess.Minimum = 0;

            lbNewslist.Items.Clear();
            Thread oThread = new Thread(() =>
            {
                DataFunction.GetNewsToTool(newsModel, NewsCount, this);
            });
            oThread.IsBackground = true;
            oThread.Start();
        }

        public void AddNewsList(List<nachricht> nachrichtList)
        {
            NachrichtList = NachrichtList.Concat(nachrichtList).ToList();
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Action(() => {
                    foreach (nachricht nt in nachrichtList)
                    {
                        ListBoxItem item = new ListBoxItem();
                        item.Content = nt.Title;
                        lbNewslist.Items.Add(item);
                    }
                }));
            }
        }

        public void AddListboxItem(string CatName)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Action(() => {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = CatName;
                    lbNewslistCat.Items.Add(item);
                }));
            }
        }

        private void btnGetAllDataToTool_Click(object sender, RoutedEventArgs e)
        {
            List<string> selectedlist = new List<string>();
            int NewsCount = txtNewsCount.Text.Equals(string.Empty) ? 0 : int.Parse(txtNewsCount.Text);

            pbProcess.Maximum = NewsCount;
            pbProcess.Minimum = 0;

            lbNewslist.Items.Clear();
            lbNewslistCat.Items.Clear();
            Thread oThread = new Thread(() =>
            {
                foreach (WebModul modul in ModulList)
                {
                    AddListboxItem(modul.Name);
                    DataFunction.GetNewsToTool(modul, NewsCount, this);
                }
            });
            oThread.IsBackground = true;
            oThread.Start();
        }

        private void lbNewslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb == null) return;
            nachricht result = (from news in NachrichtList
                               where news.Title.Equals((lb.SelectedItem as ListBoxItem).Content)
                               select news).First();
            string output = Regex.Replace(result.Content, "<[pP]>(.+?)</[pP]>","\n$1\n");

            tbNewsContent.Text = output;
        }

        private void btnNewsSelect_Click(object sender, RoutedEventArgs e)
        {
            Thread oThread = new Thread(()=> {

                NewsSelection ns = new NewsSelection(NachrichtList);
                ns.ShowDialog();
                System.Windows.Threading.Dispatcher.Run();
            });
            oThread.SetApartmentState(ApartmentState.STA);
            oThread.Start();
        }
    }
}
