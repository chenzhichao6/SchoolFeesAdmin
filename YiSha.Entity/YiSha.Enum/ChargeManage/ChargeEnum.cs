using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiSha.Enum.ChargeEnum
{

    public enum BillStatusEnum
    {
        [Description("正常")]
        Yes = 1,

        [Description("异常")]
        No = 0,
    }

    public enum MoneyStatusEnum
    {
        [Description("未缴费")]
        No = 1,

        [Description("已缴费")]
        Yes = 2
    }

    public enum RecordStatusEnum
    {
        [Description("正常")]
        Yes = 1,

        [Description("作废")]
        No = 0
    }
}
