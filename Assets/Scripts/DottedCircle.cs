using UnityEngine;

public class DottedCircle : MonoBehaviour
{
    public int numDots = 20;
    public float radius = 5f;
    public GameObject dotPrefab; // Prefab for the dot

    private void Start()
    {
        CreateDottedCircle();
    }

    private void CreateDottedCircle()
    {
        float angleIncrement = 2f * Mathf.PI / numDots;

        for (int i = 0; i < numDots; i++)
        {
            float angle = i * angleIncrement;
            float x = transform.position.x + radius * Mathf.Cos(angle);
            float y = transform.position.y + radius * Mathf.Sin(angle);

            Vector3 dotPosition = new Vector3(x, y, transform.position.z);
            Instantiate(dotPrefab, dotPosition, Quaternion.identity, transform);
        }
    }
}