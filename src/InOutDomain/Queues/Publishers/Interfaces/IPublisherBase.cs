namespace InOut.Domain.Queues.Publishers.Interfaces
{
    public interface IPublisherBase
    {
        void Publish(string json);
    }
}