using System.Text.Json.Serialization;

namespace GBot.PluginAbstractions;

/// <summary>GET /v2/groups/{group_openid}/info 响应。</summary>
public sealed class GroupInfo
{
    public string GroupOpenId { get; init; } = "";
    public string GroupName { get; init; } = "";
    public string GroupFingerMemo { get; init; } = "";
    public string GroupClassText { get; init; } = "";
    public IReadOnlyList<string> GroupTags { get; init; } = [];
    public int GroupMemberNum { get; init; }
}

/// <summary>GET /v2/groups/{group_openid}/bot_state 响应。</summary>
public sealed class GroupBotState
{
    public string MemberOpenId { get; init; } = "";
    /// <summary>入群时间（RFC3339）。</summary>
    public string JoinedAt { get; init; } = "";
    public bool AllowProactiveMsg { get; init; }
    /// <summary>all / only_mention / mention_and_context</summary>
    public string RecvMsgSetting { get; init; } = "";
    /// <summary>member / owner / admin</summary>
    public string MemberRole { get; init; } = "";
}

/// <summary>GROUP_JOIN_REQUEST 事件体（网关帧中的 d）。</summary>
public sealed class GroupJoinRequest
{
    [JsonPropertyName("group_openid")]
    public string GroupOpenId { get; init; } = "";

    /// <summary>申请 ID，审批接口需回传。</summary>
    [JsonPropertyName("join_request_id")]
    public string JoinRequestId { get; init; } = "";

    [JsonPropertyName("risk_tips")]
    public string RiskTips { get; init; } = "";

    [JsonPropertyName("union_openid")]
    public string UnionOpenId { get; init; } = "";

    [JsonPropertyName("member_openid")]
    public string MemberOpenId { get; init; } = "";

    [JsonPropertyName("username")]
    public string Username { get; init; } = "";

    /// <summary>申请时间（RFC3339）。</summary>
    [JsonPropertyName("apply_at")]
    public string ApplyAt { get; init; } = "";

    /// <summary>self_apply / invited</summary>
    [JsonPropertyName("apply_source")]
    public string ApplySource { get; init; } = "";

    /// <summary>邀请人 openid（apply_source=invited 时有效）。</summary>
    [JsonPropertyName("invited_by")]
    public string InvitedBy { get; init; } = "";

    [JsonPropertyName("bot")]
    public bool Bot { get; init; }

    [JsonPropertyName("verify_info")]
    public GroupJoinVerifyInfo? VerifyInfo { get; init; }

    [JsonPropertyName("auto_approved")]
    public GroupJoinAutoApproved? AutoApproved { get; init; }
}

public sealed class GroupJoinVerifyInfo
{
    /// <summary>verify_message / admin_review_qa</summary>
    [JsonPropertyName("method")]
    public string Method { get; init; } = "";

    [JsonPropertyName("verify_message")]
    public string VerifyMessage { get; init; } = "";

    [JsonPropertyName("review_qa_list")]
    public IReadOnlyList<GroupJoinReviewQa> ReviewQaList { get; init; } = [];
}

public sealed class GroupJoinReviewQa
{
    [JsonPropertyName("question")]
    public string Question { get; init; } = "";

    [JsonPropertyName("answer")]
    public string Answer { get; init; } = "";
}

public sealed class GroupJoinAutoApproved
{
    [JsonPropertyName("strategy_id")]
    public string StrategyId { get; init; } = "";
}

/// <summary>GET /v2/groups/{group_openid}/join_request_list 响应。</summary>
public sealed class GroupJoinRequestList
{
    [JsonPropertyName("list")]
    public IReadOnlyList<GroupJoinRequest> List { get; init; } = [];

    /// <summary>下一页游标；空串表示已到末页。</summary>
    [JsonPropertyName("next_cursor")]
    public string NextCursor { get; init; } = "";
}

/// <summary>GET /v2/groups/{group_openid}/restrict_chat_setting 响应。</summary>
public sealed class GroupRestrictChatSetting
{
    [JsonPropertyName("global_rule")]
    public GroupGlobalMuteRule? GlobalRule { get; init; }

    /// <summary>当前处于禁言中的成员（不含已过期）。</summary>
    [JsonPropertyName("members")]
    public IReadOnlyList<GroupMemberMuteState> Members { get; init; } = [];
}

public sealed class GroupGlobalMuteRule
{
    /// <summary>none / always / schedule</summary>
    [JsonPropertyName("mode")]
    public string Mode { get; init; } = "";

    [JsonPropertyName("schedule_rules")]
    public IReadOnlyList<GroupMuteScheduleRule> ScheduleRules { get; init; } = [];

    [JsonPropertyName("recurring_rules")]
    public IReadOnlyList<GroupMuteRecurringRule> RecurringRules { get; init; } = [];
}

public sealed class GroupMuteScheduleRule
{
    [JsonPropertyName("task_id")]
    public string TaskId { get; init; } = "";

    /// <summary>RFC3339</summary>
    [JsonPropertyName("start_at")]
    public string StartAt { get; init; } = "";

    /// <summary>RFC3339</summary>
    [JsonPropertyName("end_at")]
    public string EndAt { get; init; } = "";

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }
}

