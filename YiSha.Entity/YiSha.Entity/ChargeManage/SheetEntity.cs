using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YiSha.Util;

namespace YiSha.Entity.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:10
    /// 描 述：收费单实体类
    /// </summary>
    [Table("charge_sheet")]
    public class SheetEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 收费名称
        /// </summary>
        /// <returns></returns>
        public string ChargeName { get; set; }
        /// <summary>
        /// 收费单号
        /// </summary>
        /// <returns></returns>
        public string ChargeNo { get; set; }
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
        /// <summary>
        /// 部门名称
        /// </summary>
        /// <returns></returns>
        public string SysDepartmentName { get; set; }
    }
}
