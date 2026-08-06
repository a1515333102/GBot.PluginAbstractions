using System.Text.Json;

namespace GBot.PluginAbstractions;

/// <summary>官方 REST / 内部调用结果。</summary>
public sealed class ApiResponse
{
    public string Status { get; init; } = "failed";
    public int Retcode { get; init; } = -1;
    public string Message { get; init; } = "";
    public JsonElement? Data { get; init; }
    public string RawJson { get; init; } = "";

    public bool Ok => string.Equals(Status, "ok", StringComparison.OrdinalIgnoreCase) && Retcode == 0;

    public static ApiResponse Success(JsonElement? data = null, string raw = "") => new()
    {
        Status = "ok",
        Retcode = 0,
        Message = "ok",
        Data = data,
        RawJson = raw,
    };

    public static ApiResponse Fail(string message, int retcode = 1500) => new()
    {
        Status = "failed",
        Retcode = retcode,
        Message = message,
    };

    public static ApiResponse FromHttp(int statusCode, string raw)
    {
        try
        {
            using var doc = JsonDocument.Parse(string.IsNullOrWhiteSpace(raw) ? "{}" : raw);
            var root = doc.RootElement;
            JsonElement? data = root.Clone();

            if (statusCode is >= 200 and < 300)
            {
                // 官方错误体常带 code/message
                if (root.TryGetProperty("code", out var codeEl) && codeEl.TryGetInt32(out var code) && code != 0)
                {
                    var msg = root.TryGetProperty("message", out var m) ? m.GetString() ?? "" : "";
                    return new ApiResponse
                    {
                        Status = "failed",
                        Retcode = code,
                        Message = msg,
                        Data = data,
                        RawJson = raw,
                    };
                }

                return Success(data, raw);
            }

            var errMsg = root.TryGetProperty("message", out var em) ? em.GetString() ?? raw : raw;
            var errCode = root.TryGetProperty("code", out var ec) && ec.TryGetInt32(out var c) ? c : statusCode;
            return new ApiResponse
            {
                Status = "failed",
                Retcode = errCode,
                Message = errMsg ?? $"HTTP {statusCode}",
                Data = data,
                RawJson = raw,
            };
        }
        catch (Exception ex)
        {
            return statusCode is >= 200 and < 300
                ? Success(raw: raw)
                : Fail($"HTTP {statusCode}: {ex.Message}", statusCode);
        }
    }
}
