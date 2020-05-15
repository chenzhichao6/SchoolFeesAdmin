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
    /// 日 期：2020-05-11 11:07
    /// 描 述：收费记录业务类
    /// </summary>
    public class RecordBLL
    {
        private RecordService recordService = new RecordService();

        #region 获取数据
        public async Task<TData<List<RecordEntity>>> GetList(RecordListParam param)
        {
            TData<List<RecordEntity>> obj = new TData<List<RecordEntity>>();
            obj.Result = await recordService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RecordEntity>>> GetPageList(RecordListParam param, Pagination pagination)
        {
            TData<List<RecordEntity>> obj = new TData<List<RecordEntity>>();
            obj.Result = await recordService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<RecordEntity>> GetEntity(long id)
        {
            TData<RecordEntity> obj = new TData<RecordEntity>();
            obj.Result = await recordService.GetEntity(id);
            if (obj.Result != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(RecordEntity entity)
        {
            TData<string> obj = new TData<string>();
            await recordService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await recordService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
