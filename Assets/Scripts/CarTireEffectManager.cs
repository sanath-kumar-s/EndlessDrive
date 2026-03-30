using UnityEngine;

public class CarTireEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject tireMarkPrefab;
    [SerializeField] private Transform leftTireMarker, rightTireMarker;
    [SerializeField] private float spawnInterval = 0.2f;

    [SerializeField] private Vector3 leftTireMarkeOffset;
    [SerializeField] private Vector3 rightTireMarkeOffset;

    private float timer = 0f;

    private void Update()
    {
        MudEffectHandler();



    }

    //private void TireMarkHandler()
    //{
    //    timer += Time.deltaTime;
    //    if (timer >= spawnInterval)
    //    {
    //        SpawnTireMark(leftTireMarker.position + leftTireMarkeOffset, leftTireMarker);
    //        SpawnTireMark(rightTireMarker.position + rightTireMarkeOffset, rightTireMarker);
    //        timer = 0f;
    //    }
    //}

    //void SpawnTireMark(Vector3 position, Transform parent)
    //{
    //    GameObject mark = Instantiate(tireMarkPrefab, position, Quaternion.identity, parent);
    //    Destroy(mark, 10f); // optional cleanup
    //}

    private void MudEffectHandler()
    {
        if (PlayerMovement.Instance.IsGrounded())
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);

            }


        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }


        
    }
}
