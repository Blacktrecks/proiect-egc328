using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PickupCollider : MonoBehaviour
{
    private List<PickupableObject> _pickubleObjects = new();
    private List<IngredientBox> _ingredientBoxes = new();
    private List<CuttingBoard> _cuttingBoards = new();

    public List<PickupableObject> GameObjectsInside => _pickubleObjects;
    public List<IngredientBox> IngredientBoxesInside => _ingredientBoxes;
    public List<CuttingBoard> CuttingBoardsInside => _cuttingBoards;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PickupableObject>(out var pickableObj) 
            && !_pickubleObjects.Contains(pickableObj))
        {
            _pickubleObjects.Add(pickableObj);
        }

        if (other.gameObject.TryGetComponent<IngredientBox>(out var ingBox) 
            && !_ingredientBoxes.Contains(ingBox))
        {
            _ingredientBoxes.Add(ingBox);
        }

        if (other.gameObject.TryGetComponent<CuttingBoard>(out var cuttingBoard) 
            && !_cuttingBoards.Contains(cuttingBoard))
        {
            _cuttingBoards.Add(cuttingBoard);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<PickupableObject>(out var pickableObj))
        {
            _pickubleObjects.Remove(pickableObj);
        }

        if (other.gameObject.TryGetComponent<IngredientBox>(out var ingBox))
        {
            _ingredientBoxes.Remove(ingBox);
        }

        if (other.gameObject.TryGetComponent<CuttingBoard>(out var cuttingBoard))
        {
            _cuttingBoards.Remove(cuttingBoard);
        }
    }
}
