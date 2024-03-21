using System.Threading.Tasks;
using FishNet.Object;
using UnityEngine;

namespace Simulanis.Player
{
    //
    public class PlayerControlManager : NetworkBehaviour
    {
        public static PlayerControlManager Instance;
        public int userId;

        private void Awake()
        {
            userId = this.GetInstanceID();
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            SendInfo();
        }


        private async void SendInfo() // called on start , it add this player to the list whener this player is spawned
        {
            while (!GetComponent<NameTagHandler>().isUpdated)
            {
                await Task.Delay(100);
            }

            listplayerinfo.instance.addNewPlayer(this.OwnerId, GetComponent<NameTagHandler>().playerName, NetworkObject,
                IsOwner);
        }


        private void OnDestroy() //whenever this player is de-SPAWNS it removes itselfs from the player list 
        {
            listplayerinfo.instance.removeNewPlayer(this.OwnerId, this.name, NetworkObject);
            Debug.Log("destroyed");
        }
    }
}