using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

public static class CollectionUtils
{
    public static bool CheckArrayHasString(string[] c, string b)
    {
        var d = false;
        foreach (var child in c)
        {
            if (child != b)
                continue;
            d = true;
        }

        return d;
    }

    public static T GetRandom<T>(this ICollection<T> collection)
    {
        if (collection == null)
            return default;
        var t = UnityEngine.Random.Range(0, collection.Count);
        foreach (var element in collection)
        {
            if (t == 0)
                return element;
            t--;
        }

        return default;
    }

    public static T GetLast<T>(this T[] collection)
    {
        if (collection == null)
            return default;
        return collection[^1];
    }

    public static T GetLast<T>(this List<T> collection)
    {
        if (collection == null)
            return default;
        return collection[^1];
    }

    public static bool CheckHaveItemNull<T>(this List<T> collection)
    {
        foreach (var element in collection)
            if (element == null)
                return true;

        return false;
    }

    public static bool CheckHaveItemNull<T>(this T[] collection)
    {
        foreach (var element in collection)
            if (element == null)
                return true;

        return false;
    }

    public static bool CheckHaveItemNull<T, TZ>(this Dictionary<T, TZ> collection)
    {
        foreach (var element in collection)
            if (element.Value == null)
                return true;

        return false;
    }

    public static bool CheckIsNullOrEmpty<T>(this T[] collection)
    {
        if (collection == null || collection.Length == 0)
            return true;
        return false;
    }

    public static bool CheckIsNullOrEmpty<T>(this List<T> collection)
    {
        if (collection == null || collection.Count == 0)
            return true;
        return false;
    }

    public static bool CheckIsNullOrEmpty<T, TZ>(this Dictionary<T, TZ> collection)
    {
        if (collection == null || collection.Count == 0)
            return true;
        return false;
    }

    public static T GetRandom<T>(this List<T> collection)
    {
        if (collection == null)
            return default;
        var t = UnityEngine.Random.Range(0, collection.Count);
        return collection[t];
    }

    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.PickRandom(1).SingleOrDefault();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
        return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }

    public static IList<T> Shuffle<T>(this IList<T> list, Random random)
    {
        if (random == null)
            random = new Random(DateTime.Now.Millisecond);
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }

    public static IList<T> PickRandom<T>(this IList<T> source, int count, Random random = null)
    {
        return source.Shuffle(random).Take(count).ToList();
    }

    public static T PickRandom<T>(this IList<T> source, Random random)
    {
        return source.PickRandom(1, random).SingleOrDefault();
    }

    public static int GetMaxElementCount<T>(
        this IEnumerable<T> source,
        int numberElementDesireToGet
    )
    {
        if (source.Count() < numberElementDesireToGet)
            return source.Count();

        return numberElementDesireToGet;
    }

    public static bool Compare<T>(this List<T> list1, List<T> list2)
    {
        if (list1.Count != list2.Count)
            return false;

        for (var i = 0; i < list1.Count; i++)
            if (!list1[i].Equals(list2[i]))
                return false;

        var firstNotSecond = list1.Except(list2).ToList();
        var secondNotFirst = list2.Except(list1).ToList();
        return !firstNotSecond.Any() && !secondNotFirst.Any();
    }

    public static bool Compare<T>(this T[] list1, T[] list2)
    {
        if (list1.Length != list2.Length)
            return false;

        for (var i = 0; i < list1.Length; i++)
            if (!list1[i].Equals(list2[i]))
                return false;

        var firstNotSecond = list1.Except(list2).ToList();
        var secondNotFirst = list2.Except(list1).ToList();
        return !firstNotSecond.Any() && !secondNotFirst.Any();
    }

    public static void AddRangeDistinct<T>(this IList<T> source, IEnumerable<T> target)
    {
        foreach (var obj in target)
        {
            if (!source.Contains(obj))
                source.Add(obj);
        }
    }

    public static void TryAddIgnoreNull<T>(this IList<T> source, T target)
    {
        if (target != null)
            source.Add(target);
    }

    public static bool TryAdd<T>(this IList<T> source, T target)
    {
        if (!source.Contains(target))
        {
            source.Add(target);
            return true;
        }

        return false;
    }
    
    public static List<T> Clone<T>(this List<T> collection)
    {
        var newList = new List<T>();
        foreach (var element in collection)
            newList.Add(element);

        return newList;
    }
}
