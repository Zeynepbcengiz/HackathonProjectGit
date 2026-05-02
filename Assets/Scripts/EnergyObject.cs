using UnityEngine;

public class EnergyObject : MonoBehaviour
{
    [Header("API Ayarları")]
    public string zoneID; // Bu kısma Inspector'da B001, B002 yazmıştın, kalsın.

    [Header("Veri Değerleri")]
    public string ad;
    public float currentLoad; // Tüketim buraya gelecek
    public float yenilenebilir;
    public double karbon;

    // WebManager'ın çağıracağı ana fonksiyon
    public void VerileriGuncelle(string gelenAd, float gelenTuketim, float gelenYenilenebilir, double gelenKarbon)
    {
        ad = gelenAd;
        currentLoad = gelenTuketim;
        yenilenebilir = gelenYenilenebilir;
        karbon = gelenKarbon;

        RenkGuncelle();
    }

    void RenkGuncelle()
    {
        // Binanın altındaki tüm parçaları bul ve boya
        Renderer[] cocuklar = GetComponentsInChildren<Renderer>();
        
        // %80 ve üzeri tüketim kırmızıya yaklaşır
        Color hedefRenk = Color.Lerp(Color.green, Color.red, currentLoad / 100f);

        foreach (Renderer r in cocuklar)
        {
            r.material.color = hedefRenk;
        }
    }
}