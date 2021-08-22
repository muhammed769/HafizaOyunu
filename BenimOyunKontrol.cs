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
    GameObject butonunKendisi; // Eðer týklanýldýðýnda diðer fonksiyonlar ihtiyaç duyuyorsa bu objeye kullanabilmemiz için.

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

    /* 11.video anlatýmý için Uzun yol 1 : SADECE BÝLGÝ ÝÇÝN

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


        /* 11.video anlatýmý için Uzun yol 2 : SADECE BÝLGÝ ÝÇÝN

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
             
            // ToplamZaman -= 1; // Toplam zamandan 1 saniye düþmüþ olacak. Böyle yaparsan hata alýrsýn oyun tarafýnda.

            ToplamZaman -= Time.deltaTime;

           dakika = Mathf.FloorToInt(ToplamZaman / 60);    // FloorToInt = Int deðerlerine YUVARLAMA yapar. Bize 2 dakika sonucunu verir. // 119 saniye / 2 = 1 dakika .. saniye   ======> Burda Dakikamýzý aldýk.
            saniye = Mathf.FloorToInt(ToplamZaman % 60);    // 119 saniye / 2 = ... dakika 59 saniye.      ======>  Burdada saniyeyi aldýk.

            //  Sayac.text = Mathf.FloorToInt(ToplamZaman).ToString();

            Sayac.text = string.Format(" {0:00}  : {1:00}", dakika, saniye); // string formatý böyle çalýþýyor.Yani bu SABÝT BÝR KOMUTTUR ! ! ! ! ! {0.00} : {1.00} 'nýn yerlerine gelecek deðerleri hemen yanýna  VERMEM GEREKÝYOR.
                                                                             // string'Ýn hangi formata dönüþtüreleceðini belirtmiþ olduk.
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
    public void ObjeVer(GameObject objem) // Unity tarafýnda her buton obje olarak kendisini gönderecek. HAYATÝ BÝR NOT : BU METOTUN ÝLGÝLÝ YERDE EN ÜSTTE DURMASI LAZIM YOKSA SÝSTEM BOÞA DÜÞER VE OYUN ÇALIÞMAZ !!!!!!!!!!!!!!!!!!!!!!
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

             // Bu Fonksiyon butonunKendisini alýyo Kontrol metotuna gönderiyor.Kontrol fonksiyonuda iþlem yapýyor.Kontrol fonksiyonunda Gameobject gelenobje 'le
             // ilgili olan ara iþlemi KALDIRMAMDA  HERHANGÝ BÝR SAKINCA YOK.Çünkü ButonununKendisi 'ne OBjeVer fonksiyonun'da objem'i verdik.

            -------------------------------------------------------
        */

        Kontrol(deger);

    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Kontrol(int gelendeger)
    {
        //  Debug.Log(" Resmin sahip olduðu bu Butona týkladim : Gelen deðer : " +deger );

        if (ilkSecimDegeri == 0) // Daha önce TIKLANMIÞ herhangi bir Buton yoksa bana þu iþlemleri yap.
        {

            ilkSecimDegeri = gelendeger; // ilkSecimDegeri Bütün butonlarýn deger'ini  almýþ oldu.

                                                                                        //  secilenButon = gelenobje;
            secilenButon = butonunKendisi;

        }

        else // Herhangi bir  Butona 2.kez  basýlýrsa bana þu iþlemleri yap. Yani ilk SecimDegeri bir deger'e sahipse bana þu iþlemleri yap : 
        {
            StartCoroutine(kontrolEtBakalim(gelendeger));
        }

    }

    // Belli bir zamanda belli bir iþi yapabilmemizi bize IENumerator interface'i saðlar : 

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


            // Debug.Log("Evet Eþleþti.");
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

            //   Debug.Log("Eþleþmedi.");
            //    Debug.Log("Eþleþmedi ! ");


            //secilenButon.GetComponent<Image>().raycastTarget = true;
            //butonunKendisi.GetComponent<Image>().raycastTarget = true;

            ilkSecimDegeri = 0;
            secilenButon = null; // Bir sonraki týklama için bu 2 deðerde sýfýrlanmýþ olaacak.Bunu yapmazsak hafýzada en son týklanan buton kalacaktýr.
            ButonlarinDurumu(true);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
