using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject Menu;
    public Slider speedSlider;
    public GameObject TouchController;

    //void Update()
    //{

    //}

    public void MenuButton()
    {
        if (Menu.activeSelf)
        {
            Time.timeScale = 1;
            SetLookSpeed();
            Menu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            SetSliderToPlayer();
            Menu.SetActive(true);
        }
    }

    void SetLookSpeed()
    {
        player.lookspeed = speedSlider.value;
    }

    void SetSliderToPlayer()
    {
        speedSlider.value = player.lookspeed;
    }

    public void ToggleTouchController()
    {
        TouchController.SetActive(!TouchController.activeSelf);
    }
}
