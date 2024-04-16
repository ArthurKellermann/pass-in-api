﻿using PassIn.Communication.Responses;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions.CustomExceptions;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetEventByIdUseCase
{
    private readonly IEventRepository eventRepository;
    public GetEventByIdUseCase(IEventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }
    public async Task<ResponseEventJson> Execute(Guid id)
    {

        var entity = await this.eventRepository.GetEventById(id);

        if (entity is null)
        {
            throw new NotFoundException("Event does not exists.");
        }

        return new ResponseEventJson
        {
            Id = entity.Id,
            Title = entity.Title,
            Details = entity.Details,
            MaximumAttendees = entity.Maximum_Attendees,
            AttendeesAmount = entity.Attendees.Count()
        };
    }
}
