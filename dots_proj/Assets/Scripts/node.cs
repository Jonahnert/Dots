using UnityEngine;
using System.Collections;

public class node : MonoBehaviour {
    public bool isUsed = false;
    public GameObject GameManager;
    public gamemanager GMScript;

    // Use this for initialization
    void Start()
    {
        GMScript = GameManager.GetComponent<gamemanager>();
    }
    private RaycastHit2D[] linesHit;
    void OnMouseDown()
    {
        if (isUsed == false)
        {
            isUsed = true;
            //RaycastHit hit;
            if(GMScript != null)
            {
                linesHit = Physics2D.LinecastAll(new Vector2(GMScript.P1Start.position.x, GMScript.P1Start.position.y), new Vector2(transform.position.x, transform.position.y));
                foreach (RaycastHit2D hit in linesHit)
                {
                    // layer 8 is for lines
                    if(hit.collider.gameObject.layer == 8)
                    {
                        if (hit.collider.gameObject.GetComponent<lineScript>().owner == GMScript.turnTaker)
                        {
                            //shield 1
                            Debug.Log("shield ONE");
                        }
                        else
                        {
                            //score 1 for opponent
                            Debug.Log("score ONE");
                        }
                    }
                    // layer 9 is for nodes
                    else if (hit.collider.gameObject.layer == 9)
                    {
                        hit.collider.gameObject.GetComponent<node>().isUsed = true;
                        // score 1 for opponent
                    }
                }
                float xDistance = GMScript.P1Start.position.x + transform.position.x;
                float yDistance = GMScript.P1Start.position.y + transform.position.y;
                Vector2 midpoint = new Vector2((xDistance) / 2f, (yDistance) / 2f);
                GameObject newLine = (GameObject)Instantiate(GMScript.lineObj, midpoint, Quaternion.identity);
                newLine.transform.LookAt(transform);
                float lineScale = Vector3.Distance(GMScript.P1Start.position, transform.position);
                    //18.34f * Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
                newLine.transform.localScale = new Vector3(1, 1, lineScale);
                if (GMScript.turnTaker == "P1")
                {
                    GMScript.P1Start = gameObject.transform;
                }
                else if (GMScript.turnTaker == "P2")
                {
                    GMScript.P2Start = gameObject.transform;
                }
            }
        }

      
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
