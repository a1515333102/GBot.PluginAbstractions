namespace GBot.PluginAbstractions;

/// <summary>QQ 官方机器人事件（仅群聊 / 单聊）。</summary>
public enum PluginEventType
{
    Unknown = 0,

    /// <summary>群内 @ 机器人消息（GROUP_AT_MESSAGE_CREATE）。</summary>
    GroupAtMessage = 1,

    /// <summary>用户单聊消息（C2C_MESSAGE_CREATE）。</summary>
    C2CMessage = 2,

    /// <summary>群聊消息（GROUP_MESSAGE_CREATE，含非 @）。</summary>
    GroupMessage = 3,

    /// <summary>用户添加机器人（FRIEND_ADD）。</summary>
    FriendAdd = 10,

    /// <summary>用户删除机器人（FRIEND_DEL）。</summary>
    FriendDel = 11,

    /// <summary>机器人被添加到群（GROUP_ADD_ROBOT）。</summary>
    GroupAddRobot = 20,

    /// <summary>机器人被移出群（GROUP_DEL_ROBOT）。</summary>
    GroupDelRobot = 21,

    /// <summary>群成员加入（GROUP_MEMBER_ADD）。</summary>
    GroupMemberAdd = 22,

    /// <summary>群成员退出（GROUP_MEMBER_REMOVE）。</summary>
    GroupMemberRemove = 23,

    /// <summary>互动事件（INTERACTION_CREATE）。</summary>
    Interaction = 30,
}
