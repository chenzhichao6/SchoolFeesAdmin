using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YiSha.Util;

namespace YiSha.Model.Param.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:07
    /// 描 述：收费记录实体查询类
    /// </summary>
    public class RecordListParam : DateTimeParam
    {
        /// <summary>
        /// 收费单id
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? ChargeSheetId { get; set; }
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
        /// 单据号
        /// </summary>
        /// <returns></returns>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// 学生编号
        /// </summary>
        /// <returns></returns>
        public string StudentCode { get; set; }
        /// <summary>
        /// 收费类型名称
        /// </summary>
        /// <returns></returns>
        public string TypeName { get; set; }
        /// <summary>
        /// 收费人员名称
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
        /// <summary>
        /// 记录状态：1正常，0作废
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
    }
}
