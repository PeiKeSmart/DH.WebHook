using DH.WebHook.Models;
using Flurl;
using Flurl.Http;

namespace DH.WebHook;

/// <summary>企业微信机器人</summary>
public static class WeChatWorkRobot
{
    private const string BaseUrl = "https://qyapi.weixin.qq.com";

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <param name="appId">企业微信机器人密钥</param>
    /// <param name="request">请求</param>
    public static async Task<WeChatWorkRobotResponse> SendAsync(string appId, IDictionary<string, object> request)
    {
        return await BaseUrl
            .AppendPathSegment("cgi-bin/webhook/send")
            .SetQueryParam("key", appId)
            .PostJsonAsync(request)
            .ReceiveJson<WeChatWorkRobotResponse>();
    }

    /// <summary>
    /// 发送请求
    /// </summary>
    /// <typeparam name="TMessageRequest">消息请求类型</typeparam>
    /// <param name="appId">企业微信机器人密钥</param>
    /// <param name="request">请求</param>
    public static async Task<WeChatWorkRobotResponse> SendAsync<TMessageRequest>(string appId, TMessageRequest request)
        where TMessageRequest : WeChatWorkRobotRequest
    {
        return await BaseUrl
            .AppendPathSegment("cgi-bin/webhook/send")
            .SetQueryParam("key", appId)
            .PostJsonAsync(request.ToRequestBody())
            .ReceiveJson<WeChatWorkRobotResponse>();
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="appId">企业微信机器人密钥</param>
    /// <param name="file">文件路径</param>
    public static async Task<WeChatWorkRobotUploadResponse> UploadAsync(string appId, string file)
    {
        return await BaseUrl
            .AppendPathSegment("cgi-bin/webhook/upload_media")
            .SetQueryParam("key", appId)
            .SetQueryParam("type", Const.File)
            .PostMultipartAsync(mp => mp.AddFile("media", file))
            .ReceiveJson<WeChatWorkRobotUploadResponse>();
    }
}
