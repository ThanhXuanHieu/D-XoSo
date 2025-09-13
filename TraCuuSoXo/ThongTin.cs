using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraCuuSoXo
{
    internal class ThongTin
    {
        public DataTable LayKQXS(string url)
        {
            List<string> listData = new List<string>();
            var html = new HtmlWeb();
            var document = html.Load(url);
            var data = document.DocumentNode.SelectNodes("//table[contains(@class,'bkqmiennam')]");
            if (data != null)
            {
                /*foreach (var table_miennam in data)
                {
                    
                    var data2 = table_miennam.SelectNodes(".//tbody//tr");
                    if (data2 != null)
                    {
                        //test1
                        var rows = data2.Select(tr => tr
                            .Elements("td")
                            .Select(td => System.Net.WebUtility.HtmlDecode(td.InnerHtml).Trim())
                            .ToArray());

                        foreach (var row in rows)
                        {
                            // Chuyển đổi HTML sang văn bản thuần túy và xử lý
                            var temp = HtmlToPlainText(row[0].Replace("<div>", "").Replace("</div>", "-").Trim());
                            var text = string.Join(" - ", temp.Split('-').Where(x => !string.IsNullOrEmpty(x)).ToArray());
                            listData.Add(text);
                        }


                    }
                    break;

                }*/
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
                MessageBox.Show("Không tìm thấy bảng nào với lớp 'bkqmiennam'. Vui lòng kiểm tra lại URL hoặc cấu trúc HTML.");
            }

            var listOfLists = new List<IEnumerable<string>>();
            for (int i = 0; i < listData.Count(); i += 11)
            {
                listOfLists.Add(listData.Skip(i).Take(11));
            }

            //test1
            /*var table = new DataTable();
            
            if (listData.Count > 0)
            {
                // 1. Tạo cột: Dựa vào số lượng tỉnh (cột)
                int soCot = listData.Count / 11;
                for (int i = 0; i < soCot; i++)
                {
                    // Lấy tên cột từ vị trí đầu tiên của mỗi nhóm 11 phần tử
                    table.Columns.Add(listData[i * 11]);
                }

                // 2. Gán dữ liệu vào các hàng
                // Bắt đầu từ hàng 1 (do hàng 0 là tiêu đề)
                for (int i = 1; i < 11; i++) // Vòng lặp cho 10 hàng kết quả
                {
                    var dr = table.NewRow();
                    for (int j = 0; j < soCot; j++) // Vòng lặp cho từng cột
                    {
                        
                        int index = (j * 11) + i;
                        if (index < listData.Count)
                        {
                            dr[j] = listData[index];
                        }
                        else
                        {
                            dr[j] = DBNull.Value;
                        }
                    }
                    table.Rows.Add(dr);
                }
            }*/


            //test2
            var table = new DataTable();
            foreach (var item in listOfLists)
            {
                table.Columns.Add(item.ToList().FirstOrDefault());
            }

            for (int i = 1; i < 11; i++)
            {
                var dr = table.NewRow();
                int j = 0;
                foreach (var item in listOfLists)
                {
                    dr[j] = item.ToArray()[i];
                    j++;
                }
                table.Rows.Add(dr);

            }
            return table;
        }


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
