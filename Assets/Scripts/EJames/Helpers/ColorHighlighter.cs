#region

using DG.Tweening;
using UnityEngine;

#endregion

namespace EJames.Helpers
{
    public class ColorHighlighter : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Color _offColor = new Color(1, 1, 1, 0);

        private Tweener _tweener;


        public void Highlight(Color color)
        {
            _spriteRenderer.color = color;

            StopTweener();
            _tweener = _spriteRenderer.transform.DOShakeScale(1f, Vector3.one);
        }


        public void OffHighlight()
        {
            _spriteRenderer.color = _offColor;
            StopTweener();
        }


        private void StopTweener()
        {
            if (_tweener != null)
            {
                _tweener.Kill(false);
                _tweener = null;
            }
        }


        protected void OnEnable()
        {
            OffHighlight();
        }
    }
}