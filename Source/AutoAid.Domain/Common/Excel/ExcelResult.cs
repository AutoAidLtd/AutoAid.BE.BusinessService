using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAid.Domain.Common.Excel
{
    public class ExcelResult<T>
    {
        public T Data { get; set; } = default(T);

        public bool Status { get; set; }

        public string InnerErrorMessage { get; set; }

        public string ErrorMessage { get; set; }

        public string Code { get; set; }
    }
}
