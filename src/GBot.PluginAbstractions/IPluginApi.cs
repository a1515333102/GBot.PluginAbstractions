namespace GBot.PluginAbstractions;

/// <summary>宿主提供给插件的 QQ 官方机器人 API（群聊 / 单聊）。</summary>
public interface IPluginApi
{
    /// <summary>通用 REST：相对路径如 /v2/groups/{id}/messages。</summary>
    Task<ApiResponse> CallApiAsync(
        string robotId,
        string method,
        string path,
        object? body = null,
        CancellationToken ct = default);

    // ── 文本 ──────────────────────────────────────────────

    /// <summary>群文本（msg_type=0）。msgId 为空=主动消息。</summary>
    Task<ApiResponse> SendGroupMessageAsync(
        string robotId,
        string groupOpenId,
        string message,
        string? msgId = null,
        CancellationToken ct = default);

    /// <summary>私聊文本（msg_type=0）。msgId 为空=主动消息。</summary>
    Task<ApiResponse> SendPrivateMessageAsync(
        string robotId,
        string userOpenId,
        string message,
        string? msgId = null,
        CancellationToken ct = default);

    /// <summary>按事件场景自动回复文本。</summary>
    Task<ApiResponse> ReplyAsync(EventContext context, string message, CancellationToken ct = default);

    // ── 通用 payload / Markdown / 键盘 / ARK / Embed ─────

    /// <summary>群消息自定义 body（自动补 msg_seq；字段名用官方 snake_case）。</summary>
    Task<ApiResponse> SendGroupPayloadAsync(
        string robotId,
        string groupOpenId,
        IDictionary<string, object?> body,
        CancellationToken ct = default);

    /// <summary>私聊自定义 body（自动补 msg_seq）。</summary>
    Task<ApiResponse> SendPrivatePayloadAsync(
        string robotId,
        string userOpenId,
        IDictionary<string, object?> body,
        CancellationToken ct = default);

    /// <summary>群 Markdown（msg_type=2）。可附带 keyboard。</summary>
    Task<ApiResponse> SendGroupMarkdownAsync(
        string robotId,
        string groupOpenId,
        string markdown,
        string? msgId = null,
        object? keyboard = null,
        CancellationToken ct = default);

    /// <summary>私聊 Markdown（msg_type=2）。可附带 keyboard。</summary>
    Task<ApiResponse> SendPrivateMarkdownAsync(
        string robotId,
        string userOpenId,
        string markdown,
        string? msgId = null,
        object? keyboard = null,
        CancellationToken ct = default);

    /// <summary>群 ARK（msg_type=3）。ark 为官方结构（template_id + kv）。</summary>
    Task<ApiResponse> SendGroupArkAsync(
        string robotId,
        string groupOpenId,
        object ark,
        string? msgId = null,
        CancellationToken ct = default);

    /// <summary>私聊 ARK（msg_type=3）。</summary>
    Task<ApiResponse> SendPrivateArkAsync(
        string robotId,
        string userOpenId,
        object ark,
        string? msgId = null,
        CancellationToken ct = default);

    /// <summary>群 Embed（msg_type=4）。</summary>
    Task<ApiResponse> SendGroupEmbedAsync(
        string robotId,
        string groupOpenId,
        object embed,
        string? msgId = null,
        CancellationToken ct = default);

    /// <summary>私聊 Embed（msg_type=4）。</summary>
    Task<ApiResponse> SendPrivateEmbedAsync(
        string robotId,
        string userOpenId,
        object embed,
        string? msgId = null,
        CancellationToken ct = default);

    // ── 富媒体 ────────────────────────────────────────────

    /// <summary>上传群富媒体，返回含 file_info 的响应（srvSendMsg=true 时服务端直接下发）。</summary>
    Task<ApiResponse> UploadGroupMediaAsync(
        string robotId,
        string groupOpenId,
        MediaFileType fileType,
        string? url = null,
        string? fileDataBase64 = null,
        bool srvSendMsg = false,
        string? fileName = null,
        CancellationToken ct = default);

    /// <summary>上传私聊富媒体。</summary>
    Task<ApiResponse> UploadPrivateMediaAsync(
        string robotId,
        string userOpenId,
        MediaFileType fileType,
        string? url = null,
        string? fileDataBase64 = null,
        bool srvSendMsg = false,
        string? fileName = null,
        CancellationToken ct = default);

    /// <summary>群富媒体消息（msg_type=7，需已有 file_info）。</summary>
    Task<ApiResponse> SendGroupMediaAsync(
        string robotId,
        string groupOpenId,
        string fileInfo,
        string? caption = null,
        string? msgId = null,
        CancellationToken ct = default);

    /// <summary>私聊富媒体消息（msg_type=7）。</summary>
    Task<ApiResponse> SendPrivateMediaAsync(
        string robotId,
        string userOpenId,
        string fileInfo,
        string? caption = null,
        string? msgId = null,
        CancellationToken ct = default);

    /// <summary>上传 URL 并发送群富媒体（两步合一）。</summary>
    Task<ApiResponse> SendGroupMediaByUrlAsync(
        string robotId,
        string groupOpenId,
        MediaFileType fileType,
        string url,
        string? caption = null,
        string? msgId = null,
        CancellationToken ct = default);

    /// <summary>上传 URL 并发送私聊富媒体。</summary>
    Task<ApiResponse> SendPrivateMediaByUrlAsync(
        string robotId,
        string userOpenId,
        MediaFileType fileType,
        string url,
        string? caption = null,
        string? msgId = null,
        CancellationToken ct = default);

    // ── 输入状态 / 流式（C2C）────────────────────────────

    /// <summary>私聊「正在输入」（msg_type=6）。群聊无效。</summary>
    Task<ApiResponse> SendInputNotifyAsync(
        string robotId,
        string userOpenId,
        string msgId,
        int inputSeconds = 60,
        CancellationToken ct = default);

    /// <summary>
    /// 私聊流式分片（仅 C2C；群聊不支持）。
    /// state=1 中间更新，state=10 结束（建议 content 以 \n 结尾）；首次可不带 streamId。
    /// </summary>
    Task<ApiResponse> SendPrivateStreamAsync(
        string robotId,
        string userOpenId,
        string markdown,
        int state,
        string? streamId = null,
        int index = 0,
        string? msgId = null,
        CancellationToken ct = default);

    // ── 撤回 ──────────────────────────────────────────────

    /// <summary>撤回群消息（仅机器人自己发的，约 2 分钟内）。</summary>
    Task<ApiResponse> RecallGroupMessageAsync(
        string robotId,
        string groupOpenId,
        string messageId,
        CancellationToken ct = default);

    /// <summary>撤回私聊消息（仅机器人自己发的，约 2 分钟内）。</summary>
    Task<ApiResponse> RecallPrivateMessageAsync(
        string robotId,
        string userOpenId,
        string messageId,
        CancellationToken ct = default);

    /// <summary>指定 AppId 的官方网关是否已 READY（能收群消息/艾特以刷新 msg_id）。</summary>
    bool IsGatewayReady(string robotId);

    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
}
