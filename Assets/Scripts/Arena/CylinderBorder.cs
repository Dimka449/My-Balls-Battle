using UnityEngine;

public class CylinderBorder : MonoBehaviour
{
    [SerializeField] private int segments = 20; // Количество сегментов круга
    [SerializeField] private float borderHeight = 0.01f; // Высота границы над цилиндром
    [SerializeField] private float borderWidth = 0.5f;
    private LineRenderer lineRenderer;
    private float currentRadius;

    void Start()
    {
        // Создаем LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.loop = true;
        lineRenderer.startWidth = borderWidth;
        lineRenderer.endWidth = borderWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        // Получаем начальный радиус цилиндра
        currentRadius = GetComponent<MeshFilter>().mesh.bounds.extents.x;
        UpdateBorder();
    }

    void Update()
    {
        // Обновляем границу если радиус изменился
        float newRadius = transform.localScale.x / 2;
        if (Mathf.Abs(newRadius - currentRadius) > 0.001f)
        {
            currentRadius = newRadius;
            UpdateBorder();
        }
    }

    void UpdateBorder()
    {
        // Получаем высоту цилиндра (верхняя грань)
        float cylinderHeight = transform.localScale.y;
        Vector3 topCenter = transform.position + Vector3.up * cylinderHeight;

        // Рисуем круг на верхней грани
        for (int i = 0; i <= segments; i++)
        {
            float angle = 2f * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * currentRadius;
            float z = Mathf.Sin(angle) * currentRadius;

            Vector3 point = topCenter + new Vector3(x, borderHeight, z);
            lineRenderer.SetPosition(i, point);
        }
    }
}
