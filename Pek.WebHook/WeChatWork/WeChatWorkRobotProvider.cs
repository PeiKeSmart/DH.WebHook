using DH.WebHook.Models;

using Flurl;
using Flurl.Http;

using NewLife;

namespace DH.WebHook;

/// <summary>企业微信机器人</summary>
public static class WeChatWorkRobot
{
    private const string BaseUrl = "https://qyapi.weixin.qq.com";

    /// <summary>
    /// 发送请求（使用配置文件中的 Webhook URL）
    /// </summary>
    /// <param name="request">请求</param>
    public static async Task<WeChatWorkRobotResponse> SendAsync(IDictionary<string, object> request)
    {
        var webhookUrl = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (webhookUrl.IsNullOrWhiteSpace())
            throw new ArgumentException("未配置企业微信 Webhook URL，请在配置文件中设置 WeChatWorkWebhookUrl");

        return await webhookUrl
            .PostJsonAsync(request)
            .ReceiveJson<WeChatWorkRobotResponse>();
    }

    /// <summary>
    /// 发送请求（使用配置文件中的 Webhook URL）
    /// </summary>
    /// <typeparam name="TMessageRequest">消息请求类型</typeparam>
    /// <param name="request">请求</param>
    public static async Task<WeChatWorkRobotResponse> SendAsync<TMessageRequest>(TMessageRequest request)
        where TMessageRequest : WeChatWorkRobotRequest
    {
        var webhookUrl = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (webhookUrl.IsNullOrWhiteSpace())
            throw new ArgumentException("未配置企业微信 Webhook URL，请在配置文件中设置 WeChatWorkWebhookUrl");

        return await webhookUrl
            .PostJsonAsync(request.ToRequestBody())
            .ReceiveJson<WeChatWorkRobotResponse>();
    }

    /// <summary>
    /// 发送请求（手动指定 appId）
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
    /// 发送请求（手动指定 appId）
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
    /// 上传文件（使用配置文件中的 Webhook URL）
    /// </summary>
    /// <param name="file">文件路径</param>
    public static async Task<WeChatWorkRobotUploadResponse> UploadAsync(string file)
    {
        var webhookUrl = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (webhookUrl.IsNullOrWhiteSpace())
            throw new ArgumentException("未配置企业微信 Webhook URL，请在配置文件中设置 WeChatWorkWebhookUrl");

        var uploadUrl = webhookUrl.Replace("/send?", "/upload_media?");
        
        return await uploadUrl
            .SetQueryParam("type", Const.File)
            .PostMultipartAsync(mp => mp.AddFile("media", file))
            .ReceiveJson<WeChatWorkRobotUploadResponse>();
    }

    /// <summary>
    /// 上传文件（手动指定 appId）
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
