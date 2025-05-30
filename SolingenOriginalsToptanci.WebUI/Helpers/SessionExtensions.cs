﻿using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace SolingenOriginalsToptanci.WebUI.Helpers
{
    public static class SessionExtensions
    {
        // Nesneyi JSON’a çevirip session’a kaydeder
        public static void SetObjectAsJson(this ISession session, string key, object value)
            => session.SetString(key, JsonSerializer.Serialize(value));

        // JSON’dan nesneye geri çevirir
        public static T? GetObjectFromJson<T>(this ISession session, string key)
            => session.GetString(key) is string json
                ? JsonSerializer.Deserialize<T>(json)
                : default;
    }
}
