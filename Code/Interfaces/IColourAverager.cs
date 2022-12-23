namespace LegoImageCreator.Interfaces;

public interface  IColourAverager<T>
{
    T GetAverageColour(IEnumerable<T> source);
}