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
using YiSha.Util.Extension;
using YiSha.Model.Result.SystemManage;

namespace YiSha.Admin.Web.Areas.ChargeManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:07
    /// 描 述：收费记录控制器类
    /// </summary>
    [Area("ChargeManage")]
    public class RecordController :  BaseController
    {
        private RecordBLL recordBLL = new RecordBLL();
        private TypeConfigBLL typeConfigBLL = new TypeConfigBLL();
        private DetailsBLL detailsBLL = new DetailsBLL();

        #region 视图功能
        [AuthorizeFilter("charge:record:view")]
        public ActionResult RecordIndex()
        {
            return View();
        }

        public ActionResult RecordForm(string StudentCode,string StudentName)
        {
            ViewBag.StudentCode = StudentCode;
            ViewBag.StudentName = StudentName;
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("charge:record:search")]
        public async Task<ActionResult> GetListJson(RecordListParam param)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<RecordEntity>> obj = await recordBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("charge:record:search")]
        public async Task<ActionResult> GetPageListJson(RecordListParam param, Pagination pagination)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<RecordEntity>> obj = await recordBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<RecordEntity> obj = await recordBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("charge:record:add,charge:record:edit")]
        public async Task<ActionResult> SaveFormJson(RecordPostInfo entity)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            entity.SysDepartmentId = operatorInfo.DepartmentId;
            entity.SysDepartmentName = operatorInfo.DepartmentName;
            bool save = false;
            var type = await typeConfigBLL.GetEntityByType(entity.Type);
            if (entity.Id.IsNullOrZero())
            {
                save = true;
                type.No = (long.Parse(type.No) + 1).ToString();
                entity.InvoiceNo = type.No;
            }
            if (entity.Status.IsNullOrZero()) { entity.Status = 1; }
            TData<string> obj = await recordBLL.SaveForm(entity);

            if(save && obj.Tag == 1)
            {
                //更新类型配置NO
                await typeConfigBLL.SaveForm(type);

                if(entity.NoMoney == 0)
                {
                    //更新明细状态
                    await detailsBLL.UpdateStatus(Convert.ToInt64(entity.ChargeSheetId),entity.StudentCode,2);
                }
            }

            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("charge:record:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await recordBLL.DeleteForm(ids);
            return Json(obj);
        }

        [HttpPost]
        public async Task<ActionResult> SaveFormJsonStatus(RecordEntity entity)
        {
            TData<RecordEntity> obj = await recordBLL.GetEntity(Convert.ToInt64(entity.Id));
            if(obj.Tag == 1)
            {
                obj.Result.Status = entity.Status;
                TData<string> objsave = await recordBLL.SaveForm(entity);
                return Json(objsave);
            }

            return Json(obj);
        }
        #endregion
    }
}
