using DailyApp.Api.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyApp.Api.Controllers
{
    /// <summary>
    /// 待办事项接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WaitController : ControllerBase
    {
        private readonly DaliyDbContext db;

        public WaitController(DaliyDbContext _db)
        {
            db = _db;
        }

        /// <summary>
        /// 统计待办数据
        /// </summary>
        /// <returns>1：统计成功 -99：异常错误</returns>
        [HttpGet]
        public IActionResult StatWait()
        {
            DailyApp.Api.ApiReponses.ApiReponse res = new();

            try
            {
                var list = db.WaitInfo.ToList();
                var finishList = db.WaitInfo.Where(t => t.Status == 1).ToList();

                DTOs.StatWaitDTO statWaitDTO = new() { FinishCount = finishList.Count, TotalCount = list.Count };

                res.ResultCode = 1;// 统计成功
                res.Msg = "统计待办事项成功";
                res.ResultData = statWaitDTO;
            }
            catch (Exception ex)
            {
                res.ResultCode = -99;
                res.Msg = $"错误信息：{ex}";
            }
            return Ok(res);
        }
    }
}
