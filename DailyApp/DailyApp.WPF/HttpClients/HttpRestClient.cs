using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.HttpClients
{
    /// <summary>
    /// 调用Api工具类
    /// </summary>
    internal class HttpRestClient
    {
        // 客户端
        private readonly RestClient Client;

        /// 请求地址（公共部分）
        private readonly string baseUrl = "http://localhost:10036/api/";

        public HttpRestClient()
        {
            Client = new RestClient();
        }

        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="apiRequest">请求数据</param>
        /// <returns>接收数据</returns>
        public ApiResponse Execute(ApiRequest apiRequest)
        {
            RestRequest request = new RestRequest(apiRequest.Method);// 请求方式
            request.AddHeader("Content-type", apiRequest.ContentType);// 内容类型

            if (apiRequest.Parameters != null)// 参数
            {                                            //json序列化：对象->json字符串
                request.AddParameter("param", JsonConvert.SerializeObject(apiRequest.Parameters),ParameterType.RequestBody);
            }

            Client.BaseUrl = new Uri(baseUrl + apiRequest.Route);
            var res = Client.Execute(request);// 请求
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {                      // json反序列号 json字符串->对象
                return JsonConvert.DeserializeObject<ApiResponse>(res.Content);
            }
            else
            {
                return new ApiResponse { ResultCode = -99, Msg = $"请求失败，状态码：{res.StatusCode}" };
            }
        }
    }
}
