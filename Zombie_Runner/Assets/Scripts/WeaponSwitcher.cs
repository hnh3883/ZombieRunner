using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    void Start()
    {
        SetWeaponActive();    
    }
    void Update()
    {
        int previousWeapon = currentWeapon;

        ProcesskeyInput();
        ProcessScrollWheel();

        if(previousWeapon != currentWeapon)  // 이전 총기번호와 현재 총기번호가 다르면
        {
            SetWeaponActive(); // 총을 바꿔 주겠다.
        }
    }

    private void ProcesskeyInput() // 키보드로 무기 교체하기
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;

        }
    }

    private void ProcessScrollWheel()  // 마우스 휠로 무기 교체하기
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0) // 휠 위로 올릴때
        {
            if(currentWeapon >= transform.childCount - 1)  // childcount 총 개수 (오브젝트)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // 휠 아래로 올릴때
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void SetWeaponActive()  //  무기 체인지
    {
        int weaponIndex = 0;
        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }


}
