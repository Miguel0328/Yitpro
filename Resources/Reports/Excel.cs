using ClosedXML.Excel;
using ClosedXML.Report;
using Resources.Errors;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Resources.Reports
{
    public static class Excel
    {
        public static byte[] ToExcel<T>(this T data)
        {
            XLWorkbook wb = new XLWorkbook();

            if (data is DataSet)
            {
                var dataSet = data as DataSet;

                foreach (DataTable table in dataSet.Tables)
                    wb.Worksheets.Add(table, table.TableName);
            }
            else if (data is DataTable)
            {
                var dataTable = data as DataTable;
                wb.Worksheets.Add(dataTable, dataTable.TableName);
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Type = "Wrong type to excel" });
            }

            wb.Worksheets.ToList().ForEach(x =>
            {
                x.Columns().AdjustToContents();
                x.Cells().Style.Alignment.SetWrapText(true);
                x.Cells().Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
            });

            MemoryStream stream = GetStream(wb);
            return stream.ToArray();
        }

        private static MemoryStream GetStream<T>(this T workbook)
        {
            MemoryStream ms = new MemoryStream();

            if (workbook is XLWorkbook)
            {
                var book = workbook as XLWorkbook;
                book.SaveAs(ms);
                ms.Position = 0;
            }
            else if (workbook is XLTemplate)
            {
                var book = workbook as XLTemplate;
                book.SaveAs(ms);
                ms.Position = 0;
            }

            return ms;
        }
    }
}
