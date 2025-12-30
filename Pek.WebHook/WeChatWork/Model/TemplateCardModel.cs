namespace DH.WebHook.WeChatWork.Model;

/// <summary>模板卡片消息模型</summary>
public class TemplateCardModel
{
    /// <summary>消息类型，固定为template_card</summary>
    public string msgtype { get; set; } = "template_card";

    /// <summary>模板卡片内容</summary>
    public TemplateCard template_card { get; set; }
}

/// <summary>模板卡片</summary>
public class TemplateCard
{
    /// <summary>模板卡片类型，text_notice(文本通知) 或 news_notice(图文展示)</summary>
    public string card_type { get; set; }

    /// <summary>卡片来源样式信息</summary>
    public CardSource source { get; set; }

    /// <summary>模版卡片的主要内容</summary>
    public CardMainTitle main_title { get; set; }

    /// <summary>关键数据样式</summary>
    public CardEmphasis emphasis_content { get; set; }

    /// <summary>引用文献样式</summary>
    public CardQuoteArea quote_area { get; set; }

    /// <summary>二级普通文本</summary>
    public string sub_title_text { get; set; }

    /// <summary>二级标题+文本列表</summary>
    public List<CardHorizontalContent> horizontal_content_list { get; set; }

    /// <summary>跳转指引样式的列表</summary>
    public List<CardJump> jump_list { get; set; }

    /// <summary>整体卡片的点击跳转事件</summary>
    public CardAction card_action { get; set; }

    // 以下为 news_notice 特有字段

    /// <summary>图片样式（图文展示模板卡片专用）</summary>
    public CardImage card_image { get; set; }

    /// <summary>左图右文样式（图文展示模板卡片专用）</summary>
    public CardImageTextArea image_text_area { get; set; }

    /// <summary>卡片二级垂直内容（图文展示模板卡片专用）</summary>
    public List<CardVerticalContent> vertical_content_list { get; set; }
}

/// <summary>卡片来源</summary>
public class CardSource
{
    /// <summary>来源图片的url</summary>
    public string icon_url { get; set; }

    /// <summary>来源图片的描述</summary>
    public string desc { get; set; }

    /// <summary>来源文字的颜色：0灰色, 1黑色, 2红色, 3绿色</summary>
    public int desc_color { get; set; }
}

/// <summary>卡片主标题</summary>
public class CardMainTitle
{
    /// <summary>一级标题</summary>
    public string title { get; set; }

    /// <summary>标题辅助信息</summary>
    public string desc { get; set; }
}

/// <summary>关键数据</summary>
public class CardEmphasis
{
    /// <summary>关键数据样式的数据内容</summary>
    public string title { get; set; }

    /// <summary>关键数据样式的数据描述内容</summary>
    public string desc { get; set; }
}

/// <summary>引用区域</summary>
public class CardQuoteArea
{
    /// <summary>引用文献样式区域点击事件：0无, 1跳转url, 2跳转小程序</summary>
    public int type { get; set; }

    /// <summary>点击跳转的url</summary>
    public string url { get; set; }

    /// <summary>点击跳转的小程序appid</summary>
    public string appid { get; set; }

    /// <summary>点击跳转的小程序pagepath</summary>
    public string pagepath { get; set; }

    /// <summary>引用文献样式的标题</summary>
    public string title { get; set; }

    /// <summary>引用文献样式的引用文案</summary>
    public string quote_text { get; set; }
}

/// <summary>水平内容</summary>
public class CardHorizontalContent
{
    /// <summary>类型：1是url, 2是文件附件, 3是成员详情</summary>
    public int type { get; set; }

    /// <summary>二级标题</summary>
    public string keyname { get; set; }

    /// <summary>二级文本</summary>
    public string value { get; set; }

    /// <summary>链接跳转的url</summary>
    public string url { get; set; }

    /// <summary>附件的media_id</summary>
    public string media_id { get; set; }

    /// <summary>成员详情的userid</summary>
    public string userid { get; set; }
}

/// <summary>跳转链接</summary>
public class CardJump
{
    /// <summary>跳转链接类型：0不是链接, 1跳转url, 2跳转小程序</summary>
    public int type { get; set; }

    /// <summary>跳转链接样式的文案内容</summary>
    public string title { get; set; }

    /// <summary>跳转链接的url</summary>
    public string url { get; set; }

    /// <summary>跳转链接的小程序appid</summary>
    public string appid { get; set; }

    /// <summary>跳转链接的小程序pagepath</summary>
    public string pagepath { get; set; }
}

/// <summary>卡片动作</summary>
public class CardAction
{
    /// <summary>卡片跳转类型：1跳转url, 2打开小程序</summary>
    public int type { get; set; }

    /// <summary>跳转事件的url</summary>
    public string url { get; set; }

    /// <summary>跳转事件的小程序appid</summary>
    public string appid { get; set; }

    /// <summary>跳转事件的小程序pagepath</summary>
    public string pagepath { get; set; }
}

/// <summary>卡片图片</summary>
public class CardImage
{
    /// <summary>图片的url</summary>
    public string url { get; set; }

    /// <summary>图片的宽高比</summary>
    public float aspect_ratio { get; set; }
}

/// <summary>左图右文区域</summary>
public class CardImageTextArea
{
    /// <summary>左图右文样式区域点击事件：0无, 1跳转url, 2跳转小程序</summary>
    public int type { get; set; }

    /// <summary>点击跳转的url</summary>
    public string url { get; set; }

    /// <summary>点击跳转的小程序appid</summary>
    public string appid { get; set; }

    /// <summary>点击跳转的小程序pagepath</summary>
    public string pagepath { get; set; }

    /// <summary>左图右文样式的标题</summary>
    public string title { get; set; }

    /// <summary>左图右文样式的描述</summary>
    public string desc { get; set; }

    /// <summary>左图右文样式的图片url</summary>
    public string image_url { get; set; }
}

/// <summary>垂直内容</summary>
public class CardVerticalContent
{
    /// <summary>卡片二级标题</summary>
    public string title { get; set; }

    /// <summary>二级普通文本</summary>
    public string desc { get; set; }
}
