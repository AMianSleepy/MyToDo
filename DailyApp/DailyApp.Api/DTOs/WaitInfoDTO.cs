namespace DailyApp.Api.DTOs
{
    /// <summary>
    /// 待办事项DTO（接收添加待办事项）
    /// </summary>
    public class WaitInfoDTO
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        ///<summary>
        /// 内容
        ///</summary>
        public string Content { get; set; }
        ///<summary>
        /// 状态 0-待办；1-已完成
        ///  </summary>
        public int Status { get; set; }
    }
}
