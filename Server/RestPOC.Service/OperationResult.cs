namespace RestPOC.Service
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class OperationResult
    {
        public OperationResult(bool isOk)
        {
            IsOK = isOk;
            Exceptions = new Collection<Exception>();
        }

        public bool IsOK { get; private set; }
        public string Message { get; set; }
        public ICollection<Exception> Exceptions { get; set; }
    }

    public class OperationResult<T> : OperationResult
    {
        public OperationResult(bool isOk)
            : base(isOk)
        {
        }

        public OperationResult(bool isOk, T result)
            : this(isOk) {
            Result = result;
        }

        public T Result { get; private set; }
    }
}