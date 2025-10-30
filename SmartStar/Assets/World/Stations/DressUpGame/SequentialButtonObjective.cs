using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(PlayCombinedVoiceLine))]
public class SequentialButtonObjective : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private AudioClip[] audioSequence;
    private int buttonIndex;

    [SerializeField] private float nextButtonDelay = 1f;

    [SerializeField] private UnityEvent onAllButtonsPressed;

    private void Awake()
    {
        SelectNextButton();
    }

    private void SelectNextButton()
    {
        if (buttonIndex > buttons.Length - 1)
        {
            onAllButtonsPressed.Invoke();
            return;
        }
        
        SFXManager.Instance.PlaySFXClip(audioSequence[buttonIndex], SFXManager.VoiceCategory.VoiceLine);
        
        int i = 0;
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
            
            if(i == buttonIndex)
                button.onClick.AddListener(delegate{ObjectiveClicked(button);});
            else
                button.onClick.AddListener(IncorrectButtonClicked);
            i++;
        }

        buttonIndex++;
    }

    private void IncorrectButtonClicked()
    {
        AudioClip[] clips = { SFXManager.Instance.GetRandomIncorrectSound(), audioSequence[buttonIndex - 1]};

        if (audioSequence[buttonIndex - 1] == null)
        {
            clips[1] = GetComponent<PlayCombinedVoiceLine>().presetLines[1];
        }

        StartCoroutine(GetComponent<PlayCombinedVoiceLine>().PlayVoiceLines(clips));
    }

    private void ObjectiveClicked(Button button)
    {
        EventManager.Instance.InvokeOnCorrect();
        button.gameObject.SetActive(false);
        StartCoroutine(NextButtonDelay());
    }

    private IEnumerator NextButtonDelay()
    {
        yield return new WaitForSeconds(nextButtonDelay);
        SelectNextButton();
    }
}
