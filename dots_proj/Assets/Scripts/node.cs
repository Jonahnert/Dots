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
    /// <summary>
    /// 
    /// </summary>
    void OnMouseDown()
    {
        if (isUsed == false)
        {
            isUsed = true;
            //RaycastHit hit;
            if(GMScript != null)
            {
                if (GMScript.turnTaker == "P1")
                {
                    linesHit = Physics2D.LinecastAll(new Vector2(GMScript.P1Start.position.x, GMScript.P1Start.position.y), new Vector2(transform.position.x, transform.position.y));
                }
                else if (GMScript.turnTaker == "P2")
                {
                    linesHit = Physics2D.LinecastAll(new Vector2(GMScript.P2Start.position.x, GMScript.P2Start.position.y), new Vector2(transform.position.x, transform.position.y));
                }
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
                        Debug.Log("score ONE NODE");
                        // score 1 for opponent
                    }
                }
                //these are used to find the midpoint at which to spawn and stretch the line object
                float xDistance = 0f;
                float yDistance = 0f;
                float lineScale = 0f;
                if (GMScript.turnTaker == "P1")
                {
                    xDistance = GMScript.P1Start.position.x + transform.position.x;
                    yDistance = GMScript.P1Start.position.y + transform.position.y;
                    lineScale = Vector3.Distance(GMScript.P1Start.position, transform.position);
                }
                else if (GMScript.turnTaker == "P2")
                {
                    xDistance = GMScript.P2Start.position.x + transform.position.x;
                    yDistance = GMScript.P2Start.position.y + transform.position.y;
                    lineScale = Vector3.Distance(GMScript.P2Start.position, transform.position);
                }

                Vector2 midpoint = new Vector2((xDistance) / 2f, (yDistance) / 2f);
                GameObject newLine = (GameObject)Instantiate(GMScript.lineObj, midpoint, Quaternion.identity);
                newLine.transform.LookAt(transform);
                //newLine.transform.Rotate(0f, 90f, 0);
                //newLine.transform.eulerAngles = new Vector3(newLine.transform.rotation.x, newLine.transform.rotation.y, newLine.transform.rotation.z);
                //newLine.transform.eulerAngles = new Vector3(newLine.transform.rotation.x, 180, newLine.transform.rotation.z);
                if (GMScript.turnTaker == "P1") 
                {
                    newLine.GetComponent<lineScript>().owner = "P1";
                }
                else if (GMScript.turnTaker == "P2")
                {
                    newLine.GetComponent<lineScript>().owner = "P2";
                }
                //18.34f * Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

                //-1 in the x scale is to compensate for the width of the nodes
                newLine.transform.localScale = new Vector3(lineScale-1, newLine.transform.localScale.y, newLine.transform.localScale.z);
                if (GMScript.turnTaker == "P1")
                {
                    GMScript.P1Start = gameObject.transform;
                    GMScript.SwitchTurn("P1");
                }
                else if (GMScript.turnTaker == "P2")
                {
                    GMScript.P2Start = gameObject.transform;
                    GMScript.SwitchTurn("P2");
                }
         
            }
        }

      
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
