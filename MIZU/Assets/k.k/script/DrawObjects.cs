using UnityEngine;
using System.Collections.Generic;

public class DrawObjects : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject drawObjectPrefab;  // �u�������I�u�W�F�N�g��Prefab
    public float minDistance = 0.1f;     // �I�u�W�F�N�g��z�u����ŏ�����

    private List<GameObject> drawObjects;
    private Vector3 lastPosition;

    void Start()
    {
        drawObjects = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �I�u�W�F�N�g���폜���ă��Z�b�g
            foreach (var obj in drawObjects)
            {
                Destroy(obj);
            }
            drawObjects.Clear();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // �J��������̓K�؂ȋ�����ݒ�
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0f;

            if (drawObjects.Count == 0 || Vector3.Distance(worldPosition, lastPosition) > minDistance)
            {
                GameObject drawObject = Instantiate(drawObjectPrefab, worldPosition, Quaternion.identity);
                drawObjects.Add(drawObject);
                lastPosition = worldPosition;
            }
        }
    }
}
