using SwiftParcel.Services.Orders.Application;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SwiftParcel.Services.Orders.Application.UnitTests")]

namespace SwiftParcel.Services.Orders.Infrastructure.Contexts
{
    internal class IdentityContext : IIdentityContext
    {
        public Guid Id { get; }
        public string Role { get; } = string.Empty;
        public bool IsAuthenticated { get; }
        public bool IsOfficeWorker { get; }
        public bool IsCourier { get; }
        public IDictionary<string, string> Claims { get; } = new Dictionary<string, string>();

        internal IdentityContext()
        {
        }

        internal IdentityContext(CorrelationContext.UserContext context)
            : this(context.Id, context.Role, context.IsAuthenticated, context.Claims)
        {
        }

        internal IdentityContext(string id, string role, bool isAuthenticated, IDictionary<string, string> claims)
        {
            Id = Guid.TryParse(id, out var userId) ? userId : Guid.Empty;
            Role = role ?? string.Empty;
            IsAuthenticated = isAuthenticated;
            IsOfficeWorker = Role.Equals("officeworker", StringComparison.InvariantCultureIgnoreCase);
            IsCourier = Role.Equals("courier", StringComparison.InvariantCultureIgnoreCase);
            Claims = claims ?? new Dictionary<string, string>();
        }
        
        internal static IIdentityContext Empty => new IdentityContext();
    }
}