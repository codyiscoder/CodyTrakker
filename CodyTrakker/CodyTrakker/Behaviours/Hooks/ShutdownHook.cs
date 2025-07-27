using CodyTrakker.Utils;
using UnityEngine;

namespace CodyTrakker.Behaviours.Hooks
{
    public class ShutdownHook : MonoBehaviour
    {
        private async void OnApplicationQuit()
        {
            await WebhookUtils.SendGameClosed();
            foreach (var h in Core.helpers) h.Dispose();
        }
    }
}