using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameBehaviour : MonoBehaviour
{
    public GameObject[] blades;
    public GameObject[] lights;
    public GameObject drone;
    public GameObject enemy;
    public GameObject player;

    public droneBehaviour WinnerDrone;
    public droneBehaviour loserDrone;


    public bool gameNotEnded = true;
    private float time;
    private bool isdroneActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isdroneActive){
            droneControl();
        }
        time += Time.deltaTime;
        if(enemy == null){
            WinnerDrone.myTimeToShine = true;
            gameNotEnded = false;
            if(Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene("fighting");
            }
        }else{
            if(player == null){
                loserDrone.myTimeToShine = true;
                gameNotEnded = false;
                if(Input.GetKeyDown(KeyCode.R)){
                    SceneManager.LoadScene("fighting");
                }
            }
        }
    }

    void droneControl(){
        //spin the blades
        foreach(GameObject blade in blades){
            blade.transform.Rotate(0f, 2000f * Time.deltaTime, 0f, Space.Self);
        }
        //lights
        if(((int)(time * 2)) % 2 == 0){
            lights[0].SetActive(true);
            lights[1].SetActive(false);
            lights[2].SetActive(true);
            lights[3].SetActive(false);
        }else{
            lights[0].SetActive(false);
            lights[1].SetActive(true);
            lights[2].SetActive(false);
            lights[3].SetActive(true);
        }
        //sideToside Bobbing
        float zswivel = 4 * Mathf.Sin(0.8f * Mathf.PI * time);
        drone.transform.eulerAngles = new Vector3(drone.transform.eulerAngles.x,drone.transform.eulerAngles.y, zswivel);
        if(time < 9f){
            //bobbing
            float bobby = 0.07f * Mathf.Sin(Mathf.PI * time) + 1.65f;
            drone.transform.position = new Vector3(drone.transform.position.x, bobby ,drone.transform.position.z);
        }else{
            if(time < 12){
                drone.transform.Translate(0f, Mathf.Pow(time - 9, 4) * Time.deltaTime, 0f);
            }else{
                isdroneActive = false;
                Destroy(drone);
            }
        }

    }
}
