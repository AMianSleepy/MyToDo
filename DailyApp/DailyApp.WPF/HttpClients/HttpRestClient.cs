using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DailyApp.WPF.HttpClients
{
    /// <summary>
    /// 调用 Api 的简单封装类
    /// 作用：根据传入的 ApiRequest 生成并发送 HTTP 请求，返回统一的 ApiResponse
    /// </summary>
    internal class HttpRestClient
    {
        // RestSharp 客户端实例，复用连接
        private readonly RestClient Client;

        /// <summary>
        /// 基础地址（统一的前缀，后面拼接 Route）
        /// 例：最终地址 = baseUrl + apiRequest.Route
        /// </summary>
        private readonly string baseUrl = "http://localhost:10036/api/";

        public HttpRestClient()
        {
            // 使用默认配置初始化 RestClient
            Client = new RestClient();
        }

        /// <summary>
        /// 执行请求
        /// </summary>
        /// <param name="apiRequest">请求数据（包含：Route、Method、Parameters、ContentType）</param>
        /// <returns>统一封装的响应对象 ApiResponse</returns>
        public ApiResponse Execute(ApiRequest apiRequest)
        {
            // 用请求方法构造 RestRequest（注意：这里没有直接写 URL，URL 后面通过 BaseUrl 设置）
            RestRequest request = new RestRequest(apiRequest.Method);

            // 判断当前请求方法是否需要携带请求体（只有 POST / PUT / PATCH 常规携带 Body）
            bool hasBody = apiRequest.Method == Method.POST || 
                           apiRequest.Method == Method.PUT  || 
                           apiRequest.Method == Method.PATCH;

            // 如果是需要请求体的方法并且有参数，才序列化为 JSON 加入请求体
            if (hasBody && apiRequest.Parameters != null)
            {
                // 添加请求头：声明本次发送的是 JSON 数据
                request.AddHeader("Content-Type", "application/json");
                // 自动将对象序列化为 JSON 并作为请求体发送（比 AddParameter 手动拼更规范）
                request.AddJsonBody(apiRequest.Parameters);
            }
            // 如果是 GET 且有参数，此实现当前未处理（参数应拼到查询字符串），可在后续扩展

            // 拼接完整地址（BaseUrl + Route），并赋值给客户端的 BaseUrl
            Client.BaseUrl = new Uri(baseUrl + apiRequest.Route);

            // 发起请求（同步执行；如果需要异步可用 await Client.ExecuteAsync(request)）
            var res = Client.Execute(request);

            // 如果状态码是 200(OK) 或 201(Created)，认为成功，反序列化为 ApiResponse 返回
            if (res.StatusCode == HttpStatusCode.OK || res.StatusCode == HttpStatusCode.Created)
            {
                // 反序列化后返回；如果内容为空或格式不对可能抛异常（调用方自行保证服务端格式）
                return JsonConvert.DeserializeObject<ApiResponse>(res.Content);
            }
            else
            {
                // 非成功状态统一包装错误信息，ResultCode 使用 -99 表示请求失败
                return new ApiResponse 
                { 
                    ResultCode = -99, 
                    Msg = $"请求失败，状态码：{res.StatusCode}" 
                };
            }
        }
    }
}
