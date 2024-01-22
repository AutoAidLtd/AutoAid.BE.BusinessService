////using CCI.Business.FIN.Helpers;
////using CCI.DTO.FIN.Common;
////using Core.DTO.Response;
//using AutoAid.Domain.Common.Excel;
//using Newtonsoft.Json;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using OfficeOpenXml.Table;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;


//namespace AutoAid.Infrastructure.Excel;


//public static class ExcelExtensions
//{
//    public static async Task<ExcelResult<IEnumerable<T>>> ConvertExcelToList<T>(string filePath) where T : new()
//    {
//        try
//        {
//            T obj = new T();
//            List<T> datas = new List<T>();
//            List<string> lstNameProp = typeof(T).GetProperties().Select(c => c.Name).ToList();
//            List<string> lstNameFile = new List<string>();
//            List<Mapping> lstMappingProp = new List<Mapping>();

//            var fileBytes = await FileHandlerHelper.GetFile(filePath);

//            if (fileBytes == null)
//            {
//                return new ExcelResult<IEnumerable<T>> { Status = false, Code = "1", Data = null };
//            }

//            using (var stream = new MemoryStream(fileBytes))
//            {
//                using (ExcelPackage package = new ExcelPackage(stream))
//                {
//                    ExcelWorksheet workSheet = package.Workbook.Worksheets["Values"];
//                    int totalRows = workSheet.Dimension.Rows;//get total rows counts of excel file
//                    int totalColumns = workSheet.Dimension.Columns;// get total columns count of excel file.
//                    if (totalRows > 1)
//                    {
//                        for (int i = 2; i <= totalRows; i++)
//                        {
//                            var headerModel = new Dictionary<object, string>();
//                            for (int j = 1; j <= totalColumns; j++)
//                            {
//                                if (workSheet.Cells[i, j].Value != null)
//                                    headerModel.Add(workSheet.Cells[1, j].Value, workSheet.Cells[i, j].Value.ToString());
//                                else
//                                    headerModel.Add(workSheet.Cells[1, j].Value, "0");
//                            }

//                            var json = JsonConvert.SerializeObject(headerModel);
//                            var header = JsonConvert.DeserializeObject<Mapping>(json);
//                            lstMappingProp.Add(new Mapping { Name = header.Name, Title = header.Title, IsNull = header.IsNull });
//                        }

//                        ExcelWorksheet workSheetData = package.Workbook.Worksheets.FirstOrDefault(x => x.Name.Contains("Data"));
//                        totalRows = workSheetData.Dimension.Rows;//get total rows counts of excel file
//                        totalColumns = workSheetData.Dimension.Columns;// get total columns count of excel file.

//                        for (int i = 2; i <= totalRows; i++)
//                        {
//                            var excelViewModels = new Dictionary<string, object>();
//                            for (int j = 1; j <= totalColumns; j++)
//                            {
//                                var title = workSheetData.Cells[1, j].Text;
//                                var headerModel = lstMappingProp.FirstOrDefault(x => title.Equals(x.Title) && !string.IsNullOrWhiteSpace(x.Title));

//                                if (headerModel == null)
//                                {
//                                    continue;
//                                }

//                                excelViewModels.Add(headerModel.Name, workSheetData.Cells[i, j].Value);
//                            }

//                            var data = excelViewModels.ToObject<T>();
//                            datas.Add(data);
//                        }
//                    }
//                }
//            }

//            if (datas == null || !datas.Any())
//            {
//                return new ExcelResult<IEnumerable<T>> { Status = false, ErrorMessage = "File excel không có dữ liệu" };
//            }

//            return new ExcelResult<IEnumerable<T>> { Status = true, Code = "", Data = datas };
//        }
//        catch (DataException ex)
//        {
//            return new ExcelResult<IEnumerable<T>> { Status = false, ErrorMessage = "File excel không đúng format", InnerErrorMessage = ex.Message };
//        }
//        catch (Exception ex)
//        {
//            return new ExcelResult<IEnumerable<T>> { Status = false, ErrorMessage = "File excel không đúng format", InnerErrorMessage = ex.InnerException?.Message };
//        }
//    }

//    public static void ExportExcel(this IDataReader dataReader, string path, string fileName, bool sum, List<string> columnName, string nameSheet, out int totalRow)
//    {
//        using (ExcelPackage pack = new ExcelPackage())
//        {
//            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(nameSheet);
//            DataTable schemaTable = dataReader.GetSchemaTable();
//            ws.Cells["A1"].LoadFromDataReader(dataReader, true, "", TableStyles.Dark10);

