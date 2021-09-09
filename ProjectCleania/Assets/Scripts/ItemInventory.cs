using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    int ct = 0;
    public Item[] items;
    public int[,] blocks = new int[5,5];

    public bool Acquire(Item item)
    {
        int w, h;
        if(FindPosition(item.width, item.height, out w, out h))
        {
            SetItem(h, w, item.height, item.width, ++ct);
        }

        return true;
    }

    bool FindPosition(int width, int height, out int _w, out int _h)
    {
        _w = 0;
        _h = 0;
        
        for(int i = 0; i < blocks.GetLength(0) - height; ++i)
        {
            for(int j = 0; j < blocks.GetLength(1) - width; ++j)
            {
                if (isEmpty(i, j, width, height))
                {
                    _h = i;
                    _w = j;
                    return true;
                }
            }
        }

        return false;
    }

    bool isEmpty(int _i, int _j, int _w, int _h)
    {
        for (int h = 0; h < _h; ++h)
        {
            for (int w = 0; w < _w; ++w)
            {
                if (blocks[_i + h, _j + w] > 0)
                    return false;
            }
        }
        return false;
    }

    void SetItem(int _i, int _j, int _w, int _h, int index)
    {
        for (int h = 0; h < _h; ++h)
        {
            for (int w = 0; w < _w; ++w)
            {
                blocks[h, w] = index;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Blocks");
            Debug.Log(blocks);

        }
    }
}
