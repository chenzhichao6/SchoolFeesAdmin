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
    /// 日 期：2020-05-11 11:07
    /// 描 述：收费记录服务类
    /// </summary>
    public class RecordService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<RecordEntity>> GetList(RecordListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<RecordEntity>> GetPageList(RecordListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<RecordEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<RecordEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(RecordEntity entity)
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
            //await this.BaseRepository().Delete<RecordEntity>(idArr);
        }

        
        
        #endregion

        #region 私有方法
        private Expression<Func<RecordEntity, bool>> ListFilter(RecordListParam param)
        {
            var expression = LinqExtensions.True<RecordEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ChargeNo))
                {
                    expression = expression.And(t => t.ChargeNo.Contains(param.ChargeNo));
                }
                if (!string.IsNullOrEmpty(param.InvoiceNo))
                {
                    expression = expression.And(t => t.InvoiceNo.Contains(param.InvoiceNo));
                }
                if (!string.IsNullOrEmpty(param.StudentCode))
                {
                    expression = expression.And(t => t.StudentCode.Contains(param.StudentCode));
                }
                if (!string.IsNullOrEmpty(param.TypeName))
                {
                    expression = expression.And(t => t.TypeName.Contains(param.TypeName));
                }
                if (!string.IsNullOrEmpty(param.UserName))
                {
                    expression = expression.And(t => t.UserName.Contains(param.UserName));
                }
                if (param.Status > -1)
                {
                    expression = expression.And(t => t.Status == param.Status);
                }
                if (!param.SysDepartmentId.IsNullOrZero())
                {
                    expression = expression.And(t => t.SysDepartmentId == param.SysDepartmentId);
                }
                if (!param.ChargeSheetId.IsNullOrZero())
                {
                    expression = expression.And(t => t.ChargeSheetId == param.ChargeSheetId);
                }
                if (!string.IsNullOrEmpty(param.StartTime.ParseToString()))
                {
                    expression = expression.And(t => t.ChargeDate >= param.StartTime);
                }
                if (!string.IsNullOrEmpty(param.EndTime.ParseToString()))
                {
                    param.EndTime = (param.EndTime.Value.ToString("yyyy-MM-dd") + " 23:59:59").ParseToDateTime();
                    expression = expression.And(t => t.ChargeDate <= param.EndTime);
                }
            }
            return expression;
        }
        #endregion
    }
}
