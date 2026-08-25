namespace GBot.PluginAbstractions;

/// <summary>富媒体上传 file_type（官方 /files 接口）。</summary>
public enum MediaFileType
{
    /// <summary>图片 png/jpg。</summary>
    Image = 1,

    /// <summary>视频 mp4。</summary>
    Video = 2,

    /// <summary>语音。非 silk 的 url/file_data 由宿主自动转码为腾讯 silk 再上传。</summary>
    Voice = 3,

    /// <summary>文件（需开通权限）。</summary>
    File = 4,
}

/// <summary>C2C 流式消息 state（群聊不支持 stream）。</summary>
public static class StreamStates
{
    /// <summary>开始/中间分片（增量更新）。</summary>
    public const int Streaming = 1;

    /// <summary>结束分片（content 建议以换行结尾）。</summary>
    public const int Done = 10;
}

/// <summary>消息 msg_type。</summary>
public static class OfficialMsgTypes
{
    public const int Text = 0;
    public const int Markdown = 2;
    public const int Ark = 3;
    public const int Embed = 4;
    public const int InputNotify = 6;
    public const int Media = 7;
}
