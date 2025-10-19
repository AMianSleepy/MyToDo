// Data Transger Object 数据传输对象
namespace DailyApp.API.DTOs
{
    /// <summary>
    /// 账号DTO（用来接收数据信息）
    /// </summary>
    public class AccountInfoDTO
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
    }
}
