using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HTMLArbeiter
{
    public class WebModul:ICloneable
    {
        public string Name { get; set; }
        public string StartURL { get; set; }
        public List<UrlModel> Urls { get; set; }
        public string Modul { get; set; }
        public string RegularLink { get; set; }
        public string RegularIntroduction { get; set; }
        public string RegularTitel { get; set; }
        public string RegularDate { get; set; }
        public string RegularContent { get; set; }

        public string Min { get; set; }
        public string Max { get; set; }
        public string Step { get; set; }

        public class UrlModel
        {
            public string CatName { get; set; }
            public string URLT1 { get; set; }
            public string URLT2 { get; set; }
            public string URLT3 { get; set; }

            public UrlModel(string name, string urlt1, string urlt2, string urlt3)
            {
                CatName = name;
                URLT1 = urlt1;
                URLT2 = urlt2;
                URLT3 = urlt3;
            }
        }

        public WebModul(string name, string starturl, List<UrlModel> list, string modul, string rlink,string rIntroduction, string rtitel, string rdate, string rcontent,
            string min, string max, string step)
        {
            Name = name;
            Urls = list;
            StartURL = starturl;
            Modul = modul;
            RegularLink = rlink;
            RegularIntroduction = rIntroduction;
            RegularTitel = rtitel;
            RegularDate = rdate;
            RegularContent = rcontent;
            Min = min;
            Max = max;
            Step = step;
        }

        public static List<WebModul> GetFromXML()
        {
            List<WebModul> ModulList = new List<WebModul>();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += @"Info\info.xml";
            XDocument xdoc = XDocument.Load(path);

            IEnumerable<WebModul> result = from web in xdoc.Descendants().Elements("Web")
                                           select new WebModul(web.Attribute("Name").Value, web.Attribute("StartURL").Value, null, web.Attribute("Modul").Value,
                                           web.Element("RegularLink").Value,web.Element("RegularIntroduction").Value, web.Element("RegularTitel").Value, web.Element("RegularDate").Value,
                                           web.Element("RegularContent").Value, web.Attribute("Min").Value, web.Attribute("Max").Value, web.Attribute("Step").Value)
                                           {
                                               Urls = (from url in web.Element("URLs").Elements("URL")
                                                      select new UrlModel(url.Attribute("Catagory").Value,url.Element("t1").Value, url.Element("t2").Value, url.Element("t3").Value)).ToList()
                                           };

            #region alt
            ////ModulList = result.ToList(); 
            //foreach (var item in result)
            //{
            //    ModulList.Add(new WebModul(item.Name, item.CatName,item.CatUrl,item.URLT1,item.URLT2,item.URLT3, item.StartURL, item.Modul,item.RegularLink.Value.ToString(), item.RegularTitel.Value.ToString(),
            //        item.RegularDate.Value.ToString(), item.RegularContent.Value.ToString()));

            //} 
            #endregion

            ModulList = result.ToList();
            return (ModulList);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
