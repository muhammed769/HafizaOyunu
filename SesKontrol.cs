using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesKontrol : MonoBehaviour
{

    private static GameObject instance;
    // static : static olan deðiþkenler  Sistemde bir kere tanýmlandýktan sonra  tekrar DEÐÝÞTÝRÝLEMEYEN  DEÐÝÞKENLERDÝR.

    private void Awake()
    {
        // DontDestroyOnLoad : Buraya yazacaðýn þey þu anlama gelir : Buraya yazacaðýn obje ASLA YOK OLMASIN DEMEKTÝR.

        //  DontDestroyOnLoad(GetComponent<AudioSource>()); 

        DontDestroyOnLoad(gameObject); // Üstteki kod'la ayný iþlevi görür. Yani Menunun baþlagýçtaki ses oyuna baþladýðýmýzdada DEVAM EDER.

        // SAHNELER ARASINDA YOK OLMAMASINI ÝSTEDÝÐÝN OBJELER VAR ÝSE BÖYLE YAPABÝLÝRSÝN.

        if (instance == null) // Genel BÝr kullanýmdýr her yerde kullanabilirsin yani.
            instance = gameObject;
        else
            Destroy(gameObject);


    }
}
