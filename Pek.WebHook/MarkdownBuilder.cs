using System.Text;

namespace DH.WebHook;

/// <summary>
/// Markdown 构建器
/// </summary>
/// <remarks>
/// 使用链式调用方式构建 Markdown 内容，支持钉钉和企业微信机器人。
/// 注意：颜色标签（AppendInfoMsg/AppendCommentMsg/AppendWarningMsg）仅企业微信支持。
/// </remarks>
public class MarkdownBuilder
{
    /// <summary>
    /// 内容拼接器
    /// </summary>
    protected StringBuilder Builder { get; set; } = new StringBuilder();

    /// <summary>
    /// 换行
    /// </summary>
    public MarkdownBuilder NewLine()
    {
        Builder.Append("\n");
        return this;
    }

    /// <summary>
    /// 自定义内容
    /// </summary>
    /// <param name="normal">内容</param>
    public MarkdownBuilder AppendNormal(string normal)
    {
        Builder.Append(normal);
        return this;
    }

    /// <summary>
    /// 追加1-6级标题
    /// </summary>
    /// <param name="text">标题文本</param>
    /// <param name="level">标题等级</param>
    public MarkdownBuilder AppendLevelTitle(string text, int level)
    {
        if (!(level > 0 && level < 7))
            throw new ArgumentOutOfRangeException(nameof(level), "超出标题级别[1,6]");
        for (var i = 0; i < level; i++)
            Builder.Append("#");
        Builder.Append($" {text}");
        return this;
    }

    /// <summary>
    /// 加粗
    /// </summary>
    /// <param name="text">加粗文本</param>
    public MarkdownBuilder AppendBold(string text)
    {
        Builder.Append($" **{text}** ");
        return this;
    }

    /// <summary>
    /// 链接
    /// </summary>
    /// <param name="url">链接</param>
    /// <param name="text">显示文本</param>
    public MarkdownBuilder AppendHref(string url, string text)
    {
        Builder.Append($"[{text}]({url})");
        return this;
    }

    /// <summary>
    /// 行内代码
    /// </summary>
    /// <param name="text">代码文本</param>
    public MarkdownBuilder AppendLineCode(string text)
    {
        Builder.Append($"`{text}`");
        return this;
    }

    /// <summary>
    /// 引用
    /// </summary>
    /// <param name="text">引用文本</param>
    public MarkdownBuilder AppendReference(string text)
    {
        Builder.Append($"> {text}");
        return this;
    }

    /// <summary>
    /// 绿色文字（仅企业微信支持）
    /// </summary>
    /// <param name="text">文本</param>
    public MarkdownBuilder AppendInfoMsg(string text)
    {
        Builder.Append($"<font color=\"info\">{text}</font>");
        return this;
    }

    /// <summary>
    /// 灰色文字（仅企业微信支持）
    /// </summary>
    /// <param name="text">文字</param>
    public MarkdownBuilder AppendCommentMsg(string text)
    {
        Builder.Append($"<font color=\"comment\">{text}</font>");
        return this;
    }

    /// <summary>
    /// 橙红色文字（仅企业微信支持）
    /// </summary>
    /// <param name="text">文字</param>
    public MarkdownBuilder AppendWarningMsg(string text)
    {
        Builder.Append($"<font color=\"warning\">{text}</font>");
        return this;
    }

    /// <summary>
    /// 输出字符串
    /// </summary>
    public override string ToString() => Builder.ToString();
}
