using System;
using Castle.Core;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.DDD.Core.AggregateRootContext;

namespace Raven.DDD.SampleWebservice.Startables
{
    public class DocumentChangedListener : IStartable
    {
        private readonly IDocumentStore _documentStore;
        private IDisposable _subscription;

        public DocumentChangedListener(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void Start()
        {
            _subscription = _documentStore
                .Changes()
                .ForDocumentsInCollection<TestAggregateRoot>()
                .Subscribe(new TestAggregrateRootDocumentObserver());

        }
        public void Stop()
        {
            _subscription?.Dispose();
        }
    }

    public class TestAggregrateRootDocumentObserver : IObserver<DocumentChangeNotification>
    {
        public void OnNext(DocumentChangeNotification value)
        {
            
        }

        public void OnError(Exception error)
        {
           
        }

        public void OnCompleted()
        {
            
        }
    }
}