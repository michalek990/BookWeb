namespace Infrastructure.Presistence.Pipeline.Common;

public interface IOperation<TInput>
{
    void Invoke(TInput data);
}