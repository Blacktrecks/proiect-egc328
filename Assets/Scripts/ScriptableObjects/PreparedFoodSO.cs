using Assets.Scripts;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PreparedFoodSO", menuName = "ScriptableObjects/PreparedFoodSO")]
public class PreparedFoodSO : ScriptableObject
{
    public string id = Guid.NewGuid().ToString();
    public string foodName = "Food Name";
    public List<IngredientSO> ingredients = new();
    public Sprite sprite;
    public GameObject PfObjectOnFoodTray = null;
}