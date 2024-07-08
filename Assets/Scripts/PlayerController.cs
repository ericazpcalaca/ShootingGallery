using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _targetIndicator;
    [SerializeField] private float _raycastDistance = 15f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.DrawLine(_targetIndicator.position, _targetIndicator.forward * _raycastDistance, Color.cyan, 3);
            if (Physics.Raycast(_targetIndicator.position, _targetIndicator.forward, _raycastDistance))
            {
                Debug.Log("Hit!!!!");
            }
            else
            {
                Debug.Log("No hit :(");
            }
        }
    }
}
