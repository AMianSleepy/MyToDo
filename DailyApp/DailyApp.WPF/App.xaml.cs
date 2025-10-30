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
        }

        /// <summary>
        /// 初始化
        /// </summary>
        //protected override void OnInitialized()
        //{
        //    var dialog = Container.Resolve<IDialogService>();
        //    dialog.ShowDialog("LoginUC", callback =>
        //    {
        //        if (callback.Result != ButtonResult.OK)
        //        {
        //            Environment.Exit(0);
        //            return;
        //        }
        //        base.OnInitialized();
        //    });
        //}
    }
}
