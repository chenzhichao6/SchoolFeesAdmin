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
    /// 日 期：2020-05-11 11:10
    /// 描 述：收费单服务类
    /// </summary>
    public class SheetService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<SheetEntity>> GetList(SheetListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<SheetEntity>> GetPageList(SheetListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<SheetEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<SheetEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(SheetEntity entity)
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
                SheetEntity entity = new SheetEntity();
                await entity.Modify();
                entity.Id = id;
                entity.BaseIsDelete = 1;
                await this.BaseRepository().Update<SheetEntity>(entity);
            }
            //await this.BaseRepository().Delete<SheetEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<SheetEntity, bool>> ListFilter(SheetListParam param)
        {
            var expression = LinqExtensions.True<SheetEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ChargeName))
                {
                    expression = expression.And(t => t.ChargeName.Contains(param.ChargeName));
                }
                if (param.Status > -1)
                {
                    expression = expression.And(t => t.Status == param.Status);
                }
                if (!param.SysDepartmentId.IsNullOrZero())
                {
                    expression = expression.And(t => t.SysDepartmentId == param.SysDepartmentId);
                }
                if (!string.IsNullOrEmpty(param.StartTime.ParseToString()))
                {
                    expression = expression.And(t => t.BaseCreateTime >= param.StartTime);
                }
                if (!string.IsNullOrEmpty(param.EndTime.ParseToString()))
                {
                    param.EndTime = (param.EndTime.Value.ToString("yyyy-MM-dd") + " 23:59:59").ParseToDateTime();
                    expression = expression.And(t => t.BaseCreateTime <= param.EndTime);
                }
            }
            return expression;
        }
        #endregion
    }
}
