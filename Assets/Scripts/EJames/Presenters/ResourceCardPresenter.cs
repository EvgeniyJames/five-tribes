using EJames.Models;
using UnityEngine;

namespace EJames.Presenters
{
    public class ResourceCardPresenter : MonoBehaviour
    {
        private static Vector3 _flipAngles = new Vector3(0, 0, 180);

        public Resource Resource { get; set; }

        public void Flip()
        {
            transform.Rotate(_flipAngles);
        }
    }
}
