using System.Collections;
using System.Collections.Generic;
using UnityEditor.SettingsManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SequentialButtonObjective : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private int buttonIndex;

    [SerializeField] private UnityEvent onAllButtonsPressed;

    private void Awake()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(delegate{ObjectiveClicked(button);});
        }
        
        SelectNextButton();
    }

    private void SelectNextButton()
    {
        if (buttonIndex > buttons.Length - 1)
        {
            onAllButtonsPressed.Invoke();
            return;
        }
        
        int i = 0;
        foreach (Button button in buttons)
        {
            button.interactable = i == buttonIndex;
            i++;
        }

        buttonIndex++;
    }

    private void ObjectiveClicked(Button button)
    {
        EventManager.Instance.InvokeOnCorrect();
        Destroy(button.gameObject);
        SelectNextButton();
    }
}
