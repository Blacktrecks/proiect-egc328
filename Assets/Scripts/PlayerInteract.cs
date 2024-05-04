using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private PickupCollider _pickupCollider;
    [SerializeField] private Transform _holdingLocation;
    [SerializeField] private PlayerMovement _playerMovement;

    private PickupableObject _objectHeld = null;
    private CuttingBoard _currentlyUsedCuttingBoard = null;

    public void OnPickupObjectButton(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Canceled) return;
        if (_playerMovement.IsCutting) return;

        // drop currently held object
        if (_objectHeld != null)
        {
            List<CuttingBoard> cuttingBoards = _pickupCollider.CuttingBoardsInside;

            if (cuttingBoards.Count > 0)
            {
                CuttingBoard boardToPlace = null;
                foreach(CuttingBoard board in cuttingBoards)
                {
                    if (board.ObjectOnIt == null)
                    {
                        boardToPlace = board;
                        break;
                    }
                }

                if (boardToPlace != null)
                {
                    boardToPlace.PlaceObjectOnit(_objectHeld);
                    _pickupCollider.GameObjectsInside.Remove(_objectHeld);
                    _objectHeld = null;
                    return;
                }
            }

            _objectHeld.transform.SetParent(null);
            _objectHeld.EnableCollider();
            _objectHeld = null;
            return;
        }

        List<CuttingBoard> cuttingBoardsToTakeFrom = _pickupCollider.CuttingBoardsInside;
        foreach (CuttingBoard board in cuttingBoardsToTakeFrom)
        {
            if (board.ObjectOnIt != null)
            {
                PutPickupableObjectInHands(board.RemoveObjectFromIt());
                return;
            }
        }

        List<PickupableObject> possibleObjs = _pickupCollider.GameObjectsInside;
        
        if (possibleObjs.Count > 0 )
        {
            PickupableObject obj = possibleObjs[0];

            PutPickupableObjectInHands(obj);
            return;
        }

        List<IngredientBox> ingredientBoxes = _pickupCollider.IngredientBoxesInside;

        if (ingredientBoxes.Count > 0)
        {
            IngredientBox box = ingredientBoxes[0];
            PickupableObject newIngredient = box.GetFoodItem();
            PutPickupableObjectInHands(newIngredient);
            return;
        }
    }

    public void PutPickupableObjectInHands(PickupableObject pickupable)
    {
        pickupable.DisableCollider();
        pickupable.transform.SetParent(_holdingLocation);
        pickupable.transform.position = _holdingLocation.transform.position;
        _objectHeld = pickupable;
    }

    public void OnPlayerStartCutting(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Canceled) return;

        List<CuttingBoard> cuttingBoardsToTakeFrom = _pickupCollider.CuttingBoardsInside;
        foreach (CuttingBoard board in cuttingBoardsToTakeFrom)
        {
            if (board.ObjectOnIt != null)
            {
                board.StartCutting(this);

                _currentlyUsedCuttingBoard = board;
                _playerMovement.IsCutting = true;
                _playerMovement.PlayerMovedAwayFromCuttingBoard += OnMovedAwayFromBoard;
                return;
            }
        }
    }

    private void OnMovedAwayFromBoard()
    {
        _currentlyUsedCuttingBoard.StopCutting();
        _playerMovement.PlayerMovedAwayFromCuttingBoard -= OnMovedAwayFromBoard;
    }
}
