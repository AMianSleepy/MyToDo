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
    /// 备忘录视图模型
    /// </summary>
    internal class MemoUCViewModel : BindableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MemoUCViewModel()
        {
            CreateMemoList();

            // 显示添加备忘录命令
            ShowAddMemoCmm = new DelegateCommand(ShowAddMemo);
        }

        private List<DailyApp.WPF.DTOs.MemoInfoDTO> _MemoList;
        /// <summary>
        /// 备忘录事项数据
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
        /// 创建备忘录事项模拟数据
        /// </summary>
        private void CreateMemoList()
        {
            MemoList = new List<MemoInfoDTO>
            {
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
                new MemoInfoDTO() { Title = "会议一", Content = "仔细Content"},
                new MemoInfoDTO() { Title = "会议二", Content = "1234567890"},
            };
        }

        #region 显示“添加备忘录”
        private bool _IsShowAddMemo;
        /// <summary>
        /// 是否显示“添加备忘录”
        /// </summary>
        public bool IsShowAddMemo
        {
            get { return _IsShowAddMemo; }
            set
            {
                _IsShowAddMemo = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 显示添加备忘录
        /// </summary>
        private void ShowAddMemo()
        {
            IsShowAddMemo = true;
        }

        /// <summary>
        /// 显示“添加备忘录”命令
        /// </summary>
        public DelegateCommand ShowAddMemoCmm { get; set; }
        #endregion
    }
}
