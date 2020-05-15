using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YiSha.Util;

namespace YiSha.Entity.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:06
    /// 描 述：收费单明细实体类
    /// </summary>
    [Table("charge_details")]
    public class DetailsEntity : BaseExtensionEntity
    {
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
    }
}
