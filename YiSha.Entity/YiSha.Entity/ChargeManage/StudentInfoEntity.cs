using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using YiSha.Util;

namespace YiSha.Entity.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:13
    /// 描 述：学生信息实体类
    /// </summary>
    [Table("student_info")]
    public class StudentInfoEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 班级
        /// </summary>
        /// <returns></returns>
        public string Class { get; set; }
        /// <summary>
        /// 学生编码
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// 绑定微信开放id
        /// </summary>
        /// <returns></returns>
        public string Openid { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        /// <returns></returns>
        public string Phone { get; set; }
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
