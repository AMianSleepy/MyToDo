using DailyApp.WPF.DTOs;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyApp.WPF.ViewModels.Dialogs
{
    /// <summary>
    /// 修改备忘录视图模型
    /// </summary>
    internal class EditMemoUCViewModel : Service.IDialogHostAware
    {
        /// <summary>
        /// 确定命令
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }
        /// <summary>
        /// 取消命令
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

        public void OnDialogOpening(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("OldMemoInfo"))
            {
                MemoInfoDTO = parameters.GetValue<MemoInfoDTO>("OldMemoInfo");
            }
            else
            {
                MemoInfoDTO = new MemoInfoDTO();
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public EditMemoUCViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        // 对话框主机唯一标识
        private const string DialogHostName = "RootDialog";

        /// <summary>
        /// 待办事项信息
        /// </summary>
        public MemoInfoDTO MemoInfoDTO { get; set; } = new MemoInfoDTO();

        /// <summary>
        /// 确定方法
        /// </summary>
        private void Save()
        {
            if (string.IsNullOrEmpty(MemoInfoDTO.Title) || string.IsNullOrEmpty(MemoInfoDTO.Content))
            {
                MessageBox.Show("标题和内容均不可为空！");
                return;
            }

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters paras = new();
                paras.Add("EditMemoInfo", MemoInfoDTO);
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, paras));
            }
        }

        /// <summary>
        /// 取消方法
        /// </summary>
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
            }
        }
    }
}
