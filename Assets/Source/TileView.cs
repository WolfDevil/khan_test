using System;
using UnityEngine;

namespace Source
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer squareSpriteRenderer;

        private Action _onClick;

        private void Setup(Color color, Action onClick)
        {
            _onClick += onClick;
            squareSpriteRenderer.color = color;
        }

        private void OnMouseDown()
        {
            _onClick?.Invoke();
        }

        private void OnDestroy()
        {
            _onClick = null;
        }
    }
}