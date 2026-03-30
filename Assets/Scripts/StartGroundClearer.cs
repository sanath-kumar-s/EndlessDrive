using System.Collections;
using UnityEngine;

public class StartGroundClearer : MonoBehaviour
{
    [SerializeField] private LayerMask obstracleLayerMask;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f) return;


        RaycastHit hit;
        bool didHit = Physics.Raycast(transform.position, transform.forward, out hit, 1000f, obstracleLayerMask);


        if(hit.collider.gameObject != null)Destroy(hit.collider.gameObject);

    }

    
}
