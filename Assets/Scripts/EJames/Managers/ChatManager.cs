using System;
using EJames.Serializers;
using EJames.Utility;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace EJames.Managers
{
    public class ChatManager : SingletonNetwork<ChatManager>
    {
        [SerializeField] private TMP_Text _text;

        public void NewMessage()
        {
            var random = new NetworkStringSerializer { Value = DateTime.Now.ToLongTimeString() };
            UserMessageServerRpc(random);
        }

        [ServerRpc(RequireOwnership = false)]
        public void UserMessageServerRpc(NetworkStringSerializer stringSerializer)
        {
            OnClientRpc(stringSerializer);
        }

        [ClientRpc]
        public void OnClientRpc(NetworkStringSerializer stringSerializer)
        {
            _text.text = stringSerializer.Value;
        }
    }
}