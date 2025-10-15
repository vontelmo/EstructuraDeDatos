
public class MySetArray<T> : MySet<T>
{
    private T[] elementsInternalArray;
    public override T[] Elements => elementsInternalArray;

    private int count;
    private int capacity;
    private int defaultCapacity = 10;

    public MySetArray(int capacity)
    {
        this.capacity = capacity;
        elementsInternalArray = new T[capacity];
    }

    public MySetArray()
    {
        capacity = defaultCapacity;
        elementsInternalArray = new T[capacity];
    }

    public override void Add(T element)
    {
        if (Contains(element) || count >= capacity)
        {
            return;
        }

        Elements[count] = element;
        count++;
    }

    public override int Cardinality()
    {
        return count;
    }

    public override void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            Elements[i] = default;
        }

        count = 0;
    }

    public override bool Contains(T element)
    {
        for (int i = 0; i < count; i++)
        {
            if (Equals(Elements[i], element))
            {
                return true;
            }
        }

        return false;
    }

    public override void Remove(T element)
    {
        for (int i = 0; i < count; i++)
        {
            if (Equals(Elements[i], element))
            {
                for (int j = i; j < count - 1; j++)
                    Elements[j] = Elements[j + 1];

                Elements[count - 1] = default;
                count--;
                return;
            }
        }
    }

    public override void Union(MySet<T> element1, MySet<T> element2)
    {
        Clear();

        foreach (T element in element1.Elements)
        {
            if (!Equals(element, default)) // Evitamos elementos no inicializados o de valor default
                Add(element);
        }

        foreach (T element in element2.Elements)
        {
            if (!Equals(element, default))
                Add(element); // Add ya evita duplicados
        }
    }

    public override void Intersect(MySet<T> element1, MySet<T> element2)
    {
        Clear();

        foreach (T element in element1.Elements)
        {
            if (!Equals(element, default) && element2.Contains(element))
            {
                Add(element);
            }
        }
    }

    public override void Difference(MySet<T> element1, MySet<T> element2)
    {
        Clear();

        foreach (T element in element1.Elements)
        {
            if (!Equals(element, default) && !element2.Contains(element)) 
            {
                Add(element);
            }
        }
    }
}
