using AutoMapper;
using DailyApp.Api.DataModel;
using DailyApp.Api.DTOs;
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

        private readonly IMapper mapper;

        public WaitController(DaliyDbContext _db, IMapper _mapper)
        {
            db = _db;

            mapper = _mapper;
        }

        /// <summary>
        /// 统计待办数据
        /// </summary>
        /// <returns>1：统计成功 -99：异常错误</returns>
        [HttpGet]
        public IActionResult StatWait()
        {
            ApiResponses.ApiResponse res = new();

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

        /// <summary>
        /// 添加待办事项
        /// </summary>
        /// <param name="waitInfoDTO">待办事项信息</param>
        /// <returns>1：添加成功；-99：异常</returns>
        [HttpPost]
        public IActionResult AddWait(WaitInfoDTO waitInfoDTO)
        {
            ApiResponses.ApiResponse reponses = new();

            try
            {
                // DTO -> Info
                WaitInfo accountInfo = mapper.Map<WaitInfo>(waitInfoDTO);
                db.WaitInfo.Add(accountInfo);
                int result = db.SaveChanges();
                if (result == 1)
                {
                    reponses.ResultCode = 1;
                    reponses.Msg = "添加待办事项成功";
                }
                else
                {
                    reponses.ResultCode = -99;
                    reponses.Msg = "添加待办事项失败，改变的行数≠1";
                }
            }
            catch (Exception ex)
            {
                reponses.ResultCode = -99;
                reponses.Msg = $"ex:{ex}";
            }

            return Ok(reponses);
        }
    }
}
