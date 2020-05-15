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
using YiSha.Web.Code;

namespace YiSha.Business.ChargeManage
{
    /// <summary>
    /// 创 建：admin
    /// 日 期：2020-05-11 11:13
    /// 描 述：学生信息业务类
    /// </summary>
    public class StudentInfoBLL
    {
        private StudentInfoService studentInfoService = new StudentInfoService();

        #region 获取数据
        public async Task<TData<List<StudentInfoEntity>>> GetList(StudentInfoListParam param)
        {
            TData<List<StudentInfoEntity>> obj = new TData<List<StudentInfoEntity>>();
            obj.Result = await studentInfoService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<StudentInfoEntity>>> GetPageList(StudentInfoListParam param, Pagination pagination)
        {
            TData<List<StudentInfoEntity>> obj = new TData<List<StudentInfoEntity>>();
            obj.Result = await studentInfoService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<StudentInfoEntity>> GetEntity(long id)
        {
            TData<StudentInfoEntity> obj = new TData<StudentInfoEntity>();
            obj.Result = await studentInfoService.GetEntity(id);
            if (obj.Result != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }


        /// <summary>
        /// 获取最新一条数据
        /// </summary>
        /// <returns></returns>
        public async Task<TData<StudentInfoEntity>> GetCodeTop(string code)
        {
            TData<StudentInfoEntity> obj = new TData<StudentInfoEntity>();
            obj.Result = await studentInfoService.GetCodeTop(code);
            if (obj.Result != null)
            {
                obj.Tag = 1;
            }
            return obj;
        }

        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(StudentInfoEntity entity)
        {
            TData<string> obj = new TData<string>();
            await studentInfoService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await studentInfoService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
