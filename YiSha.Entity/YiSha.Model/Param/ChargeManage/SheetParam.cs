using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YiSha.Util;

namespace YiSha.Model.Param.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:10
    /// 描 述：收费单实体查询类
    /// </summary>
    public class SheetListParam : DateTimeParam
    {
        /// <summary>
        /// 收费名称
        /// </summary>
        /// <returns></returns>
        public string ChargeName { get; set; }
        /// <summary>
        /// 状态：0禁用，1启用
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? SysDepartmentId { get; set; }

    }
}
