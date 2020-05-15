using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YiSha.Util;

namespace YiSha.Entity.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:12
    /// 描 述：收费类型管理实体类
    /// </summary>
    [Table("charge_type_config")]
    public class TypeConfigEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 最新编号
        /// </summary>
        /// <returns></returns>
        public string No { get; set; }
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
        /// <summary>
        /// 类型
        /// </summary>
        /// <returns></returns>
        public string Type { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        /// <returns></returns>
        public string TypeName { get; set; }
    }
}
