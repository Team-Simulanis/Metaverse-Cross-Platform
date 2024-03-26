using FishNet;
using UnityEngine;

namespace FF
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;


        public DefaultData defaultData = new();


        public UserData userData = new();

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateAvatarInfo(AvatarDetails avatarDetails)
        {
            userData.avatarDetails = avatarDetails;
        }
    }
}