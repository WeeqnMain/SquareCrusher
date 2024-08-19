using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool flipDirection;

    private float direction;

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * direction);
    }

    private void OnValidate()
    {
        direction = flipDirection ? -1 : 1;
    }
}
