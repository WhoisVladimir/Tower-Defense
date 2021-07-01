using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public delegate void SceneChange(string sceneName);
public class GameManager : Singleton<GameManager>
{
    public static event SceneChange OnSceneChange;

    public enum GameState
    {
        //�������� ��������� ����.
        PRE_GAME, IN_GAME, GAME_OVER
    }
    public GameState CurrentGameState { get; private set; } = GameState.PRE_GAME;
    public bool IsGameActive { get; private set; }
    string currentScene;

    [SerializeField] GameObject[] SystemPrefabs;
    List<GameObject> instancedSystemPrefabs;
    
    protected override void Awake()
    {
        base.Awake();
        instancedSystemPrefabs = new List<GameObject>();
        
    }
    void Start()
    {
        LoadScene("Start");
    }
    
    void InstatiateSystemPrefabs()
    {
        // ����� ���������� ������ � ���������� �� � ������.

        foreach (var item in SystemPrefabs) instancedSystemPrefabs.Add(Instantiate(item));
    }

    void LoadScene(string sceneToLoad)
    {
        // �������� �����.

        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        ao.completed += OnLoadOperationComplete;
        if (currentScene != null) UnloadScene(currentScene);
        currentScene = sceneToLoad;
    }

    void OnLoadOperationComplete(AsyncOperation operation)
    {
        // �������� �� ������� �������� �����.

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
        CurrentGameState = GameState.IN_GAME;
        InstatiateSystemPrefabs();
        OnSceneChange?.Invoke(currentScene);
    }

    void UnloadScene(string sceneToUnload)
    {
        // �������� ������������ �����.

        AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneToUnload);
        ao.completed += OnUnloadOperationComplete;
    }

    void OnUnloadOperationComplete(AsyncOperation operation)
    {
        // ������������� ����������.
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        foreach (var item in instancedSystemPrefabs) Destroy(item);
        instancedSystemPrefabs.Clear();
    }
}
