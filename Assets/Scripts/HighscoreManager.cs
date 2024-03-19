using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighscoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slowText;
    [SerializeField] private TextMeshProUGUI mediumText;
    [SerializeField] private TextMeshProUGUI fastText;
    // Start is called before the first frame update
    void Start()
    {
        // Placeholder for testing
        Highscore[] slowScores = new Highscore[10];
        for (int i = 0; i < slowScores.Length; ++i) {
            slowScores[i].score = 10 - i;
            slowScores[i].name = "Bob";
        }
        SetText(slowText, slowScores);
    }

    private void SetText(TextMeshProUGUI text, Highscore[] scores) {
        text.text = string.Empty;

        for (int i = 0; i < scores.Length; ++i) {
            string line = i + 1 + ". " + scores[i].name + ": " + scores[i].score + '\n';
            text.text += line;
        }
    }

    public void StartMenu() {
        SceneManager.LoadScene((int)MenuManager.E_scenes.START_MENU);
    }

    // Placeholder for testing
    struct Highscore {
        public string name;
        public int score;
    }
}
