using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour
{
    int ilkSecimDegeri;

    GameObject secilenButon;
    GameObject butonunKendisi; // E�er t�klan�ld���nda di�er fonksiyonlar ihtiya� duyuyorsa bu objeye kullanabilmemiz i�in.

    public Sprite defaultSprite;


    void Start()
    {
        ilkSecimDegeri = 0;
    }

    public void ObjeVer(GameObject objem) // Unity taraf�nda her buton obje olarak kendisini g�nderecek. HAYAT� B�R NOT : BU METOTUN �LG�L� YERDE EN �STTE DURMASI LAZIM YOKSA S�STEM BO�A D��ER VE OYUN �ALI�MAZ !!!!!!!!!!!!!!!!!!!!!!
    {
        butonunKendisi = objem;

        butonunKendisi.GetComponent<Image>().sprite = butonunKendisi.GetComponentInChildren<SpriteRenderer>().sprite;


    }

    public void ButonTikladi(int deger)
    {
        Kontrol(deger, butonunKendisi);
    }


    void Kontrol(int gelendeger, GameObject gelenobje)
    {
        //  Debug.Log(" Resmin sahip oldu�u bu Butona t�kladim : Gelen de�er : " +deger );

        if (ilkSecimDegeri == 0) // Daha �nce TIKLANMI� herhangi bir Buton yoksa bana �u i�lemleri yap.
        {
            ilkSecimDegeri = gelendeger; // ilkSecimDegeri B�t�n butonlar�n deger'ini  alm�� oldu.
            secilenButon = gelenobje;

        }

        else // Herhangi bir  Butona 2.kez  bas�l�rsa bana �u i�lemleri yap. Yani ilk SecimDegeri bir deger'e sahipse bana �u i�lemleri yap : 
        {
            if (ilkSecimDegeri == gelendeger)
            {
                // Debug.Log("Evet E�le�ti.");
                Destroy(secilenButon.gameObject);
                Destroy(gelenobje.gameObject);

                ilkSecimDegeri = 0;
                secilenButon=null;
            }
            else
            {
                secilenButon.GetComponent<Image>().sprite = defaultSprite;
                gelenobje.GetComponent<Image>().sprite = defaultSprite;
              //   Debug.Log("E�le�medi.");
                 // Debug.Log("E�le�medi ! ");

                ilkSecimDegeri = 0;
                secilenButon = null; // Bir sonraki t�klama i�in bu 2 de�erde s�f�rlanm�� olaacak.Bunu yapmazsak haf�zada en son t�klanan buton kalacakt�r.
            }
        }

    }
}
