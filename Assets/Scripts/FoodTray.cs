using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class FoodTray : MonoBehaviour
{
    [SerializeField] private RectTransform _ingredientsDisplay;

    private GameObject _foodObjectOnIt;
    private List<PickupableObject> _ingredients;

    public List<PickupableObject> Ingredients => _ingredients;

    private void Awake()
    {
        _ingredients = new();
    }

    public void PutFoodOnit(List<PickupableObject> ingredients)
    {
        var foodObject = GameplayManager.Instance.FindFood(
            ingredients.Select(el => el.FoodType).ToList(),
            GameplayManager.Instance.CurrentlyAvailableRecipes
        );
        _foodObjectOnIt = Instantiate(foodObject.PfObjectOnFoodTray, transform);

        _ingredients = ingredients;
            
        foreach (PickupableObject item in _ingredients)
        {
            Instantiate(item.PfImageInsidePot, _ingredientsDisplay);
        }
    }

    public void RemoveFood()
    {
        if (_foodObjectOnIt == null) return;

        for (int i = 0; i < _ingredientsDisplay.childCount; i++)
        {
            Destroy(_ingredientsDisplay.GetChild(i).gameObject);
        }
        _ingredients.Clear();

        Destroy(_foodObjectOnIt);
    }
}
