using System.Threading.Tasks;
using CodyTrakker.Behaviours;
using CodyTrakker.Behaviours.Config;
using GorillaNetworking;
using UnityEngine;

namespace CodyTrakker.Utils
{
    public static class WebhookUtils
    {
        public static async Task SendGameStarted()
        {
            await Broadcast(async h =>
            {
                h.NewMessage();
                h.AddEmbed("Game Started", $"{ConfigManager.Name.Value} started {ConfigManager.Pronoun.Value.ToLower()} game", "#00FF00");
                await h.SendAsync();
            });
        }

        public static async Task SendGameClosed()
        {
            await Broadcast(async h =>
            {
                h.NewMessage();
                h.AddEmbed("Game Closed", $"{ConfigManager.Name.Value} left {ConfigManager.Pronoun.Value.ToLower()} game", "#FF0000");
                await h.SendAsync();
            });
        }

        public static async Task SendRoomJoined(string desc)
        {
            await Broadcast(async h =>
            {
                h.NewMessage();
                h.AddEmbed("Room Joined", desc, "#00FF00");
                h.AddField("Room:", NetworkSystem.Instance.RoomName);
                h.AddField("Username:", NetworkSystem.Instance.LocalPlayer?.NickName);
                h.AddField("Players in room:", NetworkSystem.Instance.RoomPlayerCount.ToString());
                h.AddField("Map:", GetMap());
                h.AddField("Gamemode:", GetGamemode());
                h.AddField("Queue:", GorillaComputer.instance.currentQueue);
                await h.SendAsync();
            });
        }

        public static async Task SendRoomLeft(string desc)
        {
            await Broadcast(async h =>
            {
                h.NewMessage();
                h.AddEmbed("Room Left", desc, "#FFFF00");
                await h.SendAsync();
            });
        }

        private static async Task Broadcast(System.Func<DiscordUtils, Task> action)
        {
            foreach (var h in Core.helpers)
            {
                try
                {
                    await action(h);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }

        private static string GetGamemode()
        {
            var s = NetworkSystem.Instance.GameModeString?.ToLower() ?? "";
            bool modded = NetworkSystem.Instance.InRoom && s.Contains("modded");

            if (s.Contains("casual")) return modded ? "[M] Casual" : "Casual";
            if (s.Contains("infection")) return modded ? "[M] Infection" : "Infection";
            if (s.Contains("battle")) return modded ? "[M] Paintbrawl" : "Paintbrawl";
            if (s.Contains("freeze tag")) return modded ? "[M] Freeze Tag" : "Freeze Tag";
            if (s.Contains("hunt")) return modded ? "[M] Hunt" : "Hunt";
            if (s.Contains("guardian")) return modded ? "[M] Guardian" : "Guardian";

            return "Unknown";
        }

        private static string GetMap()
        {
            var s = NetworkSystem.Instance.GameModeString?.ToLower() ?? "";

            if (s.Contains("forest")) return "Forest";
            if (s.Contains("canyon")) return "Canyons";
            if (s.Contains("cave")) return "Caves";
            if (s.Contains("mines")) return "Mines";
            if (s.Contains("beach")) return "Beach";
            if (s.Contains("city")) return "City";
            if (s.Contains("mountain")) return "Mountains";
            if (s.Contains("arcade")) return "Arcade";
            if (s.Contains("rotating")) return "Rotating";

            return "Private/Other";
        }
    }
}