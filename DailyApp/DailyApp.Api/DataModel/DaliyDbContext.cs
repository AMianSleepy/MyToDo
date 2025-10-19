using Microsoft.EntityFrameworkCore;

namespace DailyApp.Api.DataModel
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DaliyDbContext : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public DaliyDbContext(DbContextOptions<DaliyDbContext> options) : base (options)
        {
            
        }

        // 定义要迁移的数据模型
        public DbSet<AccountInfo> AccountInfo { get; set; }

    }
}
