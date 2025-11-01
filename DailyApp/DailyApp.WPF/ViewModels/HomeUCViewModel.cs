using DailyApp.WPF.DTOs;
using DailyApp.WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.ViewModels
{
	internal class HomeUCViewModel : Prism.Mvvm.BindableBase
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
    }
}
