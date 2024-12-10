using Unity.AI.Navigation;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [Header("生成速度(个/秒)")]
    public float spwanSpeed;

    private Transform[] spwanPoints;
    private GameObject[] enemies;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        spwanPoints = new Transform[transform.childCount];
        for (int i = 0; i < spwanPoints.Length; i++)
        {
            spwanPoints[i] = transform.GetChild(i);
        }

        enemies = Resources.LoadAll<GameObject>("Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameOver)
            return;
        
        
        timer += Time.deltaTime;
        if (timer >= (1f / spwanSpeed))
        {
            timer = 0;
            Spwan();
        }
    }

    void Spwan()
    {
        int idxOfPoint = Random.Range(0, spwanPoints.Length);
        int idxOfPrefab = Random.Range(0, enemies.Length);

        Instantiate(enemies[idxOfPrefab], spwanPoints[idxOfPoint].position, Quaternion.identity);
    }
}
