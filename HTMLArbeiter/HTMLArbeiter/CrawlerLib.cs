﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using HTMLArbeiter;
using HTMLArbeiter.Model;

namespace CrawlerLib
{
    public class Crawler
    {
        string startUrl;
        string rLink;
        string rTitle;
        string rDate;
        string rContent;
        string rIntroduction;

        public Crawler(string startUrl, string rlink, string rintro, string rtitle, string rdate, string rcontent)
        {
            this.startUrl = startUrl;
            this.rLink = rlink;
            this.rIntroduction = rintro;
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

        public List<string> GetIntroductions(string htmlStr)
        {
            string instrostr;
            List<string> InstroductionList = new List<string>();

            Regex regexLink = new Regex(rIntroduction);
            foreach (var match in regexLink.Matches(htmlStr))
            {
                if (!InstroductionList.Contains(match.ToString()))
                {
                    instrostr = match.ToString();
                    InstroductionList.Add(instrostr);
                }
            }
            return InstroductionList;
        }

        public List<NewsModul> GetNews(List<string> urlList,string newsCat,string newsquelle, int max, MainWindow mw)
        {
            List<NewsModul> resultlist = new List<NewsModul>();
            int num = 0;
          
            foreach (string urlstr in urlList)
            {
                if (num >= max) break;
                string url = urlstr;
                if (!startUrl.Equals("")) url = startUrl + urlstr;
                string resultstr = GetResponsetHtmlStr(url);
                string Linkstr;

                Linkstr = url;

                resultlist.Add(GetContent(resultstr, rTitle, rDate, rContent, Linkstr, newsCat, newsquelle));
                num++;
                mw.SetProgressbarValue(num,newsCat+" fertig");
            }
            return resultlist;
        }

        public NewsModul GetContent(string htmlStr, string rTitel, string rDate, string rContent, string Linkstr, string NewsCat, string quelle)
        {
            string Matchstr;
            NewsModul nr = new NewsModul();
            List<string> zlist = new List<string>();
            nr.Link = Linkstr;
            nr.Htmlstr = htmlStr;

            Regex regexTitel = new Regex(rTitel);
            Regex regexDate = new Regex(rDate);
            Regex regexContent = new Regex(rContent);
            Match match;
            if (regexTitel.Matches(htmlStr).Count > 0)
            {
                match = regexTitel.Matches(htmlStr)[0];
                if (!zlist.Contains(match.ToString()))
                {
                    Matchstr = match.ToString();
                    Matchstr = Matchstr.Replace("\n", "");
                    Matchstr = GetCleanTitle(Matchstr);
                    nr.Title = Matchstr;
                }
            }

            if (regexDate.Matches(htmlStr).Count > 0)
            {
                match = regexDate.Matches(htmlStr)[0];
                if (!zlist.Contains(match.ToString()))
                {
                    Matchstr = match.ToString();
                    Matchstr = Matchstr.Replace("\n", "");
                    nr.date = GetCleanDate(Matchstr);
                }
            }

            if (regexContent.Matches(htmlStr).Count > 0)
            {
                match = regexContent.Matches(htmlStr)[0];
                if (!zlist.Contains(match.ToString()))
                {
                    Matchstr = match.ToString();
                    Matchstr = Matchstr.Replace("\n", "");
                    nr.Content = GetCleanText(Matchstr);
                }
            }

            nr.Catagory = NewsCat;
            nr.Quelle = quelle;
            nr.Introduction = "";

            return nr;
        }

        private string GetCleanText(string matchstr)
        {
            string result = matchstr;
            result = Regex.Replace(result, "<a(.|\\n)*?>(.|\\n)*?</a>|<div\\s*?class=(?:'|\").*?(?:'|\")\\s*?>(.|\\n)*?</div>|[&#].*?;", "");
            result = Regex.Replace(result, "<div>|</div>|<br>|function.*?}|<img.*?>", "");
            result = Regex.Replace(result, "</[Pp]>|<[Pp].*?>|<br />","\n");
            result = Regex.Replace(result, "<span.*?>(.*?)</span>","\n$1\n");
            result = Regex.Replace(result, "<li.*?>(.*?)</li>", "\n$1\n");
            result = Regex.Replace(result, @"<(.+?>)((.|\n)*?)</\1", " $2 ");
            if (result[0] == '\n') result = result.Substring(1, result.Length-1);
            return result;
        }

        private string GetCleanTitle(string matchstr)
        {
            string result = matchstr;
            result = Regex.Replace(result, "[&#].*?;", "");
            return result;
        }

        private string GetCleanDate(string matchstr)
        {
            string Datestr = matchstr;
            string Outstr = "";
            DateTime outDate = DateTime.Now;
            if (DateTime.TryParse(Datestr, out outDate)) Outstr = outDate.ToString();
            if (Outstr.Equals(""))
            {
                string[] arrTime = Datestr.Split('/');
                Datestr = "20" + arrTime[2] + "-" + arrTime[0] + "-" + arrTime[1];
            }
            if (DateTime.TryParse(Datestr, out outDate)) Outstr = outDate.ToString();
            return Outstr;
        }

        #region Check Functions
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
        #endregion
    }
}