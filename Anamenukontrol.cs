using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anamenukontrol : MonoBehaviour
{

    public GameObject cikisPanel;


    private void Start()
    {
        if(Time.timeScale==0)
        Time.timeScale = 1;
    }



    public void OyundanCik()
    {
         cikisPanel.SetActive(true);
     
    }
    public void Cevap(string cevap)
    {

        if (cevap=="evet")
        {
            Application.Quit();
        }
        else
        {
            cikisPanel.SetActive(false);
        }
       
    }


    // Çoklu level var ise oyuncunun en son kaldýðý level'i Player Prefs ile sisteme kayýt edip oyuncunun oyunu kapatýp yeniden açtýðýnda  OYUNA KALDIÐI YERDEN DEVAM ETMESÝNÝ SAÐLAYABÝLÝRÝZ.
    public void OyunaBasla()
    {
        SceneManager.LoadScene(0);
       // SceneManager.LoadScene(PlayerPrefs.GetInt("KaldigiBolum")); // Hangi Levelde kaldýysa oyunu kapatsa bile kaldýðý yerden devam etmesini böylece saðlamýþ olduk.
    }


  
}
