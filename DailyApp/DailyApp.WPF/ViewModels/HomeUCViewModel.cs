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
		/// 创建统计数据
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
	}
}
