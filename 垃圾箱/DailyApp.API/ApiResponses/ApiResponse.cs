namespace DailyApp.API.ApiResponses
{
    /// <summary>
    /// 响应模型
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// 结果编码
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 结果信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 结果数据
        /// </summary>
        public object ResultData { get; set; }
    }
}
