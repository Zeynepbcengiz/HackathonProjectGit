using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class WebManager : MonoBehaviour
{
    private string url = "http://localhost:5218/api/binalar";

    void Start()
    {
        Debug.Log("API Bağlantı testi başlatılıyor...");
        StartCoroutine(CheckConnection());
    }

    IEnumerator CheckConnection()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // 1. Ham JSON metnini al
                string json = request.downloadHandler.text;
                
                // 2. JSON'u listeye çevir (Bağlantının kalitesini ölçmek için)
                List<BuildingData> gelenVeriler = JsonConvert.DeserializeObject<List<BuildingData>>(json);

                Debug.Log("<color=green>✓ BAĞLANTI BAŞARILI!</color>");
                Debug.Log($"Backend'den toplam {gelenVeriler.Count} adet bina verisi alındı.");

                // Her binanın adını konsola yazdırarak veriyi teyit et
                foreach (var bina in gelenVeriler)
                {
                    Debug.Log($"Gelen Veri -> ID: {bina.id}, Ad: {bina.ad}, Tüketim: {bina.tuketim}");
                }
            }
            else
            {
                Debug.LogError("<color=red>X BAĞLANTI HATASI:</color> " + request.error);
                Debug.LogError("Lütfen VS Code'da backend'in (dotnet run) çalıştığından emin ol.");
            }
        }
    }
}

[System.Serializable]
public class BuildingData
{
    public string id;
    public string ad;
    public float tuketim;
    public float yenilenebilir;
    public double karbon;
}