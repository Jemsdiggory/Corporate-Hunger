using System.Collections.Generic;
using UnityEngine;

namespace CorporateHunger.Customer
{
    /// <summary>
    /// Data jadwal spawn pelanggan untuk satu Day/level.
    /// Setiap entry menentukan kapan (detik ke berapa)
    /// dan tipe pelanggan apa yang muncul.
    /// </summary>
    [CreateAssetMenu(fileName = "SpawnWave", menuName = "CorporateHunger/Spawn Wave")]
    public class SpawnWaveData : ScriptableObject
    {
        [System.Serializable]
        public class SpawnEntry
        {
            [Tooltip("Detik ke berapa pelanggan ini muncul sejak Day dimulai")]
            public float spawnTime;

            [Tooltip("Tipe pelanggan yang akan di-spawn")]
            public CustomerTypeData customerType;
        }

        public List<SpawnEntry> entries;
    }
}