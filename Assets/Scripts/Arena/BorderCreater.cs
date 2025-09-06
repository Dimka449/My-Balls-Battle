using UnityEngine;

public class BorderCreater : MonoBehaviour
{
    private const float ANGLE360 = 360f;

    [Header("Box settings")]
    [SerializeField] private int boxCount = 20;
    [SerializeField] private float boxHeight = 2f;
    [SerializeField] private float boxWidth = 0.05f;

    [Header("Required settings")]
    [SerializeField] private Transform arenaTransform;
    [SerializeField] private GameObject boxPrefab;

    private void Awake()
    {
        CreateBorder();
    }

    private void CreateBorder()
    {
        float spawnAngleStep = ANGLE360 / boxCount;
        float spawnAngleStepOffset = spawnAngleStep / 2;
        float boxLenthAddition = 2f * boxWidth * Mathf.Tan(spawnAngleStepOffset * Mathf.Deg2Rad);
        float boxLenth = Mathf.Sin(Mathf.PI / boxCount) + boxLenthAddition;
        float distanceFromCenter = arenaTransform.localScale.x / 2 * Mathf.Cos(Mathf.PI / boxCount) +
            boxWidth * arenaTransform.localScale.x / 2;

        Vector3 spawnScale = new Vector3(boxLenth, boxHeight, boxWidth);

        for (int i = 0; i < boxCount; i++)
        {
            float spawnAngle = i * spawnAngleStep + spawnAngleStepOffset;
            Quaternion spawnAngleQuaternion = Quaternion.Euler(0f, spawnAngle, 0f);
            Vector3 spawnPos = spawnAngleQuaternion * Vector3.forward * distanceFromCenter;
            GameObject obj = Instantiate(boxPrefab, spawnPos, spawnAngleQuaternion, transform);
            obj.transform.localScale = spawnScale;
        }
    }
}