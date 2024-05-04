using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBox : MonoBehaviour
{
    [SerializeField] private PickupableObject _pfItemSpawned;

    public PickupableObject GetFoodItem()
    {
        return Instantiate(_pfItemSpawned);
    }
}
