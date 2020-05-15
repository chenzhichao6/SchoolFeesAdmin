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
    /// 日 期：2020-05-11 11:10
    /// 描 述：收费单业务类
    /// </summary>
    public class SheetBLL
    {
        private SheetService sheetService = new SheetService();

        #region 获取数据
        public async Task<TData<List<SheetEntity>>> GetList(SheetListParam param)
        {
            TData<List<SheetEntity>> obj = new TData<List<SheetEntity>>();
            obj.Result = await sheetService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<SheetEntity>>> GetPageList(SheetListParam param, Pagination pagination)
        {
            TData<List<SheetEntity>> obj = new TData<List<SheetEntity>>();
            obj.Result = await sheetService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<SheetEntity>> GetEntity(long id)
        {
            TData<SheetEntity> obj = new TData<SheetEntity>();
            obj.Result = await sheetService.GetEntity(id);
            if (obj.Result != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(SheetEntity entity)
        {
            TData<string> obj = new TData<string>();
            await sheetService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await sheetService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
