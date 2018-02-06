using CrawlerLib;
using HTMLArbeiter.Model;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static CrawlerLib.Crawler;

namespace HTMLArbeiter.Functions
{
    public static class DataFunction
    {
        private static void WriteDataToFile(IWorkbook wk,List<NewsModul> lists,string TableName)
        {
            ISheet sheet = wk.CreateSheet(TableName);
            int zeile =0;

            foreach (NewsModul n in lists)
            {
                IRow row = sheet.CreateRow(zeile);
                row.CreateCell(0).SetCellValue(n.Title);
                row.CreateCell(1).SetCellValue(n.Content);
                row.CreateCell(2).SetCellValue(n.date);
                row.CreateCell(3).SetCellValue(n.Link);
                row.CreateCell(4).SetCellValue(n.Catagory);
                zeile++;
            }

        }

        public static void GetNews(ref IWorkbook wk, WebModul nm, int MaxNewsCount, MainWindow mw)
        {
            int startIndex = int.Parse(nm.Min);
            int Step = int.Parse(nm.Step);
            int EndIndex = startIndex + Step*(MaxNewsCount/10);

            List<string> urlList = new List<string>();
            List<NewsModul> nachrichtList = new List<NewsModul>();

            string htmlLinkResponsestr= "";
            Crawler crawler = new Crawler(nm.StartURL, nm.RegularLink,nm.RegularIntroduction, nm.RegularTitel, nm.RegularDate,
                nm.RegularContent);

            foreach (WebModul.UrlModel url in nm.Urls)
            {
                urlList.Clear();
                for (int i = startIndex; i <= EndIndex; i += Step)
                {
                    string htmlurl = "";
                    if (nm.Modul.Equals("0"))
                    {
                        if ( i == 0) htmlurl = url.URLT1;
                        else if (!url.URLT3.Equals("")) htmlurl = url.URLT1 + url.URLT2 + i.ToString() + url.URLT3;
                        else htmlurl = url.URLT1 + url.URLT2 + i.ToString() + "/";
                    }
                    else
                    {
                        if (i == 1 || i == 0) htmlurl = url.URLT1;
                        else if (!url.URLT3.Equals("")) htmlurl = url.URLT1 + url.URLT2 + i.ToString() + url.URLT3;
                        else htmlurl = url.URLT1 + url.URLT2 + i.ToString() + "/";
                    }
                    
                    htmlLinkResponsestr = crawler.GetResponsetHtmlStr(htmlurl);
                    urlList = urlList.Concat(crawler.GetLinks(htmlLinkResponsestr)).ToList();
                }
                nachrichtList = nachrichtList.Concat(crawler.GetNews(urlList, url.CatName,MaxNewsCount,mw)).ToList();
            }
            WriteDataToFile(wk, nachrichtList,nm.Name);
        }

        public static void GetNewsToTool(WebModul nm, int MaxNewsCount, MainWindow mw)
        {
            int startIndex = int.Parse(nm.Min);
            int Step = int.Parse(nm.Step);
            int EndIndex = int.Parse(nm.Max);

            List<string> urlList = new List<string>();
            List<NewsModul> NewsList = new List<NewsModul>();

            Crawler crawler = new Crawler(nm.StartURL, nm.RegularLink,nm.RegularIntroduction, nm.RegularTitel, nm.RegularDate, nm.RegularContent);

            foreach (WebModul.UrlModel url in nm.Urls)
            {
                urlList.Clear();
                int i = startIndex;
                bool IsReloaded = false;
                do
                {
                    string htmlurl = CheckUrl(nm.Modul, url, i);
                    string htmlLinkResponsestr = crawler.GetResponsetHtmlStr(htmlurl);
                    urlList = urlList.Concat(crawler.GetLinks(htmlLinkResponsestr)).ToList();
                    if (!IsReloaded) { EndIndex = startIndex + Step * (MaxNewsCount / urlList.Count); IsReloaded = true; }
                    i++;
                }
                while (i <= EndIndex);
                NewsList = crawler.GetNews(urlList, url.CatName, MaxNewsCount, mw);
                mw.AddNewsList(NewsList);
            }
        }

        private static string CheckUrl(string Model, WebModul.UrlModel url, int index)
        {
            string htmlurl = "";
            if (Model.Equals("0"))
            {
                if (index == 0) htmlurl = url.URLT1;
                else if (!url.URLT3.Equals("")) htmlurl = url.URLT1 + url.URLT2 + index.ToString() + url.URLT3;
                else htmlurl = url.URLT1 + url.URLT2 + index.ToString() + "/";
            }
            else
            {
                if (index == 1 || index == 0) htmlurl = url.URLT1;
                else if (!url.URLT3.Equals("")) htmlurl = url.URLT1 + url.URLT2 + index.ToString() + url.URLT3;
                else htmlurl = url.URLT1 + url.URLT2 + index.ToString() + "/";
            }
            return htmlurl;
        }
    }
}
