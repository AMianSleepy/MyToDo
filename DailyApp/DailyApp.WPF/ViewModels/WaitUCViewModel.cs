using DailyApp.WPF.DTOs;
using DailyApp.WPF.HttpClients;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyApp.WPF.ViewModels
{

    /// <summary>
    /// 待办事项视图模型
    /// </summary>
    internal class WaitUCViewModel : BindableBase, INavigationAware
    {
        private readonly HttpRestClient HttpClient;
        /// <summary>
        /// 构造函数
        /// </summary>
        public WaitUCViewModel(HttpRestClient _HttpClient)
        {
            HttpClient = _HttpClient;

            // 显示添加待办命令
            ShowAddWaitCmm = new DelegateCommand(ShowAddWait);
            // 查询待办数据
            QueryWaitListCmm = new DelegateCommand(QueryWaitList);
            // 添加待办事项
            AddWaitCmm = new DelegateCommand(AddWait);
        }

        private List<DailyApp.WPF.DTOs.WaitInfoDTO> _WaitList;
        /// <summary>
        /// 待办事项数据
        /// </summary>
        public List<DailyApp.WPF.DTOs.WaitInfoDTO> WaitList
        {
            get { return _WaitList; }
            set
            {
                _WaitList = value;
                RaisePropertyChanged();
            }
        }

        #region 查询待办事项数据
        // 使用标题筛选
        public string SearchWaitTitle { get; set; }

        private int _SearchWaitIndex;
        /// <summary>
        /// 使用状态筛选
        /// </summary>
        public int SearchWaitIndex
        {
            get { return _SearchWaitIndex; }
            set 
            {
                _SearchWaitIndex = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand QueryWaitListCmm { get; set; }
        /// <summary>
        /// 查询待办事项数据
        /// </summary>
        private void QueryWaitList()
        {
            int? status = SearchWaitIndex - 1;
            if (status == -1)
            {
                status = null;
            }

            ApiRequest apiRequest = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"Wait/QueryWait?title={SearchWaitTitle}&status={status}",
            };

            ApiResponse apiResponse = HttpClient.Execute(apiRequest);

            if (apiResponse.ResultCode == 1)
            {
                WaitList = JsonConvert.DeserializeObject<List<WaitInfoDTO>>(apiResponse.ResultData.ToString());

                Visibility = (WaitList.Count > 0) ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                WaitList = new List<WaitInfoDTO>();
            }
        }
        #endregion

        #region 显示“添加待办”
        private bool _IsShowAddWait;
        /// <summary>
        /// 是否显示“添加待办”
        /// </summary>
        public bool IsShowAddWait
        {
            get { return _IsShowAddWait; }
            set
            { 
                _IsShowAddWait = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 显示添加待办
        /// </summary>
        private void ShowAddWait()
        {
            IsShowAddWait = true;
        }

        /// <summary>
        /// 显示“添加待办”命令
        /// </summary>
        public DelegateCommand ShowAddWaitCmm { get; set; }
        #endregion

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("SelectedIndex"))
            {
                SearchWaitIndex = navigationContext.Parameters.GetValue<int>("SelectedIndex");
            }
            else
            {
                SearchWaitIndex = 0;
            }
            // 查询待办事项数据
            QueryWaitList();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        #region 备忘录查询显示
        private Visibility _Visibility;
        /// <summary>
        /// 是否显示列表
        /// </summary>
        public Visibility Visibility
        {
            get { return _Visibility; }
            set
            {
                _Visibility = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 添加待办事项
        public WaitInfoDTO WaitInfoDTO { get; set; } = new WaitInfoDTO();
        public DelegateCommand AddWaitCmm { get; set; }
        private void AddWait()
        {
            if (WaitInfoDTO.Title == null || WaitInfoDTO.Content == null)
            {
                MessageBox.Show("标题和内容均不可为空！！！");
                return;
            }

            ApiRequest apiRequest = new()
            {
                Method = RestSharp.Method.POST,
                Route = $"Wait/AddWait",
                Parameters = WaitInfoDTO
            };
            ApiResponse response = HttpClient.Execute(apiRequest);
            if (response.ResultCode == 1)
            {
                QueryWaitList();
                IsShowAddWait = false;
            }
            else
            {
                MessageBox.Show($"添加失败：{response.Msg}");
            }
        }
        #endregion
    }
}
