using PactNet.Mocks.MockHttpService;

namespace PactSandbox.ClientTests
{
    public interface IVisitableMockProviderService : IMockProviderService
    {
        void Accept(IMockProviderServiceVisitor visitor);
    }
}