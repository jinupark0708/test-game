using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 추가

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void ButtonClick()
    {
        SceneManager.LoadScene(1);


}
}