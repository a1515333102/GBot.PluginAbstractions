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

    /// <summary>
    /// 群 Markdown（msg_type=2）。可附带 keyboard。
    /// <paramref name="forceVerifyImageResource"/> 为 true 时，图片转存失败则整条消息失败（默认 false，保持原行为）。
    /// </summary>
    Task<ApiResponse> SendGroupMarkdownAsync(
        string robotId,
        string groupOpenId,
        string markdown,
        string? msgId = null,
        object? keyboard = null,
        bool forceVerifyImageResource = false,
        CancellationToken ct = default);

    /// <summary>
    /// 私聊 Markdown（msg_type=2）。可附带 keyboard。
    /// <paramref name="forceVerifyImageResource"/> 为 true 时，图片转存失败则整条消息失败（默认 false，保持原行为）。
    /// </summary>
    Task<ApiResponse> SendPrivateMarkdownAsync(
        string robotId,
        string userOpenId,
        string markdown,
        string? msgId = null,
        object? keyboard = null,
        bool forceVerifyImageResource = false,
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

    // ── 群资料（内邀能力，无权限时会失败）────────────────

    /// <summary>获取群基础信息：GET /v2/groups/{group_openid}/info（群名、人数等）。</summary>
    Task<ApiResponse> GetGroupInfoAsync(
        string robotId,
        string groupOpenId,
        CancellationToken ct = default);

    /// <summary>获取机器人群内状态：GET /v2/groups/{group_openid}/bot_state。</summary>
    Task<ApiResponse> GetGroupBotStateAsync(
        string robotId,
        string groupOpenId,
        CancellationToken ct = default);

    /// <summary>
    /// 拉取入群申请列表（分页）：GET /v2/groups/{group_openid}/join_request_list。
    /// 机器人须为群管理员。响应见 <see cref="GroupJoinRequestList"/>。
    /// </summary>
    /// <param name="cursor">分页游标；首次可不传。</param>
    /// <param name="limit">单页数量，默认 20，最大 100。</param>
    Task<ApiResponse> GetGroupJoinRequestListAsync(
        string robotId,
        string groupOpenId,
        string? cursor = null,
        int? limit = null,
        CancellationToken ct = default);

    /// <summary>
    /// 审批入群申请：POST /v2/groups/{group_openid}/approval_join_request/{member_openid}。
    /// <paramref name="op"/> 为 <c>approve</c> / <c>decline</c>；机器人须为群管理员。
    /// </summary>
    Task<ApiResponse> ApproveGroupJoinRequestAsync(
        string robotId,
        string groupOpenId,
        string memberOpenId,
        string op,
        string? joinRequestId = null,
        string? rejectReason = null,
        bool addToMemberBlacklist = false,
        CancellationToken ct = default);

    /// <summary>
    /// 查询群禁言状态：GET /v2/groups/{group_openid}/restrict_chat_setting。
    /// 含全员禁言规则与成员级禁言列表；机器人须为群管理员。响应见 <see cref="GroupRestrictChatSetting"/>。
    /// </summary>
    Task<ApiResponse> GetGroupRestrictChatSettingAsync(
        string robotId,
        string groupOpenId,
        CancellationToken ct = default);

    /// <summary>
    /// 设置群成员禁言：POST /v2/groups/{group_openid}/restrict_chat_setting。
    /// 每项 <see cref="GroupSetMemberMuteState.Op"/> 为 <c>add</c> / <c>update</c> / <c>del</c>；单次最多 10 个；机器人须为群管理员。
    /// </summary>
    Task<ApiResponse> SetGroupMemberMuteAsync(
        string robotId,
        string groupOpenId,
        IReadOnlyList<GroupSetMemberMuteState> members,
        CancellationToken ct = default);

    /// <summary>
    /// 查询入群自动审批策略列表（分页）：GET /v2/groups/join_approval_strategy。
    /// 响应见 <see cref="GroupJoinApprovalStrategyList"/>。
    /// </summary>
    Task<ApiResponse> GetGroupJoinApprovalStrategiesAsync(
        string robotId,
        string? cursor = null,
        int? limit = null,
        CancellationToken ct = default);

    /// <summary>
    /// 创建入群自动审批策略：POST /v2/groups/join_approval_strategy。
    /// <paramref name="groupOpenIds"/> 与 <paramref name="groupIds"/> 二选一必填（互斥，最多各 100）；响应见 <see cref="GroupJoinApprovalStrategyCreated"/>。
    /// </summary>
    Task<ApiResponse> CreateGroupJoinApprovalStrategyAsync(
        string robotId,
        IReadOnlyList<string>? groupOpenIds = null,
        IReadOnlyList<string>? groupIds = null,
        string? isEnable = null,
        string? expireAt = null,
        string? remark = null,
        CancellationToken ct = default);

    /// <summary>
    /// 修改入群自动审批策略：PATCH /v2/groups/join_approval_strategy/{strategy_id}。
    /// 可改启用状态、过期时间、备注，或通过 <paramref name="groupAction"/> 增删关联群；响应见 <see cref="GroupJoinApprovalStrategyUpdated"/>。
    /// </summary>
    Task<ApiResponse> UpdateGroupJoinApprovalStrategyAsync(
        string robotId,
        string strategyId,
        string? isEnable = null,
        string? expireAt = null,
        string? remark = null,
        GroupJoinApprovalGroupAction? groupAction = null,
        CancellationToken ct = default);

    /// <summary>
    /// 删除入群自动审批策略：DELETE /v2/groups/join_approval_strategy/{strategy_id}。
    /// </summary>
    Task<ApiResponse> DeleteGroupJoinApprovalStrategyAsync(
        string robotId,
        string strategyId,
        CancellationToken ct = default);

    /// <summary>
    /// 执行入群自动审批策略（异步全量扫描）：POST /v2/groups/join_approval_strategy/{strategy_id}/execute。
    /// 命中白名单的入群申请自动通过；约 10 分钟完成。
    /// </summary>
    Task<ApiResponse> ExecuteGroupJoinApprovalStrategyAsync(
        string robotId,
        string strategyId,
        CancellationToken ct = default);

    /// <summary>
    /// 修改入群自动审批策略白名单：POST /v2/groups/join_approval_strategy/{strategy_id}/whitelist_users。
    /// <paramref name="op"/> 为 <c>add</c> / <c>del</c>；单次最多 10000 个 QQ 号（字符串）；响应见 <see cref="GroupJoinApprovalWhitelistResult"/>。
    /// </summary>
    Task<ApiResponse> UpdateGroupJoinApprovalWhitelistAsync(
        string robotId,
        string strategyId,
        string op,
        IReadOnlyList<string> whitelistUsers,
        CancellationToken ct = default);

    // ── 图片识别（第三方 OCR，非 QQ 官方）────────────────

    /// <summary>
    /// 图片文字识别：向识别服务 POST <c>{"url": imageUrl}</c>，成功时 <see cref="ApiResponse.Message"/> 为识别文本。
    /// <paramref name="endpoint"/> 为空则用宿主设置里的图片识别地址。
    /// </summary>
    Task<ApiResponse> RecognizeImageAsync(
        string imageUrl,
        string? endpoint = null,
        CancellationToken ct = default);

    /// <summary>指定 AppId 的官方网关是否已 READY（能收群消息/艾特以刷新 msg_id）。</summary>
    bool IsGatewayReady(string robotId);

    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
}
