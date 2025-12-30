namespace DH.WebHook.WeChatWork.Model;

/// <summary>语音消息模型</summary>
public class VoiceModel
{
    /// <summary>消息类型，固定为voice</summary>
    public string msgtype { get; set; } = "voice";

    /// <summary>语音内容</summary>
    public VoiceContent voice { get; set; }
}

/// <summary>语音内容</summary>
public class VoiceContent
{
    /// <summary>语音文件id，通过文件上传接口获取</summary>
    public string media_id { get; set; }
}
