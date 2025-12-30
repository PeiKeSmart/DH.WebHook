using DH.WebHook.WeChatWork.Model;
using System.Text.Json;

namespace DH.WebHook;

/// <summary>企业微信机器人</summary>
public static class WeChatWorkRobot
{
    /// <summary>
    /// 发起请求
    /// </summary>
    /// <param name="url">webhook地址</param>
    /// <param name="data">数据</param>
    private static async Task<string> RequestAsync(string url, string data)
    {
        using var client = new HttpClient();
        var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, content);
        return await response.Content.ReadAsStringAsync();
    }

    #region 文本消息

    /// <summary>
    /// 发送文本消息（使用配置）
    /// </summary>
    /// <param name="content">文本内容</param>
    /// <param name="mentionedList">userid列表，@all表示提醒所有人</param>
    /// <param name="mentionedMobileList">手机号列表，@all表示提醒所有人</param>
    public static string SendText(string content, List<string> mentionedList, List<string> mentionedMobileList)
    {
        var url = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("未配置企业微信 Webhook URL");

        var model = new TextModel
        {
            text = new TextContent
            {
                content = content,
                mentioned_list = mentionedList,
                mentioned_mobile_list = mentionedMobileList
            }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(url, data).Result;
    }

    /// <summary>
    /// 发送文本消息（手动指定url）
    /// </summary>
    /// <param name="webhookUrl">webhook地址</param>
    /// <param name="content">文本内容</param>
    /// <param name="mentionedList">userid列表，@all表示提醒所有人</param>
    /// <param name="mentionedMobileList">手机号列表，@all表示提醒所有人</param>
    public static string SendText(string webhookUrl, string content, List<string> mentionedList, List<string> mentionedMobileList)
    {
        var model = new TextModel
        {
            text = new TextContent
            {
                content = content,
                mentioned_list = mentionedList,
                mentioned_mobile_list = mentionedMobileList
            }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(webhookUrl, data).Result;
    }

    #endregion

    #region Markdown消息

    /// <summary>
    /// 发送Markdown消息（使用配置）
    /// </summary>
    /// <param name="content">markdown内容</param>
    public static string SendMarkdown(string content)
    {
        var url = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("未配置企业微信 Webhook URL");

        var model = new MarkdownModel
        {
            markdown = new MarkdownContent { content = content }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(url, data).Result;
    }

    /// <summary>
    /// 发送Markdown消息（手动指定url）
    /// </summary>
    /// <param name="webhookUrl">webhook地址</param>
    /// <param name="content">markdown内容</param>
    public static string SendMarkdown(string webhookUrl, string content)
    {
        var model = new MarkdownModel
        {
            markdown = new MarkdownContent { content = content }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(webhookUrl, data).Result;
    }

    #endregion

    #region Markdown V2消息

    /// <summary>
    /// 发送Markdown V2消息（使用配置）
    /// </summary>
    /// <param name="content">markdown_v2内容，支持表格、列表等更丰富的语法</param>
    public static string SendMarkdownV2(string content)
    {
        var url = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("未配置企业微信 Webhook URL");

        var model = new MarkdownV2Model
        {
            markdown_v2 = new MarkdownV2Content { content = content }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(url, data).Result;
    }

    /// <summary>
    /// 发送Markdown V2消息（手动指定url）
    /// </summary>
    /// <param name="webhookUrl">webhook地址</param>
    /// <param name="content">markdown_v2内容，支持表格、列表等更丰富的语法</param>
    public static string SendMarkdownV2(string webhookUrl, string content)
    {
        var model = new MarkdownV2Model
        {
            markdown_v2 = new MarkdownV2Content { content = content }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(webhookUrl, data).Result;
    }

    #endregion

    #region 图片消息

    /// <summary>
    /// 发送图片消息（使用配置）
    /// </summary>
    /// <param name="base64">图片内容的base64编码</param>
    /// <param name="md5">图片内容的md5值</param>
    public static string SendImage(string base64, string md5)
    {
        var url = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("未配置企业微信 Webhook URL");

        var model = new ImageModel
        {
            image = new ImageContent { base64 = base64, md5 = md5 }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(url, data).Result;
    }

    /// <summary>
    /// 发送图片消息（手动指定url）
    /// </summary>
    /// <param name="webhookUrl">webhook地址</param>
    /// <param name="base64">图片内容的base64编码</param>
    /// <param name="md5">图片内容的md5值</param>
    public static string SendImage(string webhookUrl, string base64, string md5)
    {
        var model = new ImageModel
        {
            image = new ImageContent { base64 = base64, md5 = md5 }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(webhookUrl, data).Result;
    }

    #endregion

    #region 图文消息

    /// <summary>
    /// 发送图文消息（使用配置）
    /// </summary>
    /// <param name="articles">图文列表，最多8条</param>
    public static string SendNews(List<NewsArticle> articles)
    {
        var url = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("未配置企业微信 Webhook URL");

        var model = new NewsModel
        {
            news = new NewsContent { articles = articles }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(url, data).Result;
    }

    /// <summary>
    /// 发送图文消息（手动指定url）
    /// </summary>
    /// <param name="webhookUrl">webhook地址</param>
    /// <param name="articles">图文列表，最多8条</param>
    public static string SendNews(string webhookUrl, List<NewsArticle> articles)
    {
        var model = new NewsModel
        {
            news = new NewsContent { articles = articles }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(webhookUrl, data).Result;
    }

    #endregion

    #region 文件消息

    /// <summary>
    /// 发送文件消息（使用配置）
    /// </summary>
    /// <param name="mediaId">文件id，通过文件上传接口获取</param>
    public static string SendFile(string mediaId)
    {
        var url = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("未配置企业微信 Webhook URL");

        var model = new FileModel
        {
            file = new FileContent { media_id = mediaId }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(url, data).Result;
    }

    /// <summary>
    /// 发送文件消息（手动指定url）
    /// </summary>
    /// <param name="webhookUrl">webhook地址</param>
    /// <param name="mediaId">文件id，通过文件上传接口获取</param>
    public static string SendFile(string webhookUrl, string mediaId)
    {
        var model = new FileModel
        {
            file = new FileContent { media_id = mediaId }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(webhookUrl, data).Result;
    }

    #endregion

    #region 语音消息

    /// <summary>
    /// 发送语音消息（使用配置）
    /// </summary>
    /// <param name="mediaId">语音文件id，通过文件上传接口获取</param>
    public static string SendVoice(string mediaId)
    {
        var url = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("未配置企业微信 Webhook URL");

        var model = new VoiceModel
        {
            voice = new VoiceContent { media_id = mediaId }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(url, data).Result;
    }

    /// <summary>
    /// 发送语音消息（手动指定url）
    /// </summary>
    /// <param name="webhookUrl">webhook地址</param>
    /// <param name="mediaId">语音文件id，通过文件上传接口获取</param>
    public static string SendVoice(string webhookUrl, string mediaId)
    {
        var model = new VoiceModel
        {
            voice = new VoiceContent { media_id = mediaId }
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(webhookUrl, data).Result;
    }

    #endregion

    #region 模板卡片消息

    /// <summary>
    /// 发送模板卡片消息（使用配置）
    /// </summary>
    /// <param name="templateCard">模板卡片内容</param>
    public static string SendTemplateCard(TemplateCard templateCard)
    {
        var url = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("未配置企业微信 Webhook URL");

        var model = new TemplateCardModel
        {
            template_card = templateCard
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(url, data).Result;
    }

    /// <summary>
    /// 发送模板卡片消息（手动指定url）
    /// </summary>
    /// <param name="webhookUrl">webhook地址</param>
    /// <param name="templateCard">模板卡片内容</param>
    public static string SendTemplateCard(string webhookUrl, TemplateCard templateCard)
    {
        var model = new TemplateCardModel
        {
            template_card = templateCard
        };

        var data = JsonSerializer.Serialize(model);
        return RequestAsync(webhookUrl, data).Result;
    }

    #endregion

    #region 文件上传

    /// <summary>
    /// 上传文件（使用配置）
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="type">文件类型：file(普通文件) 或 voice(语音)</param>
    public static async Task<string> UploadFileAsync(string filePath, string type = "file")
    {
        var url = WebHookSetting.Current.WeChatWorkWebhookUrl;
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("未配置企业微信 Webhook URL");

        var uploadUrl = url.Replace("/send?", "/upload_media?") + $"&type={type}";
        
        using var client = new HttpClient();
        using var form = new MultipartFormDataContent();
        using var fileStream = File.OpenRead(filePath);
        using var fileContent = new StreamContent(fileStream);
        
        form.Add(fileContent, "media", Path.GetFileName(filePath));
        
        var response = await client.PostAsync(uploadUrl, form);
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// 上传文件（手动指定url）
    /// </summary>
    /// <param name="webhookUrl">webhook地址</param>
    /// <param name="filePath">文件路径</param>
    /// <param name="type">文件类型：file(普通文件) 或 voice(语音)</param>
    public static async Task<string> UploadFileAsync(string webhookUrl, string filePath, string type = "file")
    {
        var uploadUrl = webhookUrl.Replace("/send?", "/upload_media?") + $"&type={type}";
        
        using var client = new HttpClient();
        using var form = new MultipartFormDataContent();
        using var fileStream = File.OpenRead(filePath);
        using var fileContent = new StreamContent(fileStream);
        
        form.Add(fileContent, "media", Path.GetFileName(filePath));
        
        var response = await client.PostAsync(uploadUrl, form);
        return await response.Content.ReadAsStringAsync();
    }

    #endregion
}
