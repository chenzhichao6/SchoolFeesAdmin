using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiSha.Entity.ChargeManage;
using YiSha.Util;
using Newtonsoft.Json;

namespace YiSha.Model.Result.SystemManage
{
    public class RecordPostInfo : RecordEntity
    {
        /// <summary>
        /// 欠费金额
        /// </summary>
        /// <returns></returns>
        public decimal? NoMoney { get; set; }
    }
}
