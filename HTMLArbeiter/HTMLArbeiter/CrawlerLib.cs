using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using HTMLArbeiter;

namespace CrawlerLib
{
    public class Crawler
    {
        string startUrl;
        string rLink;
        string rTitle;
        string rDate;
        string rContent;

        public Crawler(string startUrl, string rlink, string rtitle, string rdate, string rcontent)
        {
            this.startUrl = startUrl;
            this.rLink = rlink;
            this.rTitle = rtitle;
            this.rDate = rdate;
            this.rContent = rcontent;
        }
       
        public string GetResponsetHtmlStr(string url)
        {
            string htmlStr = "";
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                WebRequest webRequest = (WebRequest)httpWebRequest;
                webRequest.Proxy = null;
                WebResponse webResponse = httpWebRequest.GetResponse();
                StreamReader sr = new StreamReader(webResponse.GetResponseStream());
                htmlStr = sr.ReadToEnd();
            }
            catch (WebException ex)
            {
                throw ex;
            }
            return htmlStr;
        }


        /// <summary>
        /// Durchsuche den HTML-String nach href-Links, 
        /// füge diese einer Liste hinzu und gebe die Liste 
        /// zurück
        /// </summary>
        /// <param name="htmlStr">HTML-String</param>
        /// <returns>Url-Liste</returns>
        /// 
        //public List<string> GetUrlList(string htmlStr)
        //{
        //    string linkedUrl;
        //    List<string> urlList = new List<string>();

        //    Regex regexLink = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");

        //    foreach (var match in regexLink.Matches(htmlStr))
        //    {
        //        if (!urlList.Contains(match.ToString()))
        //        {
        //            linkedUrl = GetLinkedUrl(match.ToString());
        //            urlList.Add(linkedUrl);
        //        }
        //    }
        //    return urlList;
        //}

        public List<string> GetLinks(string htmlStr)
        {
            string linkedUrl;
            string htmlstr2 = htmlStr;
            List<string> zlist = new List<string>() ;
            List<string> urlList = new List<string>();

            Regex regexLink = new Regex(rLink);
            foreach (var match in regexLink.Matches(htmlstr2))
            {
                if (!zlist.Contains(match.ToString()))
                {
                    linkedUrl = match.ToString();
                    zlist.Add(linkedUrl);                 
                }
            }

            Regex regexLink2 = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");
            foreach (string str in zlist)
            {
                foreach (var match in regexLink2.Matches(str))
                {
                    if (!urlList.Contains(match.ToString()))
                    {
                        linkedUrl = match.ToString();
                        urlList.Add(linkedUrl);
                    }
                }
            }

             return urlList;
        }

        public List<nachricht> GetNews(List<string> urlList, string newsCat, int max, MainWindow mw)
        {
            List<nachricht> resultlist = new List<nachricht>();
            int num = 0;
          
            foreach (string urlstr in urlList)
            {
                if (num >= max) break;
                string url = urlstr;
                if (!startUrl.Equals("")) url = startUrl + urlstr;
                string resultstr = GetResponsetHtmlStr(url);
                string Linkstr;

                Linkstr = urlstr;

                resultlist.Add(GetContent(resultstr, rTitle, rDate, rContent, Linkstr, newsCat));
                num++;
                mw.SetProgressbarValue(num,newsCat+" fertig");
            }
            return resultlist;
        }

        public struct nachricht
        {
            public string Link { get; set; }
            public string Title { get; set; }
            public string Introduction { get; set; }
            public string date { get; set; }
            public string Content { get; set; }
            public string Catagory { get; set; }
        }

        public nachricht GetContent(string htmlStr, string rTitel, string rDate, string rContent, string Linkstr, string NewsCat)
        {
            string linkedUrl;
            string htmlstr2 = htmlStr;
            nachricht nr = new nachricht();
            List<string> zlist = new List<string>();
            nr.Link = Linkstr;

            Regex regexTitel = new Regex(rTitel);
            Regex regexDate = new Regex(rDate);
            Regex regexContent = new Regex(rContent);

            Match match;
            if (regexTitel.Matches(htmlstr2).Count > 0)
            {
                match = regexTitel.Matches(htmlstr2)[0];
                if (!zlist.Contains(match.ToString()))
                {
                    linkedUrl = match.ToString();
                    linkedUrl = linkedUrl.Replace("\n", "");
                    nr.Title = linkedUrl;
                }
            }

            if (regexDate.Matches(htmlstr2).Count > 0)
            {
                match = regexDate.Matches(htmlstr2)[0];
                if (!zlist.Contains(match.ToString()))
                {
                    linkedUrl = match.ToString();
                    linkedUrl = linkedUrl.Replace("\n", "");
                    nr.date = linkedUrl;
                }
            }

            if (regexContent.Matches(htmlstr2).Count > 0)
            {
                match = regexContent.Matches(htmlstr2)[0];
                if (!zlist.Contains(match.ToString()))
                {
                    linkedUrl = match.ToString();
                    linkedUrl = linkedUrl.Replace("<br />", "<P>");
                    linkedUrl = linkedUrl.Replace("\n", "");
                    nr.Content = linkedUrl;
                }
            }

            nr.Catagory = NewsCat;
            nr.Introduction = "";

            return nr;
        }

        private string GetLinkedUrl(string url)
        {
            if (!url.Contains("http://"))
            {
                if (url.IndexOf("/", 0) != -1)
                {
                    url = this.startUrl + url;
                }
                else
                {
                    url = this.startUrl + "/" + url;
                }
            }

            return url;
        }

        /// <summary>
        /// Gibt einen Link mit http:// zurück, sofern 
        /// die Url kein http:// besitzt. Ansonsten 
        /// funktioniert der Request nicht
        /// </summary>
        /// <param name="url">Die zu überprüfende Url</param>
        /// <returns>Fertige überprüfte Url</returns>
        public string GetCheckedUrl(string url)
        {
            if (!url.Contains("http://"))
            {
                if (!url.Contains("https://"))
                {
                    url = "http://" + url;
                }
            }
            return url;
        }

        public List<string> CompareUrlInList(List<string> urlList1, List<string> urlList2)
        {
            List<string> newComparedList = urlList2.Except(urlList1).ToList();
            return newComparedList;
        }
    }
}