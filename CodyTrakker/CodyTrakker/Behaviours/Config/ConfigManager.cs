using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;

namespace CodyTrakker.Behaviours.Config
{
    public class ConfigManager
    {
        private static ConfigFile config;

        public static ConfigEntry<bool> Enable;
        public static ConfigEntry<string> Name, Webhooks, Pronoun;

        public static void CreateConfig()
        {
            config = new ConfigFile(Path.Combine(Paths.ConfigPath, "SelfTrakker.cfg"), true);

            Enable = config.Bind("Settings", "Enable", true, "Enable or disable the mod.");
            Name = config.Bind("Settings", "Name", "Gorilla", "The name the self tracker displays. Please capitalize the first letter.");
            Webhooks = config.Bind("Settings", "Webhooks", "LINK LINK", "The webhooks the Discord messages are sent to. Multiple webhooks can be separated by spaces.");
            Pronoun = config.Bind("Settings", "Pronouns", "their", "Enter a pronoun like 'his', 'her', or 'their'.");
        }

        public static List<string> GetWebhookUrls()
        {
            return Webhooks.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}