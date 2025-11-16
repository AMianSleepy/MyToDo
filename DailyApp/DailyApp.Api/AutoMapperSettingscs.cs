using AutoMapper;
using DailyApp.Api.DataModel;
using DailyApp.Api.DTOs;

namespace DailyApp.Api
{
    /// <summary>
    /// model之间转换设置
    /// </summary>
    public class AutoMapperSettingscs : Profile
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoMapperSettingscs()
        {
            // 登录用户信息
            CreateMap<AccountInfoDTO, AccountInfo>().ReverseMap();

            // 待办事项信息
            CreateMap<WaitInfoDTO, WaitInfo>().ReverseMap();

            // 备忘录信息
            CreateMap<MemoInfoDTO, MemoInfo>().ReverseMap();
        }
    }
}
