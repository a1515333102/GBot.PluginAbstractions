namespace GBot.PluginAbstractions;

/// <summary>网关会话收发统计（自本次连接起）。</summary>
public readonly record struct GatewayTrafficInfo(
    long Sent,
    long Received,
    DateTimeOffset? ConnectedAt);
