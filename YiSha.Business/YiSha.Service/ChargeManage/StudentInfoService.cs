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
using StackExchange.Redis;

namespace YiSha.Service.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:13
    /// 描 述：学生信息服务类
    /// </summary>
    public class StudentInfoService :  RepositoryFactory
    {
        #region 获取数据
        public async Task<List<StudentInfoEntity>> GetList(StudentInfoListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<StudentInfoEntity>> GetPageList(StudentInfoListParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list= await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<StudentInfoEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<StudentInfoEntity>(id);
        }

        /// <summary>
        /// 获取最新一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<StudentInfoEntity> GetCodeTop(string code)
        {
            
            return (await this.BaseRepository().FindList<StudentInfoEntity>(x=>x.Code.Contains(code))).OrderByDescending(x=>x.Code).FirstOrDefault();
        }

        #endregion

        #region 提交数据
        public async Task SaveForm(StudentInfoEntity entity)
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
            foreach(var id in idArr)
            {
                StudentInfoEntity entity = new StudentInfoEntity();
                await entity.Modify();
                entity.Id = id;
                entity.BaseIsDelete = 1;
                await this.BaseRepository().Update<StudentInfoEntity>(entity);
            }
            //await this.BaseRepository().Delete<StudentInfoEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<StudentInfoEntity, bool>> ListFilter(StudentInfoListParam param)
        {
            var expression = LinqExtensions.True<StudentInfoEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    expression = expression.And(t => t.Name.Contains(param.Name));
                }
                if (!string.IsNullOrEmpty(param.Class))
                {
                    expression = expression.And(t => t.Class.Contains(param.Class));
                }
                if (param.Status > -1)
                {
                    expression = expression.And(t => t.Status == param.Status);
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

