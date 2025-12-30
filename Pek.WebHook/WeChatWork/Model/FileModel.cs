namespace DH.WebHook.WeChatWork.Model;

/// <summary>文件消息模型</summary>
public class FileModel
{
    /// <summary>消息类型，固定为file</summary>
    public string msgtype { get; set; } = "file";

    /// <summary>文件内容</summary>
    public FileContent file { get; set; }
}

/// <summary>文件内容</summary>
public class FileContent
{
    /// <summary>文件id，通过文件上传接口获取</summary>
    public string media_id { get; set; }
}
