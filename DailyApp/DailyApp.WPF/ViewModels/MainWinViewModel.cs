using DailyApp.WPF.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DailyApp.WPF.ViewModels
{
    /// <summary>
    /// 主界面视图模型
    /// </summary>
    internal class MainWinViewModel : BindableBase
    {
        #region 左侧菜单
        private List<LeftMenuInfo> _LeftMenuList;

        public List<LeftMenuInfo> LeftMenuList
        {
            get { return _LeftMenuList; }
            set
            { 
                _LeftMenuList = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainWinViewModel()
        {
            LeftMenuList = new List<LeftMenuInfo>();

            // 创建菜单数据
            CreateMenu();
        }

        /// <summary>
        /// 创建菜单数据
        /// </summary>
        private void CreateMenu()
        {
            LeftMenuList.Add(new LeftMenuInfo() { Icon = "Home", MenuName = "首页", ViewName = "IndexView" });
            LeftMenuList.Add(new LeftMenuInfo() { Icon = "NotebookOutline", MenuName = "待办事项", ViewName = "ToDoView" });
            LeftMenuList.Add(new LeftMenuInfo() { Icon = "NotebookPlus", MenuName = "备忘录", ViewName = "MemoView" });
            LeftMenuList.Add(new LeftMenuInfo() { Icon = "Cog", MenuName = "设置", ViewName = "SettingsView" });
        }
    }
}
