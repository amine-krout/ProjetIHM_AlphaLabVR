using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNotification : MonoBehaviour
{
    private void Start()
    {
        NotificationManager.Instance.SetNewNotification("Notification with 4s delay", 4f);
    }

}
