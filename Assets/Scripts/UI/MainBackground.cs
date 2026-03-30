using UnityEngine;

public class MainBackground : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        transform.position = player.position + offset;
    }
}
