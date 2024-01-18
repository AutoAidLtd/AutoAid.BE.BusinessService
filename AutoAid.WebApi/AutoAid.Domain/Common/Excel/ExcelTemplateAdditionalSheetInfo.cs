using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAid.Domain.Common.Excel
{
    public class ExcelTemplateAdditionalSheetInfo
    {
        public bool IsUseValidate { get; set; } = true;
        public string SheetName { get; set; }

        public List<string> Headers { get; set; }

        public string RankList { get; set; }

        public List<ExcelAdditionalSheetDataModel> Data { get; set; }
    }

    public class ExcelAdditionalSheetDataModel
    {
        public string Value { get; set; }
    }
}
