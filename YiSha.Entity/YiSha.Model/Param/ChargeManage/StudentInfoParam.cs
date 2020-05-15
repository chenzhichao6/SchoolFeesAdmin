using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using YiSha.Util;

namespace YiSha.Model.Param.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:13
    /// 描 述：学生信息实体查询类
    /// </summary>
    public class StudentInfoListParam
    {
        /// <summary>
        /// 班级
        /// </summary>
        /// <returns></returns>
        public string Class { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
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
