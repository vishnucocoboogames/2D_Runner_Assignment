using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public static class ExtentionMethods
{

    /*public static int FindDuplicateCount(List<T> list, T element)
    {
        return list.Count(item => item == element);
    }*/
    public static float GetWorldXPositionOfScreenEnd() => Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    public static List<T> RemoveDuplicateElements<T>(this List<T> inputList)
    {
        return inputList.Distinct().ToList();
    }
    public static void ResetTransform(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.rotation = Quaternion.identity;
        trans.localScale = Vector3.one;
    }

    public static T GetRandomElement<T>(this List<T> list)
    {
        int RandomIndex = Random.Range(0, list.Count);
        return list[RandomIndex];
    }

    public static T GetRandomElement<T>(this T[] list)
    {
        int RandomIndex = Random.Range(0, list.Length);
        return list[RandomIndex];
    }

    public static string ToStringPrice(this int num)
    {
        if (num >= 100000000)
        {
            return ((num >= 10050000 ? num - 500000 : num) / 1000000D).ToString("#M");
        }
        if (num >= 10000000)
        {
            return ((num >= 10500000 ? num - 50000 : num) / 1000000D).ToString("0.#M");
        }
        if (num >= 1000000)
        {
            return ((num >= 1005000 ? num - 5000 : num) / 1000000D).ToString("0.##M");
        }
        if (num >= 100000)
        {
            return ((num >= 100500 ? num - 500 : num) / 1000D).ToString("0.k");
        }
        if (num >= 10000)
        {
            return ((num >= 10550 ? num - 50 : num) / 1000D).ToString("0.#k");
        }

        return num >= 1000 ? ((num >= 1005 ? num - 5 : num) / 1000D).ToString("0.##k") : num.ToString("#,0");
    }
    public static void PlaceInCircle()
    {
        /* float angleStep = 360f / wheelItems.Length;

         for (int i = 0; i < wheelItems.Length; i++)
         {
             // Calculate the position and rotation for each item
             Vector3 position = wheelTransform.position + Quaternion.Euler(0f, 0f, angleStep * i) * Vector3.up * distanceFromCenter;
             Quaternion rotation = Quaternion.Euler(0f, 0f, angleStep * i);
         }*/
    }

    // public static void LocalizeString(this string s, TMPro.TMP_Text _Text)
    // {
    //     LocalizedString myString = new() { TableReference = "UIText" };
    //     myString.TableEntryReference = s;
    //     myString.StringChanged += InvokeFunction;

    //     void InvokeFunction(string resultString)
    //     {
    //         _Text.text = resultString;
    //         myString.StringChanged -= InvokeFunction;
    //     }
    // }

    // public static void LocalizeString(this string s, object[] arguments, TMPro.TMP_Text _Text)
    // {
    //     LocalizedString myString = new() { TableReference = "UIText" };
    //     myString.Arguments = arguments;
    //     myString.TableEntryReference = s;
    //     myString.StringChanged += InvokeFunction;

    //     void InvokeFunction(string resultString)
    //     {
    //         _Text.text = resultString;
    //         myString.StringChanged -= InvokeFunction;
    //     }
    // }

    // public static void LocalizeString(this string s, object[] arguments, Action<string> result)
    // {
    //     LocalizedString myString = new() { TableReference = "UIText" };
    //     myString.Arguments = arguments;
    //     myString.TableEntryReference = s;
    //     myString.StringChanged += InvokeFunction;


    //     void InvokeFunction(string resultString)
    //     {
    //         result?.Invoke(resultString);
    //         myString.StringChanged -= InvokeFunction;
    //     }
    // }

    public static void SetProgress(this RectTransform rect, float maxWidth, float progress, bool isLeft = false, bool inverse = false)
    {
        progress = inverse ? Mathf.Clamp01(1 - progress) : Mathf.Clamp01(progress);
        if (isLeft)
            rect.offsetMin = new Vector2(progress * maxWidth, rect.offsetMin.y);
        else
            rect.offsetMax = new Vector2(-progress * maxWidth, -0);
    }

    public static void SetDivision(this RectTransform rect, float maxWidth, float startPoint, float endPoint, float topBottonExpaction = 0)
    {
        rect.offsetMin = new Vector2(startPoint * maxWidth, topBottonExpaction);
        rect.offsetMax = new Vector2(endPoint * maxWidth, -topBottonExpaction);

        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 1);
    }

    public static int CalculatePoints(this int value, int maxValue)
    {
        value = Mathf.Clamp(value, 0, maxValue);

        float normalizedScore = (float)value / maxValue; // Normalize to [0, 1]
        int points = Mathf.CeilToInt(normalizedScore * 5);

        return points;
    }
}
