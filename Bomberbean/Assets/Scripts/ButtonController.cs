using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class ButtonController : MonoBehaviour
{

    [SerializeField] RectTransform fader;

    private void Start() 
    {
        fader.gameObject.SetActive (true);

        LeanTween.scale (fader, new Vector3 (1, 1, 1), 0);
        LeanTween.scale (fader, Vector3.zero, 0.5f).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => {
            fader.gameObject.SetActive (false);
        });
    }

    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("Level 1");
        fader.gameObject.SetActive (true);

        LeanTween.scale (fader, Vector3.zero, 0f);
        LeanTween.scale (fader, new Vector3 (1, 1, 1), 0.5f).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => {
            SceneManager.LoadScene("Level 1");
        });
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else   
            Application.Quit();
        #endif
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        //SceneManager.LoadScene("MainMenu");
        fader.gameObject.SetActive (true);

        LeanTween.scale (fader, Vector3.zero, 0f);
        LeanTween.scale (fader, new Vector3 (1, 1, 1), 0.5f).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => {
            SceneManager.LoadScene("MainMenu");
        });
        
    }

    public void Instructions()
    {
        //SceneManager.LoadScene("Instructions");
        fader.gameObject.SetActive (true);

        LeanTween.scale (fader, Vector3.zero, 0f);
        LeanTween.scale (fader, new Vector3 (1, 1, 1), 0.5f).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => {
            SceneManager.LoadScene("Instructions");
        });
    }

    public void Credits()
    {
        //SceneManager.LoadScene("Credits");
        fader.gameObject.SetActive (true);

        LeanTween.scale (fader, Vector3.zero, 0f);
        LeanTween.scale (fader, new Vector3 (1, 1, 1), 0.5f).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => {
            SceneManager.LoadScene("Credits");
        });
    }


}
