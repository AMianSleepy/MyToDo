namespace DailyApp.Api.DTOs
{
    /// <summary>
    /// 账号DTO（用来接收注册的信息）
    /// </summary>
    public class AccountInfoDTO
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
    }
}
