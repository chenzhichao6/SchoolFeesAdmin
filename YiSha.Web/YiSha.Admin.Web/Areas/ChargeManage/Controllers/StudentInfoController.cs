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
using YiSha.Business.Cache;
using YiSha.Web.Code;
using NPOI.SS.Formula.Functions;

namespace YiSha.Admin.Web.Areas.ChargeManage.Controllers
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:13
    /// 描 述：学生信息控制器类
    /// </summary>
    [Area("ChargeManage")]
    public class StudentInfoController : BaseController
    {
        private StudentInfoBLL studentInfoBLL = new StudentInfoBLL();

        #region 视图功能
        [AuthorizeFilter("charge:studentinfo:view")]
        public ActionResult StudentInfoIndex()
        {
            return View();
        }

        public ActionResult StudentInfoIndexSheet()
        {
            return View();
        }

        public ActionResult StudentInfoForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("charge:studentinfo:search")]
        public async Task<ActionResult> GetListJson(StudentInfoListParam param)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<StudentInfoEntity>> obj = await studentInfoBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("charge:studentinfo:search")]
        public async Task<ActionResult> GetListJsonCom(StudentInfoListParam param)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if (!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<StudentInfoEntity>> obj = await studentInfoBLL.GetList(param);
            if(obj.Tag == 1 && obj.Result.Count>0)
            {
                StudentInfoEntity info = new StudentInfoEntity();
                info.Code = "0";
                info.Name = "选择全部学生";
                obj.Result.Insert(0,info);
            }
            
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("charge:studentinfo:search")]
        public async Task<ActionResult> GetPageListJson(StudentInfoListParam param, Pagination pagination)
        {
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            if(!operatorInfo.RoleIds.Contains(GlobalContext.SystemConfig.RoleId))//不是超级管理员
            {
                param.SysDepartmentId = operatorInfo.DepartmentId;
            }
            TData<List<StudentInfoEntity>> obj = await studentInfoBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<ActionResult> GetFormJson(long id)
        {
            TData<StudentInfoEntity> obj = await studentInfoBLL.GetEntity(id);
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("charge:studentinfo:add,charge:studentinfo:edit")]
        public async Task<ActionResult> SaveFormJson(StudentInfoEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                entity.Code = await new StudentInfoCache().GetStudentCode();
            }
            OperatorInfo operatorInfo = await Operator.Instance.Current();
            entity.SysDepartmentId = operatorInfo.DepartmentId;
            entity.SysDepartmentName = operatorInfo.DepartmentName;
            TData<string> obj = await studentInfoBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("charge:studentinfo:delete")]
        public async Task<ActionResult> DeleteFormJson(string ids)
        {
            TData obj = await studentInfoBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
