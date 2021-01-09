using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        public static string SplitWords(this string text)
        {
            return
                string.Join("\r\n", text.Split()
                .Select((word, index) => new { word, index })
                .GroupBy(w => w.index / 10)
                .Select(nl => string.Join(" ", nl.Select(w => w.word))));
        }

        public static string SplitWords(this string text, int words = 10)
        {
            return
                string.Join("\r\n", text.Split()
                .Select((word, index) => new { word, index })
                .GroupBy(w => w.index / words)
                .Select(nl => string.Join(" ", nl.Select(w => w.word))));
        }

        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var enumerator = query.Provider.Execute<IEnumerable<TEntity>>(query.Expression).GetEnumerator();
            var relationalCommandCache = enumerator.Private("_relationalCommandCache");
            var selectExpression = relationalCommandCache.Private<SelectExpression>("_selectExpression");
            var factory = relationalCommandCache.Private<IQuerySqlGeneratorFactory>("_querySqlGeneratorFactory");

            var sqlGenerator = factory.Create();
            var command = sqlGenerator.GetCommand(selectExpression);

            string sql = command.CommandText;
            return sql;
        }
        private static object Private(this object obj, string privateField) => obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);
        private static T Private<T>(this object obj, string privateField) => (T)obj?.GetType().GetField(privateField, BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(obj);

        public static async Task<T> GetRequestBodyAsync<T>(this HttpRequest request) where T : new()
        {
            T objRequestBody = new T();

            // IMPORTANT: Ensure the requestBody can be read multiple times.
            HttpRequestRewindExtensions.EnableBuffering(request);

            // IMPORTANT: Leave the body open so the next middleware can read it.
            using (StreamReader reader = new StreamReader(
                request.Body,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true))
            {
                string strRequestBody = await reader.ReadToEndAsync();
                objRequestBody = JsonConvert.DeserializeObject<T>(strRequestBody);

                // IMPORTANT: Reset the request body stream position so the next middleware can read it
                request.Body.Position = 0;
            }

            return objRequestBody;
        }
    }
}
