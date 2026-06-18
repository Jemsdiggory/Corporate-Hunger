using UnityEngine;

namespace CorporateHunger.Customer
{
    /// <summary>
    /// Spawn pelanggan berdasarkan jadwal dari SpawnWaveData.
    /// </summary>
    public class CustomerSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnWaveData waveData;
        [SerializeField] private GameObject customerPrefab;
        [SerializeField] private Transform spawnPoint;

        private float dayTimer = 0f;
        private int nextSpawnIndex = 0;

        private void Update()
        {
            if (waveData == null) return;
            if (nextSpawnIndex >= waveData.entries.Count) return;

            dayTimer += Time.deltaTime;

            SpawnWaveData.SpawnEntry nextEntry = waveData.entries[nextSpawnIndex];

            if (dayTimer >= nextEntry.spawnTime)
            {
                SpawnCustomer(nextEntry.customerType);
                nextSpawnIndex++;
            }
        }

        private void SpawnCustomer(CustomerTypeData customerType)
        {
            GameObject customerObj = Instantiate(
                customerPrefab,
                spawnPoint.position,
                Quaternion.identity
            );

            CustomerController controller = customerObj.GetComponent<CustomerController>();
            controller.SetTypeData(customerType);

            QueueManager.Instance.AddToQueue(controller);

            Debug.Log($"Spawned: {customerType.typeName} pada detik {dayTimer:F1}");
        }
    }
}