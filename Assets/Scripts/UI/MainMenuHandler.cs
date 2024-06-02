using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private Canvas _gameModeCanvas;
    [SerializeField] private Canvas _creditsCanvas;

    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private Button _gameModeReturnButton;
    [SerializeField] private Slider _playerCountSlider;
    [SerializeField] private TextMeshProUGUI _playerCountText;
    [SerializeField] private Button _gameModeStartGameButton;
    [SerializeField] private TMP_Dropdown _dropDownLevel;

    [SerializeField] private Button _creditsReturnButton;

    private void Start()
    {
        _startGameButton.onClick.AddListener(OnStartButtonPressed);
        _optionsButton.onClick.AddListener(OnOptionsButtonPressed);
        _creditsButton.onClick.AddListener(OnCreditsButtonPressed);
        _exitButton.onClick.AddListener(OnExitButtonPressed);
        _creditsReturnButton.onClick.AddListener(OnCreditsReturnButtonPressed);
        _gameModeReturnButton.onClick.AddListener(OnGameModeReturnPressed);
        _playerCountSlider.onValueChanged.AddListener(OnSliderPlayerCountValueChanged);
        _gameModeStartGameButton.onClick.AddListener(OnGameModeStartGameButtonPressed);

        _mainMenuCanvas.gameObject.SetActive(true);
        _gameModeCanvas.gameObject.SetActive(false);
        _creditsCanvas.gameObject.SetActive(false);
    }

    private void OnStartButtonPressed()
    {
        _mainMenuCanvas.gameObject.SetActive(false);
        _gameModeCanvas.gameObject.SetActive(true);
    }

    private void OnGameModeReturnPressed()
    {
        _mainMenuCanvas.gameObject.SetActive(true);
        _gameModeCanvas.gameObject.SetActive(false);
    }

    private void OnSliderPlayerCountValueChanged(float newValue)
    {
        GameplayManager.PlayerCount = (int)newValue;
        _playerCountText.text = $"Numar Jucatori: {(int)newValue}";
    }

    private void OnOptionsButtonPressed()
    {

    }

    public void OnGameModeStartGameButtonPressed()
    {
        // in build settings, the levels start at index 2, the values start from 0
        SceneManager.LoadScene(_dropDownLevel.value + 2);
    }

    private void OnCreditsButtonPressed()
    {
        _creditsCanvas.gameObject.SetActive(true);
        _mainMenuCanvas.gameObject.SetActive(false);
    }

    private void OnExitButtonPressed()
    {
        Application.Quit();
    }

    private void OnCreditsReturnButtonPressed()
    {
        _creditsCanvas.gameObject.SetActive(false);
        _mainMenuCanvas.gameObject.SetActive(true);
    }
}
