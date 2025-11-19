using AutoMapper;
using DailyApp.Api.ApiResponses;
using DailyApp.Api.DataModel;
using DailyApp.Api.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyApp.Api.Controllers
{
    /// <summary>
    /// 账户接口
    /// </summary>           更改路由
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly DaliyDbContext db;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_db"></param>
        public AccountController(DaliyDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="accountInfoDTO">注册信息</param>
        /// <returns>-1：账号以被注册；1：账号注册成功；-98：账号注册失败；-99：账号注册出错</returns>
        [HttpPost]
        public IActionResult Reg(AccountInfoDTO accountInfoDTO)
        {
            ApiResponse res = new ApiResponse(); // 响应的数据

            // 业务
            try
            {
                // 1、账号是否存在（未考虑高并发）
                var dbAccount = db.AccountInfo.Where(t => t.Account == accountInfoDTO.Account).FirstOrDefault();
                if (dbAccount != null)
                {
                    res.ResultCode = -1;// 账号已经存在
                    res.Msg = "对不起，账号已被注册！";
                    return Ok(res);
                }

                // 2、如果不存在则添加账号
                // DTO -> AccountInfo
                AccountInfo accountInfo = mapper.Map<AccountInfo>(accountInfoDTO);

                db.AccountInfo.Add(accountInfo);
                int result = db.SaveChanges();// 保存 受影响的行数
                if (result == 1)
                {
                    res.ResultCode = 1;
                    res.Msg = "账号注册成功";
                }
                else
                {
                    res.ResultCode = -98;
                    res.Msg = "账号注册失败";
                }
            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "账号注册出错";
            }
            return Ok(res);
        }

        /// <summary>
        /// 登录Api
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="pwd">密码（MD5值）</param>
        /// <returns>登录信息 -1：账号或密码错误；1：登录成功；-99：未知错误</returns>
        [HttpGet]
        public IActionResult Login(string account, string pwd)
        {
            ApiResponse res = new ApiResponse();

            try
            {
                var dnAccountInfo = db.AccountInfo.Where(t => t.Account == account && t.Pwd == pwd).FirstOrDefault();

                if (dnAccountInfo == null)
                {
                    res.ResultCode = -1;
                    res.Msg = "账号或密码错误！";
                    return Ok(res);
                }

                res.ResultCode = 1;// 1表示登录成功
                res.Msg = "登录成功";
                res.ResultData = dnAccountInfo;
            }
            catch (Exception)
            {
                res.ResultCode = -99;
                res.Msg = "未知错误，登录失败！";
            }
            return Ok(res);
        }
    }
}
