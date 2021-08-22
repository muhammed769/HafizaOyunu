using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesKontrol : MonoBehaviour
{

    private static GameObject instance;
    // static : static olan de�i�kenler  Sistemde bir kere tan�mland�ktan sonra  tekrar DE���T�R�LEMEYEN  DE���KENLERD�R.

    private void Awake()
    {
        // DontDestroyOnLoad : Buraya yazaca��n �ey �u anlama gelir : Buraya yazaca��n obje ASLA YOK OLMASIN DEMEKT�R.

        //  DontDestroyOnLoad(GetComponent<AudioSource>()); 

        DontDestroyOnLoad(gameObject); // �stteki kod'la ayn� i�levi g�r�r. Yani Menunun ba�lag��taki ses oyuna ba�lad���m�zdada DEVAM EDER.

        // SAHNELER ARASINDA YOK OLMAMASINI �STED���N OBJELER VAR �SE B�YLE YAPAB�L�RS�N.

        if (instance == null) // Genel B�r kullan�md�r her yerde kullanabilirsin yani.
            instance = gameObject;
        else
            Destroy(gameObject);


    }
}
