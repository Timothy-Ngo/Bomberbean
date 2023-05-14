using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] RectTransform fader;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Shrink");
        fader.gameObject.SetActive(true);
        /*
        LeanTween.scale (fader, new Vector3 (1, 1, 1), 0);
        LeanTween.scale (fader, Vector3.zero, 0.5f).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => {
            fader.gameObject.SetActive (false);
        });
        */
        LeanTween.delayedCall(0.5f, () =>
        {
            LeanTween.scale(fader, Vector3.zero, 0.5f)
                .setEase(LeanTweenType.easeInOutQuad)
                .setOnComplete(() =>
                {
                    fader.gameObject.SetActive(false);
                });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
