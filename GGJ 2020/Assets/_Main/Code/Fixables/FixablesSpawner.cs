using UnityEngine;
using System.Collections;

using Zenject;

using Game;
using Utilities.Inspector;

namespace Fixables
{
    public class FixablesSpawner : MonoBehaviour
    {
        #region FIELDS

        [Inject] private GameManager gameManager = null;
        [Inject] private Timer timer = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Vector4 spawnArea = default(Vector4);
        [SerializeField] private float spawnHeight = default(float);
        [SerializeField] private Transform fixablesParent = null;
        [SerializeField] private Fixable fixablePrefab = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private LayerMask fixablesLayer = default(LayerMask);
        [SerializeField] private float raycastRadius = 1.5f;
        [SerializeField] private float initialInterval = 5f;
        [SerializeField] private float intervalReduction = 0.05f;
        [SerializeField] private float accelerationFactor = 2f;
        [SerializeField] private float minimumInterval = 0.7f;
        [SerializeField] private float randomExtraIntervalRange = 0.1f;

        [Header("STATES")]
        [ReadOnly] [SerializeField] private float currentInterval = default(float);

        #endregion

        #region BEHAVIORS

        private void Awake()
        {
            gameManager.onStartGame += Restart;
            gameManager.onEndGame += Stop;
            timer.onFewTime += Accelerate;
        }

        private void OnDestroy()
        {
            gameManager.onStartGame -= Restart;
            gameManager.onEndGame -= Stop;
            timer.onFewTime -= Accelerate;
        }

        private void Restart()
        {
            currentInterval = initialInterval;
            StartCoroutine(SpawnRoutine());
        }

        private void Stop()
        {
            StopAllCoroutines();
        }

        private IEnumerator SpawnRoutine()
        {
            InstantiateFixable();
            yield return new WaitForSeconds(currentInterval + Random.Range(-randomExtraIntervalRange, randomExtraIntervalRange));
            currentInterval -= intervalReduction;
            ClampInterval();
            StartCoroutine(SpawnRoutine());
        }

        private void InstantiateFixable()
        {
            Vector3 position = default(Vector3);

            do
            {
                position = GetRandomPosition();
            }
            while (FixableRightBelow(position));

            Instantiate(fixablePrefab, position, Quaternion.identity, fixablesParent);
        }

        private Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(spawnArea.x, spawnArea.y), spawnHeight, Random.Range(spawnArea.z, spawnArea.w));
        }

        private bool FixableRightBelow(Vector3 position)
        {
            RaycastHit hit = default(RaycastHit);
            return Physics.SphereCast(position, raycastRadius, Vector3.down, out hit, Mathf.Infinity, fixablesLayer);
        }

        private void Accelerate()
        {
            currentInterval /= accelerationFactor;
            ClampInterval();
        }

        private void ClampInterval()
        {
            currentInterval = Mathf.Clamp(currentInterval, minimumInterval, Mathf.Infinity);
        }

        #endregion
    }
}
