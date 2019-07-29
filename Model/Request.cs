using System;
using RequestManager.Extensions;

namespace RequestManager.Model
{
    public class Request: IEquatable<Request>
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public Package Package { get; set; }
        public DateTime SubmittedOn { get; set; }
        public RequestStatus Status { get; set; }
        public Guid CorrelationKey { get; set; }
        public DistributionType Distribution { get; set; }

        public bool Match(string pattern)
        {
            return 
                User.Match(pattern) || 
                Package.Name.Match(pattern) || 
                Email.Match(pattern);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Request request = obj as Request;
            return Equals(request);
        }
        public override int GetHashCode()
        {
            return Id;
        }
        public bool Equals(Request other)
        {
            if (other == null) return false;
            return (Id.Equals(other.Id));
        }

        public override string ToString()
        {
            return $"Id: {Id}, User: {User}, Status: {Status}, Package: {Package}, SubmittedOn: {SubmittedOn}, Distribution: {Distribution}, CorrelationKey: {CorrelationKey}";
        }
    }
}