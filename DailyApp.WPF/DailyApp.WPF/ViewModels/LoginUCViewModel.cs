using Prism.Commands;
using Prism.Mvvm;
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
    class LoginUCViewModel : BindableBase,IDialogAware// 实现对话框服务的接口
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
            // 登录命令
            LoginCmm = new DelegateCommand(Login);

            // 显示注册内容命令
            ShowRegInfoCmm = new DelegateCommand(ShowRegInfo);
            // 显示登录内容命令
            ShowLoginInfoCmm = new DelegateCommand(ShowLoginInfo);
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

        #region 显示内容的索引
        /// <summary>
        /// 显示内容的索引
        /// </summary>
        private int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set 
            { 
                _SelectedIndex = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 显示注册信息
        /// </summary>
        public DelegateCommand ShowRegInfoCmm { get; set; }
        private void ShowRegInfo()
        {
            SelectedIndex = 1;
        }

        /// <summary>
        /// 显示登录信息
        /// </summary>
        public DelegateCommand ShowLoginInfoCmm { get; set; }
        private void ShowLoginInfo()
        {
            SelectedIndex = 0;
        }
        #endregion
    }
}
