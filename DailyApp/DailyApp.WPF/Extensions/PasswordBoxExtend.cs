using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DailyApp.WPF.Extensions
{
    public class PasswordBoxExtend
    {

        /// <summary>
        /// PasswordBox扩展属性
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetPwd(DependencyObject obj)
        {
            return (string)obj.GetValue(PwdProperty);
        }

        public static void SetPwd(DependencyObject obj, string value)
        {
            obj.SetValue(PwdProperty, value);
        }

        // Using a DependencyProperty as the backing store for Pwd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PwdProperty =                                                     //默认值 属性改变时执行的方法
            DependencyProperty.RegisterAttached("Pwd", typeof(string), typeof(PasswordBoxExtend), new PropertyMetadata("",OnPwdChanged));

        /// <summary>
        /// 自定义附加属性发生变化，Password属性随之改变
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPwdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 获取Password新的值
            string newPwd = (string)e.NewValue;
            if (d is PasswordBox pwdBox && pwdBox.Password != newPwd)// 此属性是用在PasswordBox控件 && 值有变化
            {
                pwdBox.Password = newPwd;
            }
        }
    }

    /// <summary>
    /// Password行为 Password变化，自定义属性随之变化
    /// </summary>
    public class PasswordBoxBehavior : Behavior<PasswordBox>
    {
        /// <summary>
        /// 附加 注入事件
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += OnPasswordChanged;
        }

        /// <summary>
        /// Password变化，自定义附加属性随之变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pwdBox)
            {
                string password = PasswordBoxExtend.GetPwd(pwdBox);// 附加属性的值
                if (pwdBox.Password != password)
                {
                    PasswordBoxExtend.SetPwd(pwdBox, pwdBox.Password);
                }
            }
        }

        /// <summary>
        /// 销毁 移出事件
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= OnPasswordChanged;
        }
    }
}
