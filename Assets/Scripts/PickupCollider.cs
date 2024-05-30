using System.Collections.Generic;

using UnityEngine;

public class PickupCollider : MonoBehaviour
{
    private List<PickupableObject> _pickubleObjects = new();
    private List<IngredientBox> _ingredientBoxes = new();
    private List<CuttingBoard> _cuttingBoards = new();
    private List<Stove> _stoves = new();
    private List<ServingArea> _servingAreas = new();

    public List<PickupableObject> GameObjectsInside => _pickubleObjects;
    public List<IngredientBox> IngredientBoxesInside => _ingredientBoxes;
    public List<Stove> Stoves => _stoves;
    public List<CuttingBoard> CuttingBoards => _cuttingBoards;
    public List<ServingArea> ServingAreas => _servingAreas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PickupableObject>(out var pickableObj)
            && !_pickubleObjects.Contains(pickableObj))
        {
            _pickubleObjects.Add(pickableObj);
        }
        else if (other.gameObject.TryGetComponent<IngredientBox>(out var ingBox)
            && !_ingredientBoxes.Contains(ingBox))
        {
            _ingredientBoxes.Add(ingBox);
        }
        else if (other.gameObject.TryGetComponent<CuttingBoard>(out var cuttingBoard)
            && !_cuttingBoards.Contains(cuttingBoard))
        {
            _cuttingBoards.Add(cuttingBoard);
        }
        else if (other.gameObject.TryGetComponent<Stove>(out var stove)
            && !_stoves.Contains(stove))
        {
            _stoves.Add(stove);
        }
        else if (other.gameObject.TryGetComponent<ServingArea>(out var servingArea)
            && !_servingAreas.Contains(servingArea))
        {
            _servingAreas.Add(servingArea);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PickupableObject>(out var pickableObj))
        {
            _pickubleObjects.Remove(pickableObj);
        }
        else if (other.gameObject.TryGetComponent<IngredientBox>(out var ingBox))
        {
            _ingredientBoxes.Remove(ingBox);
        }

        else if (other.gameObject.TryGetComponent<CuttingBoard>(out var cuttingBoard))
        {
            _cuttingBoards.Remove(cuttingBoard);
        }
        else if (other.gameObject.TryGetComponent<Stove>(out var stove))
        {
            _stoves.Remove(stove);
        }
        else if (other.gameObject.TryGetComponent<ServingArea>(out var servingArea))
        {
            _servingAreas.Remove(servingArea);
        }
    }
}
