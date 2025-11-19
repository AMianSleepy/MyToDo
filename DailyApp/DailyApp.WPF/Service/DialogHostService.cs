using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyApp.WPF.Service
{
    internal class DialogHostService : DialogService
    {
        private readonly IContainerExtension containerExtension;

        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }

        public async Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "RootDialog")
        {
            if (parameters == null)
                parameters = new DialogParameters();

            //从容器当中去除弹出窗口的实例
            var content = containerExtension.Resolve<object>(name);

            //验证实例的有效性
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog' s content must be a FrameworkElement");

            if (dialogContent is FrameworkElement view && view.DataContext is null && Prism.Mvvm.ViewModelLocator.GetAutoWireViewModel(view) is null)
                Prism.Mvvm.ViewModelLocator.SetAutoWireViewModel(view, true);

            if (!(dialogContent.DataContext is IDialogHostAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            MaterialDesignThemes.Wpf.DialogOpenedEventHandler eyentHandler = (sender, eventArgs) =>
            {
                if (viewModel is IDialogHostAware aware)
                    aware.OnDialogOpening(parameters);

                eventArgs.Session.UpdateContent(content);
            };
            return (IDialogResult)await DialogHost.Show(dialogContent, dialogHostName, eyentHandler);
        }
    }
}
