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
    /// 日 期：2020-05-11 11:12
    /// 描 述：收费类型管理服务类
    /// </summary>
    public class TypeConfigService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<TypeConfigEntity>> GetList(TypeConfigListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<TypeConfigEntity>> GetPageList(TypeConfigListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<TypeConfigEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<TypeConfigEntity>(id);
        }

        /// <summary>
        /// 根据编号获取实体
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public async Task<TypeConfigEntity> GetEntityByType(string Type)
        {
            TypeConfigListParam param = new TypeConfigListParam();
            param.Type = Type;
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList().OrderByDescending(x=>x.Id).FirstOrDefault();
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(TypeConfigEntity entity)
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
                TypeConfigEntity entity = new TypeConfigEntity();
                await entity.Modify();
                entity.Id = id;
                entity.BaseIsDelete = 1;
                await this.BaseRepository().Update<TypeConfigEntity>(entity);
            }
            //await this.BaseRepository().Delete<TypeConfigEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<TypeConfigEntity, bool>> ListFilter(TypeConfigListParam param)
        {
            var expression = LinqExtensions.True<TypeConfigEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.TypeName))
                {
                    expression = expression.And(t => t.TypeName.Contains(param.TypeName));
                }
                if (!string.IsNullOrEmpty(param.Type))
                {
                    expression = expression.And(t => t.Type == param.Type);
                }
                if (!param.SysDepartmentId.IsNullOrZero())
                {
                    expression = expression.And(t => t.SysDepartmentId == param.SysDepartmentId);
                }
                if (param.Status > -1)
                {
                    expression = expression.And(t => t.Status == param.Status);
                }
            }
            return expression;
        }
        #endregion
    }
}
