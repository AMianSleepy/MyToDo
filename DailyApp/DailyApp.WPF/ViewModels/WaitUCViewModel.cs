using DailyApp.WPF.DTOs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.ViewModels
{

    /// <summary>
    /// 待办事项视图模型
    /// </summary>
    internal class WaitUCViewModel : BindableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WaitUCViewModel()
        {
            CreateWaitList();

            // 显示添加待办命令
            ShowAddWaitCmm = new DelegateCommand(ShowAddWait);
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
                new WaitInfoDTO() { Title = "测试录屏", Content = "仔细Content"},
                new WaitInfoDTO() { Title = "上传录屏", Content = "1234567890"},
                new WaitInfoDTO() { Title = "测试录屏", Content = "仔细Content"},
                new WaitInfoDTO() { Title = "上传录屏", Content = "1234567890"},
                new WaitInfoDTO() { Title = "测试录屏", Content = "仔细Content"},
                new WaitInfoDTO() { Title = "上传录屏", Content = "1234567890"},
                new WaitInfoDTO() { Title = "测试录屏", Content = "仔细Content"},
                new WaitInfoDTO() { Title = "上传录屏", Content = "1234567890"},
                new WaitInfoDTO() { Title = "测试录屏", Content = "仔细Content"},
                new WaitInfoDTO() { Title = "上传录屏", Content = "1234567890"},
                new WaitInfoDTO() { Title = "测试录屏", Content = "仔细Content"},
                new WaitInfoDTO() { Title = "上传录屏", Content = "1234567890"},
                new WaitInfoDTO() { Title = "测试录屏", Content = "仔细Content"},
                new WaitInfoDTO() { Title = "上传录屏", Content = "1234567890"},
                new WaitInfoDTO() { Title = "测试录屏", Content = "仔细Content"},
                new WaitInfoDTO() { Title = "上传录屏", Content = "1234567890"},
            };
        }

        #region 显示“添加待办”
        private bool _IsShowAddWait;
        /// <summary>
        /// 是否显示“添加待办”
        /// </summary>
        public bool IsShowAddWait
        {
            get { return _IsShowAddWait; }
            set
            { 
                _IsShowAddWait = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 显示添加待办
        /// </summary>
        private void ShowAddWait()
        {
            IsShowAddWait = true;
        }

        /// <summary>
        /// 显示“添加待办”命令
        /// </summary>
        public DelegateCommand ShowAddWaitCmm { get; set; }
        #endregion
    }
}
