namespace FIAP.Hackathon.OES.Campaign.Infra.CorrelationId;

public interface ICorrelationIdGenerator
{
    string Get();
    void Set(string correlationId);
}
