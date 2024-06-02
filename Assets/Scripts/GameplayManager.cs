using Assets.Scripts;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] RectTransform _foodRecipeContainersHolder;
    [SerializeField] FoodRecipeContainer _pfFoodRecipeContainer;
    [SerializeField] Image _pfIngredientImage;

    [SerializeField] TextMeshProUGUI _textMoneyEarned;
    [SerializeField] TextMeshProUGUI _textTimeLeft;
    [SerializeField] Image _timeLeftClockImage;

    [SerializeField] List<PreparedFoodSO> _levelRecipes;

    [SerializeField] private float _secondsToFinish = 600;
    [SerializeField] private int _maxRecipesAtOnce = 7;
    [SerializeField] private int _moneyPerRecipe = 50;
    [SerializeField] private int _newRecipeIntervalSeconds = 30;

    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _doneButton;

    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _player2;

    [SerializeField] private Button _leaveGameButton;

    private List<PreparedFoodSO> _currentRecipes;
    private List<FoodRecipeContainer> _foodRecipeContainers;

    private DateTime _startTime;
    private DateTime _endTime;
    private int _moneyEarned = 0;

    public static GameplayManager Instance { get; private set; }
    public static int PlayerCount = 1;

    public List<PreparedFoodSO> CurrentlyAvailableRecipes => _levelRecipes;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(this);
    }

    private void Start()
    {
        _doneButton.onClick.AddListener(OnDoneButtonPressed);
        for (int i = 0; i < _foodRecipeContainersHolder.transform.childCount; i++)
        {
            Destroy(_foodRecipeContainersHolder.transform.GetChild(i).gameObject);
        }

        _currentRecipes = new();
        _foodRecipeContainers = new();
        _moneyEarned = 0;
        for (int i = 0; i < 3; i++)
        {
            PreparedFoodSO newRecipe = _levelRecipes.RandomElement();
            AddRecipe(newRecipe);
        }

        if (PlayerCount == 1)
        {
            Destroy(_player2);
        }

        StartCoroutine(LevelCountdown());

        _leaveGameButton.onClick.AddListener(() => SceneManager.LoadScene(0));
    }

    private void OnDoneButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    private void AddRecipe(PreparedFoodSO foodRecipe)
    {
        _currentRecipes.Add(foodRecipe);
        FoodRecipeContainer foodRecipeContainer = Instantiate(_pfFoodRecipeContainer, _foodRecipeContainersHolder);
        foodRecipeContainer.SetFoodImage(foodRecipe.sprite);

        foreach (var ingredient in foodRecipe.ingredients)
        {
            Image image = Instantiate(_pfIngredientImage);
            image.sprite = ingredient.sprite;

            foodRecipeContainer.AddIngredientImage(image);
        }

        _foodRecipeContainers.Add(foodRecipeContainer);
    }

    public void OnRecipeDelivered(List<ObjectType> ingredients)
    {
        var foodWaitingForServing = FindFood(ingredients, _currentRecipes);

        if (foodWaitingForServing == null) return;

        int foodIx = _currentRecipes.IndexOf(foodWaitingForServing);
        _currentRecipes.RemoveAt(foodIx);

        Destroy(_foodRecipeContainers[foodIx].gameObject);
        _foodRecipeContainers.RemoveAt(foodIx);

        _moneyEarned += _moneyPerRecipe;

        _textMoneyEarned.text = $"Money Earned: {_moneyEarned}";
    }

    public PreparedFoodSO FindFood(List<ObjectType> ingredients, List<PreparedFoodSO> availableRecipes)
    {
        ingredients = ingredients.OrderBy(el => el).ToList();

        foreach (PreparedFoodSO recipe in availableRecipes)
        {
            if (recipe.ingredients.Count != ingredients.Count) continue;

            var sortedRecipeIngredients = recipe.ingredients.OrderBy(i => i).ToList();

            bool found = true;
            for (int i = 0; i < recipe.ingredients.Count; i++)
            {
                if (sortedRecipeIngredients[i].objectType != ingredients[i])
                {
                    found = false;
                    break;
                }
            }

            if (found) return recipe;
        }

        return null;
    }

    private IEnumerator LevelCountdown()
    {
        _startTime = DateTime.Now;
        _endTime = _startTime.AddSeconds(_secondsToFinish);
        var lastSpawnedFoodTime = _startTime;

        while (true)
        {
            var now = DateTime.Now;
            var timeLeft = (float)(_endTime - now).TotalSeconds;

            _timeLeftClockImage.fillAmount = timeLeft / _secondsToFinish;

            int minutesLeft = (int)timeLeft / 60;
            int secondsLeft = (int)timeLeft % 60;

            var timeSinceLastFood = (int)(now - lastSpawnedFoodTime).TotalSeconds;

            if (timeSinceLastFood >= _newRecipeIntervalSeconds && _currentRecipes.Count < _maxRecipesAtOnce)
            {
                AddRecipe(_levelRecipes.RandomElement());
                lastSpawnedFoodTime = now;
            }

            _textTimeLeft.text = $"Time left: {minutesLeft:00}:{secondsLeft:00}";

            if (timeLeft <= 0)
            {
                _gameOverCanvas.gameObject.SetActive(true);
                _scoreText.text = $"Score: {_moneyEarned}";
                break;
            }
            yield return null;
        }
    }
}
