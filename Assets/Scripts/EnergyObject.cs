using UnityEngine;

public class EnergyObject : MonoBehaviour
{
    [Header("API Settings")]
    public string zoneID; // API'den gelen ID ile aynı olmalı (örn: "zone_1")

    [Header("Data Values")]
    public float currentLoad;
    public float threshold = 80f; // Hangi değerden sonra kırmızı yansın?

    private MeshRenderer[] childRenderers;

    void Start()
    {
        // İçindeki tüm binaların renderer'larını toplu olarak bulur
        childRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    // Veri güncellendiğinde görselliği değiştiren fonksiyon
    public void UpdateStatus(float newLoad)
    {
        currentLoad = newLoad;
        Color targetColor = (currentLoad > threshold) ? Color.red : Color.green;

        foreach (var ren in childRenderers)
        {
            // Materyalin Emission (Işıma) rengini değiştirir (URP için)
            ren.material.SetColor("_EmissionColor", targetColor * 2f); // 2f parlaklık çarpanı
            DynamicGI.SetEmissive(ren, targetColor * 2f);
        }
    }
}