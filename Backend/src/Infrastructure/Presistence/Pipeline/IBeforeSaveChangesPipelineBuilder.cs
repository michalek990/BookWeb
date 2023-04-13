namespace Infrastructure.Presistence.Pipeline;

public interface IBeforeSaveChangesPipelineBuilder
{
    public BeforeSaveChangesPipeline Build();
}