using Assets.Scripts;

using UnityEngine;

public class PickupableObject : MonoBehaviour
{
    [SerializeField] private PickupableObject _pfObjectAfterCutting;
    [SerializeField] private ObjectType _foodType;
    [SerializeField] private bool _isCuttable = true;
    [SerializeField] private GameObject _pfImageInsidePot;
    [SerializeField] private bool _isPottable;
    [SerializeField] private GameObject _itemOnTray;

    private Collider _collider;
    private Rigidbody _rb;

    public PickupableObject CuttedVariant => _pfObjectAfterCutting;
    public ObjectType FoodType => _foodType;
    public bool IsCuttable => _isCuttable;
    public GameObject PfImageInsidePot => _pfImageInsidePot;
    public bool IsPottable => _isPottable;
    public GameObject ItemWhenOnTray => _itemOnTray;

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
