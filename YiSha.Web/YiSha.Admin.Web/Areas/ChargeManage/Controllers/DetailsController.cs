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
using NPOI.SS.Formula.Functions;
using YiSha.Model.Result.SystemManage;

namespace YiSha.Admin.Web.Areas.ChargeManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:06
    /// 描 述：收费单明细控制器类
    /// </summary>
    [Area("ChargeManage")]
    public class DetailsController : BaseController
    {
        private DetailsBLL detailsBLL = new DetailsBLL();
        private SheetBLL sheetBLL = new SheetBLL();
        private StudentInfoBLL studentInfoBLL = new StudentInfoBLL();

        #region 视图功能
        [AuthorizeFilter("charge:details:view")]
        public ActionResult DetailsIndex(string ChargeSheetId)
        {
            TData<SheetEntity> obj = sheetBLL.GetEntity(long.Parse(ChargeSheetId)).Result;
            ViewBag.ChargeNo = obj.Result.ChargeNo;
            ViewBag.ChargeSheetId = ChargeSheetId;
            ViewBag.ChargeName = obj.Result.ChargeName;
            return View();
        }

        public ActionResult DetailsForm(string ChargeSheetId, string ChargeNo)
        {
            ViewBag.ChargeSheetId = ChargeSheetId;
            ViewBag.ChargeNo = ChargeNo;
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("charge:details:search")]
        public async Task<ActionResult> GetListJson(DetailsListParam param)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<DetailsEntity>> obj = await detailsBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("charge:details:search")]
        public async Task<ActionResult> GetPageListJson(DetailsListParam param, Pagination pagination)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }

            StudentInfoListParam stuaram = new StudentInfoListParam();
            param.SysDepartmentId = operatorInfo.DepartmentId;
            TData<List<StudentInfoEntity>> stuobj = await studentInfoBLL.GetList(stuaram);

            TData<List<DetailsEntity>> obj = await detailsBLL.GetPageList(param, pagination);
            TData<List<DetailsInfo>> infos = new TData<List<DetailsInfo>>();
            
            if (stuobj.Tag == 1 && stuobj.Result.Any() && obj.Tag == 1 && obj.Result.Any())
            {
                infos.Result = obj.Result.MapToMany<DetailsEntity, DetailsInfo>();
                infos.Tag = 1;
                infos.TotalCount = obj.TotalCount;
                foreach (var model in infos.Result)
                {
                    model.StudentName = stuobj.Result.Where(x=>x.Code == model.StudentCode).FirstOrDefault().Name;
                }
                return Json(infos);
            }
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<DetailsEntity> obj = await detailsBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("charge:details:add,charge:details:edit")]
        public async Task<ActionResult> SaveFormJson(DetailsEntity entity)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            entity.SysDepartmentId = operatorInfo.DepartmentId;
            entity.SysDepartmentName = operatorInfo.DepartmentName;

            TData<string> obj = new TData<string>();
            var codelist = entity.StudentCode.Split(',');
            if (codelist.Contains("0"))//选择全部学生
            {
                StudentInfoListParam param = new StudentInfoListParam();
                param.SysDepartmentId = operatorInfo.DepartmentId;
                TData<List<StudentInfoEntity>> stuobj = await studentInfoBLL.GetList(param);
                if (stuobj.Tag == 1 && stuobj.Result.Any())
                {
                    foreach (var stumodel in stuobj.Result)//多选学生添加
                    {
                        entity.StudentCode = stumodel.Code;
                        entity.Id = 0;
                        entity.Status = 1;
                        obj = await detailsBLL.SaveForm(entity);
                    }
                }
            }
            else if (codelist.Count() > 1)
            {
                foreach (var code in codelist)//多选学生添加
                {
                    entity.StudentCode = code;
                    entity.Id = 0;
                    entity.Status = 1;
                    obj = await detailsBLL.SaveForm(entity);
                }
            }
            else
            {
                entity.Status = 1;
                obj = await detailsBLL.SaveForm(entity);
            }
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("charge:details:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await detailsBLL.DeleteForm(ids);
            return Json(obj);
        }

        /// <summary>
        /// 修改金额
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SaveFormJsonMoney(DetailsEntity entity)
        {
            TData<DetailsEntity> obj = await detailsBLL.GetEntity(Convert.ToInt64(entity.Id));
            if(obj.Tag == 1)
            {
                obj.Result.Money = entity.Money;
                TData<string> objstr = await detailsBLL.SaveForm(obj.Result);
                return Json(objstr);
            }
            return Json(obj);
        }
        #endregion
    }
}
