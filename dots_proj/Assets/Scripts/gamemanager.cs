using UnityEngine;
using System.Collections;

public class gamemanager : MonoBehaviour {
    public Transform P1Start;
    public Transform P2Start;
    public GameObject lineObj;
    public string turnTaker = "P1";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwitchTurn(string turnID)
    {
        if (turnID == "P1")
        {
            turnTaker = "P2";
            Debug.Log("Player 2 turn");
        }
        else if (turnID == "P2")
        {
            turnTaker = "P1";
            Debug.Log("Player 1 turn");
        }
    }
}
