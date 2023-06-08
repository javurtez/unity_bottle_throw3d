/*
Made by Melbert Bolocon
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine.UI;

public static class Technical
{
    //public static TimeSpan GetTime(float timer)
    //{
    //    TimeSpan t = TimeSpan.FromSeconds(timer * Constants.SecondsInMinute);

    //    return t;
    //}
    //public static string GetTimeString(float timer)
    //{
    //    string str = "";
    //    TimeSpan t = TimeSpan.FromSeconds(timer * Constants.SecondsInMinute);
    //    str = t.ToString(@"hh\:mm\:ss");

    //    return str;
    //}
    public static int EnumCount<T>()
    {
        var myEnumMemberCount = Enum.GetNames(typeof(T)).Length;
        return myEnumMemberCount;
    }
    public static IEnumerable<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }
    public static bool TryParse<T>(string value)
    {
        return Enum.IsDefined(typeof(T), value);
    }
    public static T Parse<T>(string value)
    {
        T parse = (T)Enum.Parse(typeof(T), value, true);
        return parse;// (T)System.Enum.Parse(typeof(T), value, true);
    }
    public static int RandomPercentage(int count = 2)
    {
        int random = 0;
        for (int x = 0; x < count; x++)
        {
            random += UnityEngine.Random.Range(0, 101);
        }
        random /= count; //get average
        return random;
    }
    public static void RNGShuffle<T>(IList<T> list)
    {
        var rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public static void Shuffle<T>(IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public static int Percent(this int number, int percent)
    {
        return (int)(number * (percent / 100f));
    }
    public static T RandomIndex<T>(this T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }
    public static void ScrollToTop(this ScrollRect scrollRect)
    {
        scrollRect.verticalScrollbar.value = 1;
    }
    public static void ScrollToBottom(this ScrollRect scrollRect)
    {
        scrollRect.verticalScrollbar.value = 0;
    }
}