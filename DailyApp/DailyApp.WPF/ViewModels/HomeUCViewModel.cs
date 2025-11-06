using DailyApp.WPF.DTOs;
using DailyApp.WPF.Models;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.ViewModels
{
	internal class HomeUCViewModel : Prism.Mvvm.BindableBase, INavigationAware
    {
		/// <summary>
		/// 构造函数
		/// </summary>
        public HomeUCViewModel()
        {
			CreateStatPanelList();
            CreateWaitList();
            CreateMemoList();
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
        /// <exception cref="NotImplementedException"></exception>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 检查是否携带了登录名参数（由登录流程传入）
            if (navigationContext.Parameters.ContainsKey("LoginName"))
            {
                DateTime now = DateTime.Now;
                string[] week = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
                string loginName = navigationContext.Parameters.GetValue<string>("LoginName");

                LoginInfo = $"您好！{loginName}, 今天是{now.ToString("yyyy-MM-dd")} {week[(int)now.DayOfWeek]}";
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
    }
}
