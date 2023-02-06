using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Button LoginBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        LoginBtn.onClick.AddListener(onClickLoginBtn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 点击进入游戏
    /// </summary>
    public void onClickLoginBtn()
    {
        SceneManager.LoadScene("1001");
    }
}
