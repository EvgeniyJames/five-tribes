#region

using UnityEngine;

#endregion

namespace EJames.Views
{
    public class BaseView<T> : MonoBehaviour
    {
        protected T Model { get; private set; }

        public void Init(T model)
        {
            Model = model;
            InitInternal();
        }

        protected virtual void InitInternal()
        {
        }
    }
}