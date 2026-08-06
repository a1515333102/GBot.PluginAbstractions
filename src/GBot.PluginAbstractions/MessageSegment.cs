namespace GBot.PluginAbstractions;

/// <summary>简化消息段（从官方 content / attachments 解析）。</summary>
public sealed class MessageSegment
{
    public string Type { get; init; } = "text";
    public Dictionary<string, string> Data { get; init; } = new(StringComparer.OrdinalIgnoreCase);

    public string? this[string key]
    {
        get => Data.TryGetValue(key, out var v) ? v : null;
        set
        {
            if (value is null) Data.Remove(key);
            else Data[key] = value;
        }
    }

    public static MessageSegment Text(string text) => new()
    {
        Type = "text",
        Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { ["text"] = text },
    };

    public static MessageSegment Image(
        string url,
        string? filename = null,
        string? contentType = null,
        int width = 0,
        int height = 0,
        long size = 0) => new()
    {
        Type = "image",
        Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["url"] = url,
            ["file"] = filename ?? "",
            ["content_type"] = contentType ?? "",
            ["width"] = width.ToString(),
            ["height"] = height.ToString(),
            ["size"] = size.ToString(),
        },
    };

    public static MessageSegment File(
        string url,
        string? filename = null,
        string? contentType = null,
        long size = 0) => new()
    {
        Type = "file",
        Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["url"] = url,
            ["file"] = filename ?? "",
            ["content_type"] = contentType ?? "file",
            ["size"] = size.ToString(),
        },
    };

    /// <summary>语音附件（可含 ASR 转写、wav 地址）。</summary>
    public static MessageSegment Voice(
        string url,
        string? filename = null,
        string? asrText = null,
        string? wavUrl = null,
        long size = 0) => new()
    {
        Type = "voice",
        Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["url"] = url ?? "",
            ["file"] = filename ?? "",
            ["asr"] = asrText ?? "",
            ["wav_url"] = wavUrl ?? "",
            ["content_type"] = "voice",
            ["size"] = size.ToString(),
        },
    };

    /// <summary>QQ 表情（系统表情 / emoji / 超级表情等）。</summary>
    public static MessageSegment Face(
        string faceType,
        string faceId,
        string? text = null,
        string? extJson = null,
        string? emoji = null) => new()
    {
        Type = "face",
        Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["face_type"] = faceType ?? "",
            ["face_id"] = faceId ?? "",
            ["text"] = text ?? "",
            ["ext"] = extJson ?? "",
            ["emoji"] = emoji ?? "",
        },
    };

    /// <summary>
    /// 引用/回复消息（官方 message_scene.ext 的 ref_msg_idx + msg_elements）。
    /// </summary>
    public static MessageSegment Reply(
        string refMsgIdx,
        string preview,
        string? content = null,
        string? selfMsgIdx = null) => new()
    {
        Type = "reply",
        Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["ref_msg_idx"] = refMsgIdx ?? "",
            ["msg_idx"] = selfMsgIdx ?? "",
            ["text"] = content ?? "",
            ["preview"] = preview ?? "",
        },
    };

    /// <summary>QQ 闪传（FlashTransfer）卡片。</summary>
    public static MessageSegment FlashTransfer(
        string filename,
        string? filesetId = null,
        string? coverUrl = null,
        string? schema = null,
        string? rawLink = null) => new()
    {
        Type = "flash_transfer",
        Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["file"] = filename ?? "",
            ["fileset_id"] = filesetId ?? "",
            ["cover"] = coverUrl ?? "",
            ["schema"] = schema ?? "",
            ["url"] = rawLink ?? "",
        },
    };
}
