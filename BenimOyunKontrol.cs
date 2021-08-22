using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BenimOyunKontrol : MonoBehaviour
{

    // GENEL AYARLAR
    public int hedefbasari;
    int anlikbasari;
    int ilkSecimDegeri;
   

    // ----------------------

    GameObject secilenButon;
    GameObject butonunKendisi; // E�er t�klan�ld���nda di�er fonksiyonlar ihtiya� duyuyorsa bu objeye kullanabilmemiz i�in.

    // ----------------------

    public Sprite defaultSprite;
    public AudioSource[] sesler;
    public GameObject[] Butonlar;
    public TextMeshProUGUI Sayac;
    public GameObject[] OyunSonuPanelleri;
    public Slider ZamanSlider;

    // SAYAC

    //  public  float ToplamZaman = 5;
    public float ToplamZaman;
    float dakika;
    float saniye;
    bool zamanlayici;
    float gecenZaman;

    // -------------------------

    /* 11.video anlat�m� i�in Uzun yol 1 : SADECE B�LG� ���N

    public GameObject Grid;
    public GameObject eklenecekObje; // Prefab olarak atanacak olan obje 

    */
    // -------------------------

    public GameObject Grid;
    public GameObject Havuz;
    bool olusturmaDurumu;
    int olusturmaSayisi;
    int ToplamElemanSayisi;

    void Start()
    {
        ToplamElemanSayisi = Havuz.transform.childCount;
        ilkSecimDegeri = 0;
        zamanlayici = true;
        olusturmaDurumu = true;      
        gecenZaman = 0;
        olusturmaSayisi = 0;
        ZamanSlider.value = gecenZaman;
        ZamanSlider.maxValue = ToplamZaman;


        /* 11.video anlat�m� i�in Uzun yol 2 : SADECE B�LG� ���N

        GameObject obje = Instantiate(eklenecekObje);
        RectTransform rt = obje.GetComponent<RectTransform>();
        rt.localScale = new Vector3(0.358f,0.544f, 1f);
        obje.transform.SetParent(Grid.transform);
        */


        StartCoroutine(Olustur());
    }

    private void Update()
    {

        if ( zamanlayici  && gecenZaman !=ToplamZaman)
        {

            /* ----------------------------------------------------
             
            // ToplamZaman -= 1; // Toplam zamandan 1 saniye d��m�� olacak. B�yle yaparsan hata al�rs�n oyun taraf�nda.

            ToplamZaman -= Time.deltaTime;

           dakika = Mathf.FloorToInt(ToplamZaman / 60);    // FloorToInt = Int de�erlerine YUVARLAMA yapar. Bize 2 dakika sonucunu verir. // 119 saniye / 2 = 1 dakika .. saniye   ======> Burda Dakikam�z� ald�k.
            saniye = Mathf.FloorToInt(ToplamZaman % 60);    // 119 saniye / 2 = ... dakika 59 saniye.      ======>  Burdada saniyeyi ald�k.

            //  Sayac.text = Mathf.FloorToInt(ToplamZaman).ToString();

            Sayac.text = string.Format(" {0:00}  : {1:00}", dakika, saniye); // string format� b�yle �al���yor.Yani bu SAB�T B�R KOMUTTUR ! ! ! ! ! {0.00} : {1.00} 'n�n yerlerine gelecek de�erleri hemen yan�na  VERMEM GEREK�YOR.
                                                                             // string'�n hangi formata d�n��t�relece�ini belirtmi� olduk.
            ----------------------------------------------------------
             */
            gecenZaman += Time.deltaTime;
            ZamanSlider.value = gecenZaman;

            if (ZamanSlider.maxValue == ZamanSlider.value)
            {
                zamanlayici = false;
                GameOver();
            }


        }

       /* 
        else
        {
            zamanlayici = false;
            GameOver();
        }

       */
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    IEnumerator Olustur()
    {
        yield return new WaitForSeconds(0.1f);

        while (olusturmaDurumu)
        {
            int rastgeleSayi = Random.Range(0, Havuz.transform.childCount - 1);

            if (Havuz.transform.GetChild(rastgeleSayi).gameObject != null)
            {
                Havuz.transform.GetChild(rastgeleSayi).transform.SetParent(Grid.transform);
                olusturmaSayisi++;
                if (olusturmaSayisi == ToplamElemanSayisi)
                {
                    olusturmaDurumu = false;
                    Destroy(Havuz.gameObject);
                }
            }
            //  Debug.Log(Havuz.transform.GetChild(rastgeleSayi).name);
        }

    }

    public void OyunuDurdur()
    {
        OyunSonuPanelleri[2].SetActive(true);
        Time.timeScale = 0;
    }
    public void OyunaDevamEt()
    {
        OyunSonuPanelleri[2].SetActive(false);
        Time.timeScale = 1;
    }

    void GameOver()
    {
        OyunSonuPanelleri[0].SetActive(true);
    }
    void Win()
    {
        OyunSonuPanelleri[1].SetActive(true);
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    public void AnaMenu()
    {
        SceneManager.LoadScene("AnaMenu");
    }


    public void TekrarOyna()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void ObjeVer(GameObject objem) // Unity taraf�nda her buton obje olarak kendisini g�nderecek. HAYAT� B�R NOT : BU METOTUN �LG�L� YERDE EN �STTE DURMASI LAZIM YOKSA S�STEM BO�A D��ER VE OYUN �ALI�MAZ !!!!!!!!!!!!!!!!!!!!!!
    {
        butonunKendisi = objem;

        butonunKendisi.GetComponent<Image>().sprite = butonunKendisi.GetComponentInChildren<SpriteRenderer>().sprite;
        butonunKendisi.GetComponent<Image>().raycastTarget = false;

        sesler[0].Play();


    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void ButonlarinDurumu(bool durum)
    {
        foreach (var item in Butonlar)
        {
            if (item != null)
            {
                item.GetComponent<Image>().raycastTarget = durum;
            }

            
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void ButonTikladi(int deger) 
    {
        /*
          -------------------------------------------------------

              Kontrol(deger, butonunKendisi);

             // Bu Fonksiyon butonunKendisini al�yo Kontrol metotuna g�nderiyor.Kontrol fonksiyonuda i�lem yap�yor.Kontrol fonksiyonunda Gameobject gelenobje 'le
             // ilgili olan ara i�lemi KALDIRMAMDA  HERHANG� B�R SAKINCA YOK.��nk� ButonununKendisi 'ne OBjeVer fonksiyonun'da objem'i verdik.

            -------------------------------------------------------
        */

        Kontrol(deger);

    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Kontrol(int gelendeger)
    {
        //  Debug.Log(" Resmin sahip oldu�u bu Butona t�kladim : Gelen de�er : " +deger );

        if (ilkSecimDegeri == 0) // Daha �nce TIKLANMI� herhangi bir Buton yoksa bana �u i�lemleri yap.
        {

            ilkSecimDegeri = gelendeger; // ilkSecimDegeri B�t�n butonlar�n deger'ini  alm�� oldu.

                                                                                        //  secilenButon = gelenobje;
            secilenButon = butonunKendisi;

        }

        else // Herhangi bir  Butona 2.kez  bas�l�rsa bana �u i�lemleri yap. Yani ilk SecimDegeri bir deger'e sahipse bana �u i�lemleri yap : 
        {
            StartCoroutine(kontrolEtBakalim(gelendeger));
        }

    }

    // Belli bir zamanda belli bir i�i yapabilmemizi bize IENumerator interface'i sa�lar : 

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    IEnumerator kontrolEtBakalim(int gelendeger)
    {
        ButonlarinDurumu(false);
        yield return new WaitForSeconds(1);

        if (ilkSecimDegeri == gelendeger)
        {
            anlikbasari++;

            secilenButon.GetComponent<Image>().enabled = false;
            secilenButon.GetComponent<Button>().enabled = false;

            butonunKendisi.GetComponent<Image>().enabled = false;
            butonunKendisi.GetComponent<Button>().enabled = false;


            // Debug.Log("Evet E�le�ti.");
            /* Destroy(secilenButon.gameObject);
             // Destroy(gelenobje.gameObject);
             Destroy(butonunKendisi);*/

            ilkSecimDegeri = 0;
            secilenButon = null;
            ButonlarinDurumu(true);

            if (hedefbasari == anlikbasari)
            {
                Win();
            }
        }
        else
        {
            sesler[1].Play();
            secilenButon.GetComponent<Image>().sprite = defaultSprite;
            // gelenobje.GetComponent<Image>().sprite = defaultSprite;
            butonunKendisi.GetComponent<Image>().sprite = defaultSprite;

            //   Debug.Log("E�le�medi.");
            //    Debug.Log("E�le�medi ! ");


            //secilenButon.GetComponent<Image>().raycastTarget = true;
            //butonunKendisi.GetComponent<Image>().raycastTarget = true;

            ilkSecimDegeri = 0;
            secilenButon = null; // Bir sonraki t�klama i�in bu 2 de�erde s�f�rlanm�� olaacak.Bunu yapmazsak haf�zada en son t�klanan buton kalacakt�r.
            ButonlarinDurumu(true);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
