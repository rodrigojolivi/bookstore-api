using FluentValidation;

namespace BookStore.Core.Application.Validators
{
    public abstract class Validator
    {
        protected ICollection<Notification> Notifications { get; private set; }

        protected Validator()
        {
            Notifications = [];
        }

        protected async Task<bool> IsValidAsync<TValidator, TRequest>(TValidator validator,
            TRequest request, CancellationToken cancellationToken) where TValidator : AbstractValidator<TRequest>
        {
            var result = await validator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    Notifications.Add(new Notification(error.ErrorMessage));
                }

                return false;
            }

            return true;
        }

        protected void AddNotification(string message)
        {
            Notifications.Add(new Notification(message));
        }
    }
}
