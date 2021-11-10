
public interface iItemStorage
{
    /// <summary>
    /// Default Add function.<para></para>
    /// This will work for every ItemStorage, but if you want more detailed work, cast this into subclass.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    bool Add(ItemInstance item);

    /// <summary>
    /// Default Remove function.<para></para>
    /// This will work for every ItemStorage, but if you want more detailed work, cast this into subclass.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    bool Remove(ItemInstance item);
}
