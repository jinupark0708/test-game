using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    public AudioSource audioSource; // ����� �ҽ� ����
    public AudioClip[] bgmClips; // �� ���� BGM�� ������ �迭

    void Start()
    {
        DontDestroyOnLoad(gameObject); // �� ��ȯ �� ������Ʈ ����
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