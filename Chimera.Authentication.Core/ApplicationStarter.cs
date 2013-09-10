using System;
using System.Linq;
using Chimera.Authentication.Contract.UserAccounts;
using Chimera.Authentication.Contract.UserAccounts.Events;
using Xyperico.Agres;
using Xyperico.Agres.Serialization;
using Xyperico.Base.CommonDomainTypes;


namespace Chimera.Authentication.Core
{
  public static class ApplicationStarter
  {
    public static void Initialize()
    {
      Xyperico.Agres.ProtoBuf.SerializerSetup.RegisterInheritance<Identity<Guid>, UserAccountId>();

      var serializerTypes =
        typeof(UserAccountId).Assembly.GetTypes()
        .Where(t => typeof(Identity<>).IsAssignableFrom(t) || typeof(IMessage).IsAssignableFrom(t))
        .Where(t => !t.IsAbstract);

      AbstractSerializer.RegisterKnownTypes(serializerTypes);

      AbstractSerializer.RegisterKnownType(typeof(EMail));
      AbstractSerializer.RegisterKnownType(typeof(UserAccountId));
      AbstractSerializer.RegisterKnownType(typeof(UserAccountCreatedEvent));
      AbstractSerializer.RegisterKnownType(typeof(PasswordChangedEvent));
    }
  }
}
