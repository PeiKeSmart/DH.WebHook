namespace DH.WebHook.WeChatWork.Model;

/// <summary>文本消息模型</summary>
public class TextModel
{
    /// <summary>消息类型，固定为text</summary>
    public string msgtype { get; set; } = "text";

    /// <summary>文本内容</summary>
    public TextContent text { get; set; }
}

/// <summary>文本内容</summary>
public class TextContent
{
    /// <summary>文本内容，最长不超过2048个字节</summary>
    public string content { get; set; }

    /// <summary>userid的列表，提醒群中的指定成员(@某个成员)，@all表示提醒所有人</summary>
    public List<string> mentioned_list { get; set; }

    /// <summary>手机号列表，提醒手机号对应的群成员(@某个成员)，@all表示提醒所有人</summary>
    public List<string> mentioned_mobile_list { get; set; }
}
