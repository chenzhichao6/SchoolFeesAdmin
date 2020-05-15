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
    /// 日 期：2020-05-11 11:02
    /// 描 述：收费对账记录业务类
    /// </summary>
    public class AccountBLL
    {
        private AccountService accountService = new AccountService();

        #region 获取数据
        public async Task<TData<List<AccountEntity>>> GetList(AccountListParam param)
        {
            TData<List<AccountEntity>> obj = new TData<List<AccountEntity>>();
            obj.Result = await accountService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<AccountEntity>>> GetPageList(AccountListParam param, Pagination pagination)
        {
            TData<List<AccountEntity>> obj = new TData<List<AccountEntity>>();
            obj.Result = await accountService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<AccountEntity>> GetEntity(long id)
        {
            TData<AccountEntity> obj = new TData<AccountEntity>();
            obj.Result = await accountService.GetEntity(id);
            if (obj.Result != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(AccountEntity entity)
        {
            TData<string> obj = new TData<string>();
            await accountService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await accountService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
