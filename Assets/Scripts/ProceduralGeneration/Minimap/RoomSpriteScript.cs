using UnityEngine;
using UnityEngine.UI;

namespace ProceduralGeneration.Minimap
{
    [RequireComponent(typeof(Image))]
    public class RoomSpriteScript : MonoBehaviour
    {
        [Header("References")]
        public MinimapIconSet iconSet; 
        public RoomTypeMinimap roomType;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            AssignSprite();
        }

        public void AssignSprite()
        {
            if (iconSet == null)
            {
                Debug.LogWarning($"[{name}] No MinimapIconSet assigned!");
                return;
            }

            spriteRenderer.sprite = iconSet.GetSprite(roomType);
        }
    }
}