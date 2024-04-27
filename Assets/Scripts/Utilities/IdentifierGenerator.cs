
[System.Serializable]
public struct ID
{
    public int value;

    public ID(int value)
    {
        this.value = value;
    }
    public override string ToString()
    {
        return value.ToString();
    }

    public override bool Equals(object obj)
    {
        if (obj is ID)
            return ((ID)obj).value == value;
        return false;
    }
    public override int GetHashCode()
    {
        return value;
    }

    public static bool operator ==(ID left, ID right) => left.Equals(right);
    public static bool operator !=(ID left, ID right) => !left.Equals(right);
}
public class IdentifierGenerator
{
    public int lastId;
    public ID Generate() => new ID(++lastId);
    public void Clear() { lastId = 0; }

    public IdentifierGeneratorData Save() { return new IdentifierGeneratorData() { lastId = lastId }; }
    public void Load(IdentifierGeneratorData data) { lastId = data.lastId; }
}
public class IdentifierGeneratorData
{
    public int lastId;
}
