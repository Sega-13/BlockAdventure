using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RequestNewShape : MonoBehaviour
{
    public int numberOfRequests = 3;
    public TextMeshProUGUI numberText;

    private int currentNumberOfRequests;
    private Button button;
    private bool isLocked;
    void Start()
    {
        currentNumberOfRequests = numberOfRequests;
        numberText.text = currentNumberOfRequests.ToString();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonDown);
        UnLock();
    }

    private void OnButtonDown()
    {
        if(isLocked == false)
        {
            currentNumberOfRequests--;
            GameEvents.RequestNewShapes();
            GameEvents.CheckIfPlayerLost();
            if(currentNumberOfRequests <= 0)
            {
                Lock() ;
            }
            numberText.text = currentNumberOfRequests.ToString();
        }
    }

    private void Lock()
    {
        isLocked = true;
        button.interactable = false;
        numberText.text = currentNumberOfRequests.ToString();
    }
    private void UnLock()
    {
        isLocked = false;
        button.interactable = true;
    }
}
