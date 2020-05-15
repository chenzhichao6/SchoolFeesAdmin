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
    /// 日 期：2020-05-11 11:02
    /// 描 述：收费对账记录服务类
    /// </summary>
    public class AccountService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<AccountEntity>> GetList(AccountListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<AccountEntity>> GetPageList(AccountListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<AccountEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<AccountEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(AccountEntity entity)
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
                AccountEntity entity = new AccountEntity();
                await entity.Modify();
                entity.Id = id;
                entity.BaseIsDelete = 1;
                await this.BaseRepository().Update<AccountEntity>(entity);
            }
            //await this.BaseRepository().Delete<AccountEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<AccountEntity, bool>> ListFilter(AccountListParam param)
        {
            var expression = LinqExtensions.True<AccountEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.Name.Contains(param.Name));
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
