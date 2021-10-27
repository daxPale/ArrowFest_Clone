using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public CounterDisplay scoreBoard;
    public ScreenLoader gameStates;
    public Transform arrowPrefab;
    public Material redMaterial;
    public Material blueMaterial;
    
    private bool _isPlaying = false;
    public bool IsPlaying { get => _isPlaying; set { _isPlaying = value; } }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 3);
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
