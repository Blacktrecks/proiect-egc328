using Assets.Scripts;

using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class Stove : MonoBehaviour
{
    private PickupableObject _toolOnIt;
    [SerializeField] private Transform _itemPlacePoint;
    [SerializeField] private List<PickupableObject> _itemInsideTool;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _itemsInsidePot;
    [SerializeField] private Slider _cookingProgressSlider;
    [SerializeField] private float _cookingTimePerItem = 6f;

    private float _cookingProgress = 0f;
    private Coroutine _cookingCoro;
    public PickupableObject ToolOnIt => _toolOnIt;

    public List<PickupableObject> IngredientsInside => _itemInsideTool;

    public bool TryPlaceItemOn(PickupableObject item)
    {
        if (_toolOnIt != null)
        {
            if (item.IsPottable && _itemsInsidePot.childCount < 3)
            {
                Instantiate(item.PfImageInsidePot, _itemsInsidePot);
                _itemInsideTool.Add(item);
                item.gameObject.SetActive(false);

                if (_cookingCoro != null) StopCoroutine(_cookingCoro);
                _cookingCoro = StartCoroutine(CookingCoro());
                return true;
            }
        }

        if (item.FoodType != ObjectType.Pot &&
            item.FoodType != ObjectType.Pan)
        {
            return false;
        }

        item.transform.position = _itemPlacePoint.transform.position;
        item.transform.SetParent(_itemPlacePoint);
        _canvas.gameObject.SetActive(true);
        _cookingProgress = 0f;
        _toolOnIt = item;
        UpdateCookingSlider();
        return true;
    }

    private void UpdateCookingSlider()
    {
        if (_itemsInsidePot.childCount > 0)
        {
            _cookingProgressSlider.value = GetCookingProgress();
        }
    }
    public void RemoveAllItems()
    {
        _itemInsideTool.Clear();
        _cookingProgress = 0f;

        for (int i = 0; i < _itemsInsidePot.childCount; i++)
        {
            Destroy(_itemsInsidePot.GetChild(i).gameObject);
        }

        UpdateCookingSlider();
    }
    public float GetCookingProgress() => _cookingProgress / (_cookingTimePerItem * _itemsInsidePot.childCount);

    public PickupableObject RemoveObjectFromIt()
    {
        if (_toolOnIt == null) return null;

        for(int i = 0; i < _itemsInsidePot.childCount; i++)
        {
            Destroy(_itemsInsidePot.GetChild(i).gameObject);
            Destroy(_itemInsideTool[i].gameObject);
        }

        _canvas.gameObject.SetActive(false);
        if (_cookingCoro != null) StopCoroutine(_cookingCoro);
        PickupableObject pickupableObject = _toolOnIt;
        _toolOnIt = null;

        return pickupableObject;
    }

    private IEnumerator CookingCoro()
    {
        while (true)
        {
            _cookingProgress += Time.deltaTime;
            UpdateCookingSlider();
            yield return null;

            if (GetCookingProgress() >= 1.0f)
            {
                yield break;
            }
        }
    }
}
