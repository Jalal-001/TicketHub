using FluentValidation;
using TicketHub.Application.Abstractions;
using TicketHub.Application.Events.AddEvent;
using TicketHub.Domain.Entities;

namespace TicketHub.Application.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IValidator<AddEventCommand> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IEventRepository eventRepository,
                            IUnitOfWork unitOfWork,
                            IValidator<AddEventCommand> validator)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task<int> AddEventAsync(AddEventCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var newEvent = new Event(command.Name, command.Venue, command.StartsAt);
            newEvent.AddSeats(command.Rows, command.SeatsPerRow, command.Price);

            await _eventRepository.AddAsync(newEvent, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return newEvent.Id;
        }
    }
}
