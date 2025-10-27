using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyApp.API.DataModel
{
    /// <summary>
    /// 登录账号数据模型
    /// </summary>
    [Table("AccountInfo")]// 指定表的名字
    public class AccountInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]// 主键
        public int AccountId { get; set; }

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
