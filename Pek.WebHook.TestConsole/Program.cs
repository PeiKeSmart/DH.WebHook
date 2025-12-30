using DH.WebHook;
using DH.WebHook.WeChatWork.Model;

using NewLife.Log;

namespace Pek.WebHook.TestConsole;

class Program
{
    static async Task Main(string[] args)
    {
        XTrace.UseConsole();

        XTrace.WriteLine("=== 企业微信机器人推送测试工具 ===\n");

        // 输入 Webhook URL
        XTrace.WriteLine("请输入企业微信机器人完整 Webhook 地址: ");
        var webhookUrl = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(webhookUrl))
        {
            XTrace.WriteLine("❌ Webhook 地址不能为空！");
            return;
        }

        XTrace.WriteLine($"\n使用地址: {webhookUrl}\n");

        while (true)
        {
            XTrace.WriteLine("\n========== 消息类型菜单 ==========");
            XTrace.WriteLine("1. 文本消息 (Text)");
            XTrace.WriteLine("2. Markdown 消息");
            XTrace.WriteLine("3. Markdown V2 消息");
            XTrace.WriteLine("4. 图片消息 (Image)");
            XTrace.WriteLine("5. 图文消息 (News)");
            XTrace.WriteLine("6. 文件消息 (File)");
            XTrace.WriteLine("7. 语音消息 (Voice)");
            XTrace.WriteLine("8. 模板卡片消息");
            XTrace.WriteLine("9. 使用 MarkdownBuilder 构建消息");
            XTrace.WriteLine("0. 退出");
            XTrace.WriteLine("==================================");
            XTrace.WriteLine("\n请选择消息类型 (0-9): ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        await TestTextMessage(webhookUrl);
                        break;
                    case "2":
                        await TestMarkdownMessage(webhookUrl);
                        break;
                    case "3":
                        await TestMarkdownV2Message(webhookUrl);
                        break;
                    case "4":
                        await TestImageMessage(webhookUrl);
                        break;
                    case "5":
                        await TestNewsMessage(webhookUrl);
                        break;
                    case "6":
                        await TestFileMessage(webhookUrl);
                        break;
                    case "7":
                        await TestVoiceMessage(webhookUrl);
                        break;
                    case "8":
                        await TestTemplateCardMessage(webhookUrl);
                        break;
                    case "9":
                        await TestMarkdownBuilder(webhookUrl);
                        break;
                    case "0":
                        XTrace.WriteLine("\n退出程序...");
                        return;
                    default:
                        XTrace.WriteLine("\n❌ 无效选择，请重新输入！");
                        break;
                }
            }
            catch (Exception ex)
            {
                XTrace.WriteLine($"\n❌ 发送失败: {ex.Message}");
                XTrace.WriteLine($"详细信息: {ex}");
            }
        }
    }

    static async Task TestTextMessage(string webhookUrl)
    {
        XTrace.WriteLine("\n--- 测试文本消息 ---");
        XTrace.WriteLine("输入文本内容: ");
        var content = Console.ReadLine() ?? "测试文本消息";

        XTrace.WriteLine("是否 @所有人？(y/n): ");
        var atAll = Console.ReadLine()?.ToLower() == "y";

        var mentionedList = new List<string>();
        if (!atAll)
        {
            XTrace.WriteLine("输入要 @ 的用户ID（多个用逗号分隔，留空跳过）: ");
            var userIds = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(userIds))
                mentionedList = userIds.Split(',').Select(s => s.Trim()).ToList();
        }
        else
        {
            mentionedList.Add("@all");
        }

        var result = WeChatWorkRobot.SendText(webhookUrl, content, mentionedList, null);
        XTrace.WriteLine($"\n✅ 发送结果: {result}");
    }

    static async Task TestMarkdownMessage(string webhookUrl)
    {
        XTrace.WriteLine("\n--- 测试 Markdown 消息 ---");
        XTrace.WriteLine("示例格式:");
        XTrace.WriteLine("# 标题");
        XTrace.WriteLine("**加粗** *斜体*");
        XTrace.WriteLine("[链接文字](http://example.com)");
        XTrace.WriteLine("> 引用");
        XTrace.WriteLine("\n请输入 Markdown 内容（输入 END 结束）:");

        var lines = new List<string>();
        while (true)
        {
            var line = Console.ReadLine();
            if (line?.ToUpper() == "END") break;
            lines.Add(line ?? "");
        }

        var markdown = string.Join("\n", lines);
        if (string.IsNullOrWhiteSpace(markdown))
            markdown = "# 测试标题\n**这是加粗文本**";

        var result = WeChatWorkRobot.SendMarkdown(webhookUrl, markdown);
        XTrace.WriteLine($"\n✅ 发送结果: {result}");
    }

    static async Task TestMarkdownV2Message(string webhookUrl)
    {
        XTrace.WriteLine("\n--- 测试 Markdown V2 消息（支持表格、列表等）---");
        var markdown = @"# 测试 Markdown V2
## 功能特性
- 支持无序列表
- 支持有序列表
- 支持表格

## 表格示例
| 姓名 | 年龄 | 城市 |
| --- | --- | --- |
| 张三 | 25 | 北京 |
| 李四 | 30 | 上海 |

**加粗文本** *斜体文本* `行内代码`
> 这是引用内容";

        XTrace.WriteLine($"\n将发送以下内容:\n{markdown}");
        XTrace.WriteLine("\n确认发送？(y/n): ");
        if (Console.ReadLine()?.ToLower() != "y")
        {
            XTrace.WriteLine("已取消发送");
            return;
        }

        var result = WeChatWorkRobot.SendMarkdownV2(webhookUrl, markdown);
        XTrace.WriteLine($"\n✅ 发送结果: {result}");
    }

    static async Task TestImageMessage(string webhookUrl)
    {
        XTrace.WriteLine("\n--- 测试图片消息 ---");
        XTrace.WriteLine("⚠️ 需要提供图片的 Base64 编码和 MD5 值");

        XTrace.WriteLine("输入图片 Base64 编码（或输入文件路径自动转换）: ");
        var input = Console.ReadLine();

        string base64, md5;

        if (File.Exists(input))
        {
            // 从文件读取并转换
            var bytes = File.ReadAllBytes(input);
            base64 = Convert.ToBase64String(bytes);
            md5 = BitConverter.ToString(System.Security.Cryptography.MD5.HashData(bytes))
                .Replace("-", "").ToLower();
            XTrace.WriteLine($"✅ 已读取文件，MD5: {md5}");
        }
        else
        {
            base64 = input ?? "";
            XTrace.WriteLine("输入图片 MD5 值: ");
            md5 = Console.ReadLine() ?? "";
        }

        var result = WeChatWorkRobot.SendImage(webhookUrl, base64, md5);
        XTrace.WriteLine($"\n✅ 发送结果: {result}");
    }

    static async Task TestNewsMessage(string webhookUrl)
    {
        XTrace.WriteLine("\n--- 测试图文消息 ---");
        var articles = new List<NewsArticle>();

        XTrace.WriteLine("需要添加几条图文？(1-8): ");
        if (!int.TryParse(Console.ReadLine(), out var count) || count < 1 || count > 8)
            count = 1;

        for (var i = 0; i < count; i++)
        {
            XTrace.WriteLine($"\n第 {i + 1} 条图文:");
            XTrace.WriteLine("  标题: ");
            var title = Console.ReadLine() ?? $"测试图文{i + 1}";

            XTrace.WriteLine("  描述: ");
            var description = Console.ReadLine() ?? "这是描述内容";

            XTrace.WriteLine("  跳转链接: ");
            var url = Console.ReadLine() ?? "https://example.com";

            XTrace.WriteLine("  图片链接: ");
            var picurl = Console.ReadLine() ?? "https://example.com/image.jpg";

            articles.Add(new NewsArticle
            {
                title = title,
                description = description,
                url = url,
                picurl = picurl
            });
        }

        var result = WeChatWorkRobot.SendNews(webhookUrl, articles);
        XTrace.WriteLine($"\n✅ 发送结果: {result}");
    }

    static async Task TestFileMessage(string webhookUrl)
    {
        XTrace.WriteLine("\n--- 测试文件消息 ---");
        XTrace.WriteLine("⚠️ 需要先通过上传接口获取 media_id");
        XTrace.WriteLine("输入 media_id: ");
        var mediaId = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(mediaId))
        {
            XTrace.WriteLine("❌ media_id 不能为空！");
            return;
        }

        var result = WeChatWorkRobot.SendFile(webhookUrl, mediaId);
        XTrace.WriteLine($"\n✅ 发送结果: {result}");
    }

    static async Task TestVoiceMessage(string webhookUrl)
    {
        XTrace.WriteLine("\n--- 测试语音消息 ---");
        XTrace.WriteLine("⚠️ 需要先通过上传接口获取 media_id");
        XTrace.WriteLine("输入 media_id: ");
        var mediaId = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(mediaId))
        {
            XTrace.WriteLine("❌ media_id 不能为空！");
            return;
        }

        var result = WeChatWorkRobot.SendVoice(webhookUrl, mediaId);
        XTrace.WriteLine($"\n✅ 发送结果: {result}");
    }

    static async Task TestTemplateCardMessage(string webhookUrl)
    {
        XTrace.WriteLine("\n--- 测试模板卡片消息 ---");
        XTrace.WriteLine("1. 文本通知型 (text_notice)");
        XTrace.WriteLine("2. 图文展示型 (news_notice)");
        XTrace.WriteLine("选择类型 (1/2): ");
        
        var cardType = Console.ReadLine();

        if (cardType == "1")
        {
            // 文本通知型
            var card = new TemplateCard
            {
                card_type = "text_notice",
                source = new CardSource
                {
                    icon_url = "https://example.com/icon.png",
                    desc = "测试来源",
                    desc_color = 0
                },
                main_title = new CardMainTitle
                {
                    title = "测试标题",
                    desc = "测试副标题"
                },
                emphasis_content = new CardEmphasis
                {
                    title = "100",
                    desc = "数据条数"
                },
                sub_title_text = "这是子标题",
                horizontal_content_list = new List<CardHorizontalContent>
                {
                    new() { keyname = "类型", value = "测试" },
                    new() { keyname = "状态", value = "正常" }
                },
                jump_list = new List<CardJump>
                {
                    new() { type = 1, url = "https://example.com", title = "查看详情" }
                },
                card_action = new CardAction
                {
                    type = 1,
                    url = "https://example.com"
                }
            };

            var result = WeChatWorkRobot.SendTemplateCard(webhookUrl, card);
            XTrace.WriteLine($"\n✅ 发送结果: {result}");
        }
        else if (cardType == "2")
        {
            // 图文展示型
            var card = new TemplateCard
            {
                card_type = "news_notice",
                source = new CardSource
                {
                    icon_url = "https://example.com/icon.png",
                    desc = "测试来源"
                },
                main_title = new CardMainTitle
                {
                    title = "图文展示标题",
                    desc = "这是描述"
                },
                card_image = new CardImage
                {
                    url = "https://example.com/image.jpg",
                    aspect_ratio = 2.25f
                },
                vertical_content_list = new List<CardVerticalContent>
                {
                    new() { title = "项目1", desc = "描述1" },
                    new() { title = "项目2", desc = "描述2" }
                },
                card_action = new CardAction
                {
                    type = 1,
                    url = "https://example.com"
                }
            };

            var result = WeChatWorkRobot.SendTemplateCard(webhookUrl, card);
            XTrace.WriteLine($"\n✅ 发送结果: {result}");
        }
        else
        {
            XTrace.WriteLine("❌ 无效选择！");
        }
    }

    static async Task TestMarkdownBuilder(string webhookUrl)
    {
        XTrace.WriteLine("\n--- 使用 MarkdownBuilder 构建消息 ---");

        var builder = new MarkdownBuilder();
        builder.AppendLevelTitle("系统通知", 1)
               .NewLine()
               .NewLine()
               .AppendWarningMsg("【警告】")
               .AppendNormal(" 服务器负载过高！")
               .NewLine()
               .NewLine()
               .AppendLevelTitle("详细信息", 2)
               .NewLine()
               .AppendBold("CPU使用率: ")
               .AppendInfoMsg("95%")
               .NewLine()
               .AppendBold("内存使用率: ")
               .AppendInfoMsg("87%")
               .NewLine()
               .NewLine()
               .AppendReference("此消息由 MarkdownBuilder 自动生成")
               .NewLine()
               .AppendHref("https://example.com", "点击查看详情");

        var markdown = builder.ToString();
        XTrace.WriteLine($"\n生成的 Markdown:\n{markdown}");

        XTrace.WriteLine("\n确认发送？(y/n): ");
        if (Console.ReadLine()?.ToLower() != "y")
        {
            XTrace.WriteLine("已取消发送");
            return;
        }

        var result = WeChatWorkRobot.SendMarkdown(webhookUrl, markdown);
        XTrace.WriteLine($"\n✅ 发送结果: {result}");
    }
}
