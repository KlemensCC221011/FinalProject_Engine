using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    [SerializeField] public PauseMenu pauseMenu;
    [SerializeField] public CollectionBar collectionBar;
    public int currentLevel;
    private string saveJson;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        saveJson = Path.Combine(Application.persistentDataPath, "Save.json");
    }


    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    IEnumerator LoadLevel()
    {

        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");



    }

    public void SaveToJson(int reached)
    {
        currentLevel = reached;
        SavingData data = new SavingData();
        data.LevelReached = reached;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveJson, json);
    }
    public int LoadFromJson()
    {
        if (File.Exists(saveJson))
        {
            Debug.Log(saveJson);
            string json = File.ReadAllText(saveJson);
            SavingData data = JsonUtility.FromJson<SavingData>(json);
            currentLevel = data.LevelReached;
            return data.LevelReached;
        }
        else
        {
            return 0;
        }

        
    }


    public void CollectItem()
    {
        collectionBar.CollectItem();
    }
}
