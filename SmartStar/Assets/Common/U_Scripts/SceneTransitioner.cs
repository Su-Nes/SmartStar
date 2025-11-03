using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner Instance;

    private Image overlay;
    [SerializeField] private float crossFadeTime = 1.5f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else 
        {
            Destroy(gameObject);
        }
            
        overlay = GetComponent<Image>();    
        overlay.enabled = true; // enable 'cause I've probably disabled the image in editor
        overlay.raycastTarget = false;
        overlay.CrossFadeAlpha(0, crossFadeTime, false);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneWithTransition(sceneIndex));
    }
    
    private IEnumerator LoadSceneWithTransition(int sceneIndex)
    { 
        overlay.raycastTarget = true; // so ya can't click buttons while transitioning
        overlay.CrossFadeAlpha(1, crossFadeTime, true);
        
        yield return new WaitForSeconds(crossFadeTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
