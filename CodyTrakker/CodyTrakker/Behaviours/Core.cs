using System.Collections.Generic;
using System.Linq;
using CodyTrakker.Behaviours.Config;
using CodyTrakker.Utils;
using UnityEngine;

namespace CodyTrakker.Behaviours
{
    public class Core : MonoBehaviour
    {
        public static List<DiscordUtils> helpers;

        private async void Awake()
        {
            ConfigManager.CreateConfig();
            helpers = ConfigManager.GetWebhookUrls()
                .Select(x => new DiscordUtils(x, $"{ConfigManager.Name.Value} Tracker", null))
                .ToList();

            await WebhookUtils.SendGameStarted();
        }
    }
}