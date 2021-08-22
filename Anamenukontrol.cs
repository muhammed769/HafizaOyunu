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


    // �oklu level var ise oyuncunun en son kald��� level'i Player Prefs ile sisteme kay�t edip oyuncunun oyunu kapat�p yeniden a�t���nda  OYUNA KALDI�I YERDEN DEVAM ETMES�N� SA�LAYAB�L�R�Z.
    public void OyunaBasla()
    {
        SceneManager.LoadScene(0);
       // SceneManager.LoadScene(PlayerPrefs.GetInt("KaldigiBolum")); // Hangi Levelde kald�ysa oyunu kapatsa bile kald��� yerden devam etmesini b�ylece sa�lam�� olduk.
    }


  
}
