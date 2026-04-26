
namespace FIAP.Hackathon.OES.Campaign.Infra.CorrelationId;

public class CorrelationIdGenerator : ICorrelationIdGenerator
{
    private static string _correlationId = null!;

    public string Get() => _correlationId;

    public void Set(string correlationId) => _correlationId = correlationId;
}
