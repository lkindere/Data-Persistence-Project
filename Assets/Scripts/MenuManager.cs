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
    private float speed;
    private enum E_scenes : int {
        START_MENU = 0,
        HIGH_SCORES = 1,
        MAIN = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = new(1.0f, 1.0f, 1.0f);
        selectColor = new(0.7f, 0.7f, 1.0f);
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
        if (speed == 0.0f) {
            warningText.text = "Speed not selected!";
            warningText.gameObject.SetActive(true);
            StartCoroutine(HideWarning());
            return;
        }
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
        speed = 0.5f;
        SetButtonColors(0);
    }

    public void SetMedium() {
        speed = 1.0f;
        SetButtonColors(1);
    }

    public void SetFast() {
        speed = 1.5f;
        SetButtonColors(2);
    }
}
