using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HTMLArbeiter
{
    /// <summary>
    /// Interaktionslogik für RegexHelper.xaml
    /// </summary>
    public partial class RegexHelper : Window
    {
        public RegexHelper()
        {
            InitializeComponent();
        }

        public string htmlstr;
        public string pattern;
        public string urlstr;
        
        private void btnMatch_Click(object sender, RoutedEventArgs e)
        {
            pattern = txtInput.Text;
            Match mt = Regex.Match(htmlstr, pattern);
            txtRegex.Text = mt.Groups[0].ToString();
        }

        private void btnGetResponse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                urlstr = txtUrl.Text;
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(urlstr);
                hwr.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                WebRequest wr = (WebRequest)hwr;
                wr.Proxy = null;
                WebResponse wrp = hwr.GetResponse();
                StreamReader sr = new StreamReader(wrp.GetResponseStream());
                htmlstr = sr.ReadToEnd();
                txtHtml.Text = htmlstr;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnWebClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                urlstr = txtUrl.Text;
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("start", "2");
                webClient.QueryString.Add("filter", "2630");
                txtHtml.Text = webClient.DownloadString(urlstr);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
