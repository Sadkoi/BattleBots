using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroductorySequence : MonoBehaviour
{
    //In-Game Objects
    public GameObject camera;
    public GameObject fanblades;
    public GameObject saw;
    public GameObject saw2;
    public GameObject yellowBot;
    public GameObject greenBot;
    public GameObject drone;
    public GameObject mainMenu;
    public Transform marker1;
    public Transform marker2;
    public Transform marker3;


    //UI Elemenets
    public RawImage blackout;
    public GameObject blackoutGameObject;
    public RawImage SadkoiSplashScreen;
    
    public GameObject SadkoiGameObj;

    //protected vars
    private bool CameraMove1 = false;
    private bool CameraMove2 = false;
    private bool CameraMove3 = false;
    private bool PressAnyKey = false;
    private float timeOffset;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(false);
        StartCoroutine(theSequence());
    }

    // Update is called once per frame
    void Update()
    {
        cameraAnim();    
        loadNextScene();
    }

    void cameraAnim(){
        if(CameraMove1){
            camera.transform.Translate(-0.008f,0f,0f,Space.World);
            fanblades.transform.Rotate(0f,0f,1.5f, Space.Self);
        }else{
            if(CameraMove2){
                camera.transform.Translate(0f,0f,0.0003f,Space.World);
                saw.transform.Rotate(0f,5f,0f, Space.Self);
            }else{
                if(CameraMove3){
                //    saw.transform.Rotate(0f,7f,0f, Space.Self);
                //    saw2.transform.Rotate(0f,7f,0f, Space.Self);
                //    yellowBot.transform.Translate(0,0,0.004f, Space.Self);
                //    greenBot.transform.Translate(0,0,0.004f, Space.Self);
                //    float angSpeed = 0.1f * Mathf.Sin(Mathf.PI * 0.3f * (Time.fixedTime - timeOffset)) + 0.3f; 
                //    camera.transform.Rotate(0f,angSpeed, 0f, Space.Self);
                //}else{
                    //if(Time.fixedTime > 24){
                        //camera.transform.LookAt(drone.transform.position);
                        marker3.transform.LookAt(drone.transform.position - new Vector3(0f,0.8f,0f));
                        drone.transform.LookAt(marker3.transform.position);
                        drone.transform.eulerAngles = new Vector3(Mathf.Clamp(drone.transform.eulerAngles.x,0f,30f),drone.transform.eulerAngles.y,drone.transform.eulerAngles.z);
                        float droneX = -6.126f * (Time.time - 16f) + 0.49f;
                        float droneY =  (4 / ((1.219f * (Time.time - 15f)) + 1)) - 0.063f;
                        drone.transform.position = new Vector3(droneX,droneY,drone.transform.position.z);
                        //var rotation = Quaternion.LookRotation();
                        camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, marker3.transform.rotation, 0.05f);
                    //}
                }
            }
        }
    }

    void loadNextScene(){
        if(PressAnyKey && Input.anyKey){
            SceneManager.LoadScene("fighting");
        }
    }


    IEnumerator theSequence(){
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeImage(true, blackout)); // fade in Sadkoi splash screen
        yield return new WaitForSeconds(2.2f);
        StartCoroutine(FadeImage(false, blackout)); // fade out sadkoi splash screen
        yield return new WaitForSeconds(1.6f);
        Destroy(SadkoiGameObj); // start camera moving past warehouse fan
        camera.transform.position = marker1.position;
        camera.transform.rotation = marker1.rotation;
        CameraMove1 = true;
        StartCoroutine(FadeImage(true, blackout));
        yield return new WaitForSeconds(4f); 
        StartCoroutine(FadeImage(false, blackout)); // fade in blackout image
        yield return new WaitForSeconds(2.2f);
        camera.transform.position = marker2.position;
        camera.transform.rotation = marker2.rotation;      
        CameraMove1 = false;
        CameraMove2 = true;  
        StartCoroutine(FadeImage(true, blackout)); //fade out blackout image //fade in scene described in (4) previous lines
        yield return new WaitForSeconds(2.1f);
        StartCoroutine(FadeImage(false, blackout)); //fade in blackout image
        yield return new WaitForSeconds(2);
        camera.transform.position = marker3.position;
        camera.transform.rotation = marker3.rotation;
        CameraMove2 = false;
        CameraMove3 = true; 
        timeOffset = Time.fixedTime;
        blackoutGameObject.SetActive(false);
        yield return new WaitForSeconds(1.35f);
        blackoutGameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        mainMenu.SetActive(true);
        PressAnyKey = true;
        
    }


    IEnumerator FadeImage(bool fadeAway, RawImage img)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= (Time.deltaTime * 1f))
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
