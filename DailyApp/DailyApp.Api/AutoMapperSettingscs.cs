using AutoMapper;
using DailyApp.Api.DataModel;
using DailyApp.Api.DTOs;

namespace DailyApp.Api
{
    /// <summary>
    /// model之前转换设置
    /// </summary>
    public class AutoMapperSettingscs : Profile
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoMapperSettingscs()
        {
            CreateMap<AccountInfoDTO, AccountInfo>().ReverseMap();

        }
    }
}