//            ws.Cells[1, 1, 1, dataReader.FieldCount].Style.Fill.PatternType = ExcelFillStyle.Solid;
//            ws.Cells[1, 1, 1, dataReader.FieldCount].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(155, 194, 230));
//            ws.Cells[1, 1, 1, dataReader.FieldCount].Style.Font.Bold = true;
//            int lastRow = ws.Dimension.End.Row;
//            totalRow = lastRow;
//            if (lastRow > 1)
//            {
//                foreach (DataRow schemarow in schemaTable.Rows)
//                {
//                    int colindex = Convert.ToInt32(schemarow["ColumnOrdinal"].ToString()) + 1;
//                    //ws.Column(colindex).AutoFit(); // Docker file sẽ lỗi 
//                    switch (System.Type.GetType(schemarow["DataType"].ToString()).FullName)
//                    {
//                        case "System.Int32":
//                            ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
//                            break;
//                        case "System.Int64":
//                            ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
//                            break;
//                        case "System.Double":
//                            ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
//                            break;
//                        case "System.Decimal":
//                            ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
//                            break;
//                        case "System.DateTime":
//                            ws.Column(colindex).Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
//                            break;
//                    }
//                }

//                //Sum column
//                if (sum)
//                {
//                    ws.Cells[lastRow + 1, 1, lastRow + 1, dataReader.FieldCount].Style.Font.Bold = true;
//                    columnName.ForEach(x =>
//                    {
//                        ws.Cells[lastRow + 1, Convert.ToInt32(x)].Formula =
//                            "SUM(" + ws.Cells[2, Convert.ToInt32(x)] + ":" + ws.Cells[lastRow, Convert.ToInt32(x)] +
//                            ")";
//                        ws.Cells[lastRow + 1, Convert.ToInt32(x)].Style.Numberformat.Format = "#,###,###,##0";
//                    });
//                }

//                if (File.Exists(path + fileName))
//                    File.Delete(path + fileName);
//                CreateFolder(path);
//                FileStream objStrem = File.Create(path + fileName);
//                objStrem.Close();
//                File.WriteAllBytes(path + fileName, pack.GetAsByteArray());
//            }
//            else
//            {
//                if (File.Exists(path + fileName))
//                    File.Delete(path + fileName);
//            }
//        }
//    }

//    public static async Task<string> ExportExcel(this DataTable objTable, string fileName, string sheetName)
//    {
//        using (var excelPackage = new ExcelPackage())
//        {
//            var ws = excelPackage.Workbook.Worksheets.Add(sheetName);
//            ws.Cells["A1"].LoadFromDataTable(objTable, true);
//            int colNumber = 1;

//            foreach (DataColumn col in objTable.Columns)
//            {
//                if (col.DataType == typeof(DateTime))
//                {
//                    ws.Column(colNumber).Style.Numberformat.Format = "dd/MM/yyyy hh:mm";
//                }
//                colNumber++;
//            }

//            //ws.Cells.AutoFitColumns(); // Docker file sẽ lỗi 
//            ws.Cells[1, 1, 1, objTable.Columns.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
//            ws.Cells[1, 1, 1, objTable.Columns.Count].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(155, 194, 230));
//            ws.Cells[1, 1, 1, objTable.Columns.Count].Style.Font.Bold = true;

//            var multiContent = new MultipartFormDataContent();
//            multiContent.Add(new ByteArrayContent(excelPackage.GetAsByteArray()), "file", $"{fileName}.xlsx");

//            var result = await FileHandlerHelper.Upload(multiContent);

//            return result.FilePath;
//        }
//    }

//    public static async Task<string> ExportExcel<T>(this IEnumerable<T> data, List<string> headers, string fileName, string sheetName)
//    {
//        using (var excelPackage = new ExcelPackage())
//        {
//            var ws = excelPackage.Workbook.Worksheets.Add(sheetName);
//            ws.Cells["A1"].LoadFromCollection(data, true);
//            int colNumber = 1;
//            var totalColumns = headers.Count;

//            foreach (var property in data.First().GetType().GetProperties())
//            {
//                if (property.PropertyType == typeof(Nullable<DateTime>) || property.PropertyType == typeof(DateTime))
//                {
//                    ws.Column(colNumber).Style.Numberformat.Format = "dd/MM/yyyy hh:mm";
//                }

//                colNumber++;
//            }

//            for (int i = 0; i < totalColumns; i++)
//            {
//                ws.Cells[1, i + 1].Value = headers[i];
//            }

