using CodyTrakker.Behaviours.Config;
using CodyTrakker.Utils;
using Photon.Pun;
using UnityEngine;

namespace CodyTrakker.Behaviours.Room
{
    public class RoomInfo : MonoBehaviour
    {
        bool sentJoin;
        bool sentLeave;
        bool hasBeenInRoom;

        private async void Update()
        {
            if (PhotonNetwork.InRoom && !sentJoin)
            {
                State.RoomCode = PhotonNetwork.CurrentRoom?.Name ?? "???";
                sentJoin = true;
                sentLeave = false;
                hasBeenInRoom = true;

                await WebhookUtils.SendRoomJoined($"{ConfigManager.Name.Value} joined `{State.RoomCode}`");
            }

            if (!PhotonNetwork.InRoom && hasBeenInRoom && !sentLeave)
            {
                sentLeave = true;
                sentJoin = false;

                await WebhookUtils.SendRoomLeft($"{ConfigManager.Name.Value} left `{State.RoomCode}`");
            }
        }
    }
}