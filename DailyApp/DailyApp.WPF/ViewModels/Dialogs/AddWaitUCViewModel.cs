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
    /// 添加待办事项视图模型
    /// </summary>
    internal class AddWaitUCViewModel : Service.IDialogHostAware
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
         
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public AddWaitUCViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }

        // 对话框主机唯一标识
        private const string DialogHostName = "RootDialog";

        /// <summary>
        /// 待办事项信息
        /// </summary>
        public WaitInfoDTO WaitInfoDTO { get; set; } = new WaitInfoDTO();

        /// <summary>
        /// 确定方法
        /// </summary>
        private void Save()
        {
            if (string.IsNullOrEmpty(WaitInfoDTO.Title) || string.IsNullOrEmpty(WaitInfoDTO.Content))
            {
                MessageBox.Show("标题和内容均不可为空！");
                return;
            }

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters paras = new();
                paras.Add("AddWaitInfo", WaitInfoDTO);
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
