using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false; // 죽으면 무기 교체 못하게 함
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; //죽으면 마우스 커서 보이게함 (버튼 클릭해야 되니까)
    }

}
