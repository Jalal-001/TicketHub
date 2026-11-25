using FluentValidation;
using TicketHub.Application.Abstractions;
using TicketHub.Application.Events.AddEvent;
using TicketHub.Application.Events.GetEvents;
using TicketHub.Application.Events.GetEventSeats;
using TicketHub.Application.Exceptions;
using TicketHub.Domain.Entities;

namespace TicketHub.Application.Events
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IValidator<AddEventCommand> _addEventValidator;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IEventRepository eventRepository,
                            IUnitOfWork unitOfWork,
                            IValidator<AddEventCommand> addEventValidator)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
            _addEventValidator = addEventValidator;
        }



        public async Task<int> AddEventAsync(AddEventCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _addEventValidator.ValidateAsync(command, cancellationToken);
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

        public async Task<IReadOnlyList<EventSummaryDto>> GetEventsAsync(CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetEvents(cancellationToken);

            return events;
        }

        public async Task<EventSeatsDto> GetEventSeatsAsync(int id, SeatStatus? status, CancellationToken cancellationToken)
        {
            var eventSeats = await _eventRepository.GetEventSeatsAsync(id, status, cancellationToken);

            return eventSeats ?? throw new NotFoundException($"Event {id} was not found.");
        }
    }
}
