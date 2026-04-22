using UnityEngine;

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

public class DataSelection : MonoBehaviour
{
    public PlayerID selectedPlayer;
    public LevelID selectedLevel;
    GameObject buttonSelection;

    public void Start()
    {
        buttonSelection = GameObject.Find("buttonSelection");
    }
    public void PlayerSelected(int idValue)
    {
        selectedPlayer = (PlayerID)idValue;
        buttonSelection.SetActive(false);
        EventManager.setPlayer.Invoke(selectedPlayer);
    }
    public void levelSelected()
    {
       
    }
}
