﻿using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace PersonSearch.Tests
{
    /// <summary>
    /// Class taken from https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking
    /// as a solution to async query testing
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public T Current {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current {
            get { return Current; }
        }
    }
}
