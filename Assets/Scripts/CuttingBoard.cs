using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class CuttingBoard : MonoBehaviour
{
    [SerializeField] private Transform _cuttingBoardTransform;
    [SerializeField] private Animation _knifeAnimation;
    [SerializeField] private float _secondsToCut = 5.0f;
    [SerializeField] private Slider _cutProgressSlider;
    [SerializeField] private Canvas _canvas;
    
    private float _timePassedSinceCutStart = 0f;
    private PickupableObject _objectOnIt;
    private Coroutine _cutObjectCoro;
    private PlayerInteract _playerUsingIt;
    private Quaternion _initialKnifeRotation;

    public PickupableObject ObjectOnIt => _objectOnIt;

    private void Awake()
    {
        _initialKnifeRotation = _knifeAnimation.transform.rotation;
    }

    public PickupableObject RemoveObjectFromIt()
    {
        PickupableObject obj = _objectOnIt;
        _objectOnIt = null;
        _canvas.gameObject.SetActive(false);
        return obj;
    }

    public void PlaceObjectOnit(PickupableObject obj)
    {
        obj.DisableCollider();
        obj.transform.SetParent(_cuttingBoardTransform);
        obj.transform.position = _cuttingBoardTransform.transform.position;
        _objectOnIt = obj;
        _timePassedSinceCutStart = 0f;
        UpdateProgressBar();
        _canvas.gameObject.SetActive(true);
    }

    private void UpdateProgressBar()
    {
        _cutProgressSlider.value = _timePassedSinceCutStart / _secondsToCut;
    }

    public void StartCutting(PlayerInteract player)
    {
        _knifeAnimation.Play();
        _cutObjectCoro = StartCoroutine(CutObjectCoroutine());
        _playerUsingIt = player;
    }

    public void StopCutting()
    {
        if (_cutObjectCoro != null)
        {
            StopCoroutine(_cutObjectCoro);
        }

        _knifeAnimation.transform.rotation = _initialKnifeRotation;
        _knifeAnimation.Stop();
        _playerUsingIt = null;
    }

    private void OnCuttingComplete()
    {
        PickupableObject cuttedFood = Instantiate(_objectOnIt.CuttedVariant);
        Destroy(RemoveObjectFromIt().gameObject);

        _playerUsingIt.PutPickupableObjectInHands(cuttedFood);
        StopCutting();
    }

    public IEnumerator CutObjectCoroutine()
    {
        while(true)
        {
            _timePassedSinceCutStart += Time.deltaTime;
            UpdateProgressBar();
        
            if (_timePassedSinceCutStart >= _secondsToCut )
            {
                OnCuttingComplete();
                yield break;
            }

            yield return null;
        }
    }
}
