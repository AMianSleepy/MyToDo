using DailyApp.WPF.DTOs;
using DailyApp.WPF.HttpClients;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.ViewModels
{
    /// <summary>
    /// 备忘录视图模型
    /// </summary>
    internal class MemoUCViewModel : BindableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MemoUCViewModel(HttpRestClient _httpRestClient)
        {
            httpRestClient = _httpRestClient;

            QueryMemoList();

            // 显示添加备忘录命令
            ShowAddMemoCmm = new DelegateCommand(ShowAddMemo);
            // 查询备忘录数据
            QueryMemoListCmm = new DelegateCommand(QueryMemoList);
        }

        #region 
        private List<DailyApp.WPF.DTOs.MemoInfoDTO> _MemoList;
        /// <summary>
        /// 备忘录事项数据
        /// </summary>
        public List<DailyApp.WPF.DTOs.MemoInfoDTO> MemoList
        {
            get { return _MemoList; }
            set
            {
                _MemoList = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 查询标题
        /// </summary>
        public string SearchTitle { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        private readonly HttpRestClient httpRestClient;

        /// <summary>
        /// 查询备忘录事项数据
        /// </summary>
        private void QueryMemoList()
        {
            MemoList = new List<MemoInfoDTO>
            {
                
            };
            ApiRequest apiRequest = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"Memo/QueryMemo?title={SearchTitle}",
            };

            ApiResponse response = httpRestClient.Execute(apiRequest);

            if (response.ResultCode == 1)
            {
                MemoList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MemoInfoDTO>>(response.ResultData.ToString());
            }
            else
            {
                MemoList = new List<MemoInfoDTO>();
            }
        }

        /// <summary>
        /// 设置查询备忘录列表的命令
        /// </summary>
        public DelegateCommand QueryMemoListCmm { get; set; }
        #endregion

        #region 显示“添加备忘录”
        private bool _IsShowAddMemo;
        /// <summary>
        /// 是否显示“添加备忘录”
        /// </summary>
        public bool IsShowAddMemo
        {
            get { return _IsShowAddMemo; }
            set
            {
                _IsShowAddMemo = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 显示添加备忘录
        /// </summary>
        private void ShowAddMemo()
        {
            IsShowAddMemo = true;
        }

        /// <summary>
        /// 显示“添加备忘录”命令
        /// </summary>
        public DelegateCommand ShowAddMemoCmm { get; set; }
        #endregion
    }
}
