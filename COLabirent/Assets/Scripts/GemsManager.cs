using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsManager : MonoBehaviour
{
    public static GemsManager instance;

    [Header("Objeler")]
    [SerializeField] private List<GameObject> elmasObjeleri = new List<GameObject>();
    [SerializeField] private GameObject bitisKapisiEfekt;
    [SerializeField] private PlayerManager playerManager;

    public Gems elmaslar;

    [SerializeField] private bool[] elmaslarBittiMi = new bool[5];
    private int randomEnumIndex;

    public enum Gems
    {
        Zumrut,
        Zumrut_Yesil,
        Z_Kalp,
        Z_Elmas,
        Z_Gex,
        Yildiz,
        Bos
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        bitisKapisiEfekt.SetActive(false);
        randomEnumIndex = Random.Range(0, 6);

        Baslangic_Islemleri();
        Rastegele_Enum();
    }

    void Update()
    {


        Rastegele_Enum();


        switch (elmaslar) // Toplanacak elmas kontrolü
        {
            case Gems.Zumrut:
                if (transform.GetChild(0).transform.childCount <= 0)
                {
                    elmaslarBittiMi[0] = true;

                    elmaslar = Gems.Zumrut_Yesil;
                    randomEnumIndex = 1;
                }
                Objeleri_Aktif_Et(0);
                break;
            case Gems.Zumrut_Yesil:
                if (transform.GetChild(1).transform.childCount <= 0)
                {
                    elmaslarBittiMi[1] = true;

                    elmaslar = Gems.Z_Kalp;
                    randomEnumIndex = 2;

                }
                Objeleri_Aktif_Et(1);
                break;
            case Gems.Z_Kalp:
                if (transform.GetChild(2).transform.childCount <= 0)
                {
                    elmaslarBittiMi[2] = true;

                    elmaslar = Gems.Z_Elmas;
                    randomEnumIndex = 3;

                }
                Objeleri_Aktif_Et(2);
                break;
            case Gems.Z_Elmas:
                if (transform.GetChild(3).transform.childCount <= 0)
                {
                    elmaslarBittiMi[3] = true;

                    elmaslar = Gems.Z_Gex;
                    randomEnumIndex = 4;

                }
                Objeleri_Aktif_Et(3);
                break;
            case Gems.Z_Gex:
                if (transform.GetChild(4).transform.childCount <= 0)
                {
                    elmaslarBittiMi[4] = true;

                    elmaslar = Gems.Yildiz;
                    randomEnumIndex = 5;

                }
                Objeleri_Aktif_Et(4);
                break;
            case Gems.Yildiz:
                if (transform.GetChild(5).transform.childCount <= 0)
                {
                    elmaslarBittiMi[5] = true;

                    elmaslar = Gems.Zumrut;
                    randomEnumIndex = 0;

                }
                Objeleri_Aktif_Et(5);
                break;
            default:
                break;
        } // Toplanacak elmas kontrolü

        if (elmaslarBittiMi[0] && elmaslarBittiMi[1] && elmaslarBittiMi[2] && elmaslarBittiMi[3] && elmaslarBittiMi[4] && elmaslarBittiMi[5])
        {
            elmaslar = Gems.Bos;
            playerManager.goFinish = true;
            bitisKapisiEfekt.SetActive(true);
        }


    }

    public void Objeleri_Aktif_Et(int index)
    {
        for (int i = 0; i < elmasObjeleri[index].transform.childCount; i++)
        {
            elmasObjeleri[index].transform.GetChild(i).GetComponent<AnimationScript>().enabled = true;      // Elmaslardaki scriptleri aktif ediliyor.
            elmasObjeleri[index].transform.GetChild(i).GetChild(0).gameObject.SetActive(true);              // Events altýndaki objenin içindeki elmas

        }
    }

    public void Baslangic_Islemleri()
    {
        for (int i = 0; i < elmaslarBittiMi.Length - 1; i++)
        {
            elmaslarBittiMi[i] = false;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            elmasObjeleri.Add(transform.GetChild(i).gameObject);     // Events içindeki tüm elmaslarý listeye atýyoruz.
        }


    }
    public void Rastegele_Enum()
    {
        switch (randomEnumIndex)
        {
            case 0:
                elmaslar = Gems.Zumrut;
                break;
            case 1:
                elmaslar = Gems.Zumrut_Yesil;
                break;
            case 2:
                elmaslar = Gems.Z_Kalp;
                break;
            case 3:
                elmaslar = Gems.Z_Elmas;
                break;
            case 4:
                elmaslar = Gems.Z_Gex;
                break;
            case 5:
                elmaslar = Gems.Yildiz;
                break;
            default:
                break;
        }

    }
}
