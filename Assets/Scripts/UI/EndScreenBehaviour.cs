using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenBehaviour : StateInteractor
{
    private GameManager.GameState state;

    [SerializeField] private GameObject winPanel;

    [SerializeField] private GameObject losePanel;
    // Start is called before the first frame update
    void Start()
    {
        state = GameManager.Instance.CurrentState;

        if (state == GameManager.GameState.Lose)
        {
            losePanel.SetActive(true);
        }
        else
        {
            winPanel.SetActive(true);
        }
    }

    public void LoadMenu()
    {
        requestStateChange.RaiseEvent((int)GameManager.GameState.Menu);
        LoadManager.Instance.LoadSingle("Title");
    }

    protected override void OnStateChange(int arg0)
    {
        
    }
}
