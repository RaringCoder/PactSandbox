using PactNet.Mocks.MockHttpService;

namespace PactSandbox.ClientTests
{
    public interface IMockProviderServiceVisitor
    {
        void Visit(IMockProviderService mockProvider);
    }
}