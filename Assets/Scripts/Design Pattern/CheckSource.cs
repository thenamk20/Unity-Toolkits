using System;
using System.Collections.Generic;

public class CheckSource
{
    private List<Func<bool>> _checks = new List<Func<bool>>();

    public void Add(Func<bool> check)
    {
        _checks.TryAdd(check);
    }

    public void Remove(Func<bool> check)
    {
        _checks.Remove(check);
    }

    public bool Check()
    {
        foreach (var check in _checks)
        {
            if (!check())
                return false;
        }

        return true;
    }
}

public class CheckSource<T>
{
    private List<Func<T, bool>> _checks = new List<Func<T, bool>>();

    public void Add(Func<T, bool> check)
    {
        _checks.TryAdd(check);
    }

    public void Remove(Func<T, bool> check)
    {
        _checks.Remove(check);
    }

    public bool Check(T t)
    {
        foreach (var check in _checks)
        {
            if (!check(t))
                return false;
        }

        return true;
    }
}

public class CheckSource<T1, T2>
{
    private List<Func<T1, T2, bool>> _checks = new List<Func<T1, T2, bool>>();

    public void Add(Func<T1, T2, bool> check)
    {
        _checks.TryAdd(check);
    }

    public void Remove(Func<T1, T2, bool> check)
    {
        _checks.Remove(check);
    }

    public bool Check(T1 t1, T2 t2)
    {
        foreach (var check in _checks)
        {
            if (!check(t1, t2))
                return false;
        }

        return true;
    }
}