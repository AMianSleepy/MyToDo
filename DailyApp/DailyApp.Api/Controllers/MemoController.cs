using AutoMapper;
using DailyApp.Api.DataModel;
using DailyApp.Api.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyApp.Api.Controllers
{
    /// <summary>
    /// 备忘录接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemoController : ControllerBase
    {
        private readonly DaliyDbContext db;

        private readonly IMapper mapper;

        public MemoController(DaliyDbContext _db, IMapper _mapper)
        {
            db = _db;

            mapper = _mapper;
        }

        /// <summary>
        /// 统计备忘录总数据
        /// </summary>
        /// <returns>1：查询正确；-99：查询异常</returns>
        [HttpGet]
        public IActionResult StatMemo()
        {
            ApiResponses.ApiResponse apiResponse = new();

            try
            {
                int count = db.MemoInfo.Count();
                apiResponse.ResultCode = 1;
                apiResponse.Msg = "查询成功";
                apiResponse.ResultData = count;
            }
            catch (Exception ex)
            {
                apiResponse.ResultCode = -99;
                apiResponse.Msg = $"ex{ex}";
            }
            return Ok(apiResponse);
        }

        /// <summary>
        /// 添加备忘录
        /// </summary>
        /// <param name="memoInfoDTO">备忘录信息</param>
        /// <returns>1：添加成功；-98：添加失败；-99：异常</returns>
        [HttpPost]
        public IActionResult AddMemo(DTOs.MemoInfoDTO memoInfoDTO)
        {
            ApiResponses.ApiResponse response = new();
            try
            {
                MemoInfo memoInfo = mapper.Map<MemoInfo>(memoInfoDTO);

                db.MemoInfo.Add(memoInfo);

                int result = db.SaveChanges();
                if (result == 1)
                {
                    response.ResultCode = 1;
                    response.Msg = "备忘录添加成功";
                }
                else
                {
                    response.ResultCode = -98;
                    response.Msg = "备忘录添加失败";
                }
            }
            catch (Exception ex)
            {
                response.ResultCode = -99;
                response.Msg = $"ex:{ex}";
            }
            return Ok(response);
        }

        /// <summary>
        /// 备忘录查询
        /// </summary>
        /// <param name="title">标题（模糊查询）</param>
        /// <returns>1：查询成功；-99：异常</returns>
        [HttpGet]
        public IActionResult QueryMemo(string? title)
        {
            ApiResponses.ApiResponse response = new();

            try
            {
                var query = from A in db.MemoInfo
                            select new DTOs.MemoInfoDTO
                            {
                                MemoId = A.MemoId,
                                Title = A.Title,
                                Content = A.Content
                            };
                if (!string.IsNullOrEmpty(title))
                {
                    query = query.Where(t => t.Title.Contains(title));
                }
                response.ResultCode = 1;
                response.Msg = "查询成功";
                response.ResultData = query;
            }
            catch (Exception ex)
            {
                response.ResultCode = -99;
                response.Msg = $"ex:{ex}";
            }
            return Ok(response);
        }

        /// <summary>
        /// 编辑备忘录信息
        /// </summary>
        /// <param name="memoInfoDTO">备忘录新信息</param>
        /// <returns>1：编辑成功；-97：ID错误；-98：编辑失败；-99：异常</returns>
        [HttpPut]
        public IActionResult EditMemo(MemoInfoDTO memoInfoDTO)
        {
            ApiResponses.ApiResponse response = new();

            try
            {
                var dbInfo = db.MemoInfo.Find(memoInfoDTO.MemoId);
                if (dbInfo == null)
                {
                    response.ResultCode = -97;
                    response.Msg = "请传入正确的备忘录ID";
                    return Ok(response);
                }

                dbInfo.Title = memoInfoDTO.Title;
                dbInfo.Content = memoInfoDTO.Content;

                int result = db.SaveChanges();
                if (result == 1)
                {
                    response.ResultCode = 1;
                    response.Msg = "备忘录修改成功";
                }
                else
                {
                    response.ResultCode = -98;
                    response.Msg = "备忘录修改失败";
                }
            }
            catch (Exception ex)
            {
                response.ResultCode = -99;
                response.Msg = $"ex:{ex}";
            }
            return Ok(response);
        }
    }
}
