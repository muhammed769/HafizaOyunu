using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour
{
    int ilkSecimDegeri;

    GameObject secilenButon;
    GameObject butonunKendisi; // Eðer týklanýldýðýnda diðer fonksiyonlar ihtiyaç duyuyorsa bu objeye kullanabilmemiz için.

    public Sprite defaultSprite;


    void Start()
    {
        ilkSecimDegeri = 0;
    }

    public void ObjeVer(GameObject objem) // Unity tarafýnda her buton obje olarak kendisini gönderecek. HAYATÝ BÝR NOT : BU METOTUN ÝLGÝLÝ YERDE EN ÜSTTE DURMASI LAZIM YOKSA SÝSTEM BOÞA DÜÞER VE OYUN ÇALIÞMAZ !!!!!!!!!!!!!!!!!!!!!!
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
        //  Debug.Log(" Resmin sahip olduðu bu Butona týkladim : Gelen deðer : " +deger );

        if (ilkSecimDegeri == 0) // Daha önce TIKLANMIÞ herhangi bir Buton yoksa bana þu iþlemleri yap.
        {
            ilkSecimDegeri = gelendeger; // ilkSecimDegeri Bütün butonlarýn deger'ini  almýþ oldu.
            secilenButon = gelenobje;

        }

        else // Herhangi bir  Butona 2.kez  basýlýrsa bana þu iþlemleri yap. Yani ilk SecimDegeri bir deger'e sahipse bana þu iþlemleri yap : 
        {
            if (ilkSecimDegeri == gelendeger)
            {
                // Debug.Log("Evet Eþleþti.");
                Destroy(secilenButon.gameObject);
                Destroy(gelenobje.gameObject);

                ilkSecimDegeri = 0;
                secilenButon=null;
            }
            else
            {
                secilenButon.GetComponent<Image>().sprite = defaultSprite;
                gelenobje.GetComponent<Image>().sprite = defaultSprite;
              //   Debug.Log("Eþleþmedi.");
                 // Debug.Log("Eþleþmedi ! ");

                ilkSecimDegeri = 0;
                secilenButon = null; // Bir sonraki týklama için bu 2 deðerde sýfýrlanmýþ olaacak.Bunu yapmazsak hafýzada en son týklanan buton kalacaktýr.
            }
        }

    }
}
