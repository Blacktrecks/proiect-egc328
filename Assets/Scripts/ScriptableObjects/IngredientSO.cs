using Assets.Scripts;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientSO", menuName = "ScriptableObjects/IngredientSO")]
public class IngredientSO : ScriptableObject
{
    public string id = Guid.NewGuid().ToString();
    public string ingredientName = "Ingredient Name";
    public ObjectType objectType;
    public Sprite sprite;
}
