using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Resources.Extension
{
    public static class Extensions
    {
        public static DataTable ToTable<T>(this T data, string name = "table")
        {
            DataTable table = new DataTable(name);
            DataRow row;

            if (data is IList)
            {
                IList list = data as IList;

                foreach (var item in list[0].GetType().GetProperties())
                    table.Columns.Add(item.Name.Replace("_", " "), Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType);

                foreach (var prop in list)
                {
                    row = table.NewRow();
                    foreach (var item in prop.GetType().GetProperties())
                        row[item.Name.Replace("_", " ")] = prop.GetType().GetProperty(item.Name).GetValue(prop, null) ?? DBNull.Value;
                    table.Rows.Add(row);
                }
            }
            else
            {
                foreach (var item in data.GetType().GetProperties())
                    table.Columns.Add(item.Name.Replace("_", " "), Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType);

                row = table.NewRow();
                foreach (var item in data.GetType().GetProperties())
                    row[item.Name.Replace("_", " ")] = data.GetType().GetProperty(item.Name).GetValue(data, null) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }

        public static string SplitWords(this string text, int words = 10)
        {
            return
                string.Join("\r\n", text.Split()
                .Select((word, index) => new { word, index })
                .GroupBy(w => w.index / words)
                .Select(nl => string.Join(" ", nl.Select(w => w.word))));
        }
    }
}
