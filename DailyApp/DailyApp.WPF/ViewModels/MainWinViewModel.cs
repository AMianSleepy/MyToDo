using DailyApp.WPF.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        /// <summary>
        /// 左侧菜单列表集合
        /// </summary>
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
        public MainWinViewModel(IRegionManager _RegionManager)
        {
            LeftMenuList = new List<LeftMenuInfo>();

            // 创建菜单数据
            CreateMenu();

            // 区域
            RegionManager = _RegionManager;
            // 导航命令
            NavigateCmm = new DelegateCommand<LeftMenuInfo>(Navigate);
            // 后退
            GoBackCmm = new DelegateCommand(GoBack);
            // 前进
            GoForwardCmm = new DelegateCommand(GoForward);
        }

        /// <summary>
        /// 创建菜单数据
        /// </summary>
        private void CreateMenu()
        {
            LeftMenuList.Add(new LeftMenuInfo() { Icon = "Home", MenuName = "首页", ViewName = "HomeUC" });
            LeftMenuList.Add(new LeftMenuInfo() { Icon = "NotebookOutline", MenuName = "待办事项", ViewName = "WaitUC" });
            LeftMenuList.Add(new LeftMenuInfo() { Icon = "NotebookPlus", MenuName = "备忘录", ViewName = "MemoUC" });
            LeftMenuList.Add(new LeftMenuInfo() { Icon = "Cog", MenuName = "设置", ViewName = "SettingsUC" });
        }

        #region 区域+导航 实现导航功能
        private readonly IRegionManager RegionManager;
        public DelegateCommand<LeftMenuInfo> NavigateCmm { get; set; }
        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="menu">菜单信息</param>
        private void Navigate(LeftMenuInfo menu)
        {
            if (menu == null || string.IsNullOrEmpty(menu.ViewName))
            {
                return;
            }
            // 导航 区域
            RegionManager.Regions["MainViewRegion"].RequestNavigate(menu.ViewName, callback =>
            {
                Journal = callback.Context.NavigationService.Journal;// 记录导航足迹
            });
        }
        #endregion

        #region 前进 后退
        // 历史记录
        private IRegionNavigationJournal Journal;
        // 后退命令
        public DelegateCommand GoBackCmm { get; private set; }
        // 前进命令
        public DelegateCommand GoForwardCmm { get; private set; }
        /// <summary>
        /// 后退方法
        /// </summary>
        private void GoBack()
        {
            if (Journal != null && Journal.CanGoBack)
            {
                Journal.GoBack();
            }
        }
        /// <summary>
        /// 前进方法
        /// </summary>
        private void GoForward()
        {
            if (Journal != null && Journal.CanGoForward)
            {
                Journal.GoForward();
            }
        }
        #endregion

        /// <summary>
        /// 默认首页
        /// </summary>
        /// <param name="loginName">登录名</param>
        public void SetDefaultNav(string loginName)
        {
            NavigationParameters pairs = new();
            pairs.Add("LoginName", loginName);

            RegionManager.Regions["MainViewRegion"].RequestNavigate("HomeUC", callback =>
            {
                Journal = callback.Context.NavigationService.Journal;// 记录导航足迹
            }, pairs);
        }
    }
}
