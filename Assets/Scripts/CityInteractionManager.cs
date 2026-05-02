using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine; // Unity 6 kullanıyorsan bu kütüphane şart

public class CityInteractionManager : MonoBehaviour
{
    [Header("Kameralar")]
    public CinemachineCamera vcamMap;    // Kuşbakışı kamera
    public CinemachineCamera vcamDetail; // Odaklanacak kamera

    [Header("Ayarlar")]
    public LayerMask buildingLayer;      // Sadece binaları seçmek için
    public float distanceToBuilding = 15f; // Binadan ne kadar uzakta dursun?
    public float heightOffset = 8f;        // Binadan ne kadar yüksekte dursun?

    [Header("UI")]
    public GameObject detailPanel;       // Sağ taraftaki analiz paneli

   void Update()
{
    // Yeni sistemde sol tık kontrolü
    if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
    {
        // Farenin o anki ekran konumunu al
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, buildingLayer))
        {
            FocusOnBuilding(hit.transform);
        }
    }
}

    public void FocusOnBuilding(Transform target)
{
    vcamDetail.LookAt = target;
    vcamDetail.Follow = target;

    // 1. Binanın gerçek boyutunu hesapla (Mesh'e göre)
    float buildingSize = 5f; // Varsayılan
    if (target.TryGetComponent<Renderer>(out var renderer))
    {
        buildingSize = renderer.bounds.size.magnitude;
    }

    // 2. Dinamik Mesafe: Binanın boyuna göre bir tampon bölge oluştur
    float totalDistance = buildingSize + distanceToBuilding;

    // 3. Yönü Test Et: Eğer içine giriyorsa 'forward' yerine '-forward' veya 'right' dene
    // Genellikle assetlerde ön yüz '-target.forward' veya 'target.forward' dır.
    Vector3 offsetDirection = -target.forward; 

    Vector3 viewPosition = target.position + (offsetDirection * totalDistance) + (Vector3.up * heightOffset);
    
    vcamDetail.transform.position = viewPosition;
    vcamDetail.Priority = 20;

    if (detailPanel != null) detailPanel.SetActive(true);
}

    public void BackToMap()
    {
        // Geri dön butonu için fonksiyon
        vcamDetail.Priority = 5;
        if (detailPanel != null) detailPanel.SetActive(false);
    }
}