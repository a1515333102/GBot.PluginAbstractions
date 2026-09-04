namespace GBot.PluginAbstractions;

/// <summary>
/// 插件契约 ABI 版本。宿主与插件引用的 Abstractions 主版本必须一致；
/// 市场清单用 <see cref="Major"/> 做硬兼容校验。
/// </summary>
public static class PluginAbstractionsVersion
{
    public const int Major = 1;
    public const int Minor = 6;
    public const int Patch = 0;

    public static string VersionString => $"{Major}.{Minor}.{Patch}";
}
