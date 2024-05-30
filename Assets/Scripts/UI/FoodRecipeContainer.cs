using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodRecipeContainer : MonoBehaviour
{
    [SerializeField] private Image _foodImage;
    [SerializeField] private RectTransform _foodItems;

    public void SetFoodImage(Sprite image) => _foodImage.sprite = image;

    public void AddIngredientImage(Image ingredientImage) => ingredientImage.transform.SetParent(_foodItems);
}
