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
        {E_speedModifiers.SLOW, 0.5f},
        {E_speedModifiers.MEDIUM, 1.0f},
        {E_speedModifiers.FAST, 1.5f},
    };

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    [System.Serializable]
    public class SaveData {
        public string playerName;
        public E_speedModifiers speedModifier;
    }
}