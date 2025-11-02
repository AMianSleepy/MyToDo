using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace DailyApp.WPF.ViewModels
{
    class PersonalUCViewModel : BindableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PersonalUCViewModel()
        {
            ChangeHueCommand = new DelegateCommand<object>(ChangeHue);

            // 初始化时获取当前主题状态
            var paletteHelper = new PaletteHelper();
            var currentTheme = paletteHelper.GetTheme();
            _isDarkTheme = currentTheme.GetBaseTheme() == BaseTheme.Dark;
            RaisePropertyChanged(nameof(IsDarkTheme)); // 通知UI更新
        }

        #region 主题背景切换
        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            if (modificationAction == null) return;

            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }

        #endregion

        #region 顶部背景颜色
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        public DelegateCommand<object> ChangeHueCommand { get; }

        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        private void ChangeHue(object? obj)
        {
            if (obj is not Color color) return;

            ITheme theme = paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(color.Lighten());
            theme.PrimaryMid = new ColorPair(color);
            theme.PrimaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }
        #endregion
    }
}
