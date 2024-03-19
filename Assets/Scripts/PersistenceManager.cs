using UnityEngine;
using System.Collections.Generic;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance;
    public SaveData data;

    public enum E_speedModifiers : int {
        SLOW = 1,
        MEDIUM = 2,
        FAST = 3
    }
    public Dictionary<E_speedModifiers, float> speedModifiers = new() {
        {E_speedModifiers.SLOW, 0.67f},
        {E_speedModifiers.MEDIUM, 1.0f},
        {E_speedModifiers.FAST, 1.5f},
    };

    public static float GetSpeedModifier() {
        if (Instance == null)
            return 1.0f;
        
        return Instance.speedModifiers[Instance.data.speedModifier];
    }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    public void UpdateHighscores(int score) {
        Highscore[] highscores;

        if (data.speedModifier == E_speedModifiers.SLOW)
            highscores = data.slowHighscores;
        else if (data.speedModifier == E_speedModifiers.MEDIUM)
            highscores = data.mediumHighscores;
        else
            highscores = data.fastHighscores;

        for (int i = 0; i < highscores.Length; ++i) {
            if (score > highscores[i].score) {
                // Move all highscores down 1 slot
                for (int j = highscores.Length - 1; j > i; --j) {
                    highscores[j] = highscores[j - 1];
                }
                // Set current slot to current player
                highscores[i].name = data.playerName;
                highscores[i].score = score;
                break;
            }
        }
    }

    public Highscore GetBestScore() {
        return Instance.data.speedModifier switch {
            E_speedModifiers.FAST => Instance.data.fastHighscores[0],
            E_speedModifiers.MEDIUM => Instance.data.mediumHighscores[0],
            E_speedModifiers.SLOW => Instance.data.slowHighscores[0],
            _ => new Highscore(),
        };
    }

    [System.Serializable]
    public struct Highscore {
        public int score;
        public string name;
    }

    [System.Serializable]
    public class SaveData {
        public string playerName;
        public E_speedModifiers speedModifier;
        public Highscore[] slowHighscores = new Highscore[10];
        public Highscore[] mediumHighscores = new Highscore[10];
        public Highscore[] fastHighscores = new Highscore[10];
    }
}
