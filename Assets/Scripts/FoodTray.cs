using System.Collections.Generic;
using UnityEngine;

public class FoodTray : MonoBehaviour
{
    [SerializeField] private RectTransform _ingredientsDisplay;

    private GameObject _foodObjectOnIt;
    private List<PickupableObject> _ingredients;

    public List<PickupableObject> Ingredients => _ingredients;

    public void PutFoodOnit(GameObject foodObject, List<PickupableObject> ingredients)
    {
        _foodObjectOnIt = Instantiate(foodObject, transform);

        _ingredients = ingredients;

        foreach(PickupableObject item in  _ingredients)
        {
            Instantiate(item.PfImageInsidePot, _ingredientsDisplay);
        }
    }
}
