using BepInEx;
using CodyTrakker.Behaviours;
using CodyTrakker.Behaviours.Hooks;
using CodyTrakker.Behaviours.Room;

namespace CodyTrakker
{
    [BepInPlugin(Constants.GUID, Constants.NAME, Constants.VERS)]
    public class Plugin : BaseUnityPlugin
    {
        private void Start()
        {
            gameObject.AddComponent<Core>();
            gameObject.AddComponent<RoomInfo>();
            gameObject.AddComponent<ShutdownHook>();
        }
    }

    public class Constants
    {
        public const string
            GUID = "net.cody.codytrakker",
            NAME = "CodyTrakker",
            VERS = "1.0.0"; // technically version 2.1.0, but i switched github accounts
    }
}