#region

using Unity.Netcode;
using UnityEngine;

#endregion

namespace EJames.Helpers
{
    public class BoxMoveAction : NetworkBehaviour
    {
        [SerializeField]
        private GameObject _box;

        [SerializeField]
        private Vector3 _direction;

        public void OnBoxMoveAction()
        {
            OnBoxMoveServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void OnBoxMoveServerRpc()
        {
            _box.transform.Translate(_direction);
        }
    }
}