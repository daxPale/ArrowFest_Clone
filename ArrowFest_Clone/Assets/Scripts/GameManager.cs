using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public Canvas scoreBoard;
    public Transform arrowPrefab;
    public Material redMaterial;
    public Material blueMaterial;

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

    public void UpdateScoreBoard(int arrows)
    {
        scoreBoard.GetComponentInChildren<Text>().text = arrows.ToString();
    }
}
