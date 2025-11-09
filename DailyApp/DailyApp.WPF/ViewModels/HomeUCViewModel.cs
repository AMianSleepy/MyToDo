using DailyApp.WPF.DTOs;
using DailyApp.WPF.HttpClients;
using DailyApp.WPF.Models;
using DailyApp.WPF.Service;
using DailyApp.WPF.Views.Dialogs;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyApp.WPF.ViewModels
{
	internal class HomeUCViewModel : Prism.Mvvm.BindableBase, INavigationAware
    {
		/// <summary>
		/// 构造函数
		/// </summary>
        public HomeUCViewModel(HttpClients.HttpRestClient _HttpClient, DialogHostService _DialogHostService)
        {
			CreateStatPanelList(); // 创建统计数据面板
            CreateWaitList(); // 创建待办事项模拟数据
            CreateMemoList(); // 创建备忘录测试数据

            HttpClient = _HttpClient; // 请求API的客户端

            // 打开添加待办事项命令：使用标准 DelegateCommand 构造函数
            ShowAddWaitDialogCmm = new DelegateCommand(async () => await ShowAddWaitDialog());

            DialogHostService = _DialogHostService;
        }
        private List<StatPanelInfo> _StatPanelList;
		/// <summary>
		/// 统计面板数据
		/// </summary>
		public List<StatPanelInfo> StatPanelList
		{
			get { return _StatPanelList; }
			set
			{
				_StatPanelList = value;
				RaisePropertyChanged();
			}
		}

		/// <summary>
		/// 创建统计面板数据
		/// </summary>
		private void CreateStatPanelList()
		{
			StatPanelList = new List<StatPanelInfo>
            {
                new StatPanelInfo() { Icon = "ClockFast", ItemName = "汇总", BackColor = "#FF0CA0FF", ViewName = "WaitUC", Result = "9" },
                new StatPanelInfo() { Icon = "ClockCheckOutline", ItemName = "已完成", BackColor = "#FF1ECA3A", ViewName = "WaitUC", Result = "9" },
                new StatPanelInfo() { Icon = "ChartLineVariant", ItemName = "完成比例", BackColor = "#FF02C6DC", ViewName = "", Result = "90%" },
                new StatPanelInfo() { Icon = "PlaylistStar", ItemName = "备忘录", BackColor = "#FFFFA000", ViewName = "MemoUC", Result = "20" }
            };
		}

        private List<DailyApp.WPF.DTOs.WaitInfoDTO> _WaitList;
        /// <summary>
        /// 待办事项数据
        /// </summary>
        public List<DailyApp.WPF.DTOs.WaitInfoDTO> WaitList
        {
            get { return _WaitList; }
            set
            {
                _WaitList = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 创建待办事项模拟数据
        /// </summary>
        private void CreateWaitList()
        {
            WaitList = new List<WaitInfoDTO>
            { 
                new WaitInfoDTO() { Title = "测试录屏", Content = "仔细Content"},
                new WaitInfoDTO() { Title = "上传录屏", Content = "1234567890"},
            };
        }

        private List<DailyApp.WPF.DTOs.MemoInfoDTO> _MemoList;
        /// <summary>
        /// 备忘录数据
        /// </summary>
        public List<DailyApp.WPF.DTOs.MemoInfoDTO> MemoList
        {
            get { return _MemoList; }
            set
            {
                _MemoList = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 创建备忘录测试数据
        /// </summary>
        private void CreateMemoList()
        {
            MemoList = new List<MemoInfoDTO>
            {
                new MemoInfoDTO() { Title = "会议一", Content = "项目方向"},
                new MemoInfoDTO() { Title = "会议二", Content = "项目内容"},
            };
        }

        #region 显示登录用户姓名
        private string _LoginInfo;
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public string LoginInfo
        {
            get { return _LoginInfo; }
            set 
            { 
                _LoginInfo = value;
                RaisePropertyChanged(); 
            }
        }

        /// <summary>
        /// 进入当前视图时触发。用于读取导航参数并更新
        /// </summary>
        /// <param name="navigationContext">导航上下文，包含来源、目标及参数等信息</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 检查是否携带了登录名参数（由登录流程传入）
            if (navigationContext.Parameters.ContainsKey("LoginName"))
            {
                DateTime now = DateTime.Now;
                string[] week = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
                string loginName = navigationContext.Parameters.GetValue<string>("LoginName");

                LoginInfo = $"您好！{loginName}, 今天是{now.ToString("yyyy-MM-dd")} {week[(int)now.DayOfWeek]}";

                // 统计待办事项
                CallStatWait();
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// 从当前视图导航离开时触发。可在此保存状态、释放资源或取消订阅事件等。
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
        #endregion

        #region 待办事项统计
        private StatWaitDTO StatWaitDTO { get; set; } = new();

        private readonly HttpRestClient HttpClient;

        /// <summary>
        /// 调用API获取统计待办实现数据
        /// </summary>
        private void CallStatWait()
        {
            ApiRequest apiRequest = new();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = "Wait/StatWait";
            apiRequest.Parameters = StatWaitDTO;

            ApiResponse response = HttpClient.Execute(apiRequest);

            if (response.ResultCode == 1)
            {
                StatWaitDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<StatWaitDTO>(response.ResultData.ToString());

                RefreshWaitStat();
            }
        }

        /// <summary>
        /// 更新待办统计数据
        /// </summary>
        private void RefreshWaitStat()
        {
            StatPanelList[0].Result = StatWaitDTO.TotalCount.ToString();
            StatPanelList[1].Result = StatWaitDTO.FinishCount.ToString();
            StatPanelList[2].Result = StatWaitDTO.FinishPercent;
        }
        #endregion

        #region 
        // 对话服务（自定义的）
        private readonly DialogHostService DialogHostService;

        public DelegateCommand ShowAddWaitDialogCmm { get; set; }
        /// <summary>
        /// 打开添加待办事项对话框
        /// </summary>
        private async Task ShowAddWaitDialog()
        {
            var result = await DialogHostService.ShowDialog("AddWaitUC", null);
            if (result.Result == ButtonResult.OK)
            {
                // 接收数据
                if (result.Parameters.ContainsKey("AddWaitInfo"))
                {
                    var addModel = result.Parameters.GetValue<WaitInfoDTO>("AddWaitInfo");

                    // 调用API实现添加待办事项

                }
            }
        }
        #endregion
    }
}
