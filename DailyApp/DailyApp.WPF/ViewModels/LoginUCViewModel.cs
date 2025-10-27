using DailyApp.WPF.DTOs;
using DailyApp.WPF.HttpClients;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyApp.WPF.ViewModels
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    class LoginUCViewModel : BindableBase,IDialogAware// 实现对话框服务的接口
    {
        public string Title { get; set; } = "我的日常";

        public event Action<IDialogResult> RequestClose;

        private readonly HttpRestClient HttpRestClient;

        /// <summary>
        /// 登录命令
        /// </summary>
        public DelegateCommand LoginCmm { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginUCViewModel(HttpRestClient _HttpRestClient)
        {
            // 登录命令
            LoginCmm = new DelegateCommand(Login);

            // 显示注册内容命令
            ShowRegInfoCmm = new DelegateCommand(ShowRegInfo);
            // 显示登录内容命令
            ShowLoginInfoCmm = new DelegateCommand(ShowLoginInfo);
            // 注册命令
            RegCmm = new DelegateCommand(Reg);
            // 实例化注册信息
            AccountInfoDTO = new AccountInfoDTO();
            // 请求client
            HttpRestClient = _HttpRestClient;
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
            if (string.IsNullOrEmpty(AccountInfoDTO.Name) || string.IsNullOrEmpty(AccountInfoDTO.Account) || string.IsNullOrEmpty(AccountInfoDTO.Pwd) || string.IsNullOrEmpty(AccountInfoDTO.ConfirmPwd))
            {
                MessageBox.Show("注册信息不全！", "警告！");
                return;
            }
            else if (AccountInfoDTO.Pwd != AccountInfoDTO.ConfirmPwd)
            {
                MessageBox.Show("两次输入密码不一致！", "警告！");
                return;
            }
            else if (AccountInfoDTO.Pwd.Length < 4)
            {
                MessageBox.Show("密码长度小于4！", "警告！");
                return;
            }

            // 调用Api
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.POST;
            apiRequest.Route = "Account/Reg";
            apiRequest.Parameters = AccountInfoDTO;

            ApiResponse response = HttpRestClient.Execute(apiRequest);// 请求Api
            if (response.ResultCode == 1)
            {
                MessageBox.Show(response.Msg);
                SelectedIndex = 0;// 切换到登陆
            }
            else
            {
                MessageBox.Show(response.Msg);
            }
        }

        /// <summary>
        /// 注册信息
        /// </summary>
        private AccountInfoDTO _AccountInfoDTO;
        /// <summary>
        /// 注册信息
        /// </summary>
        public AccountInfoDTO AccountInfoDTO
        {
            get { return _AccountInfoDTO; }
            set
            {
                _AccountInfoDTO = value;
                RaisePropertyChanged();
            }
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
