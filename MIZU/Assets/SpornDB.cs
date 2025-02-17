using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject[] boxPrefabs; // �_���{�[����Prefab���i�[����z��
    public Transform spawnPoint;   // �X�|�[���ʒu
    public float spawnInterval = 2f; // �X�|�[���Ԋu

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBox();
            timer = 0f;
        }
    }

    void SpawnBox()
    {
        if (boxPrefabs.Length > 0 && spawnPoint != null)
        {
            // �z�񂩂烉���_����Prefab��I�����Đ���
            int randomIndex = Random.Range(0, boxPrefabs.Length);
            Instantiate(boxPrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
        }
    }
}

