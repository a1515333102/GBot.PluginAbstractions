namespace GBot.PluginAbstractions;

/// <summary>纯 C# 插件契约。宿主通过反射扫描实现类。</summary>
public interface IBotPlugin
{
    PluginInfo GetPluginInfo();

    bool OnLoad();
    bool OnEnable();
    bool OnDisable();
    bool OnUnload();

    /// <summary>宿主插件页「设置」回调。返回 true 表示已处理。</summary>
    bool OnSettings(object? parent = null);

    /// <summary>统一事件入口。0=继续，1=阻止后续插件。</summary>
    int OnEvent(EventContext context);
}
