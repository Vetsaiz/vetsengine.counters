using VetsEngine.LibCore.Notifications.Core;
using VetsEngine.Systems;

namespace VetsEngine.LibCore.Notifications
{
    public class CounterCreator
    {
        public static ICounterManager CreateManager(ILocalStorageProvider storage, string idsClear, string idsNew)
        {
            return new CounterManager(storage, idsClear, idsNew);
        }

        public static INotificationBuilder Builder => NotificationBuilder.Create();

        class NotificationBuilder : INotificationBuilder
        {
            string current = "";

            private NotificationBuilder()
            {
                current = "";
            }

            public INotificationBuilder AddNode(string node)
            {
                current += $".{node}";
                return this;
            }

            public ICounterId Build()
            {
                return  new CounterId(current);
            }

            public static NotificationBuilder Create()
            {
                return new NotificationBuilder();
            }
        }
    }
}
