using System.Text.Json.Serialization;

namespace GBot.PluginAbstractions;

// ── 自定义菜单（C2C）────────────────────────────────────
// 文档：https://bot.q.qq.com/wiki/develop/api-v2/server-inter/menu-panel/
// GET/PUT /v2/menu

/// <summary>GET /v2/menu 响应。</summary>
public sealed class BotGlobalMenuResult
{
    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("menu")]
    public BotMenu? Menu { get; set; }
}

/// <summary>PUT /v2/menu 请求体中的 menu，或 GET 响应中的 menu。</summary>
public sealed class BotMenu
{
    [JsonPropertyName("items")]
    public List<BotMenuItem> Items { get; set; } = [];
}

/// <summary>
/// 自定义菜单项。type：<c>switch</c> / <c>send_message</c> / <c>link</c> / <c>menu</c>。
/// </summary>
public sealed class BotMenuItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    /// <summary>switch / send_message / link / menu</summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    [JsonPropertyName("sub_menu_items")]
    public List<BotSubMenuItem>? SubMenuItems { get; set; }

    [JsonPropertyName("send_message")]
    public string? SendMessage { get; set; }

    [JsonPropertyName("link")]
    public string? Link { get; set; }

    [JsonPropertyName("switch")]
    public BotMenuSwitch? Switch { get; set; }
}

/// <summary>二级菜单项（仅 send_message / link）。</summary>
public sealed class BotSubMenuItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    /// <summary>send_message / link</summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    [JsonPropertyName("send_message")]
    public string? SendMessage { get; set; }

    [JsonPropertyName("link")]
    public string? Link { get; set; }
}

public sealed class BotMenuSwitch
{
    [JsonPropertyName("switch_id")]
    public string SwitchId { get; set; } = "";

    [JsonPropertyName("default")]
    public bool Default { get; set; }
}

// ── 指令面板 ────────────────────────────────────────────
// /v2/panels …

/// <summary>GET /v2/panels 分页列表。</summary>
public sealed class BotCommandPanelList
{
    [JsonPropertyName("records")]
    public List<BotCommandPanelRecord> Records { get; set; } = [];

    [JsonPropertyName("next_cursor")]
    public string NextCursor { get; set; } = "";

    [JsonPropertyName("is_end")]
    public bool IsEnd { get; set; }
}

/// <summary>指令面板列表项 / 详情（详情多 user_openids / group_openids）。</summary>
public sealed class BotCommandPanelRecord
{
    [JsonPropertyName("panel_id")]
    public string PanelId { get; set; } = "";

    /// <summary>c2c / group / channel / dm</summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; } = "";

    /// <summary>all / specific</summary>
    [JsonPropertyName("target_type")]
    public string TargetType { get; set; } = "";

    [JsonPropertyName("panel")]
    public BotCommandPanel? Panel { get; set; }

    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; } = "";

    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; set; } = "";

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("user_openids")]
    public List<string>? UserOpenIds { get; set; }

    [JsonPropertyName("group_openids")]
    public List<string>? GroupOpenIds { get; set; }
}

public sealed class BotCommandPanel
{
    [JsonPropertyName("items")]
    public List<BotCommandPanelItem> Items { get; set; } = [];

    [JsonPropertyName("remark")]
    public string? Remark { get; set; }

    [JsonPropertyName("version")]
    public int? Version { get; set; }
}

/// <summary>面板元素。type：<c>command</c> / <c>link</c>。</summary>
public sealed class BotCommandPanelItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("desc")]
    public string? Desc { get; set; }

    /// <summary>command / link</summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    [JsonPropertyName("only_admin")]
    public bool OnlyAdmin { get; set; }

    [JsonPropertyName("link")]
    public string? Link { get; set; }
}
