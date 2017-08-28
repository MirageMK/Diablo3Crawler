using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using D3Core.Models;
using HtmlAgilityPack;
using log4net;

namespace D3Core.Readers
{
    public class IcyVeinsReader
    {
        private static readonly ILog log = LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);

        private Dictionary<string, Build> buildCollection = new Dictionary<string, Build>();
        private Dictionary<string, string> itemCollection = new Dictionary<string, string>();

        public List<Item> GetAll()
        {
            var toReturn = new List<Item>();

            HtmlWeb web = new HtmlWeb();
 
            HtmlDocument doc = web.Load($"https://www.icy-veins.com/d3/legendary-item-salvage-guide");

            if(doc.DocumentNode.SelectNodes("//*[contains(@class,'salvage_table')]") == null) return toReturn;

            foreach(HtmlNode row in doc.DocumentNode.SelectNodes("//*[contains(@class,'salvage_table')]/tr").Skip(1))
            {
                string name = row.SelectSingleNode("td").InnerText.Trim();
                string link = null;
                string slot = null;

                if(itemCollection.ContainsKey(name))
                {
                    slot = itemCollection[name];
                }
                else
                {
                    if(row.SelectSingleNode("td/span/a") != null)
                    {
                        link = row.SelectSingleNode("td/span/a").Attributes["href"].Value;

                        if(!link.StartsWith("//"))
                        {

                            using(WebClient client = new WebClient())
                            {
                                string htmlCode = client.DownloadString(link);
                                HtmlDocument itemPageDocument = new HtmlDocument();
                                itemPageDocument.LoadHtml(htmlCode);
                                slot = itemPageDocument.DocumentNode.SelectSingleNode("//*[contains(@class,'item-slot')]").InnerText;
                                itemCollection.Add(name, slot);
                            }
                        }
                    }
                }

                foreach (var buildRow in row.SelectNodes("td[2]/ul/li"))
                {
                    var fullBuildName = buildRow.InnerText;
                    if(fullBuildName.EndsWith("outdated")) continue;
                    var buildHref = buildRow.SelectSingleNode("a");

                    Build build;

                    if(buildCollection.ContainsKey(buildHref.InnerText))
                    {
                        build = buildCollection[buildHref.InnerText];
                    }
                    else
                    {
                        build = new Build
                                {
                                    Name = buildHref.InnerText.Trim(),
                                    URL = buildHref.Attributes["href"].Value
                                };
                    }

                    Item item = new Item
                                {
                                    Name = name,
                                    Build = build,
                                    BuildSlot = fullBuildName.Split('(')[1].Split(')')[0].Trim(),
                                    URL = link,
                                    Slot = slot
                                };
                    toReturn.Add(item);
                }
            }

            return toReturn;
        }
    }
}
