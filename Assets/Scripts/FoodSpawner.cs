using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private int borderX = 9;
    [SerializeField] private int borderY = 9;
    [SerializeField] private Transform foodParent;

  private void Start()
    {
        SpawnFood();
    }

    public void SpawnFood()
    {
        int x = Random.Range(-borderX, borderX + 1);
        int y = Random.Range(-borderY, borderY + 1);
        Vector2 spawnPos = new Vector2(x, y);
        Instantiate(foodPrefab, spawnPos, Quaternion.identity, foodParent);
      
    }

  
}
