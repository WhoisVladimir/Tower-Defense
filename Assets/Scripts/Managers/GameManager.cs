using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO:
// 1. �������� ��������� ����: ������, ����������.
// 2. ��������� ���� � ������������ � ���������� ����.
// 3. �������� ����������.

public delegate void SceneChange(string sceneName);
public class GameManager : Singleton<GameManager>
{
    public static event SceneChange OnSceneChange;
    //public static event UnityAction OnSceneChanged;

    //�������� ��������� ����.
    public enum GameState
    {
        PRE_GAME, IN_GAME, GAME_OVER
    }
    //������� ��������� ����.
    public GameState CurrentGameState { get; private set; } = GameState.PRE_GAME;
    //���������� ��� �������� �������� ��������.
    public bool IsGameActive { get; private set; }
    //������ ������� ������ � ������ ��� �������� ����������� ������ � ����.
    [SerializeField] GameObject[] SystemPrefabs;
    List<GameObject> instancedSystemPrefabs;
    //�������� ������� �����.
    string currentScene;

    protected override void Awake()
    {
        base.Awake();
        instancedSystemPrefabs = new List<GameObject>();
        
    }
    void Start()
    {
        LoadScene("Start");
    }
    
    /// <summary>
    /// ����� ���������� ������ � ���������� �� � ������.
    /// </summary>
    void InstatiateSystemPrefabs()
    {
        foreach (var item in SystemPrefabs) instancedSystemPrefabs.Add(Instantiate(item));
    }

    /// <summary>
    /// �������� �����.
    /// </summary>
    /// <param name="sceneToLoad"> ��� ����� ��� ��������. </param>
    void LoadScene(string sceneToLoad)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        ao.completed += OnLoadOperationComplete;
        if (currentScene != null) UnloadScene(currentScene);
        currentScene = sceneToLoad;
    }

    /// <summary>
    /// �������� �� ������� �������� �����.
    /// </summary>
    /// <param name="operation"> ����������� ��������.</param>
    void OnLoadOperationComplete(AsyncOperation operation)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
        CurrentGameState = GameState.IN_GAME;
        InstatiateSystemPrefabs();
        OnSceneChange?.Invoke(currentScene);
    }

    /// <summary>
    /// �������� ������������ �����.
    /// </summary>
    /// <param name="sceneToUnload"> ��� �����. </param>
    void UnloadScene(string sceneToUnload)
    {
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
