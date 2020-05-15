using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YiSha.Util;

namespace YiSha.Model.Param.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:06
    /// 描 述：收费单明细实体查询类
    /// </summary>
    public class DetailsListParam
    {
        /// <summary>
        /// 部门id
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? SysDepartmentId { get; set; }
        /// <summary>
        /// 收费单号
        /// </summary>
        /// <returns></returns>
        public string ChargeNo { get; set; }
        /// <summary>
        /// 收费单id
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? ChargeSheetId { get; set; }
        /// <summary>
        /// 学生编号
        /// </summary>
        /// <returns></returns>
        public string StudentCode { get; set; }
    }
}
