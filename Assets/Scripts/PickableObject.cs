using Assets.Scripts;

using UnityEngine;

public class PickupableObject : MonoBehaviour
{
    [SerializeField] private PickupableObject _pfObjectAfterCutting;
    [SerializeField] private FoodType _foodType;

    private Collider _collider;
    private Rigidbody _rb;

    public PickupableObject CuttedVariant => _pfObjectAfterCutting;
    public FoodType FoodType => _foodType;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
        _rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void EnableCollider()
    {
        _collider.enabled = true;
        _rb.constraints = RigidbodyConstraints.None;

    }
}
