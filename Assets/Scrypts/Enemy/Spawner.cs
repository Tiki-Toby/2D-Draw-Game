using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scrypts.Enemy
{
    public enum RespawnArea
    {
        Left,
        Right,
        Top
    }
    [Serializable]
    public struct UnitInfos
    {
        public RespawnArea respawnArea;
        public float respawnTimeout;
        public Enemy enemyPrefab;

        public UnitInfos(RespawnArea respawnArea, float respawnTimeout, Enemy enemyPrefab)
        {
            this.respawnArea = respawnArea;
            this.respawnTimeout = respawnTimeout;
            this.enemyPrefab = enemyPrefab;
        }
    }
    class Spawner : MonoBehaviour
    {
        [SerializeField] float depth;

        private Vector2 leftBottom, rightTop;
        void Start()
        {
            rightTop = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            leftBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)); 
        }
        public void InitUnitInfos(UnitInfos[] enemyPrefabs)
        {
            foreach (UnitInfos unit in enemyPrefabs)
                StartCoroutine(SpawnEnemy(unit));
        }
        private IEnumerator SpawnEnemy(UnitInfos unit)
        {
            yield return new WaitForSeconds(unit.respawnTimeout);
            Vector2 position = Vector2.zero;
            switch (unit.respawnArea)
            {
                case RespawnArea.Top:
                    position.y = UnityEngine.Random.Range(0, depth) + rightTop.y;
                    position.x = UnityEngine.Random.Range(leftBottom.x, rightTop.x);
                    break;
                case RespawnArea.Left:
                    position.y = UnityEngine.Random.Range(leftBottom.y, rightTop.y);
                    position.x = leftBottom.x - UnityEngine.Random.Range(0, depth);
                    break;
                case RespawnArea.Right:
                    position.y = UnityEngine.Random.Range(leftBottom.y, rightTop.y);
                    position.x = rightTop.x + UnityEngine.Random.Range(0, depth);
                    break;
            }
            Instantiate(unit.enemyPrefab, position, Quaternion.identity, transform);
        }
    }
}
