# GBot.PluginAbstractions

GBot 插件契约（公开）。主程序 GBot 可为私有；插件作者引用本仓库即可开发与打包。

当前 ABI：`PluginAbstractionsVersion` = **1.5.0**（`Major = 1`，市场 `abstractionsMajor` 填 `1`）

## 引用方式

`xml
<ItemGroup>
  <ProjectReference Include="..\GBot.PluginAbstractions\src\GBot.PluginAbstractions\GBot.PluginAbstractions.csproj">
    <Private>false</Private>
  </ProjectReference>
</ItemGroup>
`

或把本仓 clone 到本地后 `dotnet add reference`。

**重要：** 打包进市场的 zip **不要**包含 `GBot.PluginAbstractions.dll`（`Private=false`）。

## 上架

见 https://github.com/a1515333102/GBot-PluginMarketplace/blob/master/CONTRIBUTING.md