public sealed class GroupMuteRecurringRule
{
    [JsonPropertyName("task_id")]
    public string TaskId { get; init; } = "";

    /// <summary>1=周一 … 7=周日。</summary>
    [JsonPropertyName("weekdays")]
    public IReadOnlyList<int> Weekdays { get; init; } = [];

    /// <summary>HH:mm（北京时间）</summary>
    [JsonPropertyName("start_time")]
    public string StartTime { get; init; } = "";

    /// <summary>HH:mm（北京时间）；小于 start_time 表示跨天。</summary>
    [JsonPropertyName("end_time")]
    public string EndTime { get; init; } = "";

    [JsonPropertyName("enabled")]
    public bool Enabled { get; init; }
}

public sealed class GroupMemberMuteState
{
    [JsonPropertyName("member_openid")]
    public string MemberOpenId { get; init; } = "";

    /// <summary>RFC3339</summary>
    [JsonPropertyName("mute_expire_at")]
    public string MuteExpireAt { get; init; } = "";

    [JsonPropertyName("username")]
    public string Username { get; init; } = "";

    [JsonPropertyName("union_openid")]
    public string UnionOpenId { get; init; } = "";
}

/// <summary>POST /v2/groups/{group_openid}/restrict_chat_setting 请求项。</summary>
public sealed class GroupSetMemberMuteState
{
    /// <summary>add / update / del</summary>
    [JsonPropertyName("op")]
    public string Op { get; init; } = "";

    [JsonPropertyName("member_openid")]
    public string MemberOpenId { get; init; } = "";

    /// <summary>RFC3339；op=del 时可传空串表示立即解除。</summary>
    [JsonPropertyName("mute_expire_at")]
    public string? MuteExpireAt { get; init; }
}

/// <summary>GET /v2/groups/join_approval_strategy 响应。</summary>
public sealed class GroupJoinApprovalStrategyList
{
    [JsonPropertyName("strategies")]
    public IReadOnlyList<GroupJoinApprovalStrategy> Strategies { get; init; } = [];

    /// <summary>下一页游标；空串表示已到末页。</summary>
    [JsonPropertyName("next_cursor")]
    public string NextCursor { get; init; } = "";
}

/// <summary>入群自动审批策略。</summary>
public sealed class GroupJoinApprovalStrategy
{
    [JsonPropertyName("strategy_id")]
    public string StrategyId { get; init; } = "";

    [JsonPropertyName("group_openids")]
    public IReadOnlyList<string> GroupOpenIds { get; init; } = [];

    /// <summary>关联 QQ 群号（创建时用 group_ids 时返回）。</summary>
    [JsonPropertyName("group_ids")]
    public IReadOnlyList<string> GroupIds { get; init; } = [];

    [JsonPropertyName("whitelist_user_count")]
    public int WhitelistUserCount { get; init; }

    /// <summary>on / off</summary>
    [JsonPropertyName("is_enable")]
    public string IsEnable { get; init; } = "";

    /// <summary>RFC3339</summary>
    [JsonPropertyName("expire_at")]
    public string ExpireAt { get; init; } = "";

    /// <summary>RFC3339</summary>
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = "";

    /// <summary>RFC3339</summary>
    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; init; } = "";

    [JsonPropertyName("remark")]
    public string Remark { get; init; } = "";
}

/// <summary>POST /v2/groups/join_approval_strategy 创建结果。</summary>
public sealed class GroupJoinApprovalStrategyCreated
{
    [JsonPropertyName("strategy_id")]
    public string StrategyId { get; init; } = "";

    /// <summary>on / off</summary>
    [JsonPropertyName("is_enable")]
    public string IsEnable { get; init; } = "";

    /// <summary>RFC3339</summary>
    [JsonPropertyName("expire_at")]
    public string ExpireAt { get; init; } = "";
}

/// <summary>PATCH /v2/groups/join_approval_strategy/{strategy_id} 的 group_action。</summary>
public sealed class GroupJoinApprovalGroupAction
{
    /// <summary>add / del</summary>
    [JsonPropertyName("op")]
    public string Op { get; init; } = "";

    [JsonPropertyName("group_openids")]
    public IReadOnlyList<string>? GroupOpenIds { get; init; }

    [JsonPropertyName("group_ids")]
    public IReadOnlyList<string>? GroupIds { get; init; }
}

/// <summary>PATCH 修改策略后的响应。</summary>
public sealed class GroupJoinApprovalStrategyUpdated
{
    /// <summary>on / off</summary>
    [JsonPropertyName("is_enable")]
    public string IsEnable { get; init; } = "";

    /// <summary>RFC3339</summary>
    [JsonPropertyName("expire_at")]
    public string ExpireAt { get; init; } = "";
}

/// <summary>POST …/whitelist_users 响应。</summary>
public sealed class GroupJoinApprovalWhitelistResult
{
    [JsonPropertyName("strategy_id")]
    public string StrategyId { get; init; } = "";

    /// <summary>操作后白名单号码数（估算）。</summary>
    [JsonPropertyName("whitelist_user_count")]
    public int WhitelistUserCount { get; init; }

    /// <summary>RFC3339</summary>
    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; init; } = "";
}
