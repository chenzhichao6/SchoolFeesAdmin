using System;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using YiSha.Util;
using YiSha.Util.Extension;
using YiSha.Util.Model;
using YiSha.Data;
using YiSha.Data.Repository;
using YiSha.Entity.ChargeManage;
using YiSha.Model.Param.ChargeManage;

namespace YiSha.Service.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:06
    /// 描 述：收费单明细服务类
    /// </summary>
    public class DetailsService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<DetailsEntity>> GetList(DetailsListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<DetailsEntity>> GetPageList(DetailsListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<DetailsEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<DetailsEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(DetailsEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            foreach (var id in idArr)
            {
                DetailsEntity entity = new DetailsEntity();
                await entity.Modify();
                entity.Id = id;
                entity.BaseIsDelete = 1;
                await this.BaseRepository().Update<DetailsEntity>(entity);
            }
            //await this.BaseRepository().Delete<DetailsEntity>(idArr);
        }

        /// <summary>
        /// 修改明细状态
        /// </summary>
        /// <param name="ChargeSheetId">收费单id</param>
        /// <param name="StudentCode">学生编号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public async Task UpdateStatus(long ChargeSheetId, string StudentCode, int Status)
        {
            string sql = string.Format("update charge_details set status={0} where charge_sheet_id={1} and student_code='{2}'", Status,ChargeSheetId,StudentCode);
            await this.BaseRepository().ExecuteBySql(sql);
        }
        #endregion

        #region 私有方法
        private Expression<Func<DetailsEntity, bool>> ListFilter(DetailsListParam param)
        {
            var expression = LinqExtensions.True<DetailsEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ChargeNo))
                {
                    expression = expression.And(t => t.ChargeNo.Contains(param.ChargeNo));
                }
                if (!param.ChargeSheetId.IsNullOrZero())
                {
                    expression = expression.And(t => t.ChargeSheetId == param.ChargeSheetId);
                }
                if (!string.IsNullOrEmpty(param.StudentCode))
                {
                    expression = expression.And(t => t.StudentCode.Contains(param.StudentCode));
                }
                if (!param.SysDepartmentId.IsNullOrZero())
                {
                    expression = expression.And(t => t.SysDepartmentId == param.SysDepartmentId);
                }
            }
            return expression;
        }
        #endregion
    }
}
