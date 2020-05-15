using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using YiSha.Util;
using YiSha.Util.Model;
using YiSha.Entity;
using YiSha.Model;
using YiSha.Admin.Web.Controllers;
using YiSha.Entity.ChargeManage;
using YiSha.Business.ChargeManage;
using YiSha.Model.Param.ChargeManage;
using YiSha.Web.Code;

namespace YiSha.Admin.Web.Areas.ChargeManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:02
    /// 描 述：收费对账记录控制器类
    /// </summary>
    [Area("ChargeManage")]
    public class AccountController :  BaseController
    {
        private AccountBLL accountBLL = new AccountBLL();

        #region 视图功能
        [AuthorizeFilter("charge:account:view")]
        public ActionResult AccountIndex()
        {
            return View();
        }

        public ActionResult AccountForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("charge:account:search")]
        public async Task<ActionResult> GetListJson(AccountListParam param)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<AccountEntity>> obj = await accountBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("charge:account:search")]
        public async Task<ActionResult> GetPageListJson(AccountListParam param, Pagination pagination)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<AccountEntity>> obj = await accountBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<AccountEntity> obj = await accountBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("charge:account:add,charge:account:edit")]
        public async Task<ActionResult> SaveFormJson(AccountEntity entity)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            entity.SysDepartmentId = operatorInfo.DepartmentId;
            entity.SysDepartmentName = operatorInfo.DepartmentName;

            TData<string> obj = await accountBLL.SaveForm(entity);

            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("charge:account:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await accountBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
