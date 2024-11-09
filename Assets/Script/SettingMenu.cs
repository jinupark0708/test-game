using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public GameObject settingsPanel; // ���� â�� ��Ÿ���� �г�
    public Slider volumeSlider; // ���� ���� �����̴�
    public AudioSource bgmSource; // BGM ����� �ҽ� ����
    void Start()
    {
        // ���� ����� �������� �ҷ��� �����̴��� ������� ����
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
        volumeSlider.value = savedVolume;
        // AudioListener.volume = savedVolume;
        bgmSource.volume = savedVolume; // ����� �ҽ��� ���� ����
        settingsPanel.SetActive(false);
        // �����̴� �̺�Ʈ�� ���� ���� �޼��� ����
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
    }


    void Update()
    {
        // ESC Ű �Է� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeSelf) // ���� â�� Ȱ��ȭ�� ���¶��
            {
                OnCloseButtonClicked(); // ���� â �ݱ�
            }
        }
    }

    // ���� ��ư�� ������ �� ���� â�� ������
    public void OnSettingsButtonClicked()
    {
        settingsPanel.SetActive(true);
    }

    // X ��ư�� ������ �� ���� â�� ����
    public void OnCloseButtonClicked()
    {
        settingsPanel.SetActive(false);
    }

    // ���� �����̴� ���� ����Ǿ��� �� ȣ��
    public void OnVolumeChange()
    {
        //AudioListener.volume
        bgmSource.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
}