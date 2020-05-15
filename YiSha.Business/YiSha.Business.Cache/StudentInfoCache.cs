using System.Linq;
using System.Collections.Generic;
using YiSha.Cache.Factory;
using YiSha.Entity.SystemManage;
using YiSha.Service.SystemManage;
using System.Threading.Tasks;
using YiSha.Entity.ChargeManage;
using YiSha.Service.ChargeManage;
using System;

namespace YiSha.Business.Cache
{
    public class StudentInfoCache : BaseBusinessCache<StudentInfoEntity>
    {
        private StudentInfoService studentInfoService = new StudentInfoService();

        public override string CacheKey => this.GetType().Name;

        public string CacheCodeKey => this.GetType().Name+"_code";

        public async Task<string> GetStudentCode()
        {
            var cacheString = CacheFactory.Cache.GetCache<string>(CacheCodeKey);
            if (cacheString == null)
            {
                var result = await studentInfoService.GetCodeTop(DateTime.Now.ToString("yyyyMM"));
                string code = "";
                if(result == null || string.IsNullOrEmpty(result.Code))
                {
                    code = DateTime.Now.ToString("yyyyMM") + "0001";
                }
                else
                {
                    code = (double.Parse(result.Code) + 1).ToString();
                }
                CacheFactory.Cache.SetCache(CacheCodeKey, code);
                return code;
            }
            else
            {
                cacheString = (double.Parse(cacheString) + 1).ToString();
                CacheFactory.Cache.SetCache(CacheCodeKey, cacheString);
                return cacheString;
            }
        }
    }
}
