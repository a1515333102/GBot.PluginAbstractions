namespace GBot.PluginAbstractions;

/// <summary>事件上下文。返回 0=继续传播，1=阻止后续插件。</summary>
public sealed class EventContext
{
    public PluginEventType EventType { get; init; }

    /// <summary>机器人 AppID。</summary>
    public string RobotId { get; init; } = "";

    /// <summary>发送者 openid（群 member_openid / 单聊 user_openid）。</summary>
    public string FromOpenId { get; init; } = "";

    /// <summary>来源 ID：群 openid；C2C 为空。</summary>
    public string SourceId { get; init; } = "";

    /// <summary>消息 ID（被动回复需携带，形如 ROBOT1.0_xxx）。</summary>
    public string MessageId { get; init; } = "";

    /// <summary>
    /// 可引用 REFIDX（来自 message_scene.ext 的 msg_idx）。
    /// 群/私聊 message_reference.message_id 必须传此值，不能传 MessageId。
    /// </summary>
    public string RefIdx { get; init; } = "";

    /// <summary>事件 ID（部分接口用）。</summary>
    public string EventId { get; init; } = "";

    public long TimeUnix { get; init; }

    /// <summary>可读正文。</summary>
    public string Text { get; set; } = "";

    public string MessageContent { get; set; } = "";
    public string DisplaySummary { get; set; } = "";

    public string SenderName { get; init; } = "";
    public string Nickname { get; init; } = "";

    /// <summary>群成员角色：owner（群主） / admin(管理员) / member（群成员）（来自 author.member_role）。</summary>
    public string MemberRole { get; init; } = "";

    /// <summary>union_openid（若官方下发）。</summary>
    public string UnionOpenId { get; init; } = "";

    /// <summary>发送者是否为机器人（author.bot）。</summary>
    public bool IsBot { get; init; }

    /// <summary>发送者头像 URL（官方 avatar 或 qqapp CDN 回退）。</summary>
    public string AvatarUrl { get; init; } = "";

    /// <summary>官方事件名，如 GROUP_AT_MESSAGE_CREATE。</summary>
    public string EventName { get; init; } = "";

    public string RawJson { get; set; } = "";
    public bool BlockEvent { get; set; }

    /// <summary>群 / 单聊。</summary>
    public MessageScene Scene { get; init; } = MessageScene.Unknown;

    public IReadOnlyList<MessageSegment> Segments { get; init; } = [];
    public IPluginApi Api { get; init; } = NullPluginApi.Instance;

    public string SenderDisplay =>
        !string.IsNullOrWhiteSpace(Nickname) ? Nickname
        : !string.IsNullOrWhiteSpace(SenderName) ? SenderName
        : !string.IsNullOrWhiteSpace(FromOpenId) ? FromOpenId
        : "未知";

    public Task<ApiResponse> ReplyAsync(string message, CancellationToken ct = default)
        => Api.ReplyAsync(this, message, ct);

    /// <summary>按场景回复 Markdown（可附键盘）。</summary>
    /// <param name="forceVerifyImageResource">为 true 时图片转存失败则整条失败（默认 false）。</param>
    public Task<ApiResponse> ReplyMarkdownAsync(
        string markdown, object? keyboard = null, bool forceVerifyImageResource = false, CancellationToken ct = default)
        => Scene switch
        {
            MessageScene.Group => Api.SendGroupMarkdownAsync(
                RobotId, SourceId, markdown, MessageId, keyboard, forceVerifyImageResource, ct),
            MessageScene.C2C => Api.SendPrivateMarkdownAsync(
                RobotId, FromOpenId, markdown, MessageId, keyboard, forceVerifyImageResource, ct),
            _ => Task.FromResult(ApiResponse.Fail($"无法回复：未知场景 {Scene}")),
        };

    /// <summary>按场景上传 URL 并回复富媒体。</summary>
    public Task<ApiResponse> ReplyMediaByUrlAsync(
        MediaFileType fileType, string url, string? caption = null, CancellationToken ct = default)
        => Scene switch
        {
            MessageScene.Group => Api.SendGroupMediaByUrlAsync(RobotId, SourceId, fileType, url, caption, MessageId, ct),
            MessageScene.C2C => Api.SendPrivateMediaByUrlAsync(RobotId, FromOpenId, fileType, url, caption, MessageId, ct),
            _ => Task.FromResult(ApiResponse.Fail($"无法回复：未知场景 {Scene}")),
        };

    /// <summary>私聊「正在输入」（仅 C2C）。</summary>
    public Task<ApiResponse> NotifyTypingAsync(int inputSeconds = 60, CancellationToken ct = default)
        => Scene == MessageScene.C2C
            ? Api.SendInputNotifyAsync(RobotId, FromOpenId, MessageId, inputSeconds, ct)
            : Task.FromResult(ApiResponse.Fail("正在输入仅支持私聊"));

    /// <summary>本条消息中第一张图片的 URL（来自 attachments / Segments）。</summary>
    public string? GetFirstImageUrl()
    {
        foreach (var seg in Segments)
        {
            if (!string.Equals(seg.Type, "image", StringComparison.OrdinalIgnoreCase))
                continue;
            var url = seg["url"];
            if (!string.IsNullOrWhiteSpace(url))
                return url.Trim();
        }

        return null;
    }

    /// <summary>识别本条消息第一张图的文字（见 <see cref="IPluginApi.RecognizeImageAsync"/>）。</summary>
    public Task<ApiResponse> RecognizeFirstImageAsync(string? endpoint = null, CancellationToken ct = default)
    {
        var url = GetFirstImageUrl();
        if (string.IsNullOrWhiteSpace(url))
            return Task.FromResult(ApiResponse.Fail("消息中无图片"));
        return Api.RecognizeImageAsync(url, endpoint, ct);
    }
}

public enum MessageScene
{
    Unknown = 0,
    Group = 1,
    C2C = 2,
}
