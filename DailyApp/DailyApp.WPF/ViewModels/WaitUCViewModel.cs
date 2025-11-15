using DailyApp.WPF.DTOs;
using DailyApp.WPF.HttpClients;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.ViewModels
{

    /// <summary>
    /// 待办事项视图模型
    /// </summary>
    internal class WaitUCViewModel : BindableBase
    {
        private readonly HttpRestClient HttpClient;
        /// <summary>
        /// 构造函数
        /// </summary>
        public WaitUCViewModel(HttpRestClient _HttpClient)
        {
            HttpClient = _HttpClient;

            QueryWaitList();

            // 显示添加待办命令
            ShowAddWaitCmm = new DelegateCommand(ShowAddWait);
            // 查询待办数据
            QueryWaitListCmm = new DelegateCommand(QueryWaitList);
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
        // 标题搜索
        public string SearchWaitTitle { get; set; }
        // 状态搜索
        public int SearchWaitIndex { get; set; }

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
    }
}
