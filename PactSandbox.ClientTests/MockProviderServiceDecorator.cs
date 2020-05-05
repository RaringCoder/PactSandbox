using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;

namespace PactSandbox.ClientTests
{
    public class MockProviderServiceDecorator : IVisitableMockProviderService
    {
        private readonly IMockProviderService _decoratedMockProvider;


        public MockProviderServiceDecorator(IMockProviderService decoratedMockProvider)
        {
            _decoratedMockProvider = decoratedMockProvider;
        }


        public void Accept(IMockProviderServiceVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IMockProviderService Given(string providerState)
        {
            return _decoratedMockProvider.Given(providerState);
        }

        public IMockProviderService UponReceiving(string description)
        {
            return _decoratedMockProvider.UponReceiving(description);
        }

        public IMockProviderService With(ProviderServiceRequest request)
        {
            return _decoratedMockProvider.With(request);
        }

        public void WillRespondWith(ProviderServiceResponse response)
        {
            _decoratedMockProvider.WillRespondWith(response);
        }

        public void Start()
        {
            _decoratedMockProvider.Start();
        }

        public void Stop()
        {
            _decoratedMockProvider.Stop();
        }

        public void ClearInteractions()
        {
            _decoratedMockProvider.ClearInteractions();
        }

        public void VerifyInteractions()
        {
            _decoratedMockProvider.VerifyInteractions();
        }

        public void SendAdminHttpRequest(HttpVerb method, string path)
        {
            _decoratedMockProvider.SendAdminHttpRequest(method, path);
        }
    }
}