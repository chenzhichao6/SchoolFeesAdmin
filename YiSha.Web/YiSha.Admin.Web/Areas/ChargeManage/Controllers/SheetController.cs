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
using YiSha.Util.Extension;
using YiSha.Web.Code;
using YiSha.Model.Result.SystemManage;

namespace YiSha.Admin.Web.Areas.ChargeManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:10
    /// 描 述：收费单控制器类
    /// </summary>
    [Area("ChargeManage")]
    public class SheetController : BaseController
    {
        private SheetBLL sheetBLL = new SheetBLL();
        private DetailsBLL detailsBLL = new DetailsBLL();
        private RecordBLL recordBLL = new RecordBLL();

        #region 视图功能
        [AuthorizeFilter("charge:sheet:view")]
        public ActionResult SheetIndex()
        {
            return View();
        }

        public ActionResult SheetForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("charge:sheet:search")]
        public async Task<ActionResult> GetListJson(SheetListParam param)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<SheetEntity>> obj = await sheetBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("charge:sheet:search")]
        public async Task<ActionResult> GetPageListJson(SheetListParam param, Pagination pagination)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<SheetEntity>> obj = await sheetBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<SheetEntity> obj = await sheetBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetListJsonStudentCode(string StudentCode)
        {
            DetailsListParam detailsListParam = new DetailsListParam();

            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                detailsListParam.SysDepartmentId = operatorInfo.DepartmentId;
            }
            detailsListParam.StudentCode = StudentCode;
            TData<List<DetailsEntity>> details = await detailsBLL.GetList(detailsListParam);
            if (details.Tag == 1 && details.Result.Any())
            {
                var infos = new List<SheetInfo>();
                var ids = details.Result.Select(x => x.ChargeSheetId).Distinct().ToList();
                foreach (var id in ids)
                {
                    TData<SheetEntity> obj = await sheetBLL.GetEntity(long.Parse(id.ToString()));
                    if (obj.Tag == 1 && obj.Result != null)
                    {
                        SheetInfo info = obj.Result.MapTo<SheetEntity, SheetInfo>();
                        var mxs = details.Result.Where(x => x.ChargeSheetId == id).ToList();
                        info.details = mxs;
                        info.SumMoney = mxs.Sum(x => x.Money);
                        info.YesMoney = 0;

                        //收款记录
                        RecordListParam recordListParam = new RecordListParam();
                        if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
                        {
                            recordListParam.SysDepartmentId = operatorInfo.DepartmentId;
                        }
                        recordListParam.StudentCode = StudentCode;
                        recordListParam.ChargeSheetId = id;
                        recordListParam.Status = 1;
                        TData<List<RecordEntity>> record = await recordBLL.GetList(recordListParam);
                        if (record.Tag == 1 && record.Result != null) {
                            info.YesMoney = record.Result.Sum(x => x.Money);
                        }
                        
                        info.NoMoney = info.SumMoney - info.YesMoney;
                        infos.Add(info);
                    }
                }
                return Json(infos);
            }

            return Json(new List<SheetInfo>());
        }

        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("charge:sheet:add,charge:sheet:edit")]
        public async Task<ActionResult> SaveFormJson(SheetEntity entity)
        {
            //if (entity.Id.IsNullOrZero())
            //{
            //    entity.ChargeNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //}
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            entity.SysDepartmentId = operatorInfo.DepartmentId;
            entity.SysDepartmentName = operatorInfo.DepartmentName;

            TData<string> obj = await sheetBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("charge:sheet:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await sheetBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
