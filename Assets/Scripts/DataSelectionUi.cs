using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public enum PlayerID
{

    player1 = 0,
    player2 = 1,
    player3 = 2,
}

public enum LevelID
{

    level1 = 0,
    level2 = 1,
    level3 = 2,
}

public class DataSelectionUi : MonoBehaviour
{
    public PlayerID selectedPlayer;
    public LevelID selectedLevel;
    GameObject buttonSelection;
    GameObject levelSelectionButton;
    GameObject newLevelButtons;

     bool applyDlc = false;

    public void Start()
    {
        buttonSelection = GameObject.Find("PlayerButtons");
        levelSelectionButton = GameObject.Find("levelButtons");
        levelSelectionButton.SetActive(false);

    }

    public void OnEnable()
    {
        EventManager.dlcCheck += DlcCheck;
    }
    public void OnDisable()
    {
        EventManager.dlcCheck -= DlcCheck;
    }

    public void PlayerSelected(int idValue)
    {
        selectedPlayer = (PlayerID)idValue;


        levelSelectionButton.SetActive(true);
        EventManager.setPlayer.Invoke(selectedPlayer);
        buttonSelection.SetActive(false);
    }

    public void DlcCheck(bool hasDlc)
    {
        Debug.Log("hasDLC? " + hasDlc);
        if (hasDlc == true)
        {
            newLevelButtons = levelSelectionButton.transform.Find("newLevel").gameObject;
            newLevelButtons.SetActive(true);
            applyDlc = true;
            //EventManager.applyDlc.Invoke();
        }
    }
    public void levelSelected(int idValue)
    {
        selectedLevel = (LevelID)idValue;
        
        levelSelectionButton.SetActive(false);
       
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        DontDestroyOnLoad(this.gameObject);

        AsyncOperation operation = SceneManager.LoadSceneAsync("GPG214");

//        operation.allowSceneActivation = false;
       
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }

       // operation.allowSceneActivation = true;
  //      yield return operation;
       
        if (applyDlc == true)
        {
            EventManager.applyDlc.Invoke();
            Debug.Log("applyDLC");
        }
       EventManager.setLevel.Invoke(selectedLevel);

    }

    //load game
}
