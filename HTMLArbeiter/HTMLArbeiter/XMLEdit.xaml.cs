using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace HTMLArbeiter
{
    /// <summary>
    /// Interaktionslogik für XMLEdit.xaml
    /// </summary>
    public partial class XMLEdit : Window
    {
        public XMLEdit()
        {
            InitializeComponent();
        }

        public XMLEdit(List<WebModul> mL)
        {
            InitializeComponent();
            ModulList = mL;
        }

        private void cbTarget_DropDownClosed(object sender, EventArgs e)
        {
            wm = (sender as ComboBox).SelectedItem as WebModul;
            if (wm == null) return;
            tbStartUrl.Text = wm.StartURL;
            tbrLink.Text = wm.RegularLink;
            tbrIntroduction.Text = wm.RegularIntroduction;
            tbrTitel.Text = wm.RegularTitel;
            tbrDate.Text = wm.RegularDate;
            tbrContent.Text = wm.RegularContent;
            selectText = wm.Name;
            cbCat.ItemsSource = wm.Urls;
            txtMax.Text = wm.Max;
            txtMin.Text = wm.Min;
            txtStep.Text = wm.Step;
            
        }

        List<WebModul> ModulList;
        public WebModul wm { get; set; }
        public string path;
        public string selectText;
        public string selectUrl;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbTarget.ItemsSource = ModulList;

            wm = ModulList.ElementAt(1);
            tbStartUrl.Text = wm.StartURL;
            tbrIntroduction.Text = wm.RegularIntroduction;
            tbrTitel.Text = wm.RegularTitel;
            tbrDate.Text = wm.RegularDate;
            tbrContent.Text = wm.RegularContent;
            
            path = AppDomain.CurrentDomain.BaseDirectory;
            path += @"Info\info.xml";
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.Load(path);

            XmlElement root = document.DocumentElement;
            XmlElement webnode = document.CreateElement("Web");
            webnode.SetAttribute("Name", txtNew.Text);
            webnode.SetAttribute("StartURL", tbStartUrl.Text);
            webnode.SetAttribute("Modul", "1");
            XmlElement Urls = document.CreateElement("URLs");
            XmlElement url = document.CreateElement("URL");
            url.SetAttribute("Catagory", txtCatName.Text);
            XmlElement e1 = document.CreateElement("t1");
            e1.InnerText = txtT1.Text;
            url.AppendChild(e1);
            XmlElement e2 = document.CreateElement("t2");
            e2.InnerText = txtT2.Text;
            url.AppendChild(e2);
            XmlElement e3 = document.CreateElement("t3");
            e3.InnerText = txtT3.Text;
            url.AppendChild(e3);
            Urls.AppendChild(url);
            webnode.AppendChild(Urls);
            XmlElement linknode = document.CreateElement("RegularLink");
            linknode.InnerText = tbrLink.Text;
            webnode.AppendChild(linknode);
            XmlElement Introductionnode = document.CreateElement("RegularIntroduction");
            Introductionnode.InnerText = tbrIntroduction.Text;
            webnode.AppendChild(Introductionnode);
            XmlElement titlenode = document.CreateElement("RegularTitel");
            titlenode.InnerText = tbrTitel.Text;
            webnode.AppendChild(titlenode);
            XmlElement datenode = document.CreateElement("RegularDate");
            datenode.InnerText = tbrDate.Text;
            webnode.AppendChild(datenode);
            XmlElement contentnode = document.CreateElement("RegularContent");
            contentnode.InnerText = tbrContent.Text;
            webnode.AppendChild(contentnode);
            root.AppendChild(webnode);

            document.Save(path);
            WebModul.UrlModel urlmodel = new WebModul.UrlModel(txtCatName.Text, txtT1.Text, txtT2.Text, txtT3.Text);
            List<WebModul.UrlModel> list = new List<WebModul.UrlModel>();
            list.Add(urlmodel);
            ModulList.Add(new WebModul(txtNew.Text, tbStartUrl.Text,list, "1", tbrLink.Text,tbrIntroduction.Text, tbrTitel.Text, tbrDate.Text, tbrContent.Text,
                txtMin.Text,txtMax.Text, txtStep.Text));

            MessageBox.Show("New Added");

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.Load(path);
            string nameid = txtNew.Text;
            if (document.SelectNodes("/WebInfo/Web[@Name='" + nameid + "']").Count <= 0) return;
            XmlNode node = document.SelectSingleNode("/WebInfo/Web[@Name='" + nameid + "']");
            document.SelectSingleNode("/WebInfo").RemoveChild(node);
            document.Save(path);
            ModulList.Remove(wm);
            MessageBox.Show("Delected");
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.Load(path);
            string nameid = selectText;
            string urlid = selectUrl;
            if (document.SelectNodes("/WebInfo/Web[@Name='" + nameid + "']").Count <= 0) return;
            if (document.SelectNodes("/WebInfo/Web/URLs/URL[@Catagory='"+urlid+"']").Count <= 0) return;

            XmlNode node = document.SelectSingleNode("/WebInfo/Web[@Name='" + nameid + "']");
            node.Attributes["StartURL"].Value = tbStartUrl.Text;

            node.SelectSingleNode("RegularLink").InnerText = tbrLink.Text;
            node.SelectSingleNode("RegularIntroduction").InnerText = tbrIntroduction.Text;
            node.SelectSingleNode("RegularTitel").InnerText = tbrTitel.Text;
            node.SelectSingleNode("RegularDate").InnerText = tbrDate.Text;
            node.SelectSingleNode("RegularContent").InnerText = tbrContent.Text;

            XmlNode urlnode = node.SelectSingleNode("URLs/URL[@Catagory='" + urlid + "']");
            urlnode.SelectSingleNode("t1").InnerText = txtT1.Text;
            urlnode.SelectSingleNode("t2").InnerText = txtT2.Text;
            urlnode.SelectSingleNode("t3").InnerText = txtT3.Text;

            document.Save(path);

            wm.StartURL = tbStartUrl.Text;
            wm.RegularContent = tbrContent.Text;
            wm.RegularDate = tbrDate.Text;
            wm.RegularLink = tbrLink.Text;
            wm.RegularIntroduction = tbrIntroduction.Text;
            wm.RegularTitel = tbrTitel.Text;

            MessageBox.Show("Edited");
        }

        private void txtCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WebModul.UrlModel um = (sender as ComboBox).SelectedItem as WebModul.UrlModel;
            if (um == null) return;
            txtCatName.Text = um.CatName;
            txtT1.Text = um.URLT1;
            txtT2.Text = um.URLT2;
            txtT3.Text = um.URLT3;
            selectUrl = um.CatName;
        }

        private void btnNewUrl_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.Load(path);

            XmlNode urlsnode = document.SelectSingleNode("/WebInfo/Web[@Name='"+selectText+"']/URLs");
            if (urlsnode == null) return;
            XmlElement node = document.CreateElement("URL");
            node.SetAttribute("Catagory",txtCatName.Text);
            XmlElement subnode1 = document.CreateElement("t1");
            subnode1.InnerText = txtT1.Text;
            XmlElement subnode2 = document.CreateElement("t2");
            subnode2.InnerText = txtT2.Text;
            XmlElement subnode3 = document.CreateElement("t3");
            subnode3.InnerText = txtT3.Text;
            node.AppendChild(subnode1);
            node.AppendChild(subnode2);
            node.AppendChild(subnode3);
            urlsnode.AppendChild(node);
            document.Save(path);

            WebModul.UrlModel urlmodel = new WebModul.UrlModel(txtCatName.Text, txtT1.Text, txtT2.Text, txtT3.Text);
            List<WebModul.UrlModel> list = new List<WebModul.UrlModel>();
            list.Add(urlmodel);
           

            MessageBox.Show("New URL Added");
        }

       
    }
}
