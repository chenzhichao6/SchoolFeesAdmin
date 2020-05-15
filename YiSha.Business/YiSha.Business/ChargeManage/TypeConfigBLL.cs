using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YiSha.Util;
using YiSha.Util.Extension;
using YiSha.Util.Model;
using YiSha.Entity.ChargeManage;
using YiSha.Model.Param.ChargeManage;
using YiSha.Service.ChargeManage;

namespace YiSha.Business.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:12
    /// 描 述：收费类型管理业务类
    /// </summary>
    public class TypeConfigBLL
    {
        private TypeConfigService typeConfigService = new TypeConfigService();

        #region 获取数据
        public async Task<TData<List<TypeConfigEntity>>> GetList(TypeConfigListParam param)
        {
            TData<List<TypeConfigEntity>> obj = new TData<List<TypeConfigEntity>>();
            obj.Result = await typeConfigService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<TypeConfigEntity>>> GetPageList(TypeConfigListParam param, Pagination pagination)
        {
            TData<List<TypeConfigEntity>> obj = new TData<List<TypeConfigEntity>>();
            obj.Result = await typeConfigService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<TypeConfigEntity>> GetEntity(long id)
        {
            TData<TypeConfigEntity> obj = new TData<TypeConfigEntity>();
            obj.Result = await typeConfigService.GetEntity(id);
            if (obj.Result != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        /// <summary>
        /// 根据编号获取实体
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public async Task<TypeConfigEntity> GetEntityByType(string Type)
        {
            return await typeConfigService.GetEntityByType(Type);
        }

        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(TypeConfigEntity entity)
        {
            TData<string> obj = new TData<string>();
            await typeConfigService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await typeConfigService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
