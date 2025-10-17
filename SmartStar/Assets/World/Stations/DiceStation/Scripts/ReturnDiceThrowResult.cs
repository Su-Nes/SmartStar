using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReturnDiceThrowResult : MonoBehaviour
{
    [SerializeField] private UnityEvent<Transform> onResultTF;
    [SerializeField] private UnityEvent<string> onResultString;
    [SerializeField] private Transform vectorParent;
    
    public void CheckResult()
    {
        float closestDot = 0f;
        Transform result = null;

        foreach (DotProductReader side in vectorParent.GetComponentsInChildren<DotProductReader>())
        {
            if (side.GetProduct() > closestDot)
            {
                closestDot = side.GetProduct();
                result = side.transform;
            }
        }

        onResultTF.Invoke(result);
        onResultString.Invoke(result.name);
    }

    public string GetResult()
    {
        float closestDot = 0f;
        Transform result = null;

        foreach (DotProductReader side in vectorParent.GetComponentsInChildren<DotProductReader>())
        {
            if (side.GetProduct() > closestDot)
            {
                closestDot = side.GetProduct();
                result = side.transform;
            }
        }
        
        return result.name;
    }
}
