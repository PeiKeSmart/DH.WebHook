namespace DH.WebHook.WeChatWork.Model;

/// <summary>图片消息模型</summary>
public class ImageModel
{
    /// <summary>消息类型，固定为image</summary>
    public string msgtype { get; set; } = "image";

    /// <summary>图片内容</summary>
    public ImageContent image { get; set; }
}

/// <summary>图片内容</summary>
public class ImageContent
{
    /// <summary>图片内容的base64编码</summary>
    public string base64 { get; set; }

    /// <summary>图片内容（base64编码前）的md5值</summary>
    public string md5 { get; set; }
}
