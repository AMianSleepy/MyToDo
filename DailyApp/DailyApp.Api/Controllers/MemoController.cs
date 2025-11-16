using AutoMapper;
using DailyApp.Api.DataModel;
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
        public IActionResult Addmemo(DTOs.MemoInfoDTO memoInfoDTO)
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
    }
}
