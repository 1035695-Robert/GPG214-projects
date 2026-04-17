using UnityEngine;

public enum PlayerID
{
    None = 0,
    player1 = 1,
    player2 = 2,
    player3 = 3,
}

public class PlayerSelection : MonoBehaviour
{
    public PlayerID selectedPlayer;
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
}
