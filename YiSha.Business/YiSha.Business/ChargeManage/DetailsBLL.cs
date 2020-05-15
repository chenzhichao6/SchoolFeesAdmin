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
    /// 日 期：2020-05-11 11:06
    /// 描 述：收费单明细业务类
    /// </summary>
    public class DetailsBLL
    {
        private DetailsService detailsService = new DetailsService();

        #region 获取数据
        public async Task<TData<List<DetailsEntity>>> GetList(DetailsListParam param)
        {
            TData<List<DetailsEntity>> obj = new TData<List<DetailsEntity>>();
            obj.Result = await detailsService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<DetailsEntity>>> GetPageList(DetailsListParam param, Pagination pagination)
        {
            TData<List<DetailsEntity>> obj = new TData<List<DetailsEntity>>();
            obj.Result = await detailsService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<DetailsEntity>> GetEntity(long id)
        {
            TData<DetailsEntity> obj = new TData<DetailsEntity>();
            obj.Result = await detailsService.GetEntity(id);
            if (obj.Result != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(DetailsEntity entity)
        {
            TData<string> obj = new TData<string>();
            await detailsService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await detailsService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }

        /// <summary>
        /// 修改明细状态
        /// </summary>
        /// <param name="ChargeSheetId">收费单id</param>
        /// <param name="StudentCode">学生编号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public async Task<TData> UpdateStatus(long ChargeSheetId, string StudentCode, int Status)
        {
            TData obj = new TData();
            await detailsService.UpdateStatus(ChargeSheetId, StudentCode, Status);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
