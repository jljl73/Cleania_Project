using UnityEngine;

interface iSavedData
{
    /// <summary>
    /// 1. call member's AfterLoad()
    /// 2. translate serial data into non-serial data
    /// </summary>
    void AfterLoad();

    /// <summary>
    /// 1. call member's BeforeSave()
    /// 2. translate non-serial data into serial data
    /// </summary>
    void BeforeSave();
}

