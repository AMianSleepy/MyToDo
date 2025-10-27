using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DailyApp.API.DataModel
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DailyDbContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public DailyDbContext(DbContextOptions<DailyDbContext> options) : base (options)
        {
            
        }

        /// <summary>
        /// 账号表
        /// </summary>
        public DbSet<AccountInfo> AccountInfos { get; set; }
    }
}
