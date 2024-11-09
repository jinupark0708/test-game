using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel; // PauseMenuPanel ������Ʈ ����
    public GameObject settingsPanel;
    private bool isPaused = false;

    void Start()
    {
        // ���� ���� �� �Ͻ����� â�� ��Ȱ��ȭ
        pauseMenuPanel.SetActive(false);
    }


    void Update()
    {
        // ESC Ű �Է� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.Debug.Log("ESC Ű�� ���Ƚ��ϴ�");
            if (isPaused)
            {
                ResumeGame(); // �Ͻ����� ����
            }
            else
            {
                PauseGame(); // �Ͻ�����
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true); // �Ͻ����� �޴� ǥ��
        Time.timeScale = 0; // ���� �ð� ����
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // �Ͻ����� �޴� ����
        Time.timeScale = 1; // ���� �ð� �簳
        isPaused = false;
    }


    public void OpenSettings()
    {
        settingsPanel.SetActive(true); // SettingsPanel Ȱ��ȭ
        UnityEngine.Debug.Log("���� â ����"); // ���� â ���� �߰� ����
    }

    public void QuitGame()
    {
      
        UnityEngine.Debug.Log("���� ����");
        UnityEngine.Application.Quit();
        
    }
}