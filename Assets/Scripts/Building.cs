using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Grup Kimlik Bilgileri")]
    public string binaID;
    public string binaAdi;

    [Header("Enerji Verileri")]
    [Range(0, 100)] public float enerjiTuketimi;
    public float yenilenebilirEnerji;
    public double karbonSalinimi;

    /// <summary>
    /// WebManager'dan gelen verileri bu gruba atar ve görseli günceller.
    /// </summary>
    public void VerileriGuncelle(string ad, float tuketim, float yenilenebilir, double karbon)
    {
        binaAdi = ad;
        enerjiTuketimi = tuketim;
        yenilenebilirEnerji = yenilenebilir;
        karbonSalinimi = karbon;

        // Veriler her güncellendiğinde renkleri de tazele
        GrupRenginiGuncelle();
    }

    /// <summary>
    /// Bu objenin altındaki tüm çocuk objelerin renklerini tüketim oranına göre değiştirir.
    /// </summary>
    void GrupRenginiGuncelle()
    {
        // 1. Bu objenin (Parent) altındaki TÜM Renderer bileşenlerini (Çocuklar dahil) bulur
        Renderer[] cocukRenderers = GetComponentsInChildren<Renderer>();

        // 2. Tüketim oranına göre renk belirle: 0 (Yeşil) -> 100 (Kırmızı)
        // Lerp fonksiyonu iki renk arasında yumuşak geçiş sağlar
        Color hedefRenk = Color.Lerp(Color.green, Color.red, enerjiTuketimi / 100f);

        // 3. Her bir parçayı tek tek boya
        foreach (Renderer r in cocukRenderers)
        {
            // Shader üzerinde renk değişimi yapabilmek için material.color kullanılır
            r.material.color = hedefRenk;
        }
        
        Debug.Log($"{binaID} grubu için {cocukRenderers.Length} parça güncellendi.");
    }
}