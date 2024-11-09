using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    public AudioSource audioSource; // 오디오 소스 참조
    public AudioClip[] bgmClips; // 각 씬의 BGM을 저장할 배열

    void Start()
    {
        DontDestroyOnLoad(gameObject); // 씬 전환 시 오브젝트 유지
        PlayBGMForScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGMForScene(scene.buildIndex);
    }

    void PlayBGMForScene(int sceneIndex)
    {
        if (sceneIndex < bgmClips.Length && bgmClips[sceneIndex] != null)
        {
            audioSource.clip = bgmClips[sceneIndex];
            audioSource.Play();
        }
    }
}