//            //ws.Cells.AutoFitColumns(); // Docker file sẽ lỗi 
//            ws.Cells[1, 1, 1, totalColumns].Style.Fill.PatternType = ExcelFillStyle.Solid;
//            ws.Cells[1, 1, 1, totalColumns].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(155, 194, 230));
//            ws.Cells[1, 1, 1, totalColumns].Style.Font.Bold = true;

//            var multiContent = new MultipartFormDataContent();
//            multiContent.Add(new ByteArrayContent(excelPackage.GetAsByteArray()), "file", $"{fileName}.xlsx");

//            var result = await FileHandlerHelper.Upload(multiContent);

//            return result.FilePath;
//        }
//    }

//    public static async Task<string> ExportExcel(this List<DataTable> objTable, string fileName, bool sum, List<string> columnName)
//    {
//        if (objTable.Count > 0)
//        {
//            using (ExcelPackage pack = new ExcelPackage())
//            {
//                foreach (DataTable dataTable in objTable)
//                {
//                    ExcelWorksheet ws = pack.Workbook.Worksheets.Add(dataTable.TableName);
//                    ws.Cells["A1"].LoadFromDataTable(dataTable, true, TableStyles.Dark10);

//                    foreach (DataColumn objTableColumn in dataTable.Columns)
//                    {
//                        int colindex = Convert.ToInt32(dataTable.Columns.IndexOf(objTableColumn)) + 1;
//                        //ws.Column(colindex).AutoFit(); // Docker file sẽ lỗi 
//                        switch (System.Type.GetType(objTableColumn.DataType.ToString()).FullName)
//                        {
//                            case "System.Int32":
//                                ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
//                                break;
//                            case "System.Int64":
//                                ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
//                                break;
//                            case "System.Double":
//                                ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
//                                break;
//                            case "System.Decimal":
//                                ws.Column(colindex).Style.Numberformat.Format = "#,###,###,##0";
//                                break;
//                            case "System.DateTime":
//                                ws.Column(colindex).Style.Numberformat.Format = "dd/MM/yyyy HH:mm:ss";
//                                break;
//                        }
//                    }

//                    //ws.Cells.AutoFitColumns(); // Docker file sẽ lỗi 
//                    ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
//                    ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Fill.BackgroundColor
//                        .SetColor(Color.FromArgb(155, 194, 230));
//                    ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Font.Bold = true;

//                    int lastRow = ws.Dimension.End.Row;
//                    if (sum)
//                    {
//                        ws.Cells[lastRow + 1, 1, lastRow + 1, dataTable.Columns.Count].Style.Font.Bold = true;
//                        columnName.ForEach(x =>
//                        {
//                            ws.Cells[lastRow + 1, Convert.ToInt32(x)].Formula = "SUM(" + ws.Cells[2, Convert.ToInt32(x)] + ":" + ws.Cells[lastRow, Convert.ToInt32(x)] + ")";
//                            ws.Cells[lastRow + 1, Convert.ToInt32(x)].Style.Numberformat.Format = "#,###,###,##0";
//                        });
//                    }
//                }

//                var multiContent = new MultipartFormDataContent();
//                multiContent.Add(new ByteArrayContent(pack.GetAsByteArray()), "file", $"{fileName}.xlsx");

//                var result = await FileHandlerHelper.Upload(multiContent);
//            }
//        }

//        return string.Empty;
//    }

//    public static async Task<string> ExportExcelTemplate(ExcelTemplateExportModel model)
//    {
//        try
//        {
//            using (var excelPackage = new ExcelPackage())
//            {
//                int index = 1;
//                var worksheet = excelPackage.Workbook.Worksheets.Add(model.SheetName);

//                #region format datatype cell vs name header
//                for (int i = 0; i < model.Headers.Count; i++)
//                {
//                    index = i + 1;
//                    if (model.StringFormatColumns != null && model.StringFormatColumns.Any(x => x == model.Headers[index - 1]))
//                    {
//                        worksheet.Column(index).Style.Numberformat.Format = "@";
//                    }
//                    else if (model.DateFormatColumns != null && model.DateFormatColumns.Any(x => x == model.Headers[index - 1]))
//                    {
//                        worksheet.Column(index).Style.Numberformat.Format = "dd/MM/yyyy";
//                    }
//                    else if (model.DateTimeFormatColumns != null && model.DateTimeFormatColumns.Any(x => x == model.Headers[index - 1]))
//                    {
//                        worksheet.Column(index).Style.Numberformat.Format = "dd/MM/yyyy hh:mm";
//                    }
//                    else
//                    {
//                        worksheet.Column(index).Style.Numberformat.Format = "#,##0";
//                    }

