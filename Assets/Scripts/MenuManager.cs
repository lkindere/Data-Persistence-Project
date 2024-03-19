using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private Button slowButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button fastButton;
    [SerializeField] private Button highscoresButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    private Color defaultColor; 
    private Color selectColor;
    private PersistenceManager.E_speedModifiers speedModifier;
    public enum E_scenes : int {
        START_MENU = 0,
        HIGH_SCORES = 1,
        MAIN = 2
    }

    void Start()
    {
        defaultColor = new(1.0f, 1.0f, 1.0f);
        selectColor = new(0.7f, 0.7f, 1.0f);

        PersistenceManager.Instance.Load();

        nameInput.text = PersistenceManager.Instance.data.playerName;
        switch (PersistenceManager.Instance.data.speedModifier) {
            case PersistenceManager.E_speedModifiers.SLOW: 
                SetSlow();
                break;
            case PersistenceManager.E_speedModifiers.MEDIUM:
                SetMedium();
                break;
            case PersistenceManager.E_speedModifiers.FAST:
                SetFast();
                break;
        }

    }

    private void SetButtonColors(int index) {
        Button[] buttons = {slowButton, mediumButton, fastButton};

        for (int i = 0; i < buttons.Length; ++i) {
            if (i == index)
                buttons[i].GetComponent<Image>().color = selectColor;
            else
                buttons[i].GetComponent<Image>().color = defaultColor;
        }
    }

    public void StartGame() {
        if (nameInput.text == string.Empty) {
            warningText.text = "Name not entered!";
            warningText.gameObject.SetActive(true);
            StartCoroutine(HideWarning());
            return;
        }
        if (speedModifier == 0) {
            warningText.text = "Speed not selected!";
            warningText.gameObject.SetActive(true);
            StartCoroutine(HideWarning());
            return;
        }
        
        PersistenceManager.Instance.data.speedModifier = speedModifier;
        PersistenceManager.Instance.data.playerName = nameInput.text;
        SceneManager.LoadScene((int)E_scenes.MAIN);
    }

    IEnumerator HideWarning() {
        yield return new WaitForSeconds(1);

        warningText.gameObject.SetActive(false);
    }

    public void StartHighscores() {
        SceneManager.LoadScene((int)E_scenes.HIGH_SCORES);
    }

    public void ExitGame() {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }

    public void SetSlow() {
        speedModifier = PersistenceManager.E_speedModifiers.SLOW;
        SetButtonColors(0);
    }

    public void SetMedium() {
        speedModifier = PersistenceManager.E_speedModifiers.MEDIUM;
        SetButtonColors(1);
    }

    public void SetFast() {
        speedModifier = PersistenceManager.E_speedModifiers.FAST;
        SetButtonColors(2);
    }
}
