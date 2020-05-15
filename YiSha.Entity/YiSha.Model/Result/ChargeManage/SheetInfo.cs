using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiSha.Entity.ChargeManage;
using YiSha.Util;
using Newtonsoft.Json;

namespace YiSha.Model.Result.SystemManage
{
    public class SheetInfo
    {
        /// <summary>
        /// 所有表的主键
        /// long返回到前端js的时候，会丢失精度，所以转成字符串
        /// </summary>
        [JsonConverter(typeof(StringJsonConverter))]
        public long? Id { get; set; }
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
        /// <summary>
        /// 明细列表
        /// </summary>
        public List<DetailsEntity> details { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        /// <returns></returns>
        public decimal? SumMoney { get; set; }
        /// <summary>
        /// 已收金额
        /// </summary>
        /// <returns></returns>
        public decimal? YesMoney { get; set; }
        /// <summary>
        /// 未收金额
        /// </summary>
        /// <returns></returns>
        public decimal? NoMoney { get; set; }

    }
}
