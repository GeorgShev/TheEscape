using System;
using System.Collections;
using Services;
using Services.PauseService;
using UnityEngine;

public class DestructibleObjects : MonoBehaviour
{

    private IPauseService _pauseService;
    private void Start()
    {
        _pauseService = AllServices.Container.Single<IPauseService>();
    }

    public void DestroyObject()
    {
        StartCoroutine(DestroyTimer());
    }
    
    private IEnumerator DestroyTimer()
    {
        float delay = 0.1f;
        float timer = 0f;

        while (timer < delay)
        {
            if (!_pauseService.IsPaused)
            {
                timer += Time.deltaTime;
            }

            yield return null;
        }
        gameObject.SetActive(false);
            
    }
}
