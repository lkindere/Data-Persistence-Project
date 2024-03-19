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
        SetText(slowText, PersistenceManager.Instance.data.slowHighscores);
        SetText(mediumText, PersistenceManager.Instance.data.mediumHighscores);
        SetText(fastText, PersistenceManager.Instance.data.fastHighscores);
    }

    private void SetText(TextMeshProUGUI text, PersistenceManager.Highscore[] scores) {
        text.text = string.Empty;

        for (int i = 0; i < scores.Length; ++i) {
            string line = i + 1 + ". " + scores[i].name + ": " + scores[i].score + '\n';
            text.text += line;
        }
    }

    public void StartMenu() {
        SceneManager.LoadScene((int)MenuManager.E_scenes.START_MENU);
    }
}
