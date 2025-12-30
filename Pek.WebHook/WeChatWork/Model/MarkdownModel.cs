namespace DH.WebHook.WeChatWork.Model;

/// <summary>Markdown消息模型</summary>
public class MarkdownModel
{
    /// <summary>消息类型，固定为markdown</summary>
    public string msgtype { get; set; } = "markdown";

    /// <summary>markdown内容</summary>
    public MarkdownContent markdown { get; set; }
}

/// <summary>Markdown内容</summary>
public class MarkdownContent
{
    /// <summary>markdown内容，最长不超过4096个字节</summary>
    public string content { get; set; }
}