//                    worksheet.Cells[1, index].Value = model.Headers[i];
//                }
//                #endregion

//                for (int i = 0; i < model.Headers.Count; i++)
//                {
//                    index = i + 1;
//                    worksheet.Cells[1, index].Style.Font.Bold = true;
//                    worksheet.Cells[1, index].Style.Fill.PatternType = ExcelFillStyle.Solid;
//                    worksheet.Cells[1, index].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(155, 194, 230));
//                    // worksheet.Column(index).AutoFit(); // Docker file sẽ lỗi 
//                }

//                var valueWorksheet = excelPackage.Workbook.Worksheets.Add("Values");
//                valueWorksheet.Cells[1, 1].Value = "Title";
//                valueWorksheet.Cells[1, 2].Value = "Name";

//                for (int i = 1; i <= model.Headers.Count; i++)
//                {
//                    index = i + 1;
//                    valueWorksheet.Cells[index, 1].Value = model.Headers[i - 1];
//                }

//                for (int i = 1; i <= model.ValueHeaders.Count; i++)
//                {
//                    index = i + 1;
//                    valueWorksheet.Cells[index, 2].Value = model.ValueHeaders[i - 1];
//                }

//                valueWorksheet.Hidden = eWorkSheetHidden.Hidden;
//                model.FileName = DateTime.Now.ToString("yyyyMMdd") + $"_{model.FileName}.xlsx";

//                if (model.AdditionalSheetInfos != null && model.AdditionalSheetInfos.Any())
//                {
//                    foreach (var item in model.AdditionalSheetInfos)
//                    {
//                        GenerateAdditionalSheet(excelPackage, worksheet, item);
//                    }
//                }

//                var multiContent = new MultipartFormDataContent();
//                multiContent.Add(new ByteArrayContent(excelPackage.GetAsByteArray()), "file", model.FileName);

//                var result = await FileHandlerHelper.Upload(multiContent);

//                return result.FilePath;
//            }
//        }
//        catch (Exception ex)
//        {
//            throw ex;
//        }
//    }

//    private static void GenerateAdditionalSheet(ExcelPackage excelPackage, ExcelWorksheet worksheet, ExcelTemplateAdditionalSheetInfo model)
//    {
//        var subWorksheet = excelPackage.Workbook.Worksheets.Add(model.SheetName);
//        for (int i = 0; i < model.Headers.Count; i++)
//        {
//            int index = i + 1;
//            subWorksheet.Cells[1, index].Value = model.Headers[i];
//        }

//        subWorksheet.Cells[2, 1].LoadFromCollection(model.Data);

//        for (int i = 0; i < model.Headers.Count; i++)
//        {
//            int index = i + 1;
//            subWorksheet.Cells[1, index].Style.Font.Bold = true;
//            subWorksheet.Cells[1, index].Style.Fill.PatternType = ExcelFillStyle.Solid;
//            subWorksheet.Cells[1, index].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(155, 194, 230));
//            //subWorksheet.Column(index).AutoFit(); // Docker file sẽ lỗi 
//        }

//        //subWorksheet.Hidden = eWorkSheetHidden.Hidden;
//        subWorksheet.Protection.IsProtected = true;
//        if (!model.Data.Any())
//        {
//            subWorksheet.Cells[2, 1, 2, 1].Style.Locked = true; // khi sheet ko co data thi khoa luon dong dau tien ngoai tru title => vi tri 2,1
//        }
//        else
//        {
//            subWorksheet.Cells[2, 1, model.Data.Count + 1, 1].Style.Locked = true;
//        }

//        if (model.IsUseValidate)
//        {
//            var validation = worksheet.DataValidations.AddListValidation(model.RankList);
//            validation.ShowErrorMessage = true;
//            validation.AllowBlank = true;
//            validation.Error = "Chọn giá trị từ danh sách";
//            if (!model.Data.Any())
//            {
//                validation.Formula.ExcelFormula = model.SheetName + "!$A$2:$A$" + (2).ToString(); // khi sheet khong co data thi khoa luon dong dau tien => vi tri 2,1
//            }
//            else
//            {
//                validation.Formula.ExcelFormula = model.SheetName + "!$A$2:$A$" + (model.Data.Count + 1).ToString();
//            }
//        }
//    }

//    private static bool CreateFolder(string path)
//    {
//        if (!Directory.Exists(path))
//        {
//            Directory.CreateDirectory(path);
//        }

//        return true;
//    }
//}

//public class Mapping
//{
//    public string Title { get; set; }
//    public string Name { get; set; }
//    public int IsNull { get; set; }
//}


