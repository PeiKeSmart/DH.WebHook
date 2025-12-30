namespace DH.WebHook.WeChatWork.Model;

/// <summary>图文消息模型</summary>
public class NewsModel
{
    /// <summary>消息类型，固定为news</summary>
    public string msgtype { get; set; } = "news";

    /// <summary>图文内容</summary>
    public NewsContent news { get; set; }
}

/// <summary>图文内容</summary>
public class NewsContent
{
    /// <summary>图文消息，一个图文消息支持1到8条图文</summary>
    public List<NewsArticle> articles { get; set; }
}

/// <summary>图文文章</summary>
public class NewsArticle
{
    /// <summary>标题，不超过128个字节，超过会自动截断</summary>
    public string title { get; set; }

    /// <summary>描述，不超过512个字节，超过会自动截断</summary>
    public string description { get; set; }

    /// <summary>点击后跳转的链接</summary>
    public string url { get; set; }

    /// <summary>图文消息的图片链接，支持JPG、PNG格式</summary>
    public string picurl { get; set; }
}
