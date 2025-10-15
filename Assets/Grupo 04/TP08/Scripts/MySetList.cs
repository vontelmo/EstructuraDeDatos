using MyLinkedList;

public class MySetList<T> : MySet<T>
{
    private MyList<T> elementsList = new MyList<T>();
    public override T[] Elements => elementsList.ToArray();

    public override void Add(T element)
    {
        if (!Contains(element))
        {
            elementsList.Add(element);
        }
    }

    public override int Cardinality()
    {
        return elementsList.Count;
    }

    public override void Clear()
    {
        elementsList.Clear();
    }

    public override bool Contains(T element)
    {
        for (int i = 0; i < elementsList.Count; i++)
        {
            if (Equals(elementsList[i], element))
            {
                return true;
            }
        }

        return false;
    }

    public override void Difference(MySet<T> element1, MySet<T> element2)
    {
        Clear();

        T[] set1 = element1.Elements;
        T[] set2 = element2.Elements;

        foreach (T element in element1.Elements)
        {
            if (!Equals(element, default) && !element2.Contains(element))
            {
                Add(element);
            }
        }
    }

    public override void Intersect(MySet<T> element1, MySet<T> element2)
    {
        Clear();

        T[] set1 = element1.Elements;
        T[] set2 = element2.Elements;

        foreach (T element in element1.Elements)
        {
            if (!Equals(element, default) && element2.Contains(element))
            {
                Add(element);
            }
        }
    }

    public override void Remove(T element)
    {
        elementsList.Remove(element);
    }

    public override void Union(MySet<T> element1, MySet<T> element2)
    {
        Clear();

        T[] set1 = element1.Elements;
        T[] set2 = element2.Elements;

        for (int i = 0; i < set1.Length; i++)
        {
            if (!Equals(set1[i], default))
                Add(set1[i]);
        }

        for (int i = 0; i < set2.Length; i++)
        {
            if (!Equals(set2[i], default))
                Add(set2[i]);
        }
    }
}
