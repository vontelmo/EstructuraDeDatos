using System;

public abstract class MySet<T>
{
    public abstract T[] Elements { get; }

    public abstract void Add(T element);

    public abstract void Remove(T element);

    public abstract void Clear();

    public abstract bool Contains(T element);

    public virtual void Show()
    {
        foreach (T element in Elements)
        {
            Console.WriteLine(element.ToString());
        }
    }

    public override string ToString() => string.Join(", ", Elements);

    public abstract int Cardinality();

    public bool IsEmpty()
    {
        if (Elements.Length > 0)
        {
            return false;
        }

        return true;
    }

    public abstract void Union(MySet<T> element1, MySet<T> element2);

    public abstract void Intersect(MySet<T> element1, MySet<T> element2);

    public abstract void Difference(MySet<T> element1, MySet<T> element2);
}
