using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// 杏z学習張よりコピペ
// https://anz-note.ghost.io/2018/04/03/1522685769/

// 使い方
// 1.UnityWebRequestをつかって該当URLにアクセス
// 2.取ってきたjsonをJsonUtility.FromJson()を使ってマッピング
// 3.あとは、それをお好きにつかっちゃう！]

public class NTP : MonoBehaviour
{
    private static readonly string NTP_URL = "https://ntp-a1.nict.go.jp/cgi-bin/json";

    #region "Methods"
    public void GetTimestamp(Action<long?> callback)
    {
        var startTimestamp = DateTime.Now.Timestamp();
        StartCoroutine(Get((response) =>
        {
            if (response == null)
            {
                callback(null);
            }
            else
            {
                var latency = (DateTime.Now.Timestamp() - startTimestamp) / 2.0;
                callback((long)(response.st + latency));
            }
        }));
    }

    public void GetTimestamp(Action<DateTime?> callback)
    {
        var startTimestamp = DateTime.Now.Timestamp();
        StartCoroutine(Get((response) =>
        {
            if (response == null)
            {
                callback(null);
            }
            else
            {
                var latency = (DateTime.Now.Timestamp() - startTimestamp) / 2.0;
                var timestamp = response.st + latency;
                var startDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var unixDate = startDate.AddSeconds(timestamp);
                callback(unixDate.ToLocalTime());
            }
        }));
    }

    private IEnumerator Get(Action<NictResponse> callback)
    {
        var req = UnityWebRequest.Get(NTP_URL);

        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            callback(null);
        }
        else
        {
            var response = JsonUtility.FromJson<NictResponse>(req.downloadHandler.text);
            callback(response);
        }
    }
    #endregion

    #region "Inner class"
    [Serializable]
    public class NictResponse
    {
        public string id;
        public double it;
        public double st;
        public int leap;
        public long next;
        public int step;
    }
    #endregion
}

// https://github.com/anzfactory/Unity-Utils/blob/master/Assets/Utils/Extension/DateTimeExtension.cs
public static class DateTimeExtension
{
    public static long Timestamp(this DateTime self)
    {
        var span = TimeZoneInfo.ConvertTimeToUtc(self) - new DateTime(year: 1970, month: 1, day: 1, hour: 0, minute: 0, second: 0, kind: DateTimeKind.Utc);
        return (long)span.TotalSeconds;
    }
}