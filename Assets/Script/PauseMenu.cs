using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel; // PauseMenuPanel 오브젝트 참조
    public GameObject settingsPanel;
    private bool isPaused = false;

    void Start()
    {
        // 게임 시작 시 일시정지 창을 비활성화
        pauseMenuPanel.SetActive(false);
    }


    void Update()
    {
        // ESC 키 입력 감지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.Debug.Log("ESC 키가 눌렸습니다");
            if (isPaused)
            {
                ResumeGame(); // 일시정지 해제
            }
            else
            {
                PauseGame(); // 일시정지
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true); // 일시정지 메뉴 표시
        Time.timeScale = 0; // 게임 시간 정지
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // 일시정지 메뉴 숨김
        Time.timeScale = 1; // 게임 시간 재개
        isPaused = false;
    }


    public void OpenSettings()
    {
        settingsPanel.SetActive(true); // SettingsPanel 활성화
        UnityEngine.Debug.Log("설정 창 열기"); // 설정 창 로직 추가 가능
    }

    public void QuitGame()
    {
      
        UnityEngine.Debug.Log("게임 종료");
        UnityEngine.Application.Quit();
        
    }
}