using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoninLabs
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField] private float initialHeight;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color compressedColor;

        private void Start()
        {
            initialHeight = transform.position.y;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                spriteRenderer.color = compressedColor;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                spriteRenderer.color = normalColor;
            }
        }
    }
}