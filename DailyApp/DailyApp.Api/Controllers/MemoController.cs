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
    }
}
