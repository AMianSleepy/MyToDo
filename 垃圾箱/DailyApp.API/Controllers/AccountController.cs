using DailyApp.API.DataModel;
using DailyApp.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyApp.API.Controllers
{
    /// <summary>
    /// 账户接口
    /// </summary>          //改路由
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        private readonly DailyDbContext Db;
        public AccountController(DailyDbContext _Db)
        {
            Db = _Db;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="accountInfoDTO">注册信息</param>
        /// <returns>-1：账号已注册；1：注册成功；-98/-99：注册失败</returns>
        [HttpPost]
        public IActionResult Reg(AccountInfoDTO accountInfoDTO)
        {
            // 响应的数据
            ApiResponses.ApiResponse res = new ApiResponses.ApiResponse();

            // 业务
            try
            {
                // 1、账号是否存在              Linq                                            查询到第一个或查询不到
                var dbAccount = Db.AccountInfos.Where(t => t.Account == accountInfoDTO.Account).FirstOrDefault();
                if (dbAccount != null)
                {
                    res.ResultCode = -1;// 账号已存在
                    res.Msg = "此账号已被注册，请修改注册信息！";

                    return Ok(res);
                }

                // 2、如果不存在则添加账号
                AccountInfo accountInfo = new AccountInfo() { Account = accountInfoDTO.Account, Name = accountInfoDTO.Name, Pwd = accountInfoDTO.Pwd };
                Db.AccountInfos.Add(accountInfo);
                int result = Db.SaveChanges();// 保存 返回受影响的行数
                if (result == 1)
                {
                    res.ResultCode = 1;// 账号注册成功
                    res.Msg = "账号注册成功";
                }
                else
                {
                    res.ResultCode = -98;// 账号注册失败
                    res.Msg = "账号注册失败";
                }
            }
            catch (Exception)
            {
                res.ResultCode = -99;// 账号注册失败
                res.Msg = "账号注册失败";
            }

            return Ok(res);
        }
    }
}
