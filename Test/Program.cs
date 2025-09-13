using HtmlAgilityPack;
using System;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XoSoLookup
{
    static class Program
    {

        static void Main(string[] args)
        {
            List<string> listData = new List<string>();
            string url = "https://www.minhngoc.com.vn/ket-qua-xo-so/12-09-2025.html?mut=mn";
            var html = new HtmlWeb();
            var document = html.Load(url);
            var data = document.DocumentNode.SelectNodes("//table[contains(@class,'bkqmiennam')]");
            Console.WriteLine("hello");
            /*Console.WriteLine(data);
            foreach (var table_miennam in data)
            {
                document.LoadHtml(table_miennam.InnerHtml);
                var data2 = document.DocumentNode.SelectNodes("//table//tbody//tr");

                var rows = data2.Select(tr => tr
                    .Elements("td")
                    .Select(td => System.Net.WebUtility.HtmlDecode(td.InnerHtml).Trim())
                    .ToArray());
                foreach (var row in rows)
                {
                    var temp = HtmlToPlainText(row[0].Replace("<div>", "").Replace("</div>", "-").Trim());
                    var text = string.Join(" - ", temp.Split('-').Where(x => !string.IsNullOrEmpty(x)).ToArray());
                    listData.Add(text);
                }
                break;
            }*/
            if (data != null)
            {
                Console.WriteLine($"Tìm thấy {data.Count} bảng.");
                foreach (var table_miennam in data)
                {
                    document.LoadHtml(table_miennam.InnerHtml);
                    var data2 = document.DocumentNode.SelectNodes("//table//tbody//tr");

                    var rows = data2.Select(tr => tr
                        .Elements("td")
                        .Select(td => System.Net.WebUtility.HtmlDecode(td.InnerHtml).Trim())
                        .ToArray());
                    foreach (var row in rows)
                    {
                        var temp = HtmlToPlainText(row[0].Replace("<div>", "").Replace("</div>", "-").Trim());
                        var text = string.Join(" - ", temp.Split('-').Where(x => !string.IsNullOrEmpty(x)).ToArray());
                        listData.Add(text);
                    }
                    break;
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy bảng nào với lớp 'bkqmiennam'. Vui lòng kiểm tra lại URL hoặc cấu trúc HTML.");
            }

            // In kết quả
            foreach (var item in listData)
            {
                Console.WriteLine(item);
            }
            return;
            
        }

        // Giả định hàm HtmlToPlainText đã tồn tại và hoạt động đúng
        
            /*foreach (var item in document.DocumentNode.SelectNodes("//table//tbody//tr"))
            {

                listData.Add(item.InnerText);
                var rows = item2.Select(tr => tr
                .Element("td")
                .Select(td => System.Net.WebUtility.HtmlDecode(td.InnerHtml).Trim())
                .Array());

            }
            Console.WriteLine(listData);*/
            /*var nodee = document.DocumentNode.SelectSingleNode("//table//tbody//tr");
            Console.WriteLine(nodee.InnerText);
            Console.WriteLine("Hello World");*/

            //*[@id="topmenu_mien_home"]/li[3]
        
             private static string HtmlToPlainText(string html)
             {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";
            const string stripFormatting = @"<[^>]*(>|$)";
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            text = System.Net.WebUtility.HtmlDecode(text);
            text = tagWhiteSpaceRegex.Replace(text, "><");
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
    }
}

