using UnityEngine;

namespace MiniGame.DodgeMiniGame
{
    public class DodgeGameManager : MonoBehaviour
    {
        [SerializeField] public GameObject player, enemy;
        [SerializeField] private int enemyCount, space = 10;
        [SerializeField] private float delay = 5f;
        private Vector2 _panelSize;

        private void Start()
        {
            _panelSize = gameObject.GetComponent<RectTransform>().sizeDelta;
            for (var i = 0; i < enemyCount; i++)
            {
                SpawnEnemies();
            }
        }

        private void SpawnEnemies()
        {
            var spawnx = Random.Range(1, 2) % 2 == 0; 
            var spawny = Random.Range(1, 2) % 2 == 0;

            Instantiate(enemy,
                new Vector2(
                    spawnx ? _panelSize.x - 20 : _panelSize.x + 20,
                    spawny ? _panelSize.y - 20 : _panelSize.y + 20
                ),
                transform.rotation
            );
        }
    }
}
