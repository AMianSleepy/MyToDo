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
            // 注册命令
            RegCmm = new DelegateCommand(Reg);
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        private void Login()
        {
            // 此处测试传入的Password
            string testInput = Pwd;
            // 模拟登录成功
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        #region 注册
        // 注册命令
        public DelegateCommand RegCmm { get; set; }

        private void Reg()
        {

        }
        #endregion

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
        /// <summary>
        /// 显示内容的索引
        /// </summary>
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
        /// <summary>
        /// 显示注册信息
        /// </summary>
        private void ShowRegInfo()
        {
            SelectedIndex = 1;
        }

        /// <summary>
        /// 显示登录信息
        /// </summary>
        public DelegateCommand ShowLoginInfoCmm { get; set; }
        /// <summary>
        /// 显示登录信息
        /// </summary>
        private void ShowLoginInfo()
        {
            SelectedIndex = 0;
        }
        #endregion

        #region 密码
        /// <summary>
        /// 密码
        /// </summary>
        private string _Pwd;
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        {
            get { return _Pwd; }
            set { _Pwd = value; }
        }
        #endregion
    }
}
