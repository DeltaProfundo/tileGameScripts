using System.Collections.Generic;
using System.Linq;

public static class Auxiliary
{
    public static T GetInvokable<T>(string invoke, T[] array) where T : Invokable
    {
        T output = null;
        for (int i = 0; i < array.Length; i++)
        {
            T sel = array[i];
            if (sel.Invoke == invoke)
            {
                output = sel;
                break;
            }
        }
        return output;
    }
    public static T GetInvokableLoose<T>(string invoke, T[] array) where T : Invokable
    {
        T output = null;
        for (int i = 0; i < array.Length; i++)
        {
            T sel = array[i];
            if (invoke.Contains(sel.Invoke))
            {
                output = sel;
                break;
            }
        }
        return output;
    }
    public static T GetInvokable<T>(string invoke, List<T> list) where T : Invokable
    {
        return GetInvokable<T>(invoke, list.ToArray());
    }
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        System.Random rnd = new System.Random();
        return source.OrderBy((item) => rnd.Next());
    }
}
