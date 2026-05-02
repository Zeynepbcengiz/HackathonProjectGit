using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class ZoneData // .NET'teki DTO ile aynı olmalı
{
    public string Id;
    public float Load;
    public float CarbonFootprint;
}

public class APIManager : MonoBehaviour
{
    public string apiURL = "https://localhost:7xxx/api/energy/status"; 
    private EnergyObject[] allZones;

    void Start()
    {
        allZones = FindObjectsByType<EnergyObject>(FindObjectsSortMode.None);
        StartCoroutine(FetchDataFromDotNet());
    }

    IEnumerator FetchDataFromDotNet()
    {
        while (true)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(apiURL))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    // .NET'ten gelen JSON dizisini çözmek için JsonHelper gerekebilir 
                    // veya Newtonsoft.Json kullanabilirsin (tavsiyemdir)
                    string jsonResponse = webRequest.downloadHandler.text;
                    UpdateCity(jsonResponse);
                }
            }
            yield return new WaitForSeconds(5f); // 5 saniyede bir güncelle
        }
    }

    void UpdateCity(string json)
    {
        // Newtonsoft.Json kullanıyorsan:
        // List<ZoneData> data = JsonConvert.DeserializeObject<List<ZoneData>>(json);
        
        // Veriyi aldıktan sonra eşleştirme:
        // foreach(var zone in allZones) {
        //    var match = data.Find(x => x.Id == zone.zoneID);
        //    if(match != null) zone.UpdateStatus(match.Load);
        // }
    }
}