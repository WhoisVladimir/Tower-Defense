using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO:
// 1. Контроль состояний игры: запуск, завершение.
// 2. Подгрузка сцен в соответствии с состоянием игры.
// 3. Загрузка менеджеров.

public delegate void SceneChange(string sceneName);
public class GameManager : Singleton<GameManager>
{
    public static event SceneChange OnSceneChange;
    //public static event UnityAction OnSceneChanged;

    //Условные состояния игры.
    public enum GameState
    {
        PRE_GAME, IN_GAME, GAME_OVER
    }
    //Текущее состояние игры.
    public GameState CurrentGameState { get; private set; } = GameState.PRE_GAME;
    //Переменная для контроля игрового процесса.
    public bool IsGameActive { get; private set; }
    //Массив игровых систем и список для условной последующей работы с ними.
    [SerializeField] GameObject[] SystemPrefabs;
    List<GameObject> instancedSystemPrefabs;
    //Название текущей сцены.
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
    /// Вызов экзепляров систем и добавление их в список.
    /// </summary>
    void InstatiateSystemPrefabs()
    {
        foreach (var item in SystemPrefabs) instancedSystemPrefabs.Add(Instantiate(item));
    }

    /// <summary>
    /// Загрузка сцены.
    /// </summary>
    /// <param name="sceneToLoad"> Имя сцены для загрузки. </param>
    void LoadScene(string sceneToLoad)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        ao.completed += OnLoadOperationComplete;
        if (currentScene != null) UnloadScene(currentScene);
        currentScene = sceneToLoad;
    }

    /// <summary>
    /// Действия на событие загрузки сцены.
    /// </summary>
    /// <param name="operation"> Завершённая операция.</param>
    void OnLoadOperationComplete(AsyncOperation operation)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
        CurrentGameState = GameState.IN_GAME;
        InstatiateSystemPrefabs();
        OnSceneChange?.Invoke(currentScene);
    }

    /// <summary>
    /// Выгрузка отработанной сцены.
    /// </summary>
    /// <param name="sceneToUnload"> Имя сцены. </param>
    void UnloadScene(string sceneToUnload)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneToUnload);
        ao.completed += OnUnloadOperationComplete;
    }

    void OnUnloadOperationComplete(AsyncOperation operation)
    {
        // Потенциальное расширение.
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        foreach (var item in instancedSystemPrefabs) Destroy(item);
        instancedSystemPrefabs.Clear();
    }
}
