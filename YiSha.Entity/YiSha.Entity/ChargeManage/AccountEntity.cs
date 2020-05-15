using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YiSha.Util;

namespace YiSha.Entity.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:02
    /// 描 述：收费对账记录实体类
    /// </summary>
    [Table("charge_account")]
    public class AccountEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 对账日期
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? Date { get; set; }
        /// <summary>
        /// 对账人
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// 对账结果
        /// </summary>
        /// <returns></returns>
        public string Result { get; set; }
        /// <summary>
        /// 状态：1正常，0异常
        /// </summary>
        /// <returns></returns>
        public int? Status { get; set; }
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
        /// 收费结束时间
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? ChargeEndDate { get; set; }
        /// <summary>
        /// 收费金额
        /// </summary>
        /// <returns></returns>
        public decimal? ChargeMoney { get; set; }
        /// <summary>
        /// 收费开始时间
        /// </summary>
        /// <returns></returns>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? ChargeStartDate { get; set; }
    }
}
