namespace DH.WebHook.WeChatWork.Model;

/// <summary>Markdown V2消息模型</summary>
public class MarkdownV2Model
{
    /// <summary>消息类型，固定为markdown_v2</summary>
    public string msgtype { get; set; } = "markdown_v2";

    /// <summary>markdown_v2内容</summary>
    public MarkdownV2Content markdown_v2 { get; set; }
}

/// <summary>Markdown V2内容</summary>
public class MarkdownV2Content
{
    /// <summary>markdown_v2内容，最长不超过4096个字节，支持表格、列表等更丰富的语法</summary>
    public string content { get; set; }
}
