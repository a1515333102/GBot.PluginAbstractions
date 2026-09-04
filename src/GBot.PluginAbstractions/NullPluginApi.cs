namespace GBot.PluginAbstractions;

internal sealed class NullPluginApi : IPluginApi
{
    public static readonly NullPluginApi Instance = new();

    private static Task<ApiResponse> Fail() => Task.FromResult(ApiResponse.Fail("PluginApi not bound"));

    public Task<ApiResponse> CallApiAsync(string robotId, string method, string path, object? body = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendGroupMessageAsync(string robotId, string groupOpenId, string message, string? msgId = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendPrivateMessageAsync(string robotId, string userOpenId, string message, string? msgId = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> ReplyAsync(EventContext context, string message, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendGroupPayloadAsync(string robotId, string groupOpenId, IDictionary<string, object?> body, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendPrivatePayloadAsync(string robotId, string userOpenId, IDictionary<string, object?> body, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendGroupMarkdownAsync(string robotId, string groupOpenId, string markdown, string? msgId = null, object? keyboard = null, bool forceVerifyImageResource = false, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendPrivateMarkdownAsync(string robotId, string userOpenId, string markdown, string? msgId = null, object? keyboard = null, bool forceVerifyImageResource = false, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendGroupArkAsync(string robotId, string groupOpenId, object ark, string? msgId = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendPrivateArkAsync(string robotId, string userOpenId, object ark, string? msgId = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendGroupEmbedAsync(string robotId, string groupOpenId, object embed, string? msgId = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendPrivateEmbedAsync(string robotId, string userOpenId, object embed, string? msgId = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> UploadGroupMediaAsync(string robotId, string groupOpenId, MediaFileType fileType, string? url = null, string? fileDataBase64 = null, bool srvSendMsg = false, string? fileName = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> UploadPrivateMediaAsync(string robotId, string userOpenId, MediaFileType fileType, string? url = null, string? fileDataBase64 = null, bool srvSendMsg = false, string? fileName = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendGroupMediaAsync(string robotId, string groupOpenId, string fileInfo, string? caption = null, string? msgId = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendPrivateMediaAsync(string robotId, string userOpenId, string fileInfo, string? caption = null, string? msgId = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendGroupMediaByUrlAsync(string robotId, string groupOpenId, MediaFileType fileType, string url, string? caption = null, string? msgId = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendPrivateMediaByUrlAsync(string robotId, string userOpenId, MediaFileType fileType, string url, string? caption = null, string? msgId = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendInputNotifyAsync(string robotId, string userOpenId, string msgId, int inputSeconds = 60, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SendPrivateStreamAsync(string robotId, string userOpenId, string markdown, int state, string? streamId = null, int index = 0, string? msgId = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> RecallGroupMessageAsync(string robotId, string groupOpenId, string messageId, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> RecallPrivateMessageAsync(string robotId, string userOpenId, string messageId, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> GetGroupInfoAsync(string robotId, string groupOpenId, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> GetGroupBotStateAsync(string robotId, string groupOpenId, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> GetGroupJoinRequestListAsync(
        string robotId, string groupOpenId, string? cursor = null, int? limit = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> ApproveGroupJoinRequestAsync(
        string robotId, string groupOpenId, string memberOpenId, string op,
        string? joinRequestId = null, string? rejectReason = null, bool addToMemberBlacklist = false,
        CancellationToken ct = default) => Fail();
    public Task<ApiResponse> GetGroupRestrictChatSettingAsync(
        string robotId, string groupOpenId, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> SetGroupMemberMuteAsync(
        string robotId, string groupOpenId, IReadOnlyList<GroupSetMemberMuteState> members,
        CancellationToken ct = default) => Fail();
    public Task<ApiResponse> GetGroupMembersAsync(
        string robotId, string groupOpenId, string? cursor = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> GetGroupMemberAsync(
        string robotId, string groupOpenId, string memberOpenId, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> BatchRemoveGroupMembersAsync(
        string robotId, string groupOpenId, IReadOnlyList<string> memberOpenIds,
        bool addToMemberBlacklist = false, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> GetGroupMemberBlacklistAsync(
        string robotId, string groupOpenId, string? cursor = null, int? limit = null,
        CancellationToken ct = default) => Fail();
    public Task<ApiResponse> UpdateGroupMemberBlacklistAsync(
        string robotId, string groupOpenId, string op, IReadOnlyList<string> memberOpenIds,
        CancellationToken ct = default) => Fail();
    public Task<ApiResponse> GetGroupJoinApprovalStrategiesAsync(
        string robotId, string? cursor = null, int? limit = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> CreateGroupJoinApprovalStrategyAsync(
        string robotId,
        IReadOnlyList<string>? groupOpenIds = null,
        IReadOnlyList<string>? groupIds = null,
        string? isEnable = null,
        string? expireAt = null,
        string? remark = null,
        CancellationToken ct = default) => Fail();
    public Task<ApiResponse> UpdateGroupJoinApprovalStrategyAsync(
        string robotId,
        string strategyId,
        string? isEnable = null,
        string? expireAt = null,
        string? remark = null,
        GroupJoinApprovalGroupAction? groupAction = null,
        CancellationToken ct = default) => Fail();
    public Task<ApiResponse> DeleteGroupJoinApprovalStrategyAsync(
        string robotId, string strategyId, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> ExecuteGroupJoinApprovalStrategyAsync(
        string robotId, string strategyId, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> UpdateGroupJoinApprovalWhitelistAsync(
        string robotId, string strategyId, string op, IReadOnlyList<string> whitelistUsers,
        CancellationToken ct = default) => Fail();

    public Task<ApiResponse> GetGlobalMenuAsync(string robotId, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> UpdateGlobalMenuAsync(string robotId, BotMenu? menu, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> GetCommandPanelsAsync(
        string robotId, string scope, string? cursor = null, int? limit = null, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> CreateCommandPanelAsync(
        string robotId, string scope, BotCommandPanel panel, string? targetType = null,
        IReadOnlyList<string>? userOpenIds = null, IReadOnlyList<string>? groupOpenIds = null,
        CancellationToken ct = default) => Fail();
    public Task<ApiResponse> GetCommandPanelAsync(string robotId, string panelId, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> UpdateCommandPanelAsync(
        string robotId, string panelId, BotCommandPanel panel, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> DeleteCommandPanelAsync(string robotId, string panelId, CancellationToken ct = default) => Fail();
    public Task<ApiResponse> UpdateCommandPanelTargetsAsync(
        string robotId, string panelId, string op,
        IReadOnlyList<string>? userOpenIds = null, IReadOnlyList<string>? groupOpenIds = null,
        CancellationToken ct = default) => Fail();

    public Task<ApiResponse> RecognizeImageAsync(
        string imageUrl, string? endpoint = null, CancellationToken ct = default) => Fail();

    public void RememberGroupMemberRole(
        string robotId, string groupOpenId, string memberOpenId, string memberRole) { }

    public string GetGroupMemberRole(string robotId, string groupOpenId, string memberOpenId) => "";

    public bool IsGroupOwnerOrAdmin(string robotId, string groupOpenId, string memberOpenId) => false;

    public bool IsGatewayReady(string robotId) => false;

    public GatewayTrafficInfo GetGatewayTraffic(string robotId) => default;

    public void LogInfo(string message) { }
    public void LogWarning(string message) { }
    public void LogError(string message) { }
}
