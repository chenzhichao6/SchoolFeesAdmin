using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiSha.Entity;
using YiSha.Util;
using Newtonsoft.Json;

namespace YiSha.Model.Result.SystemManage
{
    public class DetailsInfo
    {

        [JsonConverter(typeof(StringJsonConverter))]
        public long? Id { get; set; }
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
        /// 收费明细名称
        /// </summary>
        /// <returns></returns>
        public string DetailsName { get; set; }
        /// <summary>
        /// 收费金额
        /// </summary>
        /// <returns></returns>
        public decimal? Money { get; set; }
        /// <summary>
        /// 状态：1正常，2已缴费
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
        /// <summary>
        /// 学生编号
        /// </summary>
        /// <returns></returns>
        public string StudentCode { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        /// <returns></returns>
        public string StudentName { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? SysDepartmentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        /// <returns></returns>
        public string SysDepartmentName { get; set; }

        /// <summary>
        /// 是否删除 1是，0否
        /// </summary>
        [JsonIgnore]
        public int? BaseIsDelete { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? BaseCreateTime { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public long? BaseCreatorId { get; set; }
    }
}
