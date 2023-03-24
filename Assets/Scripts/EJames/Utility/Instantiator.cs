#region

using UnityEngine;
using Zenject;

#endregion

namespace EJames.Utility
{
    public class Instantiator
    {
        private DiContainer _diContainer;

        public Instantiator(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public T Instantiate<T>()
        {
            return _diContainer.Instantiate<T>();
        }

        public T InstantiatePrefab<T>(Object prefab, Transform parent) where T : Component
        {
            GameObject instantiatePrefab = _diContainer.InstantiatePrefab(prefab, parent);
            return instantiatePrefab.GetComponent(typeof(T)) as T;
        }

        public void DestroyGameObject(GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }

        public void Destroy<T>(T monoBehavior) where T : MonoBehaviour
        {
            DestroyGameObject(monoBehavior.gameObject);
        }
    }
}