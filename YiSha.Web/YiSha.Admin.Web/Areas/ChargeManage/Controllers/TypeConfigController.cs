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
    /// 日 期：2020-05-11 11:12
    /// 描 述：收费类型管理控制器类
    /// </summary>
    [Area("ChargeManage")]
    public class TypeConfigController :  BaseController
    {
        private TypeConfigBLL typeConfigBLL = new TypeConfigBLL();

        #region 视图功能
        [AuthorizeFilter("charge:typeconfig:view")]
        public ActionResult TypeConfigIndex()
        {
            return View();
        }

        public ActionResult TypeConfigForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("charge:typeconfig:search")]
        public async Task<ActionResult> GetListJson(TypeConfigListParam param)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<TypeConfigEntity>> obj = await typeConfigBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("charge:typeconfig:search")]
        public async Task<ActionResult> GetPageListJson(TypeConfigListParam param, Pagination pagination)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<TypeConfigEntity>> obj = await typeConfigBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<TypeConfigEntity> obj = await typeConfigBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetListJsonWeb(TypeConfigListParam param)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            param.Status = 1;
            TData<List<TypeConfigEntity>> obj = await typeConfigBLL.GetList(param);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("charge:typeconfig:add,charge:typeconfig:edit")]
        public async Task<ActionResult> SaveFormJson(TypeConfigEntity entity)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            entity.SysDepartmentId = operatorInfo.DepartmentId;
            entity.SysDepartmentName = operatorInfo.DepartmentName;
            TData<string> obj = await typeConfigBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("charge:typeconfig:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await typeConfigBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
