using AutoMapper;
using Azure;
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
            ApiResponses.ApiResponse responses = new();

            try
            {
                // DTO -> Info
                WaitInfo accountInfo = mapper.Map<WaitInfo>(waitInfoDTO);
                db.WaitInfo.Add(accountInfo);
                int result = db.SaveChanges();
                if (result == 1)
                {
                    responses.ResultCode = 1;
                    responses.Msg = "添加待办事项成功";
                }
                else
                {
                    responses.ResultCode = -99;
                    responses.Msg = "添加待办事项失败，改变的行数≠1";
                }
            }
            catch (Exception ex)
            {
                responses.ResultCode = -99;
                responses.Msg = $"ex:{ex}";
            }

            return Ok(responses);
        }

        /// <summary>
        /// 获取待办事项的所有待办状态
        /// </summary>
        /// <returns>1：成功；-99：异常</returns>
        [HttpGet]
        public IActionResult GetWaiting()
        {
            ApiResponses.ApiResponse responses = new();

            try
            {
                // Linq
                var list = from A in db.WaitInfo
                           where A.Status == 0
                           select new WaitInfo
                           {
                               WaitId = A.WaitId,
                               Title = A.Title,
                               Content = A.Content,
                               Status = A.Status
                           };
                responses.ResultCode = 1;
                responses.Msg = "获取成功";
                responses.ResultData = list;
            }
            catch (Exception ex)
            {
                responses.ResultCode = -99;
                responses.Msg = $"ex:{ex}";
            }

            return Ok(responses);
        }

        /// <summary>
        /// 修改待办事项状态
        /// </summary>
        /// <param name="newStatusDTO">新状态待办事项</param>
        /// <returns>1：修改成功；-1：状态ID错误；-98：失败；-99：异常</returns>
        [HttpPut]
        public IActionResult UpDateStatus(WaitInfoDTO newStatusDTO)
        {
            ApiResponses.ApiResponse responses = new();

            try
            {
                var dbInfo = db.WaitInfo.Find(newStatusDTO.WaitId);
                if (dbInfo != null)
                {
                    dbInfo.Status = newStatusDTO.Status;// 部分修改
                    int result = db.SaveChanges();
                    if (result == 1)
                    {
                        responses.ResultCode = 1;
                        responses.Msg = (newStatusDTO.Status == 0 ? "状态成功设置为待办" : "状态成功设置为已完成");
                    }
                    else
                    {
                        responses.ResultCode = -98;
                        responses.Msg = "修改待办事项状态失败";
                    }
                }
                else
                {
                    responses.ResultCode = -1;
                    responses.Msg = "请检查待办事项ID是否正确";
                }
            }
            catch (Exception ex)
            {
                responses.ResultCode = -99;
                responses.Msg = $"ex:{ex}";
            }

            return Ok(responses);
        }

        /// <summary>
        /// 修改待办事项内容
        /// </summary>
        /// <param name="newWaitInfoDTO">新的待办事项</param>
        /// <returns>1：修改成功；-1：状态ID错误；-98：失败；-99：异常</returns>
        [HttpPut]
        public IActionResult UpDateWaitInfo(WaitInfoDTO newWaitInfoDTO)
        {
            ApiResponses.ApiResponse responses = new();

            try
            {
                var dbInfo = db.WaitInfo.Find(newWaitInfoDTO.WaitId);
                if (dbInfo != null)
                {
                    dbInfo.Status = newWaitInfoDTO.Status;
                    dbInfo.Title = newWaitInfoDTO.Title;
                    dbInfo.Content = newWaitInfoDTO.Content;

                    int result = db.SaveChanges();
                    if (result == 1)
                    {
                        responses.ResultCode = 1;
                        responses.Msg = "编辑成功";
                    }
                    else
                    {
                        responses.ResultCode = -98;
                        responses.Msg = "编辑失败";
                    }
                }
                else
                {
                    responses.ResultCode = -1;
                    responses.Msg = "请检查待办事项ID是否正确";
                }
            }
            catch (Exception ex)
            {
                responses.ResultCode = -99;
                responses.Msg = $"ex:{ex}";
            }

            return Ok(responses);
        }
    }
}
