using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
    // [SerializeField] GameObject sphare;

    [SerializeField] float DelayInLoading = 1f;
    [SerializeField] float DelayInNextLvl = 0.5f;
    [SerializeField] AudioClip SuccessSFX;
    [SerializeField] AudioClip CrashSFX;

    AudioSource audioSource;

    bool isTransitioning = false;
    void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) {
        if (isTransitioning == false)
        {
            switch (other.gameObject.tag) {
                case "Friendly":
                    Debug.Log("This is Friendly");
                    break;
                case "Finish":
                    Debug.Log("You Win");
                    StartSuccessSequence();
                    break;
                // case "Fuel":
                //  Debug.Log("You WiAre Fueled Up");
                // sphare.SetActive(false);
                // break;
                default:
                    Debug.Log("You Blew Up");
                    // ReloadLevel();
                    StartCrashSequence();
                    break;
            }
        }
       
    }

    void StartCrashSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(CrashSFX);
        Invoke("ReloadLevel", DelayInLoading);
        GetComponent<Movement>().enabled = false;
    }
    
    void StartSuccessSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(SuccessSFX);
        Invoke("LoadNextLevel", DelayInNextLvl);
        GetComponent<Movement>().enabled = false;
    }
    void ReloadLevel() {
        //SceneManager.LoadScene(0);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
