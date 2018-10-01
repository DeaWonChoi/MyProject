using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> where T : IComparable<T>{

    public List<T> DataList;

    public PriorityQueue()
    {
        DataList = new List<T>();
    }

    public void Enqueue(T item)
    {
        DataList.Add(item);
        DataList.Sort();
    }

    public T Dequeue()
    {
        if(IsEmpty())
        {
            throw new NullReferenceException();
        }
        else
        {
            T frontItem = DataList[0];
            DataList.RemoveAt(0);
            DataList.Sort();

            return frontItem;
        }
    }

    public int Count()
    {
        return DataList.Count;
    }

    public bool IsEmpty()
    {
        if(DataList.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
