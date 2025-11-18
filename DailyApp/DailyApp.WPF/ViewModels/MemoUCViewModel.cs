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
using System.Windows;

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
            // 添加备忘录
            AddMemoCmm = new DelegateCommand(AddMemo);
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
            MemoList = new List<MemoInfoDTO>();
            ApiRequest apiRequest = new()
            {
                Method = RestSharp.Method.GET,
                Route = $"Memo/QueryMemo?title={SearchTitle}",
            };

            ApiResponse response = httpRestClient.Execute(apiRequest);

            if (response.ResultCode == 1)
            {
                MemoList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MemoInfoDTO>>(response.ResultData.ToString());

                Visibility = (MemoList.Count > 0) ? Visibility.Hidden : Visibility.Visible; 
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

        #region 添加备忘录
        public MemoInfoDTO MemoInfoDTO { get; set; } = new MemoInfoDTO();
        public DelegateCommand AddMemoCmm { get; set; }
        private void AddMemo()
        {
            if (MemoInfoDTO.Title == null || MemoInfoDTO.Content == null)
            {
                MessageBox.Show("标题和内容均不可为空！！！");
                return;
            }

            ApiRequest apiRequest = new()
            {
                Method = RestSharp.Method.POST,
                Route = $"Memo/AddMemo",
                Parameters = MemoInfoDTO
            };
            ApiResponse response = httpRestClient.Execute(apiRequest);
            if (response.ResultCode ==1)
            {
                QueryMemoList();
                IsShowAddMemo = false;
            }
            else
            {
                MessageBox.Show($"添加失败：{response.Msg}");
            }
        }
        #endregion
    }
}
