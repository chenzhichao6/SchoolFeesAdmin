using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YiSha.Util;

namespace YiSha.Model.Param.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:02
    /// 描 述：收费对账记录实体查询类
    /// </summary>
    public class AccountListParam : DateTimeParam
    {
        /// <summary>
        /// 部门id
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? SysDepartmentId { get; set; }
        /// <summary>
        /// 对账人
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
    }
}
