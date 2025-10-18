using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.ViewModels
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    class LoginUCViewModel : IDialogAware// 实现对话框服务的接口
    {
        public string Title { get; set; } = "我的日常";

        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// 登录命令
        /// </summary>
        public DelegateCommand LoginCmm { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginUCViewModel()
        {
            LoginCmm = new DelegateCommand(Login);
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        private void Login()
        {
            // 模拟登录成功
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        /// <summary>
        /// 是否能够关闭对话框
        /// </summary>
        /// <returns></returns>
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
