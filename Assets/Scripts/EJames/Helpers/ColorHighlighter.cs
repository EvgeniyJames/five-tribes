#region

using UnityEngine;

#endregion

namespace EJames.Helpers
{
    public class ColorHighlighter : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Color _offColor = new Color(1, 1, 1, 0);

        public void Highlight(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void OffHighlight()
        {
            _spriteRenderer.color = _offColor;
        }

        protected void OnEnable()
        {
            OffHighlight();
        }
    }
}