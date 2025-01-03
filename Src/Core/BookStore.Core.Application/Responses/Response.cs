using BookStore.Core.Application.Validators;

namespace BookStore.Core.Application.Responses
{
    public class Response
    {
        public ICollection<Notification> Notifications { get; set; }
        public object Data { get; set; }

        public Response()
        {
            Notifications = [];
        }

        public Response(object data)
        {
            Notifications = [];
            Data = data;
        }

        public Response(ICollection<Notification> notifications)
        {
            Notifications = notifications;
        }

        public static Response Success()
        {
            return new Response();
        }

        public static Response Success(object data)
        {
            return new Response(data);
        }

        public static Response Error()
        {
            return new Response();
        }

        public static Response Error(ICollection<Notification> notifications)
        {
            return new Response(notifications);
        }
    }
}
