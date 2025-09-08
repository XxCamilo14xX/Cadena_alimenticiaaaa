using System.Collections.Generic;
using UnityEngine;

public class PopulationSpawner : MonoBehaviour
{
    public EcosystemSimulator sim;

    public GameObject plantPrefab;
    public GameObject herbPrefab;
    public GameObject carnPrefab;

    public Rect spawnArea = new Rect(-8, -4, 16, 8);
    public float unitsPerEntity = 5f;   // 1 objeto en pantalla = 5 individuos reales
    public int maxEntities = 200;       // para no reventar FPS

    List<GameObject> plants = new();
    List<GameObject> herbs = new();
    List<GameObject> carns = new();

    void Update()
    {
        Sync(plants, Mathf.RoundToInt(sim.P / unitsPerEntity), plantPrefab);
        Sync(herbs,  Mathf.RoundToInt(sim.H / unitsPerEntity), herbPrefab);
        Sync(carns,  Mathf.RoundToInt(sim.C / unitsPerEntity), carnPrefab);
    }

    void Sync(List<GameObject> pool, int target, GameObject prefab)
    {
        target = Mathf.Min(target, maxEntities);

        // Crear si faltan
        while (pool.Count < target)
        {
            var go = Instantiate(prefab, RandomPoint(), Quaternion.identity, transform);
            pool.Add(go);
        }

        // Activar/desactivar según la población
        for (int i = 0; i < pool.Count; i++)
        {
            pool[i].SetActive(i < target);
        }
    }

    Vector3 RandomPoint()
    {
        float x = Random.Range(spawnArea.xMin, spawnArea.xMax);
        float y = Random.Range(spawnArea.yMin, spawnArea.yMax);
        return new Vector3(x, y, 0);
    }
}

