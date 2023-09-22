using EJames.Models;
using UnityEngine;

namespace EJames.Presenters
{
    public class ResourceCardPresenter : MonoBehaviour
    {
        [SerializeField]
        private GameObject _selection;

        private static Vector3 _flipAngles = new Vector3(0, 0, 180);

        public Resource Resource { get; set; }


        public void Flip()
        {
            transform.Rotate(_flipAngles);
        }

        public void Select()
        {
            _selection.SetActive(true);
        }

        public void Deselect()
        {
            _selection.SetActive(false);
        }

        protected void OnEnable()
        {
            Deselect();
        }
    }
}
