using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CanvasControl : MonoBehaviour
{
    public GameObject settingPanelUi, upgradePanel;
    CollectController col;
    PlayerManager player;
    public int stackCapMoney, TimeMoney, SpeedMoney;
    public TextMeshProUGUI stackButtonText, TimeButtonText, SpeedButtonText;
    public GameObject musicOpen, musicClose, VibrationOpen, VibretionClose
        ,videoStack,videoSpeed,videoTime
        ,closeStack,closeTime,closeSpeed;
    public AudioSource music,drop,partical;
    
    
    bool isCalling,isVib, isPaused = false;
    public bool isUpgrade = false;
    private void Start()
    {
        col = this.GetComponent<CollectController>();
        player = this.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if(player.moneyCount<stackCapMoney)
        {
            videoStack.SetActive(true);
           
            closeStack.SetActive(true);
        }
        else
        {
            videoStack.SetActive(false);
            closeStack.SetActive(false);
        }
        if(player.moneyCount<TimeMoney)
        {
            videoTime.SetActive(true);
            closeTime.SetActive(true);
        }
        else
        {
            videoTime.SetActive(false);
            closeTime.SetActive(false);
        }
        if (player.moneyCount < SpeedMoney)
        {
            videoSpeed.SetActive(true);
            closeSpeed.SetActive(true);
        }
        else
        {
            videoSpeed.SetActive(false);
            closeSpeed.SetActive(false);
        }
    }
    public void settingPanel()
    {
        pauseGame();
        settingPanelUi.SetActive(true);
        isPaused = true;
    }
    public void ExitButton()
    {
        pauseGame();
        settingPanelUi.SetActive(false);
        upgradePanel.SetActive(false);
        isPaused = false;
    }
    public void stackCapacityButton()
    {
       if(player.moneyCount>=stackCapMoney)
        {
           
            player.moneyCount -= stackCapMoney;
            stackCapMoney += 50;
            stackButtonText.text = "" + stackCapMoney.ToString();
            player.moneyText.text=player.moneyCount + "$".ToString();
            col.stackCapacity++;
        }
       else
        {
            closeStack.SetActive(true);
            videoStack.SetActive(true);
        }
    }
    public void TimeButton()
    {
       if(player.moneyCount>=TimeMoney)
        {
            TimeMoney += 50;
            TimeButtonText.text = "" + TimeMoney.ToString();
            player.moneyCount -= TimeMoney;
            player.moneyText.text = player.moneyCount + "$".ToString();
            col.tambourine.currentTime += 5;
        }
       else
        {
            closeTime.SetActive(true);
            videoTime.SetActive(true);
        }
    }
    public void PlayerSpeed()
    {
        if (player.moneyCount >= SpeedMoney)
        {
            SpeedMoney += 65;
            SpeedButtonText.text = "" + SpeedMoney.ToString();
            player.moneyCount -= SpeedMoney;
            player.moneyText.text = player.moneyCount + "$".ToString();
            player.moveSpeed++;
        }
        else
        {
            closeSpeed.SetActive(true);
            videoSpeed.SetActive(true);
        }

    }

    public void MusicButton()
    {

        if(isCalling)
        {

            musicOpen.SetActive(false);
            musicClose.SetActive(true);
            //music.Stop();
            music.volume = 0;
            partical.volume = 0;
            drop.volume = 0;
            isCalling = false;
        }
        else
        {
            musicClose.SetActive(false);
            musicOpen.SetActive(true);
            //music.Play();
            music.volume = 0.5f;
            drop.volume = 0.5f;
            partical.volume = 0.5f;
            isCalling = true;
        }


    }
    public void Levelup(int level)
    {
        SceneManager.LoadScene(level);
       
    }
    public void VibrationButton()
    {

        if(isVib)
        {
            VibrationOpen.SetActive(false);
            VibretionClose.SetActive(true);
            isVib = false;
        }
        else
        {
            VibrationOpen.SetActive(true);
            VibretionClose.SetActive(false);
            isVib = true;
        }

      
    }
   
    public void pauseGame()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Upgrade"))
        {
            upgradePanel.SetActive(true);
            isPaused = false;
            pauseGame();
            isUpgrade = true;
        }
       





    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Upgrade"))
        {
            upgradePanel.SetActive(false);
            isPaused = true;
            pauseGame();
        }
       
       

    }
}
