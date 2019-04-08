using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public TextMeshPro score;
    private void Start()
    {
        string high = PlayerPrefs.GetString("High", "");
        float value = PlayerPrefs.GetFloat("Value", -1);
        if(high == "")
        {
            score.gameObject.SetActive(false);
        }
        else
        {
            score.text = high + "--> " + value;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.name == "Start")
                    {
                        SceneManager.LoadScene("MainScene");
                    }
                    else if (hit.collider.gameObject.name == "LeaderBoard")
                    {
                        SceneManager.LoadScene("LeaderBoard");
                    }
                }
            }
        }




       /* if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.gameObject.name == "Start")
                {
                    SceneManager.LoadScene("MainScene");
                }
                else if (hit.collider.gameObject.name == "LeaderBoard")
                {
                    SceneManager.LoadScene("LeaderBoard");
                }
            }
        }*/
    }
}
