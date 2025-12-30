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

    #region 便捷方法

    /// <summary>
    /// 发送文本消息（使用配置）
    /// </summary>
    /// <param name="content">文本内容，最长不超过2048个字节</param>
    /// <param name="mentionedList">userid列表，提醒指定成员(@某个成员)，@all表示提醒所有人</param>
    /// <param name="mentionedMobileList">手机号列表，提醒手机号对应的群成员(@某个成员)，@all表示提醒所有人</param>
    public static async Task<WeChatWorkRobotResponse> SendTextAsync(string content, List<string> mentionedList = null, List<string> mentionedMobileList = null)
    {
        var request = new TextMessageRequest
        {
            Content = content,
            Users = mentionedList,
            Phones = mentionedMobileList
        };
        return await SendAsync(request);
    }

    /// <summary>
    /// 发送文本消息（手动指定 appId）
    /// </summary>
    /// <param name="appId">企业微信机器人密钥</param>
    /// <param name="content">文本内容，最长不超过2048个字节</param>
    /// <param name="mentionedList">userid列表，提醒指定成员(@某个成员)，@all表示提醒所有人</param>
    /// <param name="mentionedMobileList">手机号列表，提醒手机号对应的群成员(@某个成员)，@all表示提醒所有人</param>
    public static async Task<WeChatWorkRobotResponse> SendTextAsync(string appId, string content, List<string> mentionedList = null, List<string> mentionedMobileList = null)
    {
        var request = new TextMessageRequest
        {
            Content = content,
            Users = mentionedList,
            Phones = mentionedMobileList
        };
        return await SendAsync(appId, request);
    }

    /// <summary>
    /// 发送Markdown消息（使用配置）
    /// </summary>
    /// <param name="content">markdown内容，最长不超过4096个字节</param>
    public static async Task<WeChatWorkRobotResponse> SendMarkdownAsync(string content)
    {
        var request = new MarkdownMessageRequest { Content = content };
        return await SendAsync(request);
    }

    /// <summary>
    /// 发送Markdown消息（手动指定 appId）
    /// </summary>
    /// <param name="appId">企业微信机器人密钥</param>
    /// <param name="content">markdown内容，最长不超过4096个字节</param>
    public static async Task<WeChatWorkRobotResponse> SendMarkdownAsync(string appId, string content)
    {
        var request = new MarkdownMessageRequest { Content = content };
        return await SendAsync(appId, request);
    }

    #endregion
}
