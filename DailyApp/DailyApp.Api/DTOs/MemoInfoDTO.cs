namespace DailyApp.Api.DTOs
{
    /// <summary>
    /// 备忘录DTO
    /// </summary>
    public class MemoInfoDTO
    {
        /// <summary>
        /// 备忘录ID
        /// </summary>
        public int MemoId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }
        ///<summary>
        /// 内容
        ///</summary>
        public string? Content { get; set; }
    }
}
