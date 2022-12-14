using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationManager : MonoBehaviour
{

    public static NotificationManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<NotificationManager>();

            if(instance != null)
            {
                return instance;
            }

            CreateNewInstance();

            return instance;
        }
    }

    public static NotificationManager CreateNewInstance()
    {
        NotificationManager notificationManagerPrefab = Resources.Load<NotificationManager>("NotificationManager");
        instance = Instantiate(notificationManagerPrefab);
        return instance;
    }

    private static NotificationManager instance;

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
        }

    }

    [SerializeField] private TextMeshProUGUI notificationText;
    //[SerializeField] private float fadeTime;

    private IEnumerator notificationCoroutine;

    public void SetNewNotification(string message, float fadeTime)
    {
        if(notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
        }
        notificationCoroutine = FadeOutNotification(message, fadeTime);
        StartCoroutine(notificationCoroutine);
    }

    private IEnumerator FadeOutNotification(string message, float fadeTime)
    {
        notificationText.text = message;
        float t = 0;
        while(t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            notificationText.color = new Color(
                notificationText.color.r,
                notificationText.color.g,
                notificationText.color.b,
                Mathf.Lerp(1f, 0f, t / fadeTime));
            yield return null;
        }
    }

}
