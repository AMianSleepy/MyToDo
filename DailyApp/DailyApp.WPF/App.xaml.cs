using DailyApp.WPF.HttpClients;
using DailyApp.WPF.ViewModels;
using DailyApp.WPF.Views;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System.Configuration;
using System.Data;
using System.Windows;

namespace DailyApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <summary>
        /// 设置启动页
        /// </summary>
        /// <returns></returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWin>();
        }

        /// <summary>
        /// 设置依赖注入
        /// </summary>
        /// <param name="containerRegistry"></param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 登录
            containerRegistry.RegisterDialog<LoginUC, LoginUCViewModel>();

            // 请求
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));

            // 导航页
            containerRegistry.RegisterForNavigation<HomeUC, HomeUCViewModel>();// 首页
            containerRegistry.RegisterForNavigation<WaitUC, WaitUCViewModel>();// 待办
            containerRegistry.RegisterForNavigation<MemoUC, MemoUCViewModel>();// 备忘录
            containerRegistry.RegisterForNavigation<SettingsUC, SettingsUCViewModel>();// 设置

            // 设置 - 左侧导航
            containerRegistry.RegisterForNavigation<PersonalUC, PersonalUCViewModel>();// 个性化页面
            containerRegistry.RegisterForNavigation<AboutUsUC>();// 关于我们页面
            containerRegistry.RegisterForNavigation<SysSetUC>();// 系统设置页面
        }

        /// <summary>
        /// 应用程序初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            // 解析并获取对话框服务实例
            var dialog = Container.Resolve<IDialogService>();

            // 显示登录对话框
            dialog.ShowDialog("LoginUC", callback =>
            {
                // 如果用户未成功登录（点击取消或关闭对话框）
                if (callback.Result != ButtonResult.OK)
                {
                    // 退出应用程序
                    Environment.Exit(0);
                    return;
                }

                // 获取主窗口的 ViewModel
                var mainVM = Current.MainWindow.DataContext as MainWinViewModel;
                if (mainVM != null)
                {
                    // 检查对话框返回的参数中是否包含登录名
                    if (callback.Parameters.ContainsKey("LoginName"))
                    {
                        // 获取登录名
                        string name = callback.Parameters.GetValue<string>("LoginName");

                        // 设置默认导航页面
                        mainVM.SetDefaultNav(name);
                    }
                }

                // 调用基类的初始化方法
                base.OnInitialized();
            });
        }
    }
}
