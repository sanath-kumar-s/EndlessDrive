using UnityEngine;

public class Barriers : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z);
    }
}
