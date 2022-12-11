using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestNotificationButton : MonoBehaviour
{
    public Button buttonTestNotification;

    void Start()
    {
        buttonTestNotification.onClick.AddListener(NotificationOnClick);
    }

    void NotificationOnClick()
    {
        Debug.Log("You have clicked the button!");
        NotificationManager.Instance.SetNewNotification("You have clicked the button!", 5f);
    }

}
