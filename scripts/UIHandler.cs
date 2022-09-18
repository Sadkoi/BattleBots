using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public botBehaviour enemyBot;
    public botBehaviour playerBot;
    public Text playerHealth;
    public Text enemyHealth;
    public GameObject controlsBar;
    public GameObject restartIns;
    public gameBehaviour gameController;

    private bool controlsBarisActive;

    // Start is called before the first frame update
    void Start()
    {
        controlsBarisActive = false;
        controlsBar.SetActive(true);
        restartIns.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.fixedTime < 9){
            controlsBar.SetActive(true);
        }else{
            if(Time.fixedTime > 9 && Time.fixedTime < 10){
                controlsBar.SetActive(false);
            }
        }
        playerHealth.text = "" + Mathf.Clamp(100,0,(int)playerBot.health);
        enemyHealth.text = "" + Mathf.Clamp(100,0,(int)enemyBot.health);

        if(Input.GetKeyDown(KeyCode.H) && Time.fixedTime > 10){
            if(controlsBarisActive){
                controlsBar.SetActive(false);
                controlsBarisActive = false;
            }else{
                controlsBar.SetActive(true);
                controlsBarisActive = true;
            }
        }

        if(!gameController.gameNotEnded){
            restartIns.SetActive(true);
        }
    }
}
