namespace GBot.PluginAbstractions;

/// <summary>插件基类：默认空实现 + 按事件类型分发。</summary>
public abstract class BotPluginBase : IBotPlugin
{
    public bool IsLoaded { get; private set; }
    public bool IsEnabled { get; private set; }
    public IPluginApi Api { get; set; } = NullPluginApi.Instance;

    public abstract PluginInfo GetPluginInfo();

    public virtual bool OnLoad()
    {
        IsLoaded = true;
        return true;
    }

    public virtual bool OnEnable()
    {
        IsEnabled = true;
        return true;
    }

    public virtual bool OnDisable()
    {
        IsEnabled = false;
        return true;
    }

    public virtual bool OnUnload()
    {
        IsLoaded = false;
        IsEnabled = false;
        return true;
    }

    public virtual bool OnSettings(object? parent = null) => false;

    public virtual int OnEvent(EventContext context) => context.EventType switch
    {
        PluginEventType.GroupAtMessage => OnGroupAtMessage(context),
        PluginEventType.GroupMessage => OnGroupMessage(context),
        PluginEventType.C2CMessage => OnC2CMessage(context),
        PluginEventType.FriendAdd => OnFriendAdd(context),
        PluginEventType.FriendDel => OnFriendDel(context),
        PluginEventType.GroupAddRobot => OnGroupAddRobot(context),
        PluginEventType.GroupDelRobot => OnGroupDelRobot(context),
        PluginEventType.GroupMemberAdd => OnGroupMemberAdd(context),
        PluginEventType.GroupMemberRemove => OnGroupMemberRemove(context),
        PluginEventType.GroupJoinRequest => OnGroupJoinRequest(context),
        PluginEventType.Interaction => OnInteraction(context),
        _ => OnUnknownEvent(context),
    };

    public virtual int OnGroupAtMessage(EventContext context) => 0;
    public virtual int OnGroupMessage(EventContext context) => 0;
    public virtual int OnC2CMessage(EventContext context) => 0;
    public virtual int OnFriendAdd(EventContext context) => 0;
    public virtual int OnFriendDel(EventContext context) => 0;
    public virtual int OnGroupAddRobot(EventContext context) => 0;
    public virtual int OnGroupDelRobot(EventContext context) => 0;
    public virtual int OnGroupMemberAdd(EventContext context) => 0;
    public virtual int OnGroupMemberRemove(EventContext context) => 0;
    public virtual int OnGroupJoinRequest(EventContext context) => 0;
    public virtual int OnInteraction(EventContext context) => 0;
    public virtual int OnUnknownEvent(EventContext context) => 0;

    public string GetDataDir()
    {
        var name = GetPluginInfo().Name;
        var path = Path.Combine(AppContext.BaseDirectory, "data", name);
        Directory.CreateDirectory(path);
        return path;
    }

    public string GetConfigPath()
    {
        var name = GetPluginInfo().Name;
        var dir = Path.Combine(AppContext.BaseDirectory, "config", "plugins");
        Directory.CreateDirectory(dir);
        return Path.Combine(dir, $"{name}.json");
    }
}